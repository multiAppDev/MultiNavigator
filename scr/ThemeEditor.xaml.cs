using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Multinavigator.Idiomas;

namespace Multinavigator
{
    public partial class ThemeEditor : Window
    {
        public Idioma Trad => Idioma.Instance;

        private BrowserTheme _editingTheme;
        private Dictionary<string, Border> _colorBoxes = new Dictionary<string, Border>();
        private Dictionary<string, TextBox> _colorTexts = new Dictionary<string, TextBox>();
        private Dictionary<string, System.Windows.Media.Color> _originalColors = new();

        private Dictionary<string, System.Windows.Media.Color> _baseColors = new();
        private double _accHue = 0, _accSat = 0, _accLum = 0;

        private string _currentEditingProperty;
        private bool _isUpdatingSliders = false;
        private bool _isUpdatingHex = false;

        public ThemeEditor()
        {
            InitializeComponent();
            this.DataContext = this;
            ColorPickerPopup.Visibility = Visibility.Collapsed;

            // 1. Obtener datos (directo y sin vueltas)
            _editingTheme = ThemeManager.Instance.CurrentTheme ?? ThemeManager.Instance.CreateCustomTheme("Nuevo Tema");

            // Sincronizar el bool con la realidad de la ventana principal
            if (Application.Current.MainWindow is MainWindow mainWin)
            {
                _editingTheme.DarkWebContent = mainWin.CurrentDarkWebContentSetting;
            }

            MapColorControls();

            // 2. Cargar UI cuando todo esté listo
            this.Loaded += (s, e) => {
                LoadThemeToUI();
                UpdatePreview();
            };
        }

        private void ColorTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (_isUpdatingSliders) return;
            
            if (sender is not TextBox tb) return;
            string property = tb.Tag?.ToString();
            if (string.IsNullOrEmpty(property)) return;

            try
            {
                var color = (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString(tb.Text);

                if (_colorBoxes.ContainsKey(property))
                    _colorBoxes[property].Background = new SolidColorBrush(color);

                UpdateThemeProperty(property, tb.Text);
                _originalColors[property] = color;
                UpdatePreview();
            }
            catch
            {
                // Color inválido, no hacer nada hasta LostFocus
            }
        }

        private void ColorTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (sender is not TextBox tb) return;
            string property = tb.Tag?.ToString();
            if (string.IsNullOrEmpty(property)) return;

            try
            {
                var color = (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString(tb.Text);
                var hex = $"#{color.A:X2}{color.R:X2}{color.G:X2}{color.B:X2}";
                tb.Text = hex;

                if (_colorBoxes.ContainsKey(property))
                    _colorBoxes[property].Background = new SolidColorBrush(color);

                UpdateThemeProperty(property, hex);

                _originalColors[property] = color;
                _baseColors[property] = color;

                UpdatePreview();
            }
            catch
            {
                if (_colorBoxes.ContainsKey(property))
                {
                    var brush = _colorBoxes[property].Background as SolidColorBrush;
                    if (brush != null)
                        tb.Text = $"#{brush.Color.A:X2}{brush.Color.R:X2}{brush.Color.G:X2}{brush.Color.B:X2}";
                }
            }
        }

        private void MapColorControls()
        {
            _colorBoxes["WindowBackground"] = ColorWindowBackground;
            _colorBoxes["WindowForeground"] = ColorWindowForeground;
            _colorBoxes["TabInactive"] = ColorTabInactive;
            _colorBoxes["TabActive"] = ColorTabActive;
            _colorBoxes["TabHover"] = ColorTabHover;
            _colorBoxes["TabActiveHover"] = ColorTabActiveHover;
            _colorBoxes["TabText"] = ColorTabText;
            _colorBoxes["IncognitoInactive"] = ColorIncognitoInactive;
            _colorBoxes["IncognitoActive"] = ColorIncognitoActive;
            _colorBoxes["IncognitoHover"] = ColorIncognitoHover;
            _colorBoxes["IncognitoActiveHover"] = ColorIncognitoActiveHover;
            _colorBoxes["IncognitoText"] = ColorIncognitoText;
            _colorBoxes["NavBarBackground"] = ColorNavBarBackground;
            _colorBoxes["NavBarForeground"] = ColorNavBarForeground;
            _colorBoxes["UrlBoxBackground"] = ColorUrlBoxBackground;
            _colorBoxes["UrlBoxForeground"] = ColorUrlBoxForeground;
            _colorBoxes["ButtonAccent"] = ColorButtonAccent;
            _colorBoxes["ButtonHover"] = ColorButtonHover;
            _colorBoxes["ButtonPressed"] = ColorButtonPressed;
            _colorBoxes["ButtonCentro"] = ColorButtonCentro;

            _colorTexts["WindowBackground"] = TxtWindowBackground;
            _colorTexts["WindowForeground"] = TxtWindowForeground;
            _colorTexts["TabInactive"] = TxtTabInactive;
            _colorTexts["TabActive"] = TxtTabActive;
            _colorTexts["TabHover"] = TxtTabHover;
            _colorTexts["TabActiveHover"] = TxtTabActiveHover;
            _colorTexts["TabText"] = TxtTabText;
            _colorTexts["IncognitoInactive"] = TxtIncognitoInactive;
            _colorTexts["IncognitoActive"] = TxtIncognitoActive;
            _colorTexts["IncognitoHover"] = TxtIncognitoHover;
            _colorTexts["IncognitoActiveHover"] = TxtIncognitoActiveHover;
            _colorTexts["IncognitoText"] = TxtIncognitoText;
            _colorTexts["NavBarBackground"] = TxtNavBarBackground;
            _colorTexts["NavBarForeground"] = TxtNavBarForeground;
            _colorTexts["UrlBoxBackground"] = TxtUrlBoxBackground;
            _colorTexts["UrlBoxForeground"] = TxtUrlBoxForeground;
            _colorTexts["ButtonAccent"] = TxtButtonAccent;
            _colorTexts["ButtonHover"] = TxtButtonHover;
            _colorTexts["ButtonPressed"] = TxtButtonPressed;
            _colorTexts["ButtonCentro"] = TxtButtonCentro;
        }

        private void LoadThemeToUI()
        {
            TxtThemeName.Text = _editingTheme.Name;

            SetColorUI("WindowBackground", _editingTheme.WindowBackground);
            SetColorUI("WindowForeground", _editingTheme.WindowForeground);
            SetColorUI("TabInactive", _editingTheme.TabInactive);
            SetColorUI("TabActive", _editingTheme.TabActive);
            SetColorUI("TabHover", _editingTheme.TabHover);
            SetColorUI("TabActiveHover", _editingTheme.TabActiveHover);
            SetColorUI("TabText", _editingTheme.TabText);
            SetColorUI("IncognitoInactive", _editingTheme.IncognitoInactive);
            SetColorUI("IncognitoActive", _editingTheme.IncognitoActive);
            SetColorUI("IncognitoHover", _editingTheme.IncognitoHover);
            SetColorUI("IncognitoActiveHover", _editingTheme.IncognitoActiveHover);
            SetColorUI("IncognitoText", _editingTheme.IncognitoText);
            SetColorUI("NavBarBackground", _editingTheme.NavBarBackground);
            SetColorUI("NavBarForeground", _editingTheme.NavBarForeground);
            SetColorUI("UrlBoxBackground", _editingTheme.UrlBoxBackground);
            SetColorUI("UrlBoxForeground", _editingTheme.UrlBoxForeground);
            SetColorUI("ButtonAccent", _editingTheme.ButtonAccent);
            SetColorUI("ButtonHover", _editingTheme.ButtonHover);
            SetColorUI("ButtonPressed", _editingTheme.ButtonPressed);
            SetColorUI("ButtonCentro", _editingTheme.ButtonCentro);

            
            // 1. Forzamos el CheckBox al final de la cola de renderizado
            bool valorRealIsDark = _editingTheme.DarkWebContent;
            Dispatcher.BeginInvoke(new Action(() =>
            {
                ChkDarkWebContent.Click -= ChkDarkWebContent_Click;
                ChkDarkWebContent.IsChecked = valorRealIsDark;
                ChkDarkWebContent.Click += ChkDarkWebContent_Click;
            }), System.Windows.Threading.DispatcherPriority.ContextIdle);

            // 2. Sincronizar colores con Pattern Matching (más limpio)
            foreach (var kvp in _colorBoxes)
            {
                if (kvp.Value.Background is SolidColorBrush brush)
                {
                    _originalColors[kvp.Key] = _baseColors[kvp.Key] = brush.Color;
                }
            }
        }

        private void ChkDarkWebContent_Click(object sender, RoutedEventArgs e)
        {
            if (_editingTheme != null)
            {
                _editingTheme.DarkWebContent = ChkDarkWebContent.IsChecked ?? false;
                // Si quieres que el cambio se vea en tiempo real:
                // (App.Current.MainWindow as MainWindow).UpdateWebDarkMode(_editingTheme.DarkWebContent);
            }
        }

        private void SetColorUI(string property, string colorHex)
        {
            if (_colorBoxes.ContainsKey(property))
            {
                try
                {
                    var color = (Color)ColorConverter.ConvertFromString(colorHex);
                    _colorBoxes[property].Background = new SolidColorBrush(color);
                    _colorTexts[property].Text = colorHex;
                }
                catch { }
            }
        }

        private void SliderLuminosity_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (TxtLuminosity == null) return;
            TxtLuminosity.Text = ((int)e.NewValue).ToString();
            AplicarAjustesGlobales();
        }

        private void SliderHue_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (TxtHueShift == null) return;
            TxtHueShift.Text = $"{(int)e.NewValue}°";
            AplicarAjustesGlobales();
        }

        private void SliderSaturation_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (TxtSaturation == null) return;
            TxtSaturation.Text = ((int)e.NewValue).ToString();
            AplicarAjustesGlobales();
        }

        private void AplicarAjustesGlobales()
        {
            if (SliderLuminosity == null || SliderHue == null) return;

            double luminosity = _accLum + SliderLuminosity.Value;
            double hueShift = _accHue + SliderHue.Value;
            double saturation = _accSat + SliderSaturation.Value;

            foreach (var kvp in _colorBoxes)
            {
                string tag = kvp.Key;
                if (!_baseColors.ContainsKey(tag)) continue;

                var adjusted = AjustarColor(_baseColors[tag], luminosity, hueShift, saturation);
                var hex = $"#{adjusted.A:X2}{adjusted.R:X2}{adjusted.G:X2}{adjusted.B:X2}";

                kvp.Value.Background = new SolidColorBrush(adjusted);
                _originalColors[tag] = adjusted;
                _isUpdatingSliders = true;
                UpdateColorText(tag, hex);
                _isUpdatingSliders = false;
                UpdateThemeProperty(tag, hex);
            }

            UpdatePreview();
        }

        private void SliderGlobal_DragCompleted(object sender, MouseButtonEventArgs e)
        {
            _accHue += SliderHue.Value;
            _accSat += SliderSaturation.Value;
            _accLum += SliderLuminosity.Value;

            foreach (var kvp in _colorBoxes)
            {
                string tag = kvp.Key;
                if (!_baseColors.ContainsKey(tag)) continue;

                var base_ = _baseColors[tag];
                var adjusted = AjustarColor(base_, _accLum, _accHue, _accSat);
                var hex = $"#{adjusted.A:X2}{adjusted.R:X2}{adjusted.G:X2}{adjusted.B:X2}";

                kvp.Value.Background = new SolidColorBrush(adjusted);
                _originalColors[tag] = adjusted;
                _isUpdatingSliders = true;
                UpdateColorText(tag, hex);
                _isUpdatingSliders = false;
                UpdateThemeProperty(tag, hex);
            }

            UpdatePreview();
        }

        private void UpdateColorText(string property, string hex)
        {
            if (property != null && _colorTexts.ContainsKey(property))
                _colorTexts[property].Text = hex;
        }

        private System.Windows.Media.Color AjustarColor(System.Windows.Media.Color original, double luminosity, double hueShift, double saturation = 0)
        {
            RgbToHsl(original.R, original.G, original.B, out double h, out double s, out double l);

            h = (h + hueShift / 360.0 + 1.0) % 1.0;
            l = Math.Clamp(l + luminosity / 200.0, 0, 1);
            s = Math.Clamp(s + saturation / 100.0, 0, 1);

            HslToRgb(h, s, l, out byte r, out byte g, out byte b);

            return System.Windows.Media.Color.FromArgb(original.A, r, g, b);
        }

        private void RgbToHsl(byte r, byte g, byte b, out double h, out double s, out double l)
        {
            double rf = r / 255.0, gf = g / 255.0, bf = b / 255.0;
            double max = Math.Max(rf, Math.Max(gf, bf));
            double min = Math.Min(rf, Math.Min(gf, bf));
            l = (max + min) / 2.0;
            if (max == min) { h = s = 0; return; }
            double d = max - min;
            s = l > 0.5 ? d / (2.0 - max - min) : d / (max + min);
            if (max == rf) h = (gf - bf) / d + (gf < bf ? 6 : 0);
            else if (max == gf) h = (bf - rf) / d + 2;
            else h = (rf - gf) / d + 4;
            h /= 6.0;
        }

        private void HslToRgb(double h, double s, double l, out byte r, out byte g, out byte b)
        {
            if (s == 0) { r = g = b = (byte)(l * 255); return; }
            double q = l < 0.5 ? l * (1 + s) : l + s - l * s;
            double p = 2 * l - q;
            r = (byte)(HueToRgb(p, q, h + 1.0 / 3) * 255);
            g = (byte)(HueToRgb(p, q, h) * 255);
            b = (byte)(HueToRgb(p, q, h - 1.0 / 3) * 255);
        }

        private double HueToRgb(double p, double q, double t)
        {
            if (t < 0) t += 1; if (t > 1) t -= 1;
            if (t < 1.0 / 6) return p + (q - p) * 6 * t;
            if (t < 1.0 / 2) return q;
            if (t < 2.0 / 3) return p + (q - p) * (2.0 / 3 - t) * 6;
            return p;
        }

        private void ColorBox_Click(object sender, MouseButtonEventArgs e)
        {
            var border = sender as Border;
            if (border == null) return;

            _currentEditingProperty = border.Tag as string;
            if (string.IsNullOrEmpty(_currentEditingProperty)) return;

            try
            {
                var currentColorHex = _colorTexts[_currentEditingProperty].Text;
                var currentColor = (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString(currentColorHex);

                RgbToHsl(currentColor.R, currentColor.G, currentColor.B, out double h, out double s, out double l);

                _isUpdatingSliders = true;
                SliderH.Value = h * 360.0;
                SliderS.Value = s * 100.0;
                SliderL.Value = l * 100.0;
                TxtHex.Text = currentColorHex;
                ColorPreview.Background = new SolidColorBrush(currentColor);
                _isUpdatingSliders = false;
            }
            catch { }

            ColorPickerPopup.Visibility = Visibility.Visible;
        }

        private void SliderHSL_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (_isUpdatingSliders) return;
            if (SliderH == null || SliderS == null || SliderL == null) return;
            if (ColorPreview == null || TxtHex == null) return;

            double h = SliderH.Value / 360.0;
            double s = SliderS.Value / 100.0;
            double l = SliderL.Value / 100.0;

            HslToRgb(h, s, l, out byte r, out byte g, out byte b);
            var color = System.Windows.Media.Color.FromArgb(255, r, g, b);

            _isUpdatingSliders = true;
            ColorPreview.Background = new SolidColorBrush(color);
            TxtHex.Text = $"#FF{r:X2}{g:X2}{b:X2}";
            _isUpdatingSliders = false;
        }

        private void TxtHex_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (_isUpdatingSliders) return;

            try
            {
                _isUpdatingHex = true;
                var color = (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString(TxtHex.Text);
                ColorPreview.Background = new SolidColorBrush(color);

                RgbToHsl(color.R, color.G, color.B, out double h, out double s, out double l);

                _isUpdatingSliders = true;
                SliderH.Value = h * 360.0;
                SliderS.Value = s * 100.0;
                SliderL.Value = l * 100.0;
                _isUpdatingSliders = false;

                _isUpdatingHex = false;
            }
            catch
            {
                _isUpdatingHex = false;
                _isUpdatingSliders = false;
            }
        }

        private void BtnPickerOk_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(_currentEditingProperty)) return;

            string newColorHex = TxtHex.Text;

            SetColorUI(_currentEditingProperty, newColorHex);
            UpdateThemeProperty(_currentEditingProperty, newColorHex);

            var brush = _colorBoxes[_currentEditingProperty].Background as SolidColorBrush;
            if (brush != null)
            {
                _originalColors[_currentEditingProperty] = brush.Color;
                _baseColors[_currentEditingProperty] = brush.Color;
            }

            SliderHue.Value = 0;
            SliderSaturation.Value = 0;
            SliderLuminosity.Value = 0;

            UpdatePreview();
            ColorPickerPopup.Visibility = Visibility.Collapsed;
        }

        private void BtnPickerCancel_Click(object sender, RoutedEventArgs e)
        {
            ColorPickerPopup.Visibility = Visibility.Collapsed;
        }

        private void UpdateThemeProperty(string property, string value)
        {
            switch (property)
            {
                case "WindowBackground": _editingTheme.WindowBackground = value; break;
                case "WindowForeground": _editingTheme.WindowForeground = value; break;
                case "TabInactive": _editingTheme.TabInactive = value; break;
                case "TabActive": _editingTheme.TabActive = value; break;
                case "TabHover": _editingTheme.TabHover = value; break;
                case "TabActiveHover": _editingTheme.TabActiveHover = value; break;
                case "TabText": _editingTheme.TabText = value; break;
                case "IncognitoInactive": _editingTheme.IncognitoInactive = value; break;
                case "IncognitoActive": _editingTheme.IncognitoActive = value; break;
                case "IncognitoHover": _editingTheme.IncognitoHover = value; break;
                case "IncognitoActiveHover": _editingTheme.IncognitoActiveHover = value; break;
                case "IncognitoText": _editingTheme.IncognitoText = value; break;
                case "NavBarBackground": _editingTheme.NavBarBackground = value; break;
                case "NavBarForeground": _editingTheme.NavBarForeground = value; break;
                case "UrlBoxBackground": _editingTheme.UrlBoxBackground = value; break;
                case "UrlBoxForeground": _editingTheme.UrlBoxForeground = value; break;
                case "ButtonAccent": _editingTheme.ButtonAccent = value; break;
                case "ButtonHover": _editingTheme.ButtonHover = value; break;
                case "ButtonPressed": _editingTheme.ButtonPressed = value; break;
                case "ButtonCentro": _editingTheme.ButtonCentro = value; break;
            }
        }

        private void UpdatePreview()
        {
            var resources = this.Resources;

            try
            {
                resources["WindowBackgroundBrush"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString(_editingTheme.WindowBackground));
                resources["WindowForegroundBrush"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString(_editingTheme.WindowForeground));
                resources["TabInactiveBrush"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString(_editingTheme.TabInactive));
                resources["TabActiveBrush"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString(_editingTheme.TabActive));
                resources["TabTextBrush"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString(_editingTheme.TabText));
                resources["IncognitoInactiveBrush"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString(_editingTheme.IncognitoInactive));
                resources["IncognitoTextBrush"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString(_editingTheme.IncognitoText));
                resources["NavBarBackgroundBrush"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString(_editingTheme.NavBarBackground));
                resources["UrlBoxBackgroundBrush"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString(_editingTheme.UrlBoxBackground));
                resources["UrlBoxForegroundBrush"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString(_editingTheme.UrlBoxForeground));
                resources["ButtonAccentBrush"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString(_editingTheme.ButtonAccent));
                resources["ButtonCentroBrush"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString(_editingTheme.ButtonCentro));
            }
            catch { }
        }

  

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtThemeName.Text))
            {
                MessageBox.Show(Idioma.Instance.Theme_MsgNoName, Idioma.Instance.Theme_MsgNoNameTitle,
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string nombre = TxtThemeName.Text.Trim();

            bool esPredefinido = ThemeManager.Instance.PredefinedThemes.Any(t => t.Name == nombre);
            if (esPredefinido)
            {
                MessageBox.Show($"'{nombre}' {Idioma.Instance.Theme_MsgPredefined}",
                    Idioma.Instance.Theme_MsgPredefinedTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            bool yaExiste = ThemeManager.Instance.CustomThemes.Any(t => t.Name == nombre);
            if (yaExiste)
            {
                var result = MessageBox.Show(string.Format(Idioma.Instance.Theme_MsgOverwriteFormat, nombre),
                    Idioma.Instance.Theme_MsgOverwriteTitle, MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.No)
                    return;
            }

            _editingTheme.Name = nombre;
            _editingTheme.DarkWebContent = ChkDarkWebContent.IsChecked == true;
            ThemeManager.Instance.AddCustomTheme(_editingTheme);
            ThemeManager.Instance.ApplyTheme(_editingTheme);

            MessageBox.Show(string.Format(Idioma.Instance.Theme_MsgSavedFormat, nombre),
                Idioma.Instance.Theme_MsgSavedTitle, MessageBoxButton.OK, MessageBoxImage.Information);

            DialogResult = true;
            Close();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
