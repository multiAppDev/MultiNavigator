using Multinavigator.Idiomas;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Multinavigator
{
    public partial class DragGhostControl : UserControl
    {
        public Idioma Trad => Idioma.Instance;
        public DragGhostControl()
        {
            this.DataContext = this;
            InitializeComponent();
        }

        public void SetText(string text)
        {
            txtTitle.Text = text;
        }

        public void SetIcon(string url)
        {
            try
            {
                imgIcon.Source = new BitmapImage(new Uri(url));
            }
            catch
            {
                imgIcon.Source = null;
            }
        }
    }
}
