using Multinavigator.Idiomas;
using System.Windows;

namespace Multinavigator
{
    public partial class FavoriteEditDialog : Window
    {
        public Idioma Trad => Idioma.Instance;
        public Favorite Item { get; } = new();

        public FavoriteEditDialog() => InitializeComponent();

        public FavoriteEditDialog(Favorite existing) : this()
        {
            this.DataContext = this;
            TbTitle.Text      = existing.Title;
            TbUrl.Text        = existing.Url;
            TbFolder.Text     = existing.Folder;
            TbFaviconUrl.Text = existing.FaviconUrl;
        }

        private void Save_Click(object s, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TbUrl.Text))
            {
                MessageBox.Show(Idioma.Instance.FavDlg_Msg_UrlRequired,
                    Idioma.Instance.FavDlg_Msg_Validation,
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Item.Title      = TbTitle.Text.Trim();
            Item.Url        = TbUrl.Text.Trim();
            Item.Folder     = TbFolder.Text.Trim();
            Item.FaviconUrl = TbFaviconUrl.Text.Trim();
            DialogResult    = true;
        }
    }
}
