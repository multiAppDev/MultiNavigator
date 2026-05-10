using Microsoft.Web.WebView2.Core;
using Multinavigator.Idiomas;
using System;
using System.Windows;

namespace Multinavigator
{
    public partial class EditCookieWindow : Window
    {
        public Idioma Trad => Idioma.Instance;
        private readonly CoreWebView2Cookie _cookie;
        private readonly CoreWebView2CookieManager _manager;

        public EditCookieWindow(CoreWebView2Cookie cookie, CoreWebView2CookieManager manager)
        {
            this.DataContext = this;
            InitializeComponent();
            _cookie = cookie;
            _manager = manager;
            LoadCookie();
        }

        private void LoadCookie()
        {
            NameTextBlock.Text          = _cookie.Name;
            ValueTextBox.Text           = _cookie.Value;
            PathTextBox.Text            = _cookie.Path;
            if (!_cookie.IsSession)
                ExpiresDatePicker.SelectedDate = _cookie.Expires;
            SecureCheckBox.IsChecked   = _cookie.IsSecure;
            HttpOnlyCheckBox.IsChecked = _cookie.IsHttpOnly;
            SessionCheckBox.IsChecked  = _cookie.IsSession;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(ValueTextBox.Text))
                {
                    MessageBox.Show(Idioma.Instance.EditCookie_Msg_EmptyValue);
                    return;
                }
                if (string.IsNullOrWhiteSpace(PathTextBox.Text))
                {
                    MessageBox.Show(Idioma.Instance.EditCookie_Msg_EmptyPath);
                    return;
                }
                if (!PathTextBox.Text.StartsWith("/"))
                {
                    MessageBox.Show(Idioma.Instance.EditCookie_Msg_PathSlash);
                    return;
                }

                bool isSession = _cookie.IsSession;

                if (!isSession)
                {
                    if (!ExpiresDatePicker.SelectedDate.HasValue)
                    {
                        MessageBox.Show(Idioma.Instance.EditCookie_Msg_NoDate);
                        return;
                    }
                    if (ExpiresDatePicker.SelectedDate.Value < DateTime.Now)
                    {
                        MessageBox.Show(Idioma.Instance.EditCookie_Msg_PastDate);
                        return;
                    }
                }

                var newCookie = _manager.CreateCookie(
                    _cookie.Name,
                    ValueTextBox.Text,
                    _cookie.Domain,
                    PathTextBox.Text);

                newCookie.IsSecure   = SecureCheckBox.IsChecked == true;
                newCookie.IsHttpOnly = HttpOnlyCheckBox.IsChecked == true;

                if (!isSession)
                    newCookie.Expires = ExpiresDatePicker.SelectedDate!.Value;

                _manager.DeleteCookie(_cookie);
                _manager.AddOrUpdateCookie(newCookie);

                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format(Idioma.Instance.EditCookie_Msg_SaveError, ex.Message));
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e) => Close();
    }
}
