using Microsoft.Web.WebView2.Core;
using Multinavigator;
using Multinavigator.Idiomas;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Multinavigator
{
    public partial class Configuracion : Window
    {
        public Idioma Trad => Idioma.Instance;

        public Configuracion(int tabIndex = 0)
        {
            this.DataContext = this;
            InitializeComponent();
            LoadThemes();
            LoadGeneralSettings();
            LoadAdvancedFlags();
            Loaded += (s, e) => TabsConfig.SelectedIndex = tabIndex;
        }

        private void LoadPrivacyStats()
        {
            TxtHistoryCount.Text = History.Instance.GetAll().Count().ToString();

            try
            {
                var cookieFile = System.IO.Path.Combine(AppPaths.BrowserData, "Default", "Cookies");
                TxtCookieCount.Text = System.IO.File.Exists(cookieFile) ? "?" : "0";
                if (System.IO.File.Exists(cookieFile))
                {
                    var kb = new System.IO.FileInfo(cookieFile).Length / 1024;
                    TxtCookieCount.Text = kb.ToString();
                }
            }
            catch { TxtCookieCount.Text = "?"; }

            try
            {
                var cacheDir = System.IO.Path.Combine(AppPaths.BrowserData, "Default", "Cache");
                if (System.IO.Directory.Exists(cacheDir))
                {
                    var mb = System.IO.Directory.GetFiles(cacheDir, "*", System.IO.SearchOption.AllDirectories)
                        .Sum(f => new System.IO.FileInfo(f).Length) / 1024.0 / 1024.0;
                    TxtCacheSize.Text = mb.ToString("F1");
                }
                else TxtCacheSize.Text = "0";
            }
            catch { TxtCacheSize.Text = "?"; }
        }

        private void BtnDeleteTheme_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var theme = btn?.Tag as BrowserTheme;
            if (theme == null) return;

            var result = MessageBox.Show($"¿Eliminar el tema '{theme.Name}'?",
                "Confirmar eliminación", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                ThemeManager.Instance.CustomThemes.Remove(theme);
                ThemeManager.Instance.SaveCustomThemes();
                LoadThemes();
            }
        }

        private void TabsConfig_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TabsConfig.SelectedIndex == 2)
                LoadPrivacyStats();
            if (TabsConfig.SelectedIndex == 4)
                LoadAdvancedFlags();
        }

        private void OpenCookies_Click(object sender, RoutedEventArgs e)
        {
            // 1. Si ya está abierta, la traemos al frente y salimos
            var win = Application.Current.Windows.OfType<CookieManagerWindow>().FirstOrDefault();
            if (win != null) { win.Activate(); return; }

            // 2. Buscamos el WebView activo (usando el MainWindow)
            var main = Application.Current.MainWindow as MainWindow;
            var web = main?.GetActiveWebView(1) ?? main?.GetActiveWebView(2) ??
                      main?.GetActiveWebView(3) ?? main?.GetActiveWebView(4);

            if (web?.CoreWebView2 == null)
            {
                MessageBox.Show("No hay ninguna pestaña inicializada todavía.");
                return;
            }

            // 3. Creamos y mostramos la única instancia
            new CookieManagerWindow(web.CoreWebView2.CookieManager) { Owner = main }.Show();
        }

        public void OpenHistory_Click(object sender, RoutedEventArgs e)
{
    // 1. Si ya existe una instancia, la traemos al frente y salimos
    var win = Application.Current.Windows.OfType<HistoryWindow>().FirstOrDefault();
    if (win != null) { if (win.WindowState == WindowState.Minimized) win.WindowState = WindowState.Normal; win.Activate(); return; }

    // 2. Si no existe, la creamos
    new HistoryWindow { Owner = Application.Current.MainWindow }.Show();
}

        private void DeleteAllHistory_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("¿Eliminar todo el historial?", "Confirmación",
                MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                History.Instance.Clear();
                LoadPrivacyStats();
            }
        }

        private async void DeleteAllCookies_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("¿Eliminar todas las cookies?", "Confirmación",
                MessageBoxButton.YesNo, MessageBoxImage.Warning) != MessageBoxResult.Yes)
                return;

            var mainWindow = Application.Current.MainWindow as MainWindow;
            var wv = mainWindow?.GetActiveWebView(1);
            if (wv?.CoreWebView2 == null) return;

            var cookieManager = wv.CoreWebView2.CookieManager;
            var cookies = await cookieManager.GetCookiesAsync("");
            foreach (var c in cookies)
                cookieManager.DeleteCookie(c);

            LoadPrivacyStats();
            MessageBox.Show("Cookies eliminadas.", "Listo", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private async void DeleteCache_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = Application.Current.MainWindow as MainWindow;
            var wv = mainWindow?.GetActiveWebView(1);
            if (wv?.CoreWebView2 == null)
            {
                MessageBox.Show("No hay ninguna pestaña activa para limpiar la caché.", "Aviso",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            await wv.CoreWebView2.Profile.ClearBrowsingDataAsync(
                CoreWebView2BrowsingDataKinds.DiskCache);

            MessageBox.Show("Caché eliminada.", "Listo", MessageBoxButton.OK, MessageBoxImage.Information);
            LoadPrivacyStats();
        }

        private void LoadGeneralSettings()
        {
            var s = GeneralSettingsManager.Instance;

            // ── Idioma ──────────────────────────────────────────
            var languages = new List<LanguageItem>
                {
                    new() { Code = "es", DisplayName = "Español (Spanish)" },
                    new() { Code = "en", DisplayName = "English (English)" },
                    new() { Code = "zh", DisplayName = "中文 (Chinese)" },
                    new() { Code = "hi", DisplayName = "हिन्दी (Hindi)" },
                    new() { Code = "nl", DisplayName = "Nederlands (Dutch)" },
                    new() { Code = "pt", DisplayName = "Português (Portuguese)" },
                    new() { Code = "fr", DisplayName = "Français (French)" },
                    new() { Code = "de", DisplayName = "Deutsch (German)" },
                    new() { Code = "ja", DisplayName = "日本語 (Japanese)" },
                    new() { Code = "ru", DisplayName = "Русский (Russian)" },
                    new() { Code = "ko", DisplayName = "한국어 (Korean)" },
                    new() { Code = "tr", DisplayName = "Türkçe (Turkish)" },
                    new() { Code = "id", DisplayName = "Bahasa Indonesia (Indonesian)" },
                    new() { Code = "it", DisplayName = "Italiano (Italian)" },
                    new() { Code = "bn", DisplayName = "বাংলা (Bengali)" },
                    new() { Code = "vi", DisplayName = "Tiếng Việt (Vietnamese)" },
                    new() { Code = "pl", DisplayName = "Polski (Polish)" },
                    new() { Code = "th", DisplayName = "ภาษาไทย (Thai)" },
                    new() { Code = "sw", DisplayName = "Kiswahili (Swahili)" },
                    new() { Code = "tl", DisplayName = "Filipino (Tagalog)" },
                    new() { Code = "uk", DisplayName = "Українська (Ukrainian)" },
                    new() { Code = "cs", DisplayName = "Čeština (Czech)" },
                    new() { Code = "ro", DisplayName = "Română (Romanian)" },
                    new() { Code = "ms", DisplayName = "Bahasa Melayu (Malay)" },
                    new() { Code = "ur", DisplayName = "اردو (Urdu)" }
                };
            CmbLanguage.ItemsSource = languages;
            CmbLanguage.SelectedItem = languages.FirstOrDefault(l => l.Code == s.Language) ?? languages[0];

            // ── URLs de inicio ──────────────────────────────────
            TxtHome1.Text = s.HomeUrl1;
            TxtHome2.Text = s.HomeUrl2;
            TxtHome3.Text = s.HomeUrl3;
            TxtHome4.Text = s.HomeUrl4;
            BtnSaveHome.IsEnabled = false;

            ChkRestoreSession.IsChecked = s.RestoreSession;

            // ── Buscador ────────────────────────────────────────
            CmbSearchEngine.ItemsSource = GeneralSettingsManager.SearchEngines;
            CmbSearchEngine.SelectedItem = GeneralSettingsManager.SearchEngines
                .FirstOrDefault(e => e.Url == s.Buscador)
                ?? GeneralSettingsManager.SearchEngines.Last();

            if (CmbSearchEngine.SelectedItem is SearchEngineItem se && se.Name == "Personalizado")
            {
                PnlCustomSearch.Visibility = Visibility.Visible;
                TxtCustomSearch.Text = s.Buscador;
            }
        }

        private void CmbLanguage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CmbLanguage.SelectedItem is not LanguageItem item) return;

            var s = GeneralSettingsManager.Instance;
            if (item.Code == s.Language) return; 


            s.Language = item.Code;
            s.Save();
            Idioma.Instance.SetLanguage(item.Code);

        }

  

        private void SaveHomeUrls_Click(object sender, RoutedEventArgs e)
        {
            var s = GeneralSettingsManager.Instance;
            s.HomeUrl1 = TxtHome1.Text.Trim();
            s.HomeUrl2 = TxtHome2.Text.Trim();
            s.HomeUrl3 = TxtHome3.Text.Trim();
            s.HomeUrl4 = TxtHome4.Text.Trim();
            s.Save();

            BtnSaveHome.IsEnabled = false;
        }

        private void HomeTextChanged(object sender, TextChangedEventArgs e)
        {
            BtnSaveHome.IsEnabled = true;
        }


        private void ChkRestoreSession_Changed(object sender, RoutedEventArgs e)
        {
            var s = GeneralSettingsManager.Instance;
            s.RestoreSession = ChkRestoreSession.IsChecked ?? false;
            s.Save();
        }

        private void CmbSearchEngine_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CmbSearchEngine.SelectedItem is SearchEngineItem se)
            {
                PnlCustomSearch.Visibility = se.Name == "Personalizado"
                    ? Visibility.Visible
                    : Visibility.Collapsed;
            }
        }

        private void SaveSearchEngine_Click(object sender, RoutedEventArgs e)
        {
            var s = GeneralSettingsManager.Instance;

            if (CmbSearchEngine.SelectedItem is SearchEngineItem se)
            {
                s.Buscador = se.Name == "Personalizado"
                    ? TxtCustomSearch.Text.Trim()
                    : se.Url;
            }

            s.Save();
            MessageBox.Show("Buscador guardado.", "Guardado",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void LoadThemes()
        {
            var todosLosTemas = ThemeManager.Instance.PredefinedThemes
                .Concat(ThemeManager.Instance.CustomThemes)
                .OrderBy(t => t.Name)
                .ToList();

            LstThemes.ItemsSource = todosLosTemas;
            LstThemes.SelectedItem = ThemeManager.Instance.CurrentTheme;

            LstThemes.SelectedItem = todosLosTemas.FirstOrDefault(t => t.Name == ThemeManager.Instance.CurrentTheme?.Name);
            LstThemes.Dispatcher.BeginInvoke(() =>
            {
                LstThemes.ScrollIntoView(LstThemes.SelectedItem);
                var item = LstThemes.ItemContainerGenerator.ContainerFromItem(LstThemes.SelectedItem) as ListBoxItem;
                item?.Focus();
            }, System.Windows.Threading.DispatcherPriority.Loaded);
        }

        private void LstThemes_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (LstThemes.SelectedItem is BrowserTheme theme)
            {
                ThemeManager.Instance.ApplyTheme(theme);

                if (Owner is MainWindow mainWindow)
                {
                    // Usamos la propiedad de tu clase BrowserTheme
                    mainWindow.UpdateAllWebViewsColorScheme(theme.DarkWebContent);
                }
            }
        }

        private void BtnOpenThemeEditor_Click(object sender, RoutedEventArgs e)
        {
            var editor = new ThemeEditor { Owner = this };
            if (editor.ShowDialog() == true)
                LoadThemes();
        }

        private void ChkDarkModeIncognito_Changed(object sender, RoutedEventArgs e)
        {
            ThemeManager.Instance.AutoDarkModeIncognito = ChkDarkModeIncognito.IsChecked ?? false;
            ThemeManager.Instance.SaveTheme(ThemeManager.Instance.CurrentTheme);
        }

        private void AddDomain_Click(object sender, RoutedEventArgs e)
        {
            PermissionManager.Instance.DomainPermissions.Add(new DomainPermission
            {
                Domain = "example.com",
                Camera = CoreWebView2PermissionState.Default,
                Microphone = CoreWebView2PermissionState.Default,
                Location = CoreWebView2PermissionState.Default,
                Notifications = CoreWebView2PermissionState.Default
            });
            PermissionManager.Instance.Save();
        }

        private void ResetPermissions_Click(object sender, RoutedEventArgs e)
        {
            PermissionManager.Instance.Reset();
            DataContext = null;
            DataContext = PermissionManager.Instance;
        }

        private void ExportSettings_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.SaveFileDialog
            {
                Filter = "Multinavigator Backup (*.mnbak)|*.mnbak",
                FileName = $"multinavigator_backup_{DateTime.Now:yyyyMMdd}"
            };

            if (dialog.ShowDialog() != true) return;

            try
            {
                string tempFolder = System.IO.Path.Combine(
                    System.IO.Path.GetTempPath(), "mn7_export_" + Guid.NewGuid());
                Directory.CreateDirectory(tempFolder);

                if (ChkBackupFavorites.IsChecked == true && File.Exists(AppPaths.Favorites))
                    File.Copy(AppPaths.Favorites, System.IO.Path.Combine(tempFolder, "favorites.json"));

                if (ChkBackupThemes.IsChecked == true && File.Exists(AppPaths.Themes))
                    File.Copy(AppPaths.Themes, System.IO.Path.Combine(tempFolder, "custom_themes.json"));

                if (ChkBackupPermissions.IsChecked == true && File.Exists(AppPaths.Permissions))
                    File.Copy(AppPaths.Permissions, System.IO.Path.Combine(tempFolder, "permissions.json"));

                if (ChkBackupSettings.IsChecked == true && File.Exists(AppPaths.ThemeSettings))
                    File.Copy(AppPaths.ThemeSettings, System.IO.Path.Combine(tempFolder, "theme_setting.json"));

                if (ChkBackupHistory.IsChecked == true && File.Exists(AppPaths.History))
                    File.Copy(AppPaths.History, Path.Combine(tempFolder, "history.json"));

                ZipFile.CreateFromDirectory(tempFolder, dialog.FileName);
                Directory.Delete(tempFolder, recursive: true);

                MessageBox.Show("Configuración exportada correctamente.", "Exportar",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al exportar: {ex.Message}", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ImportSettings_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.OpenFileDialog
            {
                Filter = "Multinavigator Backup (*.mnbak)|*.mnbak"
            };

            if (dialog.ShowDialog() != true) return;

            try
            {
                string tempFolder = System.IO.Path.Combine(
                    System.IO.Path.GetTempPath(), "mn7_import_" + Guid.NewGuid());

                ZipFile.ExtractToDirectory(dialog.FileName, tempFolder);

                string roaming = System.IO.Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                    "Multinavigator");

                Directory.CreateDirectory(roaming);

                void CopyIfExists(string fileName)
                {
                    string src = System.IO.Path.Combine(tempFolder, fileName);
                    if (File.Exists(src))
                        File.Copy(src, System.IO.Path.Combine(roaming, fileName), overwrite: true);
                }

                if (ChkBackupFavorites.IsChecked == true) CopyIfExists("favorites.json");
                if (ChkBackupThemes.IsChecked == true) CopyIfExists("custom_themes.json");
                if (ChkBackupPermissions.IsChecked == true) CopyIfExists("permissions.json");
                if (ChkBackupSettings.IsChecked == true) CopyIfExists("theme_setting.json");

                Directory.Delete(tempFolder, recursive: true);

                ThemeManager.Instance.ApplyTheme(ThemeManager.Instance.CurrentTheme);
                LoadThemes();

                MessageBox.Show("Configuración importada correctamente.\nReinicia la aplicación para aplicar todos los cambios.",
                    "Importar", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al importar: {ex.Message}", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool _loadingAdvancedFlags = false;
        private void LoadAdvancedFlags()
        {
            _loadingAdvancedFlags = true;
            var s = GeneralSettingsManager.Instance;

            ChkDisableBackgroundNetworking.IsChecked = s.FlagDisableBackgroundNetworking;
            ChkDisableSync.IsChecked = s.FlagDisableSync;
            ChkDisableTranslate.IsChecked = s.FlagDisableTranslate;
            ChkDisableExtensions.IsChecked = s.FlagDisableExtensions;
            ChkDisableDefaultApps.IsChecked = s.FlagDisableDefaultApps;
            ChkNoDefaultBrowserCheck.IsChecked = s.FlagNoDefaultBrowserCheck;
            ChkMetricsRecordingOnly.IsChecked = s.FlagMetricsRecordingOnly;
            ChkDisableBreakpad.IsChecked = s.FlagDisableBreakpad;
            ChkDisablePhishingDetection.IsChecked = s.FlagDisablePhishingDetection;
            ChkDisableHangMonitor.IsChecked = s.FlagDisableHangMonitor;
            ChkDisablePromptOnRepost.IsChecked = s.FlagDisablePromptOnRepost;
            ChkDisableDomainReliability.IsChecked = s.FlagDisableDomainReliability;
            ChkDisableComponentUpdate.IsChecked = s.FlagDisableComponentUpdate;
            ChkDisableBgTimerThrottling.IsChecked = s.FlagDisableBgTimerThrottling;
            ChkDisableRendererBackgrounding.IsChecked = s.FlagDisableRendererBackgrounding;
            ChkDisableIpcFloodingProtection.IsChecked = s.FlagDisableIpcFloodingProtection;

            UpdateAdvancedStateLabels();
            _loadingAdvancedFlags = false;
        }

        private void UpdateAdvancedStateLabels()
        {
            SetStateLabel(TxtStateBackgroundNetworking, ChkDisableBackgroundNetworking.IsChecked == true);
            SetStateLabel(TxtStateSync, ChkDisableSync.IsChecked == true);
            SetStateLabel(TxtStateTranslate, ChkDisableTranslate.IsChecked == true);
            SetStateLabel(TxtStateExtensions, ChkDisableExtensions.IsChecked == true);
            SetStateLabel(TxtStateDefaultApps, ChkDisableDefaultApps.IsChecked == true);
            SetStateLabel(TxtStateDefaultBrowserCheck, ChkNoDefaultBrowserCheck.IsChecked == true);
            SetStateLabel(TxtStateMetricsRecording, ChkMetricsRecordingOnly.IsChecked == true);
            SetStateLabel(TxtStateBreakpad, ChkDisableBreakpad.IsChecked == true);
            SetStateLabel(TxtStatePhishingDetection, ChkDisablePhishingDetection.IsChecked == true);
            SetStateLabel(TxtStateHangMonitor, ChkDisableHangMonitor.IsChecked == true);
            SetStateLabel(TxtStatePromptOnRepost, ChkDisablePromptOnRepost.IsChecked == true);
            SetStateLabel(TxtStateDomainReliability, ChkDisableDomainReliability.IsChecked == true);
            SetStateLabel(TxtStateComponentUpdate, ChkDisableComponentUpdate.IsChecked == true);
            SetStateLabel(TxtStateBgTimerThrottling, ChkDisableBgTimerThrottling.IsChecked == true);
            SetStateLabel(TxtStateRendererBackgrounding, ChkDisableRendererBackgrounding.IsChecked == true);
            SetStateLabel(TxtStateIpcFloodingProtection, ChkDisableIpcFloodingProtection.IsChecked == true);
        }

        private void SetStateLabel(TextBlock label, bool active)
        {
            if (active)
            {
                label.Text = Idioma.Instance.Cfg_Adv_StateOn;
                label.Foreground = new SolidColorBrush(Color.FromRgb(46, 125, 50));
            }
            else
            {
                label.Text = Idioma.Instance.Cfg_Adv_StateOff;
                label.Foreground = new SolidColorBrush(Color.FromRgb(198, 40, 40));
            }
        }

        private void AdvancedFlag_Changed(object sender, RoutedEventArgs e)
        {
            if (_loadingAdvancedFlags) return;
            var s = GeneralSettingsManager.Instance;

            s.FlagDisableBackgroundNetworking = ChkDisableBackgroundNetworking.IsChecked == true;
            s.FlagDisableSync = ChkDisableSync.IsChecked == true;
            s.FlagDisableTranslate = ChkDisableTranslate.IsChecked == true;
            s.FlagDisableExtensions = ChkDisableExtensions.IsChecked == true;
            s.FlagDisableDefaultApps = ChkDisableDefaultApps.IsChecked == true;
            s.FlagNoDefaultBrowserCheck = ChkNoDefaultBrowserCheck.IsChecked == true;
            s.FlagMetricsRecordingOnly = ChkMetricsRecordingOnly.IsChecked == true;
            s.FlagDisableBreakpad = ChkDisableBreakpad.IsChecked == true;
            s.FlagDisablePhishingDetection = ChkDisablePhishingDetection.IsChecked == true;
            s.FlagDisableHangMonitor = ChkDisableHangMonitor.IsChecked == true;
            s.FlagDisablePromptOnRepost = ChkDisablePromptOnRepost.IsChecked == true;
            s.FlagDisableDomainReliability = ChkDisableDomainReliability.IsChecked == true;
            s.FlagDisableComponentUpdate = ChkDisableComponentUpdate.IsChecked == true;
            s.FlagDisableBgTimerThrottling = ChkDisableBgTimerThrottling.IsChecked == true;
            s.FlagDisableRendererBackgrounding = ChkDisableRendererBackgrounding.IsChecked == true;
            s.FlagDisableIpcFloodingProtection = ChkDisableIpcFloodingProtection.IsChecked == true;

            s.Save();
            UpdateAdvancedStateLabels();
        }
    }

    // ========================================
    //  SELECTOR DE IDIOMA
    // ========================================

    public class LanguageItem
    {
        public string Code { get; set; }
        public string DisplayName { get; set; }
    }

    // ========================================
    //  CONFIGURACIÓN GENERAL
    // ========================================

    public class SessionTab
    {
        public int PanelId { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
        public string MarkColor { get; set; }
    }

    public class SearchEngineItem
    {
        public string Name { get; set; }
        public string Url { get; set; }
    }

    public class GeneralSettingsManager
    {
        private static GeneralSettingsManager _instance;
        public static GeneralSettingsManager Instance => _instance ??= Load();

        public double WindowLeft { get; set; } = -1;
        public double WindowTop { get; set; } = -1;
        public double WindowWidth { get; set; } = 1200;
        public double WindowHeight { get; set; } = 800;
        public bool WindowMaximized { get; set; } = false;
        public int PanelMaximizado { get; set; } = 0;  // 0=todos, 1-4=panel maximizado
        public double BotonCentralX { get; set; } = 0.5;  // proporción 0-1
        public double BotonCentralY { get; set; } = 0.5;  // proporción 0-1
        public bool FirstRun { get; set; } = true;

        // ── Idioma ──────────────────────────────────────────────
        public string Language { get; set; } = "es";

        public string HomeUrl1 { get; set; } = "https://www.google.com";
        public string HomeUrl2 { get; set; } = "https://www.google.com";
        public string HomeUrl3 { get; set; } = "https://www.google.com";
        public string HomeUrl4 { get; set; } = "https://www.google.com";

        public bool RestoreSession { get; set; } = false;
        public string Buscador { get; set; } = "https://www.google.com/search?q=";
        public List<SessionTab> LastSession { get; set; } = new();

        public bool FlagDisableBackgroundNetworking { get; set; } = true;
        public bool FlagDisableSync { get; set; } = true;
        public bool FlagDisableTranslate { get; set; } = true;
        public bool FlagDisableExtensions { get; set; } = true;
        public bool FlagDisableDefaultApps { get; set; } = true;
        public bool FlagNoDefaultBrowserCheck { get; set; } = true;
        public bool FlagMetricsRecordingOnly { get; set; } = true;
        public bool FlagDisableBreakpad { get; set; } = true;
        public bool FlagDisablePhishingDetection { get; set; } = true;
        public bool FlagDisableHangMonitor { get; set; } = true;
        public bool FlagDisablePromptOnRepost { get; set; } = true;
        public bool FlagDisableDomainReliability { get; set; } = true;
        public bool FlagDisableComponentUpdate { get; set; } = true;
        public bool FlagDisableBgTimerThrottling { get; set; } = true;
        public bool FlagDisableRendererBackgrounding { get; set; } = true;
        public bool FlagDisableIpcFloodingProtection { get; set; } = true;

        public static readonly List<SearchEngineItem> SearchEngines = new()
        {
            new SearchEngineItem { Name = "Google",        Url = "https://www.google.com/search?q=" },
            new SearchEngineItem { Name = "Bing",          Url = "https://www.bing.com/search?q=" },
            new SearchEngineItem { Name = "DuckDuckGo",    Url = "https://duckduckgo.com/?q=" },
            new SearchEngineItem { Name = "Yahoo",         Url = "https://search.yahoo.com/search?p=" },
            new SearchEngineItem { Name = "Brave",         Url = "https://search.brave.com/search?q=" },
            new SearchEngineItem { Name = "Ecosia",        Url = "https://www.ecosia.org/search?q=" },
            new SearchEngineItem { Name = "Startpage",     Url = "https://www.startpage.com/search?q=" },
            new SearchEngineItem { Name = "Qwant",         Url = "https://www.qwant.com/?q=" },
            new SearchEngineItem { Name = "Yandex",        Url = "https://yandex.com/search/?text=" },
            new SearchEngineItem { Name = "Baidu",         Url = "https://www.baidu.com/s?wd=" },
            new SearchEngineItem { Name = "Personalizado", Url = "" }
        };

        public void Save()
        {
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(AppPaths.GeneralSettings));
                File.WriteAllText(AppPaths.GeneralSettings,
                    JsonConvert.SerializeObject(this, Formatting.Indented));
            }
            catch { }
        }

        private static GeneralSettingsManager Load()
        {
            try
            {
                if (File.Exists(AppPaths.GeneralSettings))
                    return JsonConvert.DeserializeObject<GeneralSettingsManager>(
                        File.ReadAllText(AppPaths.GeneralSettings)) ?? new GeneralSettingsManager();
            }
            catch { }
            return new GeneralSettingsManager();
        }

        public void SaveSession(List<SessionTab> tabs)
        {
            LastSession = tabs;
            Save();
        }
    }

    // ========================================
    //  SISTEMA DE TEMAS
    // ========================================

    public class BrowserTheme
    {
        public string Name { get; set; }
        public bool IsCustom { get; set; }
        public string WindowBackground { get; set; }
        public string WindowForeground { get; set; }
        public string TabInactive { get; set; }
        public string TabActive { get; set; }
        public string TabHover { get; set; }
        public string TabActiveHover { get; set; }
        public string TabText { get; set; }
        public string IncognitoInactive { get; set; }
        public string IncognitoActive { get; set; }
        public string IncognitoHover { get; set; }
        public string IncognitoActiveHover { get; set; }
        public string IncognitoText { get; set; }
        public string NavBarBackground { get; set; }
        public string NavBarForeground { get; set; }
        public string UrlBoxBackground { get; set; }
        public string UrlBoxForeground { get; set; }
        public string ButtonAccent { get; set; }
        public string ButtonHover { get; set; }
        public string ButtonPressed { get; set; }
        public string ButtonCentro { get; set; }
        public bool DarkWebContent { get; set; }

        public BrowserTheme() { SetLightTheme(); }

        public void SetAcidTheme() { Name = "Acid"; DarkWebContent = true; IsCustom = false; WindowBackground = "#FF0A0A00"; WindowForeground = "#FFCCFF00"; TabInactive = "#FF141400"; TabActive = "#FF1E1E00"; TabHover = "#FF282800"; TabActiveHover = "#FF1E1E00"; TabText = "#FFCCFF00"; IncognitoInactive = "#FF202020"; IncognitoActive = "#FF000000"; IncognitoHover = "#FF303030"; IncognitoActiveHover = "#FF101010"; IncognitoText = "#FFCCFF00"; NavBarBackground = "#FF1E1E00"; NavBarForeground = "#FFCCFF00"; UrlBoxBackground = "#FF141400"; UrlBoxForeground = "#FFCCFF00"; ButtonAccent = "#FFCCFF00"; ButtonHover = "#FF2D2D0F"; ButtonPressed = "#FF323214"; ButtonCentro = "#FF99CC00"; }
        public void SetAuroraTheme() { Name = "Aurora"; DarkWebContent = true; IsCustom = false; WindowBackground = "#FF0D1B2A"; WindowForeground = "#FF00F5D4"; TabInactive = "#FF1B2A3A"; TabActive = "#FF0D2337"; TabHover = "#FF1E3A50"; TabActiveHover = "#FF0D2337"; TabText = "#FF00F5D4"; IncognitoInactive = "#FF202020"; IncognitoActive = "#FF000000"; IncognitoHover = "#FF303030"; IncognitoActiveHover = "#FF101010"; IncognitoText = "#FF00F5D4"; NavBarBackground = "#FF0D2337"; NavBarForeground = "#FF00F5D4"; UrlBoxBackground = "#FF1B2A3A"; UrlBoxForeground = "#FF00F5D4"; ButtonAccent = "#FF00B4D8"; ButtonHover = "#FF1C3246"; ButtonPressed = "#FF21374B"; ButtonCentro = "#FF00F5D4"; }
        public void SetBlueTheme() { Name = "Blue"; DarkWebContent = false; IsCustom = false; WindowBackground = "#FFECF5FD"; WindowForeground = "#FF55AEF5"; TabInactive = "#FFBBDEFB"; TabActive = "#FFD4E9FB"; TabHover = "#90CAF9"; TabActiveHover = "#E3F2FD"; TabText = "#01579B"; IncognitoInactive = "#FF404040"; IncognitoActive = "#FF151515"; IncognitoHover = "#FF505050"; IncognitoActiveHover = "#FF222222"; IncognitoText = "#FFFFFF"; NavBarBackground = "#FFD4E9FB"; NavBarForeground = "#01579B"; UrlBoxBackground = "#FFFFFF"; UrlBoxForeground = "#01579B"; ButtonAccent = "#FF55AEF5"; ButtonHover = "#FFE3F8FF"; ButtonPressed = "#FFE8FDFF"; ButtonCentro = "#2196F3"; }
        public void SetCyberpunkTheme() { Name = "Cyberpunk"; DarkWebContent = true; IsCustom = false; WindowBackground = "#FF0D0D0D"; WindowForeground = "#FF00FFFF"; TabInactive = "#FF16213E"; TabActive = "#FF1F1F2E"; TabHover = "#FF1A1A2E"; TabActiveHover = "#FF1F1F2E"; TabText = "#FF00FFFF"; IncognitoInactive = "#FF202020"; IncognitoActive = "#FF000000"; IncognitoHover = "#FF303030"; IncognitoActiveHover = "#FF101010"; IncognitoText = "#FFFF00FF"; NavBarBackground = "#FF1F1F2E"; NavBarForeground = "#FF00FFFF"; UrlBoxBackground = "#FF1A1A2E"; UrlBoxForeground = "#FF00FFFF"; ButtonAccent = "#FFFF00FF"; ButtonHover = "#FF2E2E3D"; ButtonPressed = "#FF333342"; ButtonCentro = "#FFFF00FF"; }
        public void SetDarkTheme() { Name = "Dark"; DarkWebContent = true; IsCustom = false; WindowBackground = "#1E1E1E"; WindowForeground = "#0E639C"; TabInactive = "#FF3A3A3A"; TabActive = "#FF2D2D30"; TabHover = "#3E3E42"; TabActiveHover = "#2D2D30"; TabText = "#FFFFFF"; IncognitoInactive = "#FF202020"; IncognitoActive = "#FF000000"; IncognitoHover = "#FF303030"; IncognitoActiveHover = "#FF101010"; IncognitoText = "#FFFFFF"; NavBarBackground = "#FF2D2D30"; NavBarForeground = "#FFFFFF"; UrlBoxBackground = "#3E3E42"; UrlBoxForeground = "#FFFFFF"; ButtonAccent = "#0E639C"; ButtonHover = "#FF3C3C3F"; ButtonPressed = "#FF414144"; ButtonCentro = "#0E639C"; }
        public void SetDarkBlueTheme() { Name = "Dark Blue"; DarkWebContent = true; IsCustom = false; WindowBackground = "#FF000847"; WindowForeground = "#FFD271FF"; TabInactive = "#FF000A60"; TabActive = "#FF001384"; TabHover = "#FF00109E"; TabActiveHover = "#FF001493"; TabText = "#FF8394FF"; IncognitoInactive = "#FF202020"; IncognitoActive = "#FF000000"; IncognitoHover = "#FF303030"; IncognitoActiveHover = "#FF101010"; IncognitoText = "#FFC3C6FD"; NavBarBackground = "#FF001384"; NavBarForeground = "#FFD271FF"; UrlBoxBackground = "#FF00000A"; UrlBoxForeground = "#FFD6DDFD"; ButtonAccent = "#FF7178FF"; ButtonHover = "#FF0F2293"; ButtonPressed = "#FF142798"; ButtonCentro = "#FFB4ADFE"; }
        public void SetDarkGreenTheme() { Name = "Dark Green"; DarkWebContent = true; IsCustom = false; WindowBackground = "#FF074700"; WindowForeground = "#FF71FFD8"; TabInactive = "#FF076000"; TabActive = "#FF0F8400"; TabHover = "#FF0B9E00"; TabActiveHover = "#FF0F9300"; TabText = "#FF90FF83"; IncognitoInactive = "#FF202020"; IncognitoActive = "#FF000000"; IncognitoHover = "#FF303030"; IncognitoActiveHover = "#FF101010"; IncognitoText = "#FFC3FDC3"; NavBarBackground = "#FF0F8400"; NavBarForeground = "#FF71FFD8"; UrlBoxBackground = "#FF010A00"; UrlBoxForeground = "#FFDCFDD6"; ButtonAccent = "#FF73FF71"; ButtonHover = "#FF1E930F"; ButtonPressed = "#FF239814"; ButtonCentro = "#FFADFEB8"; }
        public void SetDarkRedTheme() { Name = "Dark Red"; DarkWebContent = true; IsCustom = false; WindowBackground = "#FF470005"; WindowForeground = "#FFFFDA71"; TabInactive = "#FF600005"; TabActive = "#FF84000C"; TabHover = "#FF9E0008"; TabActiveHover = "#FF93000C"; TabText = "#FFFF838E"; IncognitoInactive = "#FF202020"; IncognitoActive = "#FF000000"; IncognitoHover = "#FF303030"; IncognitoActiveHover = "#FF101010"; IncognitoText = "#FFFDC3C3"; NavBarBackground = "#FF84000C"; NavBarForeground = "#FFFFDA71"; UrlBoxBackground = "#FF0A0000"; UrlBoxForeground = "#FFFDD6DB"; ButtonAccent = "#FFFF7171"; ButtonHover = "#FF930F1B"; ButtonPressed = "#FF981420"; ButtonCentro = "#FFFEB9AD"; }
        public void SetForestTheme() { Name = "Forest"; DarkWebContent = false; IsCustom = false; WindowBackground = "#FF1A2E1A"; WindowForeground = "#FFB8E0B8"; TabInactive = "#FF243524"; TabActive = "#FF2E4A2E"; TabHover = "#FF3A5E3A"; TabActiveHover = "#FF2E4A2E"; TabText = "#FFB8E0B8"; IncognitoInactive = "#FF202020"; IncognitoActive = "#FF000000"; IncognitoHover = "#FF303030"; IncognitoActiveHover = "#FF101010"; IncognitoText = "#FFB8E0B8"; NavBarBackground = "#FF2E4A2E"; NavBarForeground = "#FFB8E0B8"; UrlBoxBackground = "#FF243524"; UrlBoxForeground = "#FFB8E0B8"; ButtonAccent = "#FF4CAF50"; ButtonHover = "#FF3D593D"; ButtonPressed = "#FF425E42"; ButtonCentro = "#FF4CAF50"; }
        public void SetElRanaTheme() { Name = "El rana"; DarkWebContent = false; IsCustom = false; WindowBackground = "#FFE6FCE3"; WindowForeground = "#FF117B01"; TabInactive = "#FF9BF18D"; TabActive = "#FFC3F6BA"; TabHover = "#FF72EC5E"; TabActiveHover = "#FFC3F6BA"; TabText = "#FF117B01"; IncognitoInactive = "#FF404040"; IncognitoActive = "#FF151515"; IncognitoHover = "#FF505050"; IncognitoActiveHover = "#FF222222"; IncognitoText = "#FFFFFFFF"; NavBarBackground = "#FFC3F6BA"; NavBarForeground = "#FF117B01"; UrlBoxBackground = "#FFFFFFFF"; UrlBoxForeground = "#FF117B01"; ButtonAccent = "#FF23990E"; ButtonHover = "#FFD2FFC9"; ButtonPressed = "#FFD7FFCE"; ButtonCentro = "#FF32CD00"; }
        public void SetGalaxyTheme() { Name = "Galaxy"; DarkWebContent = true; IsCustom = false; WindowBackground = "#FF1A0A2E"; WindowForeground = "#FFE040FB"; TabInactive = "#FF1E0D35"; TabActive = "#FF2D1B4E"; TabHover = "#FF4A3580"; TabActiveHover = "#FF2D1B4E"; TabText = "#FFE040FB"; IncognitoInactive = "#FF202020"; IncognitoActive = "#FF000000"; IncognitoHover = "#FF303030"; IncognitoActiveHover = "#FF101010"; IncognitoText = "#FF00E5FF"; NavBarBackground = "#FF2D1B4E"; NavBarForeground = "#FFE040FB"; UrlBoxBackground = "#FF3D2B6E"; UrlBoxForeground = "#FFE040FB"; ButtonAccent = "#FF7C4DFF"; ButtonHover = "#FF3C2A5D"; ButtonPressed = "#FF412F62"; ButtonCentro = "#FF7C4DFF"; }
        public void SetGreenTheme() { Name = "Green"; DarkWebContent = false; IsCustom = false; WindowBackground = "#FFE8F5E9"; WindowForeground = "#FF1B5E20"; TabInactive = "#FFA5D6A7"; TabActive = "#FFC8E6C9"; TabHover = "#FF81C784"; TabActiveHover = "#FFC8E6C9"; TabText = "#FF1B5E20"; IncognitoInactive = "#FF404040"; IncognitoActive = "#FF151515"; IncognitoHover = "#FF505050"; IncognitoActiveHover = "#FF222222"; IncognitoText = "#FFFFFF"; NavBarBackground = "#FFC8E6C9"; NavBarForeground = "#FF1B5E20"; UrlBoxBackground = "#FFFFFF"; UrlBoxForeground = "#FF1B5E20"; ButtonAccent = "#FF4CAF50"; ButtonHover = "#FFD7F5D8"; ButtonPressed = "#FFDCFADD"; ButtonCentro = "#FF4CAF50"; }
        public void SetHackerTheme() { Name = "Hacker"; DarkWebContent = true; IsCustom = false; WindowBackground = "#FF000000"; WindowForeground = "#FF00FF41"; TabInactive = "#FF001A00"; TabActive = "#FF002600"; TabHover = "#FF003300"; TabActiveHover = "#FF002600"; TabText = "#FF00FF41"; IncognitoInactive = "#FF202020"; IncognitoActive = "#FF000000"; IncognitoHover = "#FF303030"; IncognitoActiveHover = "#FF101010"; IncognitoText = "#FF00FF41"; NavBarBackground = "#FF002600"; NavBarForeground = "#FF00FF41"; UrlBoxBackground = "#FF001A00"; UrlBoxForeground = "#FF00FF41"; ButtonAccent = "#FF00FF41"; ButtonHover = "#FF0F350F"; ButtonPressed = "#FF143A14"; ButtonCentro = "#FF00CC33"; }
        public void SetLightTheme() { Name = "Light"; DarkWebContent = false; IsCustom = false; WindowBackground = "#FFEAEAEA"; WindowForeground = "#FF4B96EA"; TabInactive = "#FFcccccc"; TabActive = "#FFFFFF"; TabHover = "#FFF5F5F5"; TabActiveHover = "#FFFFFF"; TabText = "#000000"; IncognitoInactive = "#FF404040"; IncognitoActive = "#FF151515"; IncognitoHover = "#FF505050"; IncognitoActiveHover = "#FF222222"; IncognitoText = "#FFFFFF"; NavBarBackground = "#FFFFFF"; NavBarForeground = "#000000"; UrlBoxBackground = "#FFE9EFF9"; UrlBoxForeground = "#000000"; ButtonAccent = "#FF4B96EA"; ButtonHover = "#FFFFFBEB"; ButtonPressed = "#FFFFF5D6"; ButtonCentro = "#FF4B96EA"; }
        public void SetLight2Theme() { Name = "Light 2"; DarkWebContent = false; IsCustom = false; WindowBackground = "#FFDDDDDD"; WindowForeground = "#FF4B96EA"; TabInactive = "#FFECECEC"; TabActive = "#FFFFFF"; TabHover = "#FFF5F5F5"; TabActiveHover = "#FFFFFF"; TabText = "#000000"; IncognitoInactive = "#FF404040"; IncognitoActive = "#FF151515"; IncognitoHover = "#FF505050"; IncognitoActiveHover = "#FF222222"; IncognitoText = "#FFFFFF"; NavBarBackground = "#FFFFFF"; NavBarForeground = "#000000"; UrlBoxBackground = "#FFE9EFF9"; UrlBoxForeground = "#000000"; ButtonAccent = "#FF4B96EA"; ButtonHover = "#FFFFFBEB"; ButtonPressed = "#FFFFF5D6"; ButtonCentro = "#FF4B96EA"; }
        public void SetLightGreenTheme() { Name = "Light Green"; DarkWebContent = false; IsCustom = false; WindowBackground = "#FFEAEAEA"; WindowForeground = "#FF9DE949"; TabInactive = "#FFCCCCCC"; TabActive = "#FFFFFFFF"; TabHover = "#FFF5F5F5"; TabActiveHover = "#FFFFFFFF"; TabText = "#FF000000"; IncognitoInactive = "#FF404040"; IncognitoActive = "#FF151515"; IncognitoHover = "#FF505050"; IncognitoActiveHover = "#FF222222"; IncognitoText = "#FFFFFFFF"; NavBarBackground = "#FFFFFFFF"; NavBarForeground = "#FF000000"; UrlBoxBackground = "#FFEFF9E8"; UrlBoxForeground = "#FF000000"; ButtonAccent = "#FF9DE949"; ButtonHover = "#FFFFFBEB"; ButtonPressed = "#FFFFF5D6"; ButtonCentro = "#FF9DE949"; }
        public void SetLightPinkTheme() { Name = "Light Pink"; DarkWebContent = false; IsCustom = false; WindowBackground = "#FFEAEAEA"; WindowForeground = "#FFE947CF"; TabInactive = "#FFCCCCCC"; TabActive = "#FFFFFFFF"; TabHover = "#FFF5F5F5"; TabActiveHover = "#FFFFFFFF"; TabText = "#FF000000"; IncognitoInactive = "#FF404040"; IncognitoActive = "#FF151515"; IncognitoHover = "#FF505050"; IncognitoActiveHover = "#FF222222"; IncognitoText = "#FFFFFFFF"; NavBarBackground = "#FFFFFFFF"; NavBarForeground = "#FF000000"; UrlBoxBackground = "#FFF9E8F5"; UrlBoxForeground = "#FF000000"; ButtonAccent = "#FFE947CF"; ButtonHover = "#FFFFFBEB"; ButtonPressed = "#FFFFF5D6"; ButtonCentro = "#FFE947CF"; }
        public void SetLightRedTheme() { Name = "Light Red"; DarkWebContent = false; IsCustom = false; WindowBackground = "#FFEAEAEA"; WindowForeground = "#FFEA4954"; TabInactive = "#FFCCCCCC"; TabActive = "#FFFFFFFF"; TabHover = "#FFF5F5F5"; TabActiveHover = "#FFFFFFFF"; TabText = "#FF000000"; IncognitoInactive = "#FF404040"; IncognitoActive = "#FF151515"; IncognitoHover = "#FF505050"; IncognitoActiveHover = "#FF222222"; IncognitoText = "#FFFFFFFF"; NavBarBackground = "#FFFFFFFF"; NavBarForeground = "#FF000000"; UrlBoxBackground = "#FFF9E8E8"; UrlBoxForeground = "#FF000000"; ButtonAccent = "#FFEA4954"; ButtonHover = "#FFFFFBEB"; ButtonPressed = "#FFFFF5D6"; ButtonCentro = "#FFEA4954"; }
        public void SetLightTurquoiseTheme() { Name = "Light Turquoise"; DarkWebContent = false; IsCustom = false; WindowBackground = "#FFEAEAEA"; WindowForeground = "#FF48E9AF"; TabInactive = "#FFCCCCCC"; TabActive = "#FFFFFFFF"; TabHover = "#FFF5F5F5"; TabActiveHover = "#FFFFFFFF"; TabText = "#FF000000"; IncognitoInactive = "#FF404040"; IncognitoActive = "#FF151515"; IncognitoHover = "#FF505050"; IncognitoActiveHover = "#FF222222"; IncognitoText = "#FFFFFFFF"; NavBarBackground = "#FFFFFFFF"; NavBarForeground = "#FF000000"; UrlBoxBackground = "#FFE8F9F4"; UrlBoxForeground = "#FF000000"; ButtonAccent = "#FF48E9AF"; ButtonHover = "#FFFFFBEB"; ButtonPressed = "#FFFFF5D6"; ButtonCentro = "#FF48E9AF"; }
        public void SetLightVioletTheme() { Name = "Light Violet"; DarkWebContent = false; IsCustom = false; WindowBackground = "#FFEAEAEA"; WindowForeground = "#FFBD4AEA"; TabInactive = "#FFCCCCCC"; TabActive = "#FFFFFFFF"; TabHover = "#FFF5F5F5"; TabActiveHover = "#FFFFFFFF"; TabText = "#FF000000"; IncognitoInactive = "#FF404040"; IncognitoActive = "#FF151515"; IncognitoHover = "#FF505050"; IncognitoActiveHover = "#FF222222"; IncognitoText = "#FFFFFFFF"; NavBarBackground = "#FFFFFFFF"; NavBarForeground = "#FF000000"; UrlBoxBackground = "#FFF6E8F9"; UrlBoxForeground = "#FF000000"; ButtonAccent = "#FFBD4AEA"; ButtonHover = "#FFFFFBEB"; ButtonPressed = "#FFFFF5D6"; ButtonCentro = "#FFBD4AEA"; }
        public void SetCrazy1Theme() { Name = "Crazy 1"; DarkWebContent = false; IsCustom = false; WindowBackground = "#FFD8554A"; WindowForeground = "#FF4D0045"; TabInactive = "#FFB3AA91"; TabActive = "#FF90E0FC"; TabHover = "#FFFFFFFF"; TabActiveHover = "#FFFFFFFF"; TabText = "#FF2D4D00"; IncognitoInactive = "#FF404040"; IncognitoActive = "#FF151515"; IncognitoHover = "#FF505050"; IncognitoActiveHover = "#FF222222"; IncognitoText = "#FFFFFFFF"; NavBarBackground = "#FF90E0FC"; NavBarForeground = "#FF4D0045"; UrlBoxBackground = "#FF473E13"; UrlBoxForeground = "#FFA5FFBF"; ButtonAccent = "#FFED0038"; ButtonHover = "#FF9FEFFF"; ButtonPressed = "#FFA4F4FF"; ButtonCentro = "#FF007A2A"; }
        public void SetCrazy2Theme() { Name = "Crazy 2"; DarkWebContent = false; IsCustom = false; WindowBackground = "#FFBBD849"; WindowForeground = "#FF4D1100"; TabInactive = "#FF95B391"; TabActive = "#FFB990FC"; TabHover = "#FFFFFFFF"; TabActiveHover = "#FFFFFFFF"; TabText = "#FF004D29"; IncognitoInactive = "#FF404040"; IncognitoActive = "#FF151515"; IncognitoHover = "#FF505050"; IncognitoActiveHover = "#FF222222"; IncognitoText = "#FFFFFFFF"; NavBarBackground = "#FFB990FC"; NavBarForeground = "#FF4D1100"; UrlBoxBackground = "#FF154713"; UrlBoxForeground = "#FFA4DAFF"; ButtonAccent = "#FFEDD100"; ButtonHover = "#FFC89FFF"; ButtonPressed = "#FFCDA4FF"; ButtonCentro = "#FF00417A"; }
        public void SetCrazy3Theme() { Name = "Crazy 3"; DarkWebContent = false; IsCustom = false; WindowBackground = "#FF48CDD8"; WindowForeground = "#FF004D06"; TabInactive = "#FF9199B3"; TabActive = "#FFFCAA90"; TabHover = "#FFFFFFFF"; TabActiveHover = "#FFFFFFFF"; TabText = "#FF1E004D"; IncognitoInactive = "#FF404040"; IncognitoActive = "#FF151515"; IncognitoHover = "#FF505050"; IncognitoActiveHover = "#FF222222"; IncognitoText = "#FFFFFFFF"; NavBarBackground = "#FFFCAA90"; NavBarForeground = "#FF004D06"; UrlBoxBackground = "#FF131B47"; UrlBoxForeground = "#FFFFA3E6"; ButtonAccent = "#FF00EDB1"; ButtonHover = "#FFFFB99F"; ButtonPressed = "#FFFFBEA4"; ButtonCentro = "#FF7A0051"; }
        public void SetCrazy4Theme() { Name = "Crazy 4"; DarkWebContent = false; IsCustom = false; WindowBackground = "#FF484DD8"; WindowForeground = "#FF004D4A"; TabInactive = "#FFA791B3"; TabActive = "#FFEEFC90"; TabHover = "#FFFFFFFF"; TabActiveHover = "#FFFFFFFF"; TabText = "#FF4D0037"; IncognitoInactive = "#FF202020"; IncognitoActive = "#FF000000"; IncognitoHover = "#FF303030"; IncognitoActiveHover = "#FF101010"; IncognitoText = "#FFFFFFFF"; NavBarBackground = "#FFEEFC90"; NavBarForeground = "#FF004D4A"; UrlBoxBackground = "#FF391347"; UrlBoxForeground = "#FFFFB1A3"; ButtonAccent = "#FF0056ED"; ButtonHover = "#FFFDFF9F"; ButtonPressed = "#FFFFFFA4"; ButtonCentro = "#FF7A1B00"; }
        public void SetCrazy5Theme() { Name = "Crazy 5"; DarkWebContent = false; IsCustom = false; WindowBackground = "#FFD84885"; WindowForeground = "#FF2C004D"; TabInactive = "#FFB39991"; TabActive = "#FF90FCDF"; TabHover = "#FFFFFFFF"; TabActiveHover = "#FFFFFFFF"; TabText = "#FF4D4500"; IncognitoInactive = "#FF404040"; IncognitoActive = "#FF151515"; IncognitoHover = "#FF505050"; IncognitoActiveHover = "#FF222222"; IncognitoText = "#FFFFFFFF"; NavBarBackground = "#FF90FCDF"; NavBarForeground = "#FF2C004D"; UrlBoxBackground = "#FF472413"; UrlBoxForeground = "#FFB8FFA3"; ButtonAccent = "#FFED00B2"; ButtonHover = "#FF9FFFEE"; ButtonPressed = "#FFA4FFF3"; ButtonCentro = "#FF147A00"; }
        public void SetMidnightTheme() { Name = "Midnight"; DarkWebContent = true; IsCustom = false; WindowBackground = "#FF0A0A1A"; WindowForeground = "#FFE0E0FF"; TabInactive = "#FF1A1A30"; TabActive = "#FF12122A"; TabHover = "#FF222240"; TabActiveHover = "#FF12122A"; TabText = "#FFE0E0FF"; IncognitoInactive = "#FF202020"; IncognitoActive = "#FF000000"; IncognitoHover = "#FF303030"; IncognitoActiveHover = "#FF101010"; IncognitoText = "#FFE0E0FF"; NavBarBackground = "#FF12122A"; NavBarForeground = "#FFE0E0FF"; UrlBoxBackground = "#FF1E1E3A"; UrlBoxForeground = "#FFE0E0FF"; ButtonAccent = "#FF5C6BC0"; ButtonHover = "#FF212139"; ButtonPressed = "#FF26263E"; ButtonCentro = "#FF5C6BC0"; }
        public void SetOrangeTheme() { Name = "Orange"; DarkWebContent = false; IsCustom = false; WindowBackground = "#FFFFF3E0"; WindowForeground = "#FFE65100"; TabInactive = "#FFFFCC80"; TabActive = "#FFFFE0B2"; TabHover = "#FFFFB74D"; TabActiveHover = "#FFFFE0B2"; TabText = "#FFE65100"; IncognitoInactive = "#FF404040"; IncognitoActive = "#FF151515"; IncognitoHover = "#FF505050"; IncognitoActiveHover = "#FF222222"; IncognitoText = "#FFFFFF"; NavBarBackground = "#FFFFE0B2"; NavBarForeground = "#FFE65100"; UrlBoxBackground = "#FFFFFF"; UrlBoxForeground = "#FFE65100"; ButtonAccent = "#FFFF9800"; ButtonHover = "#FFFFEFC1"; ButtonPressed = "#FFFFF4C6"; ButtonCentro = "#FFFF9800"; }
        public void SetPinkTheme() { Name = "Pink"; DarkWebContent = false; IsCustom = false; WindowBackground = "#FFFCE4EC"; WindowForeground = "#FF880E4F"; TabInactive = "#FFF48FB1"; TabActive = "#FFF8BBD9"; TabHover = "#FFF06292"; TabActiveHover = "#FFF8BBD9"; TabText = "#FF880E4F"; IncognitoInactive = "#FF404040"; IncognitoActive = "#FF151515"; IncognitoHover = "#FF505050"; IncognitoActiveHover = "#FF222222"; IncognitoText = "#FFFFFF"; NavBarBackground = "#FFF8BBD9"; NavBarForeground = "#FF880E4F"; UrlBoxBackground = "#FFFFFF"; UrlBoxForeground = "#FF880E4F"; ButtonAccent = "#FFE91E8C"; ButtonHover = "#FFFFCAE8"; ButtonPressed = "#FFFFCFED"; ButtonCentro = "#FFE91E8C"; }
        public void SetPurpleTheme() { Name = "Purple"; DarkWebContent = false; IsCustom = false; WindowBackground = "#FFF3E5F5"; WindowForeground = "#FF4A148C"; TabInactive = "#FFCE93D8"; TabActive = "#FFE1BEE7"; TabHover = "#FFBA68C8"; TabActiveHover = "#FFE1BEE7"; TabText = "#FF4A148C"; IncognitoInactive = "#FF404040"; IncognitoActive = "#FF151515"; IncognitoHover = "#FF505050"; IncognitoActiveHover = "#FF222222"; IncognitoText = "#FFFFFF"; NavBarBackground = "#FFE1BEE7"; NavBarForeground = "#FF4A148C"; UrlBoxBackground = "#FFFFFF"; UrlBoxForeground = "#FF4A148C"; ButtonAccent = "#FF9C27B0"; ButtonHover = "#FFF0CDF6"; ButtonPressed = "#FFF5D2FB"; ButtonCentro = "#FF9C27B0"; }
        public void SetRedTheme() { Name = "Red"; DarkWebContent = false; IsCustom = false; WindowBackground = "#FFFFEBEE"; WindowForeground = "#FFB71C1C"; TabInactive = "#FFEF9A9A"; TabActive = "#FFFFCDD2"; TabHover = "#FFE57373"; TabActiveHover = "#FFFFCDD2"; TabText = "#FFB71C1C"; IncognitoInactive = "#FF404040"; IncognitoActive = "#FF151515"; IncognitoHover = "#FF505050"; IncognitoActiveHover = "#FF222222"; IncognitoText = "#FFFFFF"; NavBarBackground = "#FFFFCDD2"; NavBarForeground = "#FFB71C1C"; UrlBoxBackground = "#FFFFFF"; UrlBoxForeground = "#FFB71C1C"; ButtonAccent = "#FFF44336"; ButtonHover = "#FFFFDCE1"; ButtonPressed = "#FFFFE1E6"; ButtonCentro = "#FFF44336"; }
        public void SetSepiaTheme() { Name = "Sepia"; DarkWebContent = false; IsCustom = false; WindowBackground = "#FFF5ECD7"; WindowForeground = "#FF5D4037"; TabInactive = "#FFDEC9A0"; TabActive = "#FFE8D5B7"; TabHover = "#FFBCAAA4"; TabActiveHover = "#FFE8D5B7"; TabText = "#FF5D4037"; IncognitoInactive = "#FF404040"; IncognitoActive = "#FF151515"; IncognitoHover = "#FF505050"; IncognitoActiveHover = "#FF222222"; IncognitoText = "#FFFFF8F0"; NavBarBackground = "#FFE8D5B7"; NavBarForeground = "#FF5D4037"; UrlBoxBackground = "#FFFFF8F0"; UrlBoxForeground = "#FF5D4037"; ButtonAccent = "#FF8D6E63"; ButtonHover = "#FFF7E4C6"; ButtonPressed = "#FFFCE9CB"; ButtonCentro = "#FF8D6E63"; }
        public void SetSnowTheme() { Name = "Snow"; DarkWebContent = false; IsCustom = false; WindowBackground = "#FFF0F4F8"; WindowForeground = "#FF4299E1"; TabInactive = "#FFD9E4EE"; TabActive = "#FFE8EFF5"; TabHover = "#FFCBD5E0"; TabActiveHover = "#FFE8EFF5"; TabText = "#FF2D3748"; IncognitoInactive = "#FF404040"; IncognitoActive = "#FF151515"; IncognitoHover = "#FF505050"; IncognitoActiveHover = "#FF222222"; IncognitoText = "#FFFFFF"; NavBarBackground = "#FFE8EFF5"; NavBarForeground = "#FF2D3748"; UrlBoxBackground = "#FFFFFF"; UrlBoxForeground = "#FF2D3748"; ButtonAccent = "#FF4299E1"; ButtonHover = "#FFF7FEFF"; ButtonPressed = "#FFFCFFFF"; ButtonCentro = "#FF4299E1"; }
        public void SetSunsetTheme() { Name = "Sunset"; DarkWebContent = true; IsCustom = false; WindowBackground = "#FF2D1B00"; WindowForeground = "#FFFFD166"; TabInactive = "#FF3D2800"; TabActive = "#FF4E3500"; TabHover = "#FF5E4200"; TabActiveHover = "#FF4E3500"; TabText = "#FFFFD166"; IncognitoInactive = "#FF202020"; IncognitoActive = "#FF000000"; IncognitoHover = "#FF303030"; IncognitoActiveHover = "#FF101010"; IncognitoText = "#FFFFD166"; NavBarBackground = "#FF4E3500"; NavBarForeground = "#FFFFD166"; UrlBoxBackground = "#FF3D2800"; UrlBoxForeground = "#FFFFD166"; ButtonAccent = "#FFFF6B35"; ButtonHover = "#FF5D440F"; ButtonPressed = "#FF624914"; ButtonCentro = "#FFFFD166"; }
        public void SetTealTheme() { Name = "Teal"; DarkWebContent = false; IsCustom = false; WindowBackground = "#FFE0F2F1"; WindowForeground = "#FF004D40"; TabInactive = "#FF80CBC4"; TabActive = "#FFB2DFDB"; TabHover = "#FF4DB6AC"; TabActiveHover = "#FFB2DFDB"; TabText = "#FF004D40"; IncognitoInactive = "#FF404040"; IncognitoActive = "#FF151515"; IncognitoHover = "#FF505050"; IncognitoActiveHover = "#FF222222"; IncognitoText = "#FFFFFF"; NavBarBackground = "#FFB2DFDB"; NavBarForeground = "#FF004D40"; UrlBoxBackground = "#FFFFFF"; UrlBoxForeground = "#FF004D40"; ButtonAccent = "#FF009688"; ButtonHover = "#FFC1EEEA"; ButtonPressed = "#FFC6F3EF"; ButtonCentro = "#FF009688"; }
        public void SetUltravioletTheme() { Name = "Ultraviolet"; DarkWebContent = true; IsCustom = false; WindowBackground = "#FF240047"; WindowForeground = "#FFFF71CE"; TabInactive = "#FF340060"; TabActive = "#FF420084"; TabHover = "#FF56009E"; TabActiveHover = "#FF4B0093"; TabText = "#FFC384FF"; IncognitoInactive = "#FF202020"; IncognitoActive = "#FF000000"; IncognitoHover = "#FF303030"; IncognitoActiveHover = "#FF101010"; IncognitoText = "#FFE7C3FD"; NavBarBackground = "#FF420084"; NavBarForeground = "#FFFF71CE"; UrlBoxBackground = "#FF05000A"; UrlBoxForeground = "#FFE7D6FD"; ButtonAccent = "#FFC671FF"; ButtonHover = "#FF510F93"; ButtonPressed = "#FF561498"; ButtonCentro = "#FFEAADFE"; }
        public void SetVaporwaveTheme() { Name = "Vaporwave"; DarkWebContent = true; IsCustom = false; WindowBackground = "#FF1A0033"; WindowForeground = "#FFFF71CE"; TabInactive = "#FF2D0052"; TabActive = "#FF1A0033"; TabHover = "#FF3D0070"; TabActiveHover = "#FF1A0033"; TabText = "#FFFF71CE"; IncognitoInactive = "#FF202020"; IncognitoActive = "#FF000000"; IncognitoHover = "#FF303030"; IncognitoActiveHover = "#FF101010"; IncognitoText = "#FF01CDFE"; NavBarBackground = "#FF1A0033"; NavBarForeground = "#FFFF71CE"; UrlBoxBackground = "#FF2D0052"; UrlBoxForeground = "#FF01CDFE"; ButtonAccent = "#FFFF71CE"; ButtonHover = "#FF290F42"; ButtonPressed = "#FF2E1447"; ButtonCentro = "#FF01CDFE"; }
    
    }

    public class ThemeManager
    {
        private static ThemeManager _instance;
        public static ThemeManager Instance => _instance ??= new ThemeManager();

        public BrowserTheme CurrentTheme { get; private set; }
        public List<BrowserTheme> PredefinedThemes { get; private set; }
        public bool ApplyThemeToWeb { get; set; } = false;
        public bool AutoDarkModeIncognito { get; set; } = false;
        public List<BrowserTheme> CustomThemes { get; private set; } = new List<BrowserTheme>();

        public ThemeManager()
        {
            LoadPredefinedThemes();
            LoadCustomThemes();
            CurrentTheme = LoadSavedTheme() ?? PredefinedThemes.First(t => t.Name == "Light");
        }

        public void AddCustomTheme(BrowserTheme theme)
        {
            theme.IsCustom = true;
            CustomThemes.RemoveAll(t => t.Name == theme.Name);
            CustomThemes.Add(theme);
            SaveCustomThemes();
        }

        public void SaveCustomThemes()
        {
            var filePath = AppPaths.Themes;
            Directory.CreateDirectory(Path.GetDirectoryName(filePath));
            File.WriteAllText(filePath, JsonConvert.SerializeObject(CustomThemes, Formatting.Indented));
        }

        private void LoadCustomThemes()
        {
            var filePath = AppPaths.Themes;
            if (File.Exists(filePath))
            {
                try
                {
                    CustomThemes = JsonConvert.DeserializeObject<List<BrowserTheme>>(File.ReadAllText(filePath))
                                  ?? new List<BrowserTheme>();
                }
                catch { }
            }
        }

        private void LoadPredefinedThemes()
        {
            PredefinedThemes = new List<BrowserTheme>();
            var methods = typeof(BrowserTheme)
                .GetMethods()
                .Where(m => m.Name.StartsWith("Set") && m.Name.EndsWith("Theme") && m.GetParameters().Length == 0)
                .ToList();
            foreach (var method in methods)
            {
                var t = new BrowserTheme();
                method.Invoke(t, null);
                t.IsCustom = false;
                PredefinedThemes.Add(t);
            }
        }

        public void ApplyTheme(BrowserTheme theme)
        {
            CurrentTheme = theme;
            var resources = Application.Current.Resources;

            SetBrush(resources, "WindowBackgroundBrush", theme.WindowBackground);
            SetBrush(resources, "WindowForegroundBrush", theme.WindowForeground);
            SetBrush(resources, "TabInactiveBrush", theme.TabInactive);
            SetBrush(resources, "TabActiveBrush", theme.TabActive);
            SetBrush(resources, "TabHoverBrush", theme.TabHover);
            SetBrush(resources, "TabActiveHoverBrush", theme.TabActiveHover);
            SetBrush(resources, "TabTextBrush", theme.TabText);
            SetBrush(resources, "IncognitoInactiveBrush", theme.IncognitoInactive);
            SetBrush(resources, "IncognitoActiveBrush", theme.IncognitoActive);
            SetBrush(resources, "IncognitoHoverBrush", theme.IncognitoHover);
            SetBrush(resources, "IncognitoActiveHoverBrush", theme.IncognitoActiveHover);
            SetBrush(resources, "IncognitoTextBrush", theme.IncognitoText);
            SetBrush(resources, "NavBarBackgroundBrush", theme.NavBarBackground);
            SetBrush(resources, "NavBarForegroundBrush", theme.NavBarForeground);
            SetBrush(resources, "UrlBoxBackgroundBrush", theme.UrlBoxBackground);
            SetBrush(resources, "UrlBoxForegroundBrush", theme.UrlBoxForeground);
            SetBrush(resources, "ButtonAccentBrush", theme.ButtonAccent);
            SetBrush(resources, "ButtonHoverBrush", theme.ButtonHover);
            SetBrush(resources, "ButtonPressedBrush", theme.ButtonPressed);
            SetBrush(resources, "ButtonCentroBrush", theme.ButtonCentro);

            SaveTheme(theme);

            Application.Current.Dispatcher.Invoke(() =>
            {
                if (Application.Current.MainWindow is MainWindow mw)
                    mw.ActualizarBotonPanelActivo();
            });
        }

        private static void SetBrush(ResourceDictionary resources, string key, string colorStr)
        {
            var color = (Color)ColorConverter.ConvertFromString(colorStr);
            if (resources[key] is SolidColorBrush existing && !existing.IsFrozen)
                existing.Color = color;
            else
                resources[key] = new SolidColorBrush(color);
        }

        public void SaveTheme(BrowserTheme theme)
        {
            var filePath = AppPaths.ThemeSettings;
            Directory.CreateDirectory(Path.GetDirectoryName(filePath));
            var settings = new { Theme = theme, ApplyThemeToWeb = this.ApplyThemeToWeb, AutoDarkModeIncognito = this.AutoDarkModeIncognito };
            File.WriteAllText(filePath, JsonConvert.SerializeObject(settings, Formatting.Indented));
        }

        private BrowserTheme LoadSavedTheme()
        {
            var filePath = AppPaths.ThemeSettings;
            if (!File.Exists(filePath)) return null;
            try
            {
                var json = File.ReadAllText(filePath);
                var settings = JsonConvert.DeserializeAnonymousType(json, new { Theme = (BrowserTheme)null, ApplyThemeToWeb = false, AutoDarkModeIncognito = false });
                if (settings != null)
                {
                    this.ApplyThemeToWeb = settings.ApplyThemeToWeb;
                    this.AutoDarkModeIncognito = settings.AutoDarkModeIncognito;
                    return settings.Theme;
                }
            }
            catch
            {
                try { return JsonConvert.DeserializeObject<BrowserTheme>(File.ReadAllText(filePath)); }
                catch { }
            }
            return null;
        }

        public BrowserTheme CreateCustomTheme(string name)
        {
            var custom = new BrowserTheme { Name = name, IsCustom = true };
            CopyThemeProperties(CurrentTheme, custom);
            return custom;
        }

        private void CopyThemeProperties(BrowserTheme source, BrowserTheme target)
        {
            target.WindowBackground = source.WindowBackground; target.WindowForeground = source.WindowForeground;
            target.TabInactive = source.TabInactive; target.TabActive = source.TabActive;
            target.TabHover = source.TabHover; target.TabActiveHover = source.TabActiveHover;
            target.TabText = source.TabText; target.IncognitoInactive = source.IncognitoInactive;
            target.IncognitoActive = source.IncognitoActive; target.IncognitoHover = source.IncognitoHover;
            target.IncognitoActiveHover = source.IncognitoActiveHover; target.IncognitoText = source.IncognitoText;
            target.NavBarBackground = source.NavBarBackground; target.NavBarForeground = source.NavBarForeground;
            target.UrlBoxBackground = source.UrlBoxBackground; target.UrlBoxForeground = source.UrlBoxForeground;
            target.ButtonAccent = source.ButtonAccent; target.ButtonHover = source.ButtonHover;
            target.ButtonPressed = source.ButtonPressed; target.ButtonCentro = source.ButtonCentro;
        }

        private void ChkDarkModeIncognito_Changed(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox checkBox)
            {
                ThemeManager.Instance.AutoDarkModeIncognito = checkBox.IsChecked ?? false;
                ThemeManager.Instance.SaveTheme(ThemeManager.Instance.CurrentTheme);
            }
        }
    }

    // ========================================
    //  SISTEMA DE PERMISOS
    // ========================================

    public class DomainPermission
    {
        public string Domain { get; set; }
        public CoreWebView2PermissionState Camera { get; set; }
        public CoreWebView2PermissionState Microphone { get; set; }
        public CoreWebView2PermissionState Location { get; set; }
        public CoreWebView2PermissionState Notifications { get; set; }
    }

    public class PermissionManager
    {
        public static PermissionManager Instance { get; } = Load();

        public CoreWebView2PermissionState GlobalCamera { get; set; } = CoreWebView2PermissionState.Deny;
        public CoreWebView2PermissionState GlobalMicrophone { get; set; } = CoreWebView2PermissionState.Deny;
        public CoreWebView2PermissionState GlobalLocation { get; set; } = CoreWebView2PermissionState.Deny;
        public CoreWebView2PermissionState GlobalNotifications { get; set; } = CoreWebView2PermissionState.Deny;

        public ObservableCollection<DomainPermission> DomainPermissions { get; set; }
            = new ObservableCollection<DomainPermission>();

        private static string FilePath => AppPaths.Permissions;

        public void Save()
        {
            Directory.CreateDirectory(Path.GetDirectoryName(FilePath));
            File.WriteAllText(FilePath, JsonConvert.SerializeObject(this, Formatting.Indented));
        }

        private static PermissionManager Load()
        {
            if (!File.Exists(FilePath)) return new PermissionManager();
            return JsonConvert.DeserializeObject<PermissionManager>(File.ReadAllText(FilePath));
        }

        public void Reset()
        {
            GlobalCamera = GlobalMicrophone = GlobalLocation = GlobalNotifications = CoreWebView2PermissionState.Deny;
            DomainPermissions.Clear();
            Save();
        }

        public DomainPermission GetDomainPermission(string domain)
            => DomainPermissions.FirstOrDefault(d => d.Domain == domain);
    }

    public static class PermissionValues
    {
        public static CoreWebView2PermissionState[] All =
        {
            CoreWebView2PermissionState.Allow,
            CoreWebView2PermissionState.Default,
            CoreWebView2PermissionState.Deny
        };
    }
}
