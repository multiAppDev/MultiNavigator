using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Multinavigator.Idiomas;

namespace Multinavigator
{
    public partial class NotasWindow : Window
    {
        public Idioma Trad => Idioma.Instance;
        public NotasWindow()
        {
            this.DataContext = this;
            InitializeComponent();
            // Eliminar notas vacías
            var vacias = NotasManager.Instance.Notas
                .Where(n => string.IsNullOrWhiteSpace(n.Texto))
                .ToList();
            foreach (var v in vacias)
                NotasManager.Instance.Notas.Remove(v);
            NotasManager.Instance.Save();
            CargarNotas();
        }

        private void CargarNotas()
        {
            ListaNotas.ItemsSource = NotasManager.Instance.Notas
                .OrderByDescending(n => n.Fecha)
                .ToList();
        }

        private void BtnNuevaNota_Click(object sender, RoutedEventArgs e)
        {
            // Eliminar notas vacías
            var vacias = NotasManager.Instance.Notas
                .Where(n => string.IsNullOrWhiteSpace(n.Texto))
                .ToList();
            foreach (var v in vacias)
                NotasManager.Instance.Notas.Remove(v);
            NotasManager.Instance.Save();
            // Añadir nueva nota
            NotasManager.Instance.Add(new Nota { Fecha = DateTime.Today });
            CargarNotas();
        }

        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is Nota nota)
            {
                if (MessageBox.Show(Idioma.Instance.Notas_MsgDelete, Idioma.Instance.Notas_MsgConfirm,
                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    NotasManager.Instance.Remove(nota);
                    CargarNotas();
                }
            }
        }

        private void NotaTexto_LostFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox tb && tb.Tag is Nota nota)
            {
                nota.Texto = tb.Text;
                NotasManager.Instance.Save();
            }
        }

        private void Aviso_Changed(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox cb && cb.Tag is Nota nota)
            {
                nota.AvisoActivo = cb.IsChecked ?? false;
                if (!nota.AvisoActivo) nota.FechaAviso = null;
                NotasManager.Instance.Save();
            }
        }

        private void FechaAviso_Changed(object sender, SelectionChangedEventArgs e)
        {
            if (sender is DatePicker dp && dp.Tag is Nota nota)
            {
                nota.FechaAviso = dp.SelectedDate;
                NotasManager.Instance.Save();
            }
        }
    }
}
