using Microsoft.Win32;
using Multinavigator.Idiomas;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Multinavigator
{
    public partial class FavoritesWindow : Window
    {
        public Idioma Trad => Idioma.Instance;
        private readonly ObservableCollection<Favorite> _favorites;
        public ObservableCollection<Favorite> Favorites => _favorites;
        private readonly CollectionViewSource _cvs = new();

        private string _searchText  = "";
        private string _groupFilter = "";

        private static readonly HttpClient _http = new(new HttpClientHandler
        {
            AutomaticDecompression = System.Net.DecompressionMethods.GZip
                                     | System.Net.DecompressionMethods.Deflate,
            AllowAutoRedirect      = true,
            MaxAutomaticRedirections = 5
        })
        {
            Timeout = TimeSpan.FromSeconds(8),
            DefaultRequestHeaders = { { "User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64)" } }
        };

        private static readonly Dictionary<string, BitmapImage?> _faviconCache =
            new(StringComparer.OrdinalIgnoreCase);

        private static readonly SemaphoreSlim _faviconSem = new(6, 6);

        // ════════════════════════════════════════════════════════════════
        //  CONSTRUCTOR
        // ════════════════════════════════════════════════════════════════
        public FavoritesWindow(ObservableCollection<Favorite> favorites)
        {
            this.DataContext = this;
            InitializeComponent();
            _favorites = favorites;

            _cvs.Source   = _favorites;
            _cvs.Filter  += OnFilter;
            FavoritesGrid.ItemsSource = _cvs.View;

            DetectBrowsers();
            RefreshGroupCombo();
            UpdateStatus(Idioma.Instance.Fav_StatusReady);

            TriggerFaviconLoad(_favorites);
        }

        // ════════════════════════════════════════════════════════════════
        //  FAVICONS
        // ════════════════════════════════════════════════════════════════
        private void TriggerFaviconLoad(IEnumerable<Favorite> items)
        {
            var pending = items.Where(f => f.FaviconImage == null).ToList();
            if (!pending.Any()) return;
            _ = LoadFaviconsAsync(pending);
        }

        private async Task LoadFaviconsAsync(IEnumerable<Favorite> items)
        {
            await Task.WhenAll(items.Select(fav => LoadOneFaviconAsync(fav)));
        }

        private async Task LoadOneFaviconAsync(Favorite fav)
        {
            var url = GetFaviconUrl(fav);
            if (url == null) return;

            if (_faviconCache.TryGetValue(url, out var cached))
            {
                if (cached != null)
                    Application.Current?.Dispatcher.Invoke(() => fav.FaviconImage = cached);
                return;
            }

            await _faviconSem.WaitAsync();
            try
            {
                if (_faviconCache.TryGetValue(url, out cached))
                {
                    if (cached != null)
                        Application.Current?.Dispatcher.Invoke(() => fav.FaviconImage = cached);
                    return;
                }

                var img = await FetchImageAsync(url);

                if (img == null && Uri.TryCreate(fav.Url, UriKind.Absolute, out var uri))
                    img = await FetchImageAsync($"{uri.Scheme}://{uri.Host}/favicon.ico");

                _faviconCache[url] = img;

                if (img != null)
                    Application.Current?.Dispatcher.Invoke(() => fav.FaviconImage = img);
            }
            finally { _faviconSem.Release(); }
        }

        private static string? GetFaviconUrl(Favorite fav)
        {
            if (!string.IsNullOrWhiteSpace(fav.FaviconUrl)) return fav.FaviconUrl;
            if (!Uri.TryCreate(fav.Url, UriKind.Absolute, out var uri)
                || (uri.Scheme != "http" && uri.Scheme != "https")) return null;
            return $"https://www.google.com/s2/favicons?domain={uri.Host}&sz=32";
        }

        private static async Task<BitmapImage?> FetchImageAsync(string url)
        {
            try
            {
                var bytes = await _http.GetByteArrayAsync(url);
                if (bytes.Length < 100) return null;
                var bmp = new BitmapImage();
                bmp.BeginInit();
                bmp.StreamSource    = new MemoryStream(bytes);
                bmp.CacheOption     = BitmapCacheOption.OnLoad;
                bmp.DecodePixelWidth = 32;
                bmp.EndInit();
                bmp.Freeze();
                return bmp;
            }
            catch { return null; }
        }

        // ════════════════════════════════════════════════════════════════
        //  FILTRADO
        // ════════════════════════════════════════════════════════════════
        private void OnFilter(object sender, FilterEventArgs e)
        {
            if (e.Item is not Favorite f) { e.Accepted = false; return; }

            bool okSearch = string.IsNullOrEmpty(_searchText)
                || f.Title.Contains(_searchText, StringComparison.OrdinalIgnoreCase)
                || f.Url.Contains(_searchText, StringComparison.OrdinalIgnoreCase);

            bool okGroup = string.IsNullOrEmpty(_groupFilter) || _groupFilter == Idioma.Instance.Fav_AllGroups
                || f.Folder.Equals(_groupFilter, StringComparison.OrdinalIgnoreCase);

            e.Accepted = okSearch && okGroup;
        }

        private void SearchBox_TextChanged(object s, TextChangedEventArgs e)
        {
            _searchText = SearchBox.Text.Trim();
            _cvs.View.Refresh();
            UpdateCount();
        }

        private void GroupFilter_SelectionChanged(object s, SelectionChangedEventArgs e)
        {
            _groupFilter = GroupFilter.SelectedItem?.ToString() ?? "";
            _cvs.View.Refresh();
            UpdateCount();
        }

        private void ClearFilter_Click(object s, RoutedEventArgs e)
        {
            SearchBox.Text        = "";
            GroupFilter.SelectedIndex = 0;
            _searchText  = "";
            _groupFilter = "";
            _cvs.View.Refresh();
            UpdateCount();
        }

        private void RefreshGroupCombo()
        {
            var groups = _favorites
                .Select(f => f.Folder)
                .Where(g => !string.IsNullOrWhiteSpace(g))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .OrderBy(g => g)
                .ToList();
            groups.Insert(0, Idioma.Instance.Fav_AllGroups);
            GroupFilter.ItemsSource   = groups;
            GroupFilter.SelectedIndex = 0;
        }

        // ════════════════════════════════════════════════════════════════
        //  CRUD
        // ════════════════════════════════════════════════════════════════
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new FavoriteEditDialog { Owner = this };
            if (dlg.ShowDialog() != true) return;

            bool exists = _favorites.Any(f =>
                string.Equals(f.Url, dlg.Item.Url, StringComparison.OrdinalIgnoreCase));
            if (exists)
            {
                MessageBox.Show(Idioma.Instance.Fav_Msg_Duplicate,
                    Idioma.Instance.Fav_Msg_DuplicateTitle,
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            _favorites.Add(dlg.Item);
            RefreshGroupCombo();
            SaveFavorites();
            UpdateStatus(string.Format(Idioma.Instance.Fav_StatusAdded, dlg.Item.Title));
            TriggerFaviconLoad(new[] { dlg.Item });
        }

        private void Edit_Click(object s, RoutedEventArgs e)
        {
            if (FavoritesGrid.SelectedItem is not Favorite item) return;

            var dlg = new FavoriteEditDialog(item) { Owner = this };
            if (dlg.ShowDialog() != true) return;

            item.Title      = dlg.Item.Title;
            item.Url        = dlg.Item.Url;
            item.Folder     = dlg.Item.Folder;
            item.FaviconUrl = dlg.Item.FaviconUrl;
            item.FaviconImage = null;
            _cvs.View.Refresh();
            RefreshGroupCombo();
            SaveFavorites();
            UpdateStatus(string.Format(Idioma.Instance.Fav_StatusEdited, item.Title));
            TriggerFaviconLoad(new[] { item });
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (FavoritesGrid.SelectedItems.Count == 0) return;

            var toRemove = FavoritesGrid.SelectedItems.Cast<Favorite>().ToList();
            if (MessageBox.Show(
                    string.Format(Idioma.Instance.Fav_Msg_ConfirmDelete, toRemove.Count),
                    Idioma.Instance.Fav_Msg_Confirm,
                    MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes) return;

            foreach (var f in toRemove) _favorites.Remove(f);
            RefreshGroupCombo();
            SaveFavorites();
            UpdateStatus(string.Format(Idioma.Instance.Fav_StatusDeleted, toRemove.Count));
        }

        private void MoveUp_Click(object s, RoutedEventArgs e)
        {
            if (FavoritesGrid.SelectedItem is not Favorite item) return;
            int idx = _favorites.IndexOf(item);
            if (idx > 0) _favorites.Move(idx, idx - 1);
        }

        private void MoveDown_Click(object s, RoutedEventArgs e)
        {
            if (FavoritesGrid.SelectedItem is not Favorite item) return;
            int idx = _favorites.IndexOf(item);
            if (idx < _favorites.Count - 1) _favorites.Move(idx, idx + 1);
        }

        private void Open_Click(object s, RoutedEventArgs e)
        {
            if (FavoritesGrid.SelectedItem is Favorite f) OpenUrl(f.Url);
        }

        private void FavoritesGrid_DoubleClick(object s, MouseButtonEventArgs e)
        {
            if (FavoritesGrid.SelectedItem is Favorite f) OpenUrl(f.Url);
        }

        private void CopyUrl_Click(object s, RoutedEventArgs e)
        {
            if (FavoritesGrid.SelectedItem is Favorite f)
            {
                Clipboard.SetText(f.Url);
                UpdateStatus(Idioma.Instance.Fav_StatusUrlCopied);
            }
        }

        private static void OpenUrl(string url)
        {
            try
            {
                System.Diagnostics.Process.Start(
                    new System.Diagnostics.ProcessStartInfo(url) { UseShellExecute = true });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Idioma.Instance.Fav_Msg_ErrorOpenUrl);
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e) => Close();

        // ════════════════════════════════════════════════════════════════
        //  EXPORT
        // ════════════════════════════════════════════════════════════════
        private void ExportHtml_Click(object s, RoutedEventArgs e)
        {
            var path = SaveDialog("HTML Bookmarks|*.html", "favorites.html");
            if (path == null) return;

            var sb = new StringBuilder();
            sb.AppendLine("<!DOCTYPE NETSCAPE-Bookmark-file-1>");
            sb.AppendLine("<META HTTP-EQUIV=\"Content-Type\" CONTENT=\"text/html; charset=UTF-8\">");
            sb.AppendLine("<TITLE>Bookmarks</TITLE><H1>Bookmarks</H1>");
            sb.AppendLine("<DL><p>");
            foreach (var grp in _favorites.GroupBy(f => f.Folder))
            {
                if (!string.IsNullOrWhiteSpace(grp.Key))
                    sb.AppendLine($"    <DT><H3>{Encode(grp.Key)}</H3><DL><p>");
                foreach (var f in grp)
                    sb.AppendLine($"        <DT><A HREF=\"{f.Url}\">{Encode(f.Title)}</A>");
                if (!string.IsNullOrWhiteSpace(grp.Key))
                    sb.AppendLine("    </DL><p>");
            }
            sb.AppendLine("</DL><p>");
            File.WriteAllText(path, sb.ToString(), Encoding.UTF8);
            UpdateStatus(string.Format(Idioma.Instance.Fav_StatusExported, "HTML", Path.GetFileName(path)));
        }

        private void ExportJson_Click(object s, RoutedEventArgs e)
        {
            var path = SaveDialog("JSON files|*.json", "favorites.json");
            if (path == null) return;
            File.WriteAllText(path,
                JsonConvert.SerializeObject(_favorites.ToList(), Formatting.Indented), Encoding.UTF8);
            UpdateStatus(string.Format(Idioma.Instance.Fav_StatusExported, "JSON", Path.GetFileName(path)));
        }

        private void ExportCsv_Click(object s, RoutedEventArgs e)
        {
            var path = SaveDialog("CSV files|*.csv", "favorites.csv");
            if (path == null) return;
            var sb = new StringBuilder("Title,URL,Group,FaviconUrl\n");
            foreach (var f in _favorites)
                sb.AppendLine($"\"{f.Title}\",\"{f.Url}\",\"{f.Folder}\",\"{f.FaviconUrl}\"");
            File.WriteAllText(path, sb.ToString(), Encoding.UTF8);
            UpdateStatus(string.Format(Idioma.Instance.Fav_StatusExported, "CSV", Path.GetFileName(path)));
        }

        // ════════════════════════════════════════════════════════════════
        //  IMPORT — HTML / JSON
        // ════════════════════════════════════════════════════════════════
        private void ImportHtml_Click(object s, RoutedEventArgs e)
        {
            var path = OpenDialog("HTML Bookmarks|*.html;*.htm");
            if (path == null) return;
            MergeImport(ParseBookmarksHtml(File.ReadAllText(path, Encoding.UTF8)));
        }

        private void ImportJson_Click(object s, RoutedEventArgs e)
        {
            var path = OpenDialog("JSON files|*.json");
            if (path == null) return;
            try
            {
                var items = JsonConvert.DeserializeObject<List<Favorite>>(
                    File.ReadAllText(path, Encoding.UTF8));
                if (items != null) MergeImport(items);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format(Idioma.Instance.Fav_Msg_ImportJsonError, ex.Message),
                    Idioma.Instance.Fav_Msg_ImportJsonTitle);
            }
        }

        // ════════════════════════════════════════════════════════════════
        //  IMPORT — NAVEGADORES
        // ════════════════════════════════════════════════════════════════
        private void ImportChromiumAuto(string userDataRelative, string browserName)
        {
            var userDataPath   = Path.Combine(LocalApp, userDataRelative);
            var bookmarkFiles  = Directory.Exists(userDataPath)
                ? Directory.EnumerateFiles(userDataPath, "Bookmarks", SearchOption.AllDirectories).ToList()
                : new List<string>();

            if (!bookmarkFiles.Any())
            {
                MessageBox.Show(
                    string.Format(Idioma.Instance.Fav_Msg_BrowserNotFound, browserName, userDataPath),
                    Idioma.Instance.Fav_Msg_NotFound,
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var allItems = new List<Favorite>();
            foreach (var file in bookmarkFiles)
            {
                try
                {
                    using var doc = System.Text.Json.JsonDocument.Parse(
                        File.ReadAllText(file, Encoding.UTF8));
                    foreach (var root in doc.RootElement.GetProperty("roots").EnumerateObject())
                        ParseChromiumNode(root.Value, "", allItems);
                }
                catch { }
            }
            MergeImport(allItems);
        }

        private static void ParseChromiumNode(System.Text.Json.JsonElement node,
            string folder, List<Favorite> result)
        {
            if (!node.TryGetProperty("type", out var t)) return;
            if (t.GetString() == "url")
            {
                result.Add(new Favorite
                {
                    Title      = node.GetProperty("name").GetString() ?? "",
                    Url        = node.GetProperty("url").GetString() ?? "",
                    Folder     = folder,
                    FaviconUrl = ""
                });
            }
            else if (t.GetString() == "folder" && node.TryGetProperty("children", out var children))
            {
                string sub = node.GetProperty("name").GetString() ?? folder;
                foreach (var child in children.EnumerateArray())
                    ParseChromiumNode(child, sub, result);
            }
        }

        private void ImportFirefox_Click(object s, RoutedEventArgs e)
        {
            var profileRoot = Path.Combine(RoamingApp, @"Mozilla\Firefox\Profiles");
            if (!Directory.Exists(profileRoot))
            {
                MessageBox.Show(Idioma.Instance.Fav_Msg_FirefoxNotFound,
                    Idioma.Instance.Fav_Msg_NotFound,
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var sqlite = Directory.EnumerateFiles(profileRoot, "places.sqlite",
                             SearchOption.AllDirectories).FirstOrDefault();
            if (sqlite == null) { OfferFirefoxHtmlFallback(s, e); return; }

            var tmp = Path.GetTempFileName() + ".sqlite";
            try
            {
                File.Copy(sqlite, tmp, overwrite: true);
                MergeImport(ReadFirefoxPlaces(tmp));
            }
            catch { OfferFirefoxHtmlFallback(s, e); }
            finally { try { File.Delete(tmp); } catch { } }
        }

        private void OfferFirefoxHtmlFallback(object s, RoutedEventArgs e)
        {
            var r = MessageBox.Show(
                Idioma.Instance.Fav_Msg_FirefoxFallback,
                Idioma.Instance.Fav_Msg_FirefoxTitle,
                MessageBoxButton.YesNo, MessageBoxImage.Information);
            if (r == MessageBoxResult.Yes) ImportHtml_Click(s, e);
        }

        private static List<Favorite> ReadFirefoxPlaces(string dbPath)
        {
            var connType = Type.GetType(
                "Microsoft.Data.Sqlite.SqliteConnection, Microsoft.Data.Sqlite");
            if (connType == null)
                throw new InvalidOperationException(Idioma.Instance.Fav_Msg_SqliteRequired);

            var items = new List<Favorite>();
            dynamic conn = Activator.CreateInstance(connType,
                $"Data Source={dbPath};Mode=ReadOnly")!;
            conn.Open();
            dynamic cmd = conn.CreateCommand();
            cmd.CommandText = @"
                SELECT COALESCE(b.title,''), p.url, COALESCE(p2.title,'')
                FROM   moz_bookmarks b
                JOIN   moz_places p        ON p.id  = b.fk
                LEFT JOIN moz_bookmarks bf ON bf.id = b.parent
                LEFT JOIN moz_places p2    ON p2.id = bf.fk
                WHERE  b.type = 1 AND p.url NOT LIKE 'place:%'
                ORDER  BY b.position";
            dynamic r = cmd.ExecuteReader();
            while (r.Read())
                items.Add(new Favorite
                {
                    Title      = r.GetString(0),
                    Url        = r.GetString(1),
                    Folder     = r.GetString(2),
                    FaviconUrl = ""
                });
            conn.Close();
            return items;
        }

        // ════════════════════════════════════════════════════════════════
        //  HELPERS
        // ════════════════════════════════════════════════════════════════
        private void MergeImport(List<Favorite> items)
        {
            var existing = new HashSet<string>(
                _favorites.Select(f => f.Url.ToLowerInvariant()));
            int added = 0, skipped = 0;
            var newItems = new List<Favorite>();

            foreach (var f in items)
            {
                if (!existing.Add(f.Url.ToLowerInvariant())) { skipped++; continue; }
                _favorites.Add(f);
                newItems.Add(f);
                added++;
            }

            RefreshGroupCombo();
            SaveFavorites();

            MessageBox.Show(
                string.Format(Idioma.Instance.Fav_Msg_ImportResult, added, skipped),
                Idioma.Instance.Fav_Msg_ImportTitle,
                MessageBoxButton.OK, MessageBoxImage.Information);

            UpdateStatus(string.Format(Idioma.Instance.Fav_StatusImported, added, skipped));
            TriggerFaviconLoad(newItems);
        }

        private void SaveFavorites()
        {
            try
            {
                Directory.CreateDirectory(AppPaths.AppFolder);
                File.WriteAllText(AppPaths.Favorites,
                    JsonConvert.SerializeObject(_favorites.ToList(), Formatting.Indented));
            }
            catch { }
        }

        private static List<Favorite> ParseBookmarksHtml(string html)
        {
            var items = new List<Favorite>();
            var folder = "";
            var h3Re = new Regex(@"<H3[^>]*>([^<]+)</H3>", RegexOptions.IgnoreCase);
            var aRe  = new Regex(@"<A\s+HREF=""([^""]+)""[^>]*>([^<]*)</A>", RegexOptions.IgnoreCase);
            int pos  = 0;
            while (pos < html.Length)
            {
                var h3m = h3Re.Match(html, pos);
                var am  = aRe.Match(html, pos);
                if (!am.Success) break;
                if (h3m.Success && h3m.Index < am.Index)
                {
                    folder = Decode(h3m.Groups[1].Value);
                    pos    = h3m.Index + h3m.Length;
                }
                else
                {
                    items.Add(new Favorite
                    {
                        Url        = am.Groups[1].Value.Trim(),
                        Title      = Decode(am.Groups[2].Value),
                        Folder     = folder,
                        FaviconUrl = ""
                    });
                    pos = am.Index + am.Length;
                }
            }
            return items;
        }

        private void DetectBrowsers()
        {
            var browsers = new List<string>();
            if (ChromiumHasBookmarks(@"Google\Chrome\User Data") || IsInstalledInRegistry("chrome"))
                browsers.Add("Chrome");
            if (ChromiumHasBookmarks(@"Microsoft\Edge\User Data") || IsInstalledInRegistry("msedge"))
                browsers.Add("Edge");
            if (Directory.Exists(Path.Combine(RoamingApp, @"Mozilla\Firefox\Profiles")))
                browsers.Add("Firefox");

            ComboBrowsers.ItemsSource    = browsers;
            ComboBrowsers.SelectedIndex  = browsers.Any() ? 0 : -1;
            ComboBrowsers.IsEnabled      = browsers.Any();
        }

        private void ImportBrowser_Click(object s, RoutedEventArgs e)
        {
            switch (ComboBrowsers.SelectedItem?.ToString())
            {
                case "Chrome":  ImportChromiumAuto(@"Google\Chrome\User Data", "Chrome"); break;
                case "Edge":    ImportChromiumAuto(@"Microsoft\Edge\User Data", "Edge");  break;
                case "Firefox": ImportFirefox_Click(s, e); break;
            }
        }

        private static bool ChromiumHasBookmarks(string userDataRelative)
        {
            var path = Path.Combine(LocalApp, userDataRelative);
            return Directory.Exists(path) &&
                   Directory.EnumerateFiles(path, "Bookmarks", SearchOption.AllDirectories).Any();
        }

        private static bool IsInstalledInRegistry(string exeName)
        {
            var key = Registry.LocalMachine.OpenSubKey(
                $@"SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\{exeName}.exe");
            if (key != null) return true;
            key = Registry.CurrentUser.OpenSubKey(
                $@"SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\{exeName}.exe");
            return key != null;
        }

        private static string LocalApp  =>
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        private static string RoamingApp =>
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        private static string? SaveDialog(string filter, string name)
        {
            var d = new SaveFileDialog { Filter = filter, FileName = name };
            return d.ShowDialog() == true ? d.FileName : null;
        }
        private static string? OpenDialog(string filter)
        {
            var d = new OpenFileDialog { Filter = filter };
            return d.ShowDialog() == true ? d.FileName : null;
        }

        private static string Encode(string s) => System.Web.HttpUtility.HtmlEncode(s);
        private static string Decode(string s) => System.Web.HttpUtility.HtmlDecode(s).Trim();

        private void UpdateStatus(string msg)
        {
            StatusText.Text = msg;
            UpdateCount();
        }

        private void UpdateCount()
        {
            int visible = _cvs.View.Cast<object>().Count();
            CountText.Text = string.Format(Idioma.Instance.Fav_Count, visible, _favorites.Count);
        }
    }
}
