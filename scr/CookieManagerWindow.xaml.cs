using Microsoft.Web.WebView2.Core;
using Multinavigator.Idiomas;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Multinavigator
{
    public partial class CookieManagerWindow : Window
    {
        public Idioma Trad => Idioma.Instance;
        private readonly CoreWebView2CookieManager _cookieManager;
        private List<CoreWebView2Cookie> _allCookies = new();
        private List<CoreWebView2Cookie> _filteredCookies = new();

        public CookieManagerWindow(CoreWebView2CookieManager manager)
        {
            this.DataContext = this;
            InitializeComponent();
            _cookieManager = manager;
            LoadCookies();
        }

        private async void LoadCookies()
        {
            SearchTextBox.Text = "";
            CookieSearchTextBox.Text = "";

            var cookies = await _cookieManager.GetCookiesAsync(null);
            _allCookies = cookies.ToList();

            DomainsListBox.ItemsSource = _allCookies
                .GroupBy(c => c.Domain)
                .Select(g => new { Domain = g.Key, Count = g.Count() })
                .OrderBy(d => d.Domain)
                .ToList();

            CookiesDataGrid.ItemsSource = null;
            ClearDetails();
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string text = SearchTextBox.Text.Trim().ToLower();

            DomainsListBox.ItemsSource = _allCookies
                .GroupBy(c => c.Domain)
                .Select(g => new { Domain = g.Key })
                .Where(d => d.Domain.ToLower().Contains(text))
                .OrderBy(d => d.Domain)
                .ToList();
        }

        private void DomainsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = DomainsListBox.SelectedItem;
            if (selected == null) return;

            string domain = (string)selected.GetType().GetProperty("Domain")!.GetValue(selected)!;

            _filteredCookies = _allCookies
                .Where(c => c.Domain == domain)
                .OrderBy(c => c.Name)
                .ToList();

            CookiesDataGrid.ItemsSource = _filteredCookies;
            ClearDetails();
        }

        private void CookieSearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (_filteredCookies == null) return;

            string text = CookieSearchTextBox.Text.Trim().ToLower();

            CookiesDataGrid.ItemsSource = string.IsNullOrEmpty(text)
                ? _filteredCookies
                : _filteredCookies
                    .Where(c =>
                        c.Name.ToLower().Contains(text) ||
                        c.Value.ToLower().Contains(text) ||
                        c.Path.ToLower().Contains(text))
                    .ToList();
        }

        private void CookiesDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CookiesDataGrid.SelectedItem is not CoreWebView2Cookie c)
            {
                ClearDetails();
                return;
            }

            DetailNameTextBlock.Text    = c.Name;
            DetailValueTextBox.Text     = c.Value;
            DetailDomainTextBlock.Text  = c.Domain;
            DetailPathTextBlock.Text    = c.Path;
            DetailExpiresTextBlock.Text = c.Expires.ToString();
            DetailFlagsTextBlock.Text   = string.Format(
                Idioma.Instance.Cookie_FlagsFormat,
                c.IsSecure, c.IsHttpOnly, c.IsSession);
        }

        private void ClearDetails()
        {
            DetailNameTextBlock.Text    = "";
            DetailValueTextBox.Text     = "";
            DetailDomainTextBlock.Text  = "";
            DetailPathTextBlock.Text    = "";
            DetailExpiresTextBlock.Text = "";
            DetailFlagsTextBlock.Text   = "";
        }

        private void CopyCookieValueButton_Click(object sender, RoutedEventArgs e)
        {
            if (CookiesDataGrid.SelectedItem is CoreWebView2Cookie cookie)
                Clipboard.SetText(cookie.Value);
        }

        private void EditCookieButton_Click(object sender, RoutedEventArgs e)
        {
            if (CookiesDataGrid.SelectedItem is not CoreWebView2Cookie cookie) return;

            var win = new EditCookieWindow(cookie, _cookieManager) { Owner = this };
            win.ShowDialog();
            LoadCookies();
        }

        private async void DeleteCookieButton_Click(object sender, RoutedEventArgs e)
        {
            if (CookiesDataGrid.SelectedItem is not CoreWebView2Cookie cookie) return;

            _cookieManager.DeleteCookie(cookie);
            await System.Threading.Tasks.Task.Delay(200);
            LoadCookies();
        }

        private async void DeleteDomainCookiesButton_Click(object sender, RoutedEventArgs e)
        {
            var selected = DomainsListBox.SelectedItem;
            if (selected == null) return;

            string domain = (string)selected.GetType().GetProperty("Domain")!.GetValue(selected)!;

            foreach (var c in _allCookies.Where(c => c.Domain == domain).ToList())
                _cookieManager.DeleteCookie(c);

            await System.Threading.Tasks.Task.Delay(300);
            LoadCookies();
        }

        private async void DeleteAllCookiesButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (var c in _allCookies)
                _cookieManager.DeleteCookie(c);

            await System.Threading.Tasks.Task.Delay(300);
            LoadCookies();
        }

        private void ReloadButton_Click(object sender, RoutedEventArgs e) => LoadCookies();
        private void CloseButton_Click(object sender, RoutedEventArgs e)  => Close();
    }
}
