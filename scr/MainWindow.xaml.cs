using Microsoft.Web.WebView2.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Xml;




namespace Multinavigator
{
    using Microsoft.Web.WebView2.Core;
    using Microsoft.Web.WebView2.Wpf;
    using Microsoft.Win32;
    using Multinavigator;
    using Multinavigator.Idiomas;
    using Newtonsoft.Json;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Globalization;
    using System.IO.Pipelines;
    using System.Net.Http;
    using System.Net.ServerSentEvents;
    using System.Runtime.CompilerServices;
    using System.Text.RegularExpressions;
    using System.Windows.Controls.Primitives;
    using System.Windows.Data;
    using System.Windows.Documents;
    using System.Windows.Interop;
    using System.Windows.Media.Imaging;
    using System.Windows.Shell;

    public class TabInfo : INotifyPropertyChanged
    {
        private string _title = "";
        private string _url = "";
        private string _faviconUrl = "";
        private bool _isActive;
        private bool _isPlaceholder;
        private bool _isLoading;
        private bool _isIncognito;

        public bool IsIncognito
        {
            get => _isIncognito;
            set
            {
                _isIncognito = value;
                OnPropertyChanged();
            }
        }

        // ⭐ NUEVO: ruta del perfil temporal usado en modo incógnito
        public string? TempProfilePath { get; set; }

        public int PanelId { get; set; }

        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged();
            }
        }

        private string _markColor = "Transparent";
        public string MarkColor
        {
            get => _markColor;
            set
            {
                _markColor = value;
                OnPropertyChanged();
            }
        }

        public string Title
        {
            get => _title;
            set { _title = value; OnPropertyChanged(); }
        }

        public string Url
        {
            get => _url;
            set { _url = value; OnPropertyChanged(); }
        }

        public string FaviconUrl
        {
            get => _faviconUrl;
            set { _faviconUrl = value; OnPropertyChanged(); }
        }

        public bool IsActive
        {
            get => _isActive;
            set { _isActive = value; OnPropertyChanged(); }
        }

        public bool IsPlaceholder
        {
            get => _isPlaceholder;
            set { _isPlaceholder = value; OnPropertyChanged(); }
        }

        // Cada pestaña tiene su propio WebView2
        public WebView2? WebView { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string? name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }




    public partial class MainWindow : Window
    {
        public Idioma Trad => Idioma.Instance;
        private static CoreWebView2Environment? _sharedEnvironment;

        private static CoreWebView2EnvironmentOptions BuildEnvOptions()
        {
            var s = GeneralSettingsManager.Instance;
            var args = new System.Text.StringBuilder();

            if (s.FlagDisableBackgroundNetworking) args.Append("--disable-background-networking ");
            if (s.FlagDisableSync) args.Append("--disable-sync ");
            if (s.FlagDisableTranslate) args.Append("--disable-translate ");
            if (s.FlagDisableExtensions) args.Append("--disable-extensions ");
            if (s.FlagDisableDefaultApps) args.Append("--disable-default-apps ");
            if (s.FlagNoDefaultBrowserCheck) args.Append("--no-default-browser-check ");
            if (s.FlagMetricsRecordingOnly) args.Append("--metrics-recording-only ");
            if (s.FlagDisableBreakpad) args.Append("--disable-breakpad ");
            if (s.FlagDisablePhishingDetection) args.Append("--disable-client-side-phishing-detection ");
            if (s.FlagDisableHangMonitor) args.Append("--disable-hang-monitor ");
            if (s.FlagDisablePromptOnRepost) args.Append("--disable-prompt-on-repost ");
            if (s.FlagDisableDomainReliability) args.Append("--disable-domain-reliability ");
            if (s.FlagDisableComponentUpdate) args.Append("--disable-component-update ");
            if (s.FlagDisableBgTimerThrottling) args.Append("--disable-background-timer-throttling ");
            if (s.FlagDisableRendererBackgrounding) args.Append("--disable-renderer-backgrounding ");
            if (s.FlagDisableIpcFloodingProtection) args.Append("--disable-ipc-flooding-protection ");

            return new CoreWebView2EnvironmentOptions(args.ToString().Trim());
        }

        private static readonly CoreWebView2EnvironmentOptions _envOptions = BuildEnvOptions();

        private bool _appLoading = true;

        private static readonly string UserDataFolder = AppPaths.BrowserData;
        private readonly string FavoritesFilePath = AppPaths.Favorites;


        private bool dragging = false;
        private Point offset;
        private double currentPropX = 0.5;
        private double currentPropY = 0.5;
        private Point ghostOffset;

        private int lastDropIndex = -1;


        private readonly ObservableCollection<TabInfo> Tabs1 = new();
        private readonly ObservableCollection<TabInfo> Tabs2 = new();
        private readonly ObservableCollection<TabInfo> Tabs3 = new();
        private readonly ObservableCollection<TabInfo> Tabs4 = new();

        private readonly Dictionary<FrameworkElement, double> _lastTabX = new();

        public WebView2 Web { get; set; }


        private TabInfo? draggedTab = null;
        private TabInfo? placeholderTab = null;

        private int draggedFromPanel = 0;
        private bool isDraggingTab = false;

        // Variables para el drag
        private Point _tabDragStart;
        private bool _tabDragging = false;

        private ItemsControl? currentTargetPanel = null;
        private int currentDropIndex = -1;

        private bool ghostVisible = false;


        // --- GHOST TAB + REORDER VARIABLES ---
        private FrameworkElement? _draggedTabElement;

        private bool _autoScrollActive = false;
        private const double AutoScrollEdge = 40;   // zona sensible en px
        private const double AutoScrollMaxSpeed = 20; // velocidad máxima

        private readonly ObservableCollection<Favorite> Favorites = new();



        private TabInfo externalPlaceholderTab = null;
        private int lastExternalDropIndex = -1;
        private int lastExternalDropPanel = 0;

        private int _currentSuggestionPanel = 0;
        private bool _navigatingWithKeyboard = false;

        private int _panelMaximizado = 0; // 0 = todos, 1-4 = panel maximizado

        private readonly Stack<(int PanelId, string Url, string Title, string FaviconUrl, string MarkColor)> _closedTabs = new();

        private ObservableCollection<HistoryEntry> _sugerencias = new();

        private bool _updatingSugerencias = false;
        private bool _windowInitialized = false;
        private System.Windows.Threading.DispatcherTimer _saveTimer;

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;

            // preparar motores de navegacion
            _ = CoreWebView2Environment.CreateAsync(
                        browserExecutableFolder: null,
                        userDataFolder: UserDataFolder,
                        options: _envOptions
                    ).ContinueWith(t =>
                    {
                        if (t.IsCompletedSuccessfully)
                        {
                            _sharedEnvironment = t.Result;
                            Dispatcher.Invoke(() => InitTabs()); // ⭐ iniciar tabs cuando el env esté listo
                        }
                    });

            // ⭐ LIMPIAR RESTOS DE SESIONES INCÓGNITO ANTERIORES
            Task.Run(() => CleanupOrphanedIncognitoFolders());

            


            // color de fondo de webviews
            if (ThemeManager.Instance.CurrentTheme.DarkWebContent)
            {
                WebHost1.Background = WebHost2.Background = WebHost3.Background = WebHost4.Background = System.Windows.Media.Brushes.Black;
                CurrentDarkWebContentSetting = true;
            }
            else
            {
                WebHost1.Background = WebHost2.Background = WebHost3.Background = WebHost4.Background = System.Windows.Media.Brushes.White;
                CurrentDarkWebContentSetting = false;
            }

            // ⭐ ASEGURAR QUE LA CARPETA EXISTE
            Directory.CreateDirectory(System.IO.Path.GetDirectoryName(FavoritesFilePath));
            Directory.CreateDirectory(UserDataFolder);

            Loaded += MainWindow_Loaded;
            SizeChanged += MainWindow_SizeChanged;

            Loaded += (s, e) =>
            {
                var gridRow0 = Panel1Grid;
                var scroll = TabsScroll1;
                var tabsPanel = TabsPanel1;

                // 1. Cargar configuración
                var cfg = GeneralSettingsManager.Instance;

                if (!cfg.FirstRun && cfg.WindowWidth > 100)
                {
                    this.Left = cfg.WindowLeft;
                    this.Top = cfg.WindowTop;
                    this.Width = cfg.WindowWidth;
                    this.Height = cfg.WindowHeight;

                    if (cfg.WindowMaximized)
                        this.WindowState = WindowState.Maximized;

                    // ✅ Cargar proporciones SIEMPRE, antes de cualquier otra cosa
                    currentPropX = cfg.BotonCentralX;
                    currentPropY = cfg.BotonCentralY;

                    if (cfg.PanelMaximizado != 0)
                    {
                        MaximizarPanel(cfg.PanelMaximizado);
                        // El botón se reposicionará cuando el usuario pulse 1234
                    }
                    else
                    {
                        Dispatcher.BeginInvoke(() =>
                        {
                            double anchoG = gridPrincipal.ActualWidth;
                            double altoG = gridPrincipal.ActualHeight;
                            gridPrincipal.ColumnDefinitions[0].Width = new GridLength(currentPropX, GridUnitType.Star);
                            gridPrincipal.ColumnDefinitions[1].Width = new GridLength(1 - currentPropX, GridUnitType.Star);
                            gridPrincipal.RowDefinitions[0].Height = new GridLength(currentPropY, GridUnitType.Star);
                            gridPrincipal.RowDefinitions[1].Height = new GridLength(1 - currentPropY, GridUnitType.Star);
                            Canvas.SetLeft(botonCentral, currentPropX * anchoG - botonCentral.Width / 2);
                            Canvas.SetTop(botonCentral, currentPropY * altoG - botonCentral.Height / 2);
                        }, System.Windows.Threading.DispatcherPriority.Loaded);
                    }
                }

                if (cfg.FirstRun)
                {
                    Task.Delay(1200).ContinueWith(_ =>
                        Dispatcher.Invoke(() =>
                        {
                            
                            PopupBienvenida.IsOpen = true;

                            PopupBienvenida.Dispatcher.BeginInvoke(() =>
                            {
                                double popupWidth = ((FrameworkElement)PopupBienvenida.Child).ActualWidth;
                                PopupBienvenida.HorizontalOffset = -popupWidth + BtnHamburguesa.ActualWidth + 3;
                            }, System.Windows.Threading.DispatcherPriority.Loaded);

                            cfg.FirstRun = false;
                            cfg.Save();
                        }));
                }


                // ⭐ Evitar popup al iniciar
                PopupSugerencias.IsOpen = false;
                var web = GetActiveWebView(1);
                web?.Focus();

                ActualizarBotonPanelActivo();

                ListaSugerencias.ItemsSource = _sugerencias;
                CheckAvisosNotas();

                _windowInitialized = true;
            };

            TabsScroll1.ScrollChanged += (_, __) => UpdateScrollButtonsFor(1);
            TabsScroll2.ScrollChanged += (_, __) => UpdateScrollButtonsFor(2);
            TabsScroll3.ScrollChanged += (_, __) => UpdateScrollButtonsFor(3);
            TabsScroll4.ScrollChanged += (_, __) => UpdateScrollButtonsFor(4);

            Tabs1.CollectionChanged += (_, __) => UpdateScrollButtonsFor(1);
            Tabs2.CollectionChanged += (_, __) => UpdateScrollButtonsFor(2);
            Tabs3.CollectionChanged += (_, __) => UpdateScrollButtonsFor(3);
            Tabs4.CollectionChanged += (_, __) => UpdateScrollButtonsFor(4);


            LoadFavorites();

            _saveTimer = new System.Windows.Threading.DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(500)
            };
            _saveTimer.Tick += (s, e) =>
            {
                _saveTimer.Stop();
                if (WindowState == WindowState.Normal)
                {
                    var cfg = GeneralSettingsManager.Instance;
                    cfg.WindowLeft = Left;
                    cfg.WindowTop = Top;
                    cfg.WindowWidth = Width;
                    cfg.WindowHeight = Height;
                    cfg.Save();
                }
            };

            LocationChanged += (s, e) => { _saveTimer.Stop(); _saveTimer.Start(); };
            SizeChanged += (s, e) => { _saveTimer.Stop(); _saveTimer.Start(); };

        }

        private void BtnMinimize_Click(object sender, RoutedEventArgs e)
    => WindowState = WindowState.Minimized;

        private void BtnMaximize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState == WindowState.Maximized
                ? WindowState.Normal
                : WindowState.Maximized;

            // Actualizar icono según estado
            var path = (System.Windows.Shapes.Path)BtnMaxRestore.Content;
            path.Data = Geometry.Parse(WindowState == WindowState.Maximized
                ? "M 3,1 H 11 V 9 H 3 Z M 1,3 H 9 V 11 H 1 Z"  // ❐ restaurar
                : "M 1,1 H 9 V 9 H 1 Z");                       // □ maximizar
        }

        protected override void OnStateChanged(EventArgs e)
        {
            base.OnStateChanged(e);
            if (!_windowInitialized) return;

            base.OnStateChanged(e);

            if (WindowState == WindowState.Maximized)
            {
                _outerBorder.BorderThickness = new Thickness(8);
                var chrome = WindowChrome.GetWindowChrome(this);
                chrome.GlassFrameThickness = new Thickness(0);
                Margin = new Thickness(10);
            }
            else
            {
                _outerBorder.BorderThickness = new Thickness(10);
                var chrome = WindowChrome.GetWindowChrome(this);
                chrome.GlassFrameThickness = new Thickness(10);
                Margin = new Thickness(0);
            }

            var path = (System.Windows.Shapes.Path)BtnMaxRestore.Content;
            path.Data = Geometry.Parse(WindowState == WindowState.Maximized
                ? "M 3,1 H 11 V 9 H 3 Z M 1,3 H 9 V 11 H 1 Z"
                : "M 1,1 H 9 V 9 H 1 Z");
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
            => Close();

        protected override void OnClosing(CancelEventArgs e)
        {
            var s = GeneralSettingsManager.Instance;
            s.WindowMaximized = WindowState == WindowState.Maximized;
            if (WindowState == WindowState.Normal)
            {
                s.WindowLeft = Left;
                s.WindowTop = Top;
                s.WindowWidth = Width;
                s.WindowHeight = Height;
                s.PanelMaximizado = _panelMaximizado;
                s.BotonCentralX = currentPropX;
                s.BotonCentralY = currentPropY;
            }
            s.Save();
            base.OnClosing(e);
        }

        private void TopBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                WindowState = WindowState == WindowState.Maximized
                    ? WindowState.Normal
                    : WindowState.Maximized;
            }
            else
            {
                DragMove();
            }
        }



        private void BtnNotas_Click(object sender, RoutedEventArgs e)
        {
            // 1. Buscar si ya existe la ventana
            var win = Application.Current.Windows.OfType<NotasWindow>().FirstOrDefault();

            if (win != null)
            {
                // 2. Si existe, restaurar si está minimizada y traer al frente
                if (win.WindowState == WindowState.Minimized) win.WindowState = WindowState.Normal;
                win.Activate();
            }
            else
            {
                // 3. Si no existe, crearla vinculada a la principal
                new NotasWindow { Owner = Application.Current.MainWindow }.Show();
            }
        }


        public void ActualizarBotonPanelActivo()
        {
            Button activo = _panelMaximizado switch
            {
                1 => BtnMax1,
                2 => BtnMax2,
                3 => BtnMax3,
                4 => BtnMax4,
                _ => BtnRestore
            };
            SetActivePanel(activo);
        }

        private void CleanupOrphanedIncognitoFolders()
        {
            Task.Run(() =>
            {
                try
                {
                    string tempPath = System.IO.Path.GetTempPath();

                    var orphaned = Directory.GetDirectories(tempPath, "mn7_incog_*");

                    foreach (var folder in orphaned)
                    {
                        try { Directory.Delete(folder, recursive: true); }
                        catch { /* En uso o sin permisos, ignorar */ }
                    }
                }
                catch { }
            });
        }


        private void CheckAvisosNotas()
        {
            var avisos = NotasManager.Instance.GetAvisosHoy();
            if (avisos.Count == 0) return;

            // Construcción del mensaje (mantenemos tu lógica de idiomas)
            string msg = $"{Idioma.Instance.Main_NotesReminderPrefix}{avisos.Count}{Idioma.Instance.Main_NotesReminderSuffix}\n";
            foreach (var n in avisos)
                msg += $"• {n.Fecha:dd/MM/yyyy}: {n.Texto?.Split('\n')[0]}\n";
            msg += $"\n{Idioma.Instance.Main_NotesReminderQuestion}";

            if (MessageBox.Show(msg, Idioma.Instance.Main_NotesReminderTitle,
                MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
            {
                // Aplicamos el patrón de instancia única
                var win = Application.Current.Windows.OfType<NotasWindow>().FirstOrDefault();

                if (win != null)
                {
                    if (win.WindowState == WindowState.Minimized) win.WindowState = WindowState.Normal;
                    win.Activate();
                }
                else
                {
                    new NotasWindow { Owner = this }.Show();
                }
            }
        }


        public enum GlobalPermissionMode
        {
            AllowAlways,
            DenyAlways,
            AskAlways
        }

        public GlobalPermissionMode GlobalCameraPermission { get; set; } = GlobalPermissionMode.AskAlways;


        private CoreWebView2PermissionRequestedEventArgs permisoPendiente;
        private int permisoPanelIdPendiente;

        private void WebView_PermissionRequested(object sender, CoreWebView2PermissionRequestedEventArgs e)
        {
            string dominio = new Uri(e.Uri).Host;
            string permiso = e.PermissionKind.ToString();

            // 1. Ignorar permisos globales del sistema operativo
            //    WebView2 no decide nada, lo decides tú.
            e.State = CoreWebView2PermissionState.Deny;

            // 2. Aplicar la política global de Multinavigator
            switch (GlobalCameraPermission)
            {
                case GlobalPermissionMode.AllowAlways:
                    e.State = CoreWebView2PermissionState.Allow;
                    return;

                case GlobalPermissionMode.DenyAlways:
                    e.State = CoreWebView2PermissionState.Deny;
                    return;

                case GlobalPermissionMode.AskAlways:
                    // Aquí mostramos el popup
                    permisoPendiente = e;
                    permisoPanelIdPendiente = GetPanelIdFromWebView(sender as WebView2);
                    MostrarPopupPermisos(dominio, permiso, permisoPanelIdPendiente);
                    return;
            }
        }


        private void SetActivePanel(Button activeBtn)
        {
            var buttons = new[] { BtnMax1, BtnMax2, BtnMax3, BtnMax4, BtnRestore };
            foreach (var btn in buttons)
            {
                if (btn == activeBtn)
                {
                    btn.Background = (Brush)FindResource("ButtonAccentBrush");
                    btn.Foreground = (Brush)FindResource("TabActiveBrush");
                    btn.Tag = "active";
                    foreach (var tb in FindVisualChildren<TextBlock>(btn))
                        tb.Foreground = (Brush)FindResource("TabActiveBrush");
                }
                else
                {
                    btn.Background = (Brush)FindResource("TabActiveBrush");
                    btn.Foreground = (Brush)FindResource("ButtonAccentBrush");
                    btn.Tag = null;
                    foreach (var tb in FindVisualChildren<TextBlock>(btn))
                        tb.Foreground = (Brush)FindResource("ButtonAccentBrush");
                }
            }
        }

        private static IEnumerable<T> FindVisualChildren<T>(DependencyObject parent) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                if (child is T t) yield return t;
                foreach (var c in FindVisualChildren<T>(child)) yield return c;
            }
        }


        private void BtnMaxPanel1_Click(object sender, RoutedEventArgs e) => MaximizarPanel(1);
        private void BtnMaxPanel2_Click(object sender, RoutedEventArgs e) => MaximizarPanel(2);
        private void BtnMaxPanel3_Click(object sender, RoutedEventArgs e) => MaximizarPanel(3);
        private void BtnMaxPanel4_Click(object sender, RoutedEventArgs e) => MaximizarPanel(4);

        private void MaximizarPanel(int panelId)
        {
            _panelMaximizado = panelId;

            var activeBtn = panelId switch
            {
                1 => BtnMax1,
                2 => BtnMax2,
                3 => BtnMax3,
                4 => BtnMax4,
                _ => BtnRestore
            };
            SetActivePanel(activeBtn);

            bool col0 = panelId == 1 || panelId == 3;
            gridPrincipal.ColumnDefinitions[0].Width = new GridLength(col0 ? 1 : 0, col0 ? GridUnitType.Star : GridUnitType.Pixel);
            gridPrincipal.ColumnDefinitions[1].Width = new GridLength(col0 ? 0 : 1, col0 ? GridUnitType.Pixel : GridUnitType.Star);

            bool row0 = panelId == 1 || panelId == 2;
            gridPrincipal.RowDefinitions[0].Height = new GridLength(row0 ? 1 : 0, row0 ? GridUnitType.Star : GridUnitType.Pixel);
            gridPrincipal.RowDefinitions[1].Height = new GridLength(row0 ? 0 : 1, row0 ? GridUnitType.Pixel : GridUnitType.Star);

            Panel1Grid.Margin = panelId == 1 ? new Thickness(0, 5, 0, 0) : new Thickness(10);
            Panel2Grid.Margin = panelId == 2 ? new Thickness(0, 5, 0, 0) : new Thickness(10);
            Panel3Grid.Margin = panelId == 3 ? new Thickness(0, 5, 0, 0) : new Thickness(10);
            Panel4Grid.Margin = panelId == 4 ? new Thickness(0, 5, 0, 0) : new Thickness(10);

            botonCentral.Visibility = Visibility.Collapsed;
        }

        private void BtnRestorePanels_Click(object sender, RoutedEventArgs e)
        {
            SetActivePanel(BtnRestore);
            _panelMaximizado = 0;

            gridPrincipal.ColumnDefinitions[0].Width = new GridLength(1, GridUnitType.Star);
            gridPrincipal.ColumnDefinitions[1].Width = new GridLength(1, GridUnitType.Star);
            gridPrincipal.RowDefinitions[0].Height = new GridLength(1, GridUnitType.Star);
            gridPrincipal.RowDefinitions[1].Height = new GridLength(1, GridUnitType.Star);

            Panel1Grid.Margin = new Thickness(5, 5, 10, 10);
            Panel2Grid.Margin = new Thickness(10, 5, 5, 10);
            Panel3Grid.Margin = new Thickness(5, 10, 10, 5);
            Panel4Grid.Margin = new Thickness(10, 10, 5, 5);

            botonCentral.Visibility = Visibility.Visible;

            Dispatcher.BeginInvoke(() =>
            {
                var w = Width;
                Width = w + 1;
                Width = w;
            }, System.Windows.Threading.DispatcherPriority.Render);
        }

        private void BtnMenu_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn)
                btn.ContextMenu.IsOpen = true;
        }

        private void MostrarPopupPermisos(string dominio, string permiso, int panelId)
        {
            TxtPermisoMensaje.Text = string.Format(Idioma.Instance.Main_PermFormat, dominio, permiso);

            var urlBox = GetUrlTemplate(panelId);
            if (urlBox != null)
            {
                var point = urlBox.PointToScreen(new Point(0, urlBox.ActualHeight));
                PopupPermisos.HorizontalOffset = point.X;
                PopupPermisos.VerticalOffset = point.Y;
            }

            PopupPermisos.IsOpen = true;
        }


        private void BtnPermitir_Click(object sender, RoutedEventArgs e)
        {
            if (permisoPendiente != null)
                permisoPendiente.State = CoreWebView2PermissionState.Allow;

            PopupPermisos.IsOpen = false;
        }

        private void BtnBloquear_Click(object sender, RoutedEventArgs e)
        {
            if (permisoPendiente != null)
                permisoPendiente.State = CoreWebView2PermissionState.Deny;

            PopupPermisos.IsOpen = false;
        }



        private int GetPanelIdFromWebView(WebView2 web)
        {
            if (web == null) return 0;

            if (Tabs1.Any(t => t.WebView == web)) return 1;
            if (Tabs2.Any(t => t.WebView == web)) return 2;
            if (Tabs3.Any(t => t.WebView == web)) return 3;
            if (Tabs4.Any(t => t.WebView == web)) return 4;

            return 0;
        }


        public static T FindChild<T>(DependencyObject parent, string childName) where T : FrameworkElement
        {
            if (parent == null) return null;

            int count = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < count; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);

                if (child is T typedChild && typedChild.Name == childName)
                    return typedChild;

                var result = FindChild<T>(child, childName);
                if (result != null)
                    return result;
            }

            return null;
        }

        private TextBox GetUrlTemplate(int panelId)
        {
            ContentControl nav = panelId switch
            {
                1 => NavBar1,
                2 => NavBar2,
                3 => NavBar3,
                4 => NavBar4,
                _ => null
            };

            if (nav == null)
                return null;

            // Obtener el ContentPresenter que genera el DataTemplate
            var presenter = FindChild<ContentPresenter>(nav, null);
            if (presenter == null)
                return null;

            // Buscar el TextBox dentro del template
            return FindChild<TextBox>(presenter, "UrlTemplate");
        }



        // =============================================
        //   MÉTODO: Abrir el menú ⋮
        // =============================================

        // PARA EL BOTÓN DE OPCIONES, parpadea si y ya estaba abierto el menu
        private void AbrirMenuCompartido(object sender, EventArgs e)
        {
            var elemento = sender as FrameworkElement;
            var menu = (ContextMenu)FindResource("SharedContextMenu");

            // 1. Marcar el evento como manejado si es un clic de ratón (para el botón)
            if (e is MouseButtonEventArgs mbe)
            {
                mbe.Handled = true;
            }

            // 2. Resetear y sincronizar DataContext
            menu.IsOpen = false;
            menu.DataContext = null;
            menu.DataContext = elemento?.DataContext;

            // 3. Configurar posición
            menu.Placement = System.Windows.Controls.Primitives.PlacementMode.MousePoint;

            // 4. Lógica de apertura condicionada:
            // Si NO es el evento nativo de apertura (ContextMenuEventArgs), forzamos la apertura manual (para el botón).
            if (!(e is ContextMenuEventArgs))
            {
                menu.IsOpen = true;
            }
        }

        private void BtnMenuOpciones_Click(object sender, RoutedEventArgs e)
        {
            e.Handled = true; // no dejar subir el evento
        }

      

        private void Menu_MoveTab_Click(object sender, RoutedEventArgs e)
        {
            var mi = sender as MenuItem;
            var tab = (mi?.DataContext as TabInfo) ?? (mi?.Tag as TabInfo);

            string[] parts = (mi?.Tag as string)?.Split(',') ?? Array.Empty<string>();
            if (parts.Length < 2) return;

            string action = parts[0];
            if (!int.TryParse(parts[1], out int targetPanel)) return;

            if (tab == null) return;
            if (targetPanel == tab.PanelId) return;

            var targetList = GetTabsList(targetPanel);

            if (action == "move")
            {
                // Quitar del panel origen
                var originList = GetTabsList(tab.PanelId);
                originList.Remove(tab);

                // Si quedó vacío crear nueva pestaña
                if (originList.Count == 0)
                {
                    var newTab = new TabInfo
                    {
                        Title = Idioma.Instance.Main_NewTab,
                        Url = "https://www.google.com",
                        FaviconUrl = "https://www.google.com/s2/favicons?domain=google.com",
                        PanelId = tab.PanelId,
                        IsActive = true
                    };
                    newTab.WebView = CreateWebViewForTab(newTab);
                    originList.Add(newTab);
                }

                // Activar otra pestaña en origen si era la activa
                if (tab.IsActive)
                {
                    foreach (var t in originList) t.IsActive = false;
                    if (originList.Count > 0) originList[0].IsActive = true;
                    ApplyActiveTabToWeb(tab.PanelId);
                }

                // Insertar en destino
                tab.PanelId = targetPanel;
                foreach (var t in targetList) t.IsActive = false;
                tab.IsActive = true;
                targetList.Add(tab);
                ApplyActiveTabToWeb(targetPanel);
            }
            else if (action == "copy")
            {
                var newTab = new TabInfo
                {
                    Title = tab.Title,
                    Url = tab.Url,
                    FaviconUrl = tab.FaviconUrl,
                    PanelId = targetPanel,
                    IsActive = true,
                    MarkColor = tab.MarkColor
                };
                newTab.WebView = CreateWebViewForTab(newTab);

                foreach (var t in targetList) t.IsActive = false;
                targetList.Add(newTab);
                ApplyActiveTabToWeb(targetPanel);
            }
        }

        private void MenuNav_IrInicio_Click(object sender, RoutedEventArgs e)
        {
            var menuItem = sender as MenuItem;
            var tab = menuItem?.Tag as TabInfo;

            if (tab?.WebView != null)
            {
                tab.WebView.Source = new Uri("https://www.google.com");
            }
        }




        private async Task SetColorSchemePreference(WebView2 wv, bool isDark)
        {
            if (wv?.CoreWebView2 == null)
                return;

            try
            {
                // 1. Preferencia nativa del perfil (lo que detectan Google, YouTube, etc.)
                wv.CoreWebView2.Profile.PreferredColorScheme =
                    isDark ? CoreWebView2PreferredColorScheme.Dark
                           : CoreWebView2PreferredColorScheme.Light;

                // 2. Aplicación inmediata (solo afecta a webs que soportan color-scheme dinámico)
                string scheme = isDark ? "dark" : "light";
                string js = $"document.documentElement.style.setProperty('color-scheme', '{scheme}');";

                await wv.ExecuteScriptAsync(js);
            }
            catch
            {
                // Si la web aún no está cargada, simplemente no hace nada
            }
        }


        public bool CurrentDarkWebContentSetting { get; private set; }
        public void UpdateAllWebViewsColorScheme(bool useDarkWeb)
        {
            CurrentDarkWebContentSetting = useDarkWeb;
            var allTabs = Tabs1.Concat(Tabs2).Concat(Tabs3).Concat(Tabs4);

            foreach (var tab in allTabs)
            {
                if (tab.WebView?.CoreWebView2 != null)
                {
                    // 1. Cambiamos la preferencia (esto no rompe nada)
                    tab.WebView.CoreWebView2.Profile.PreferredColorScheme = useDarkWeb
                        ? CoreWebView2PreferredColorScheme.Dark
                        : CoreWebView2PreferredColorScheme.Light;

                    // 2. Refrescamos para que la web detecte el cambio y se pinte bien
                    // Solo si quieres que el cambio sea inmediato en webs como Google
                    tab.WebView.Reload();
                }
            }
        }


        // =============================================
        //   MÉTODO: Ir a página de inicio
        // =============================================
        private void IrInicio_Click(object sender, RoutedEventArgs e)
        {
            var menu = (sender as MenuItem).Parent as ContextMenu;
            var webView = menu.DataContext as WebView2;

            if (webView != null)
            {
                // Cambia aquí tu página de inicio
                webView.Source = new Uri("https://www.google.com");
            }
        }


        // =============================================
        //   MÉTODO: Activar modo incógnito
        // =============================================

        private void SaveCurrentSession()
        {
            var session = Tabs1.Concat(Tabs2).Concat(Tabs3).Concat(Tabs4)
                .Where(t => !t.IsIncognito && !t.IsPlaceholder && !string.IsNullOrEmpty(t.Url))
                .Select(t => new SessionTab
                {
                    PanelId = t.PanelId,
                    Url = t.Url,
                    Title = t.Title,
                    MarkColor = t.MarkColor
                })
                .ToList();
            GeneralSettingsManager.Instance.SaveSession(session);
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            SaveCurrentSession();
            foreach (var tab in Tabs1.Concat(Tabs2).Concat(Tabs3).Concat(Tabs4))
                CleanupIncognitoTab(tab);
        }

        private async void Menu_ToggleIncognito_Click(object sender, RoutedEventArgs e)
        {
            if (sender is not MenuItem mi || mi.Tag is not TabInfo tab)
                return;

            // 1) ELIMINAR EL WEBVIEW ANTERIOR
            var host = GetWebHost(tab.PanelId);
            if (host != null)
                host.Children.Clear();
            CleanupIncognitoTab(tab);

            // 2) CAMBIAR ESTADO
            tab.IsIncognito = !tab.IsIncognito;

            // 3) CREAR NUEVO WEBVIEW
            var wv = new WebView2();

            // 4) INSERTARLO EN EL HOST *ANTES* DE INICIALIZAR
            if (host != null)
            {
                host.Children.Clear();
                host.Children.Add(wv);
            }

            // 5) INICIALIZAR SEGÚN MODO
            if (tab.IsIncognito)
            {
                await InitIncognitoEnvironment(wv, tab);
            }
            else
            {
                var env = _sharedEnvironment ??
                          await CoreWebView2Environment.CreateAsync(
                              browserExecutableFolder: null,
                              userDataFolder: UserDataFolder,
                              options: _envOptions);
                await wv.EnsureCoreWebView2Async(env);
                tab.TempProfilePath = null;
            }

            // 6) NAVEGAR
            if (wv.CoreWebView2 != null)
                wv.Source = new Uri("https://www.google.com");
            else
            {
                wv.CoreWebView2InitializationCompleted += (s, args) =>
                {
                    if (!args.IsSuccess) return;
                    Dispatcher.Invoke(() => wv.Source = new Uri("https://www.google.com"));
                };
            }

            // 7) ASEGURAR PanelId correcto
            if (tab.PanelId == 0)
            {
                if (Tabs1.Contains(tab)) tab.PanelId = 1;
                else if (Tabs2.Contains(tab)) tab.PanelId = 2;
                else if (Tabs3.Contains(tab)) tab.PanelId = 3;
                else if (Tabs4.Contains(tab)) tab.PanelId = 4;
            }

            // 8) ASEGURAR que queda activa
            var tabList = GetTabsList(tab.PanelId);
            foreach (var t in tabList)
                t.IsActive = false;
            tab.IsActive = true;

            // 9) CONFIGURAR WEBVIEW (incluyendo menú contextual)
            ConfigureWebView(wv, tab);

            // 10) Si ya está inicializado, configurar CoreWebView2 directamente
            if (wv.CoreWebView2 != null)
                ConfigureCoreWebView2(wv, tab, wv.CoreWebView2);

            tab.WebView = wv;
            ApplyActiveTabToWeb(tab.PanelId);
        }



        private bool _urlBoxClicked = false;

        private void UrlBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox tb)
                tb.SelectAll();
        }

        private void UrlBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is TextBox tb && !tb.IsKeyboardFocused)
            {
                tb.Focus();
                e.Handled = true;
            }
        }

        private void UrlBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender is TextBox tb)
            {
                tb.SelectAll();
                e.Handled = true;
            }
        }


        private void OpenSettings_Click(object sender, RoutedEventArgs e)
        {
            int index = int.TryParse((sender as MenuItem)?.Tag?.ToString(), out int i) ? i : 0;

            // 1. Buscamos si ya existe la ventana
            var win = Application.Current.Windows.OfType<Configuracion>().FirstOrDefault();

            if (win != null)
            {
                // 2. Si ya existe, actualizamos la pestaña seleccionada (ajusta el nombre del TabControl)
                // win.TabControlConfig.SelectedIndex = index; 

                if (win.WindowState == WindowState.Minimized) win.WindowState = WindowState.Normal;
                win.Activate();
            }
            else
            {
                // 3. Si no existe, la creamos
                new Configuracion(index) { Owner = this }.Show();
            }
        }



        private void Menu_ClearMark_Click(object sender, RoutedEventArgs e)
        {
            if (sender is MenuItem mi &&
                mi.Tag is TabInfo tab)
            {
                tab.MarkColor = "Transparent";
            }
        }




        private void Menu_SetMark_Click(object sender, RoutedEventArgs e)
        {
            if (sender is MenuItem mi &&
                mi.Tag is TabInfo tab &&
                mi.CommandParameter is SolidColorBrush brush)
            {
                tab.MarkColor = brush.Color.ToString();
            }
        }



        private void LoadFavorites()
        {
            try
            {
                if (File.Exists(FavoritesFilePath))
                {
                    var json = File.ReadAllText(FavoritesFilePath);
                    var list = JsonConvert.DeserializeObject<List<Favorite>>(json);
                    if (list != null)
                    {
                        Favorites.Clear();
                        foreach (var f in list)
                            Favorites.Add(f);
                    }
                }
            }
            catch { }
        }

        private void SaveFavorites()
        {
            try
            {
                var list = Favorites.ToList();
                var json = JsonConvert.SerializeObject(list, Formatting.Indented);
                File.WriteAllText(FavoritesFilePath, json);
            }
            catch { }
        }

        private void AddCurrentTabToFavorites(int panelId)
        {
            var list = GetTabsList(panelId);
            var active = list.FirstOrDefault(t => t.IsActive && !t.IsPlaceholder);
            if (active == null)
                return;

            // evitar duplicados exactos por URL
            if (Favorites.Any(f => string.Equals(f.Url, active.Url, StringComparison.OrdinalIgnoreCase)))
                return;

            Favorites.Add(new Favorite
            {
                Title = active.Title,
                Url = active.Url,
                FaviconUrl = active.FaviconUrl,
                Folder = ""
            });

            SaveFavorites();
        }


        private void BtnAddFavorite_Click(object sender, RoutedEventArgs e)
        {
            PopupFavorites.IsOpen = false;
            AddCurrentTabToFavorites(_currentFavoritesPanel);
        }

        private void BtnOpenFavorites_Click(object sender, RoutedEventArgs e)
        {
            PopupFavorites.IsOpen = false;
            OpenFavoritesManager();
        }




        private void OpenFavoritesManager()
        {
            // 1. Buscar si ya existe la ventana
            var win = Application.Current.Windows.OfType<FavoritesWindow>().FirstOrDefault();

            if (win != null)
            {
                if (win.WindowState == WindowState.Minimized) win.WindowState = WindowState.Normal;
                win.Activate();
            }
            else
            {
                // 2. Si no existe, la creamos y la mostramos
                var newWin = new FavoritesWindow(Favorites) { Owner = this };

                // OPCIONAL: Si quieres que guarde al cerrar la ventana, usa el evento Closed
                newWin.Closed += (s, e) => SaveFavorites();

                newWin.Show();
            }
        }




        public void OpenHistory_Click(object sender, RoutedEventArgs e)
        {
            // 1. Buscamos si ya existe la ventana en la aplicación
            var win = Application.Current.Windows.OfType<HistoryWindow>().FirstOrDefault();

            if (win != null)
            {
                // 2. Si existe, la restauramos y traemos al frente
                if (win.WindowState == WindowState.Minimized) win.WindowState = WindowState.Normal;
                win.Activate();
            }
            else
            {
                // 3. Si no existe, la creamos vinculada a la principal
                new HistoryWindow { Owner = Application.Current.MainWindow }.Show();
            }
        }






        private void UpdateScrollButtonsFor(int panelId)
        {
            ScrollViewer sv = panelId switch
            {
                1 => TabsScroll1,
                2 => TabsScroll2,
                3 => TabsScroll3,
                4 => TabsScroll4,
                _ => TabsScroll1
            };

            ColumnDefinition colLeft = panelId switch
            {
                1 => ColScrollLeft1,
                2 => ColScrollLeft2,
                3 => ColScrollLeft3,
                4 => ColScrollLeft4,
                _ => ColScrollLeft1
            };

            ColumnDefinition colRight = panelId switch
            {
                1 => ColScrollRight1,
                2 => ColScrollRight2,
                3 => ColScrollRight3,
                4 => ColScrollRight4,
                _ => ColScrollRight1
            };

            bool overflow = sv.ExtentWidth > sv.ViewportWidth;

            colLeft.Width = overflow ? new GridLength(20) : new GridLength(0);
            colRight.Width = overflow ? new GridLength(20) : new GridLength(0);
        }





        private void HideGhost()
        {
            GhostPopup.IsOpen = false;
            ghostVisible = false;
        }



        private void AutoScrollDuringDrag(ScrollViewer scroll, Point mousePosInScroll)
        {
            double width = scroll.ActualWidth;

            double leftDist = mousePosInScroll.X;
            double rightDist = width - mousePosInScroll.X;

            double speed = 0;

            if (leftDist < AutoScrollEdge)
            {
                double t = 1.0 - (leftDist / AutoScrollEdge);
                speed = -AutoScrollMaxSpeed * t;
            }
            else if (rightDist < AutoScrollEdge)
            {
                double t = 1.0 - (rightDist / AutoScrollEdge);
                speed = AutoScrollMaxSpeed * t;
            }

            if (speed != 0)
            {
                _autoScrollActive = true;
                scroll.ScrollToHorizontalOffset(scroll.HorizontalOffset + speed);
            }
            else
            {
                _autoScrollActive = false;
            }
        }


        private void ForceTabsRemeasure()
        {
            TabsScroll1.InvalidateMeasure();
            TabsScroll1.InvalidateArrange();
            TabsScroll1.UpdateLayout();
        }


        private void InitTabs()
        {
            var settings = GeneralSettingsManager.Instance;

            if (settings.RestoreSession && settings.LastSession.Count > 0)
            {
                // ⭐ RESTAURAR SESIÓN ANTERIOR
                foreach (var sessionTab in settings.LastSession)
                {
                    var t = new TabInfo
                    {
                        Title = sessionTab.Title ?? sessionTab.Url,
                        Url = sessionTab.Url,
                        FaviconUrl = $"https://www.google.com/s2/favicons?domain={new Uri(sessionTab.Url).Host}",
                        IsActive = false,
                        PanelId = sessionTab.PanelId,
                        MarkColor = sessionTab.MarkColor
                    };
                    t.WebView = CreateWebViewForTab(t);

                    switch (sessionTab.PanelId)
                    {
                        case 1: Tabs1.Add(t); break;
                        case 2: Tabs2.Add(t); break;
                        case 3: Tabs3.Add(t); break;
                        case 4: Tabs4.Add(t); break;
                    }
                }

                // Marcar la primera pestaña de cada panel como activa
                if (Tabs1.Count > 0) Tabs1[0].IsActive = true;
                if (Tabs2.Count > 0) Tabs2[0].IsActive = true;
                if (Tabs3.Count > 0) Tabs3[0].IsActive = true;
                if (Tabs4.Count > 0) Tabs4[0].IsActive = true;

                // Si algún panel quedó vacío, abrir URL de inicio
                if (Tabs1.Count == 0) { var t = CrearTabInicio(settings.HomeUrl1, 1); Tabs1.Add(t); }
                if (Tabs2.Count == 0) { var t = CrearTabInicio(settings.HomeUrl2, 2); Tabs2.Add(t); }
                if (Tabs3.Count == 0) { var t = CrearTabInicio(settings.HomeUrl3, 3); Tabs3.Add(t); }
                if (Tabs4.Count == 0) { var t = CrearTabInicio(settings.HomeUrl4, 4); Tabs4.Add(t); }
            }
            else
            {
                // ⭐ ABRIR URLS DE INICIO
                var t1 = CrearTabInicio(settings.HomeUrl1, 1); Tabs1.Add(t1);
                var t2 = CrearTabInicio(settings.HomeUrl2, 2); Tabs2.Add(t2);
                var t3 = CrearTabInicio(settings.HomeUrl3, 3); Tabs3.Add(t3);
                var t4 = CrearTabInicio(settings.HomeUrl4, 4); Tabs4.Add(t4);
            }

            TabsPanel1.ItemsSource = Tabs1;
            TabsPanel2.ItemsSource = Tabs2;
            TabsPanel3.ItemsSource = Tabs3;
            TabsPanel4.ItemsSource = Tabs4;

            Dispatcher.BeginInvoke(new Action(() =>
            {
                NavBar1.ApplyTemplate(); 
                NavBar2.ApplyTemplate();
                NavBar3.ApplyTemplate(); 
                NavBar4.ApplyTemplate();
                NavBar1.UpdateLayout(); 
                NavBar2.UpdateLayout();
                NavBar3.UpdateLayout();
                NavBar4.UpdateLayout();
                ApplyActiveTabToWeb(1); 
                ApplyActiveTabToWeb(2);
                ApplyActiveTabToWeb(3); 
                ApplyActiveTabToWeb(4);
            }), System.Windows.Threading.DispatcherPriority.Loaded);
        }

        private TabInfo CrearTabInicio(string url, int panelId)
        {
            var t = new TabInfo
            {
                Title = url,
                Url = url,
                FaviconUrl = $"https://www.google.com/s2/favicons?domain={new Uri(url).Host}",
                IsActive = true,
                PanelId = panelId
            };
            t.WebView = CreateWebViewForTab(t);
            return t;
        }

        private void Menu_AbrirEnNavegador_Click(object sender, RoutedEventArgs e)
        {
            if (sender is not MenuItem mi || mi.Tag is not TabInfo tab) return;
            if (string.IsNullOrEmpty(tab.Url)) return;

            try
            {
                Process.Start(new ProcessStartInfo(tab.Url) { UseShellExecute = true });
            }
            catch { }
        }

        private void ReopenLastClosedTab_Click(object sender, RoutedEventArgs e)
    => ReopenLastClosedTab();



        private void Menu_RestoreSession_Click(object sender, RoutedEventArgs e)
        {
            var session = GeneralSettingsManager.Instance.LastSession;
            if (session == null || session.Count == 0)
            {
                MessageBox.Show(Idioma.Instance.Main_NoSession, Idioma.Instance.Main_RestoreSessionTitle,
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            // URLs+colores ya abiertas
            var abiertas = Tabs1.Concat(Tabs2).Concat(Tabs3).Concat(Tabs4)
                .Select(t => (t.Url, t.MarkColor))
                .ToHashSet();

            foreach (var sessionTab in session)
            {
                bool tieneMarca = sessionTab.MarkColor != "Transparent" && !string.IsNullOrEmpty(sessionTab.MarkColor);

                // Si ya está abierta con misma URL y mismo color, no abrir
                if (abiertas.Contains((sessionTab.Url, sessionTab.MarkColor))) continue;

                // Si no tiene marca y ya hay alguna con esa URL, no abrir
                if (!tieneMarca && abiertas.Any(a => a.Url == sessionTab.Url)) continue;

                var t = new TabInfo
                {
                    Title = sessionTab.Title ?? sessionTab.Url,
                    Url = sessionTab.Url,
                    FaviconUrl = $"https://www.google.com/s2/favicons?domain={new Uri(sessionTab.Url).Host}",
                    IsActive = false,
                    PanelId = sessionTab.PanelId,
                    MarkColor = sessionTab.MarkColor
                };
                t.WebView = CreateWebViewForTab(t);

                switch (sessionTab.PanelId)
                {
                    case 1: Tabs1.Add(t); break;
                    case 2: Tabs2.Add(t); break;
                    case 3: Tabs3.Add(t); break;
                    case 4: Tabs4.Add(t); break;
                }
            }
        }

        private void Menu_GroupByColor_Click(object sender, RoutedEventArgs e)
        {
            if (sender is not MenuItem mi || mi.Tag is not TabInfo tab) return;
            AgruparPorColor(tab);
        }

        private void AgruparPorColor(TabInfo tab)
        {
            var list = GetTabsList(tab.PanelId);

            var ordenadas = list
                .OrderBy(t =>
                {
                    if (t.MarkColor == "Transparent" || string.IsNullOrEmpty(t.MarkColor))
                        return -1;
                    try
                    {
                        var color = (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString(t.MarkColor);
                        double r = color.R / 255.0;
                        double g = color.G / 255.0;
                        double b = color.B / 255.0;
                        double max = Math.Max(r, Math.Max(g, b));
                        double min = Math.Min(r, Math.Min(g, b));
                        if (max == min) return 999;
                        double d = max - min;
                        double h = 0;
                        if (max == r) h = (g - b) / d + (g < b ? 6 : 0);
                        else if (max == g) h = (b - r) / d + 2;
                        else h = (r - g) / d + 4;
                        h /= 6.0;
                        return (int)(h * 1000);
                    }
                    catch { return 999; }
                })
                .ToList();

            var activa = list.FirstOrDefault(t => t.IsActive);
            list.Clear();
            foreach (var t in ordenadas)
                list.Add(t);
            if (activa != null)
                activa.IsActive = true;

            ApplyActiveTabToWeb(tab.PanelId);

            Dispatcher.BeginInvoke(() =>
            {
                ScrollViewer? target = tab.PanelId switch
                {
                    1 => TabsScroll1,
                    2 => TabsScroll2,
                    3 => TabsScroll3,
                    4 => TabsScroll4,
                    _ => null
                };
                target?.ScrollToHorizontalOffset(0);
            }, System.Windows.Threading.DispatcherPriority.Background);
        }


        private void AcercaDe_Click(object sender, RoutedEventArgs e)
        {
            // 1. Obtener versiones de forma dinámica
            string wv2Version = "N/A";
            try { wv2Version = CoreWebView2Environment.GetAvailableBrowserVersionString(); }
            catch { }

            // Lee la versión configurada en tu proyecto (ej. 7.0.0)
            string versionProyecto = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version?.ToString(3) ?? "7.0.0";

            // 2. Configuración de la Ventana
            var win = new Window
            {
                Title = Idioma.Instance.Main_AboutTitle,
                Width = 380,
                SizeToContent = SizeToContent.Height,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                Owner = this,
                ResizeMode = ResizeMode.NoResize,
                WindowStyle = WindowStyle.ToolWindow,
                ShowInTaskbar = false,
                Background = Brushes.White
            };

            var panel = new StackPanel { Margin = new Thickness(30, 40, 30, 40) };

            // 3. Logo (Icono central)
            var logo = new System.Windows.Shapes.Path
            {
                Data = Geometry.Parse("m 88.867,179.274 c -4.825,-12.714 -8.773,-23.311 -8.773,-23.547 0,-0.489 0.409,-0.573 0.677,-0.139 0.398,0.645 4.24,2.906 6.278,3.694 6.712,2.598 15.093,2.598 21.806,0 2.037,-0.788 5.879,-3.049 6.278,-3.694 0.264,-0.428 0.677,-0.353 0.677,0.123 0,0.805 -17.549,46.681 -17.857,46.681 -0.172,0 -4.261,-10.403 -9.086,-23.118 z M 117.399,153.609 c 0,-0.2 0.179,-0.513 0.399,-0.696 0.65,-0.539 2.727,-4.023 3.429,-5.75 3.572,-8.792 2.454,-20.372 -2.634,-27.284 -0.657,-0.893 -1.195,-1.697 -1.195,-1.786 0,-0.089 0.289,-0.107 0.643,-0.039 1.468,0.28 46.452,17.54 46.452,17.823 0,0.169 -1.279,0.789 -2.844,1.377 -1.564,0.588 -12.114,4.59 -23.444,8.895 -11.33,4.304 -20.646,7.825 -20.703,7.825 -0.056,0 -0.103,-0.164 -0.103,-0.364 z M 68.054,150.107 c -5.093,-1.952 -12.356,-4.724 -16.139,-6.16 -3.783,-1.435 -9.945,-3.774 -13.692,-5.197 -3.747,-1.423 -6.813,-2.72 -6.813,-2.882 0,-0.162 5.447,-2.361 12.104,-4.887 36.99,-14.033 34.991,-13.298 34.991,-12.859 0,0.071 -0.413,0.672 -0.919,1.335 -3.33,4.363 -4.983,9.863 -4.967,16.526 0.01,4.123 0.581,7.358 1.901,10.77 0.788,2.037 3.049,5.879 3.694,6.278 0.495,0.305 0.32,0.678 -0.304,0.651 -0.327,-0.014 -4.762,-1.623 -9.855,-3.575 z m 27.781,1.847 c -6.27,-0.965 -11.688,-5.662 -13.451,-11.663 -2.492,-8.483 2.822,-17.782 11.466,-20.062 2.08,-0.548 5.688,-0.612 7.672,-0.135 6.479,1.557 11.252,6.681 12.364,13.272 0.37,2.198 0.37,2.675 -0.0005,4.946 -1.024,6.271 -5.49,11.341 -11.561,13.126 -1.502,0.441 -5.11,0.728 -6.49,0.516 z M 80.093,115.987 c 0,-0.768 17.56,-46.68 17.854,-46.68 0.271,0 17.533,45.042 17.802,46.453 0.158,0.829 -0.047,0.809 -1.223,-0.116 -2.754,-2.169 -6.233,-3.689 -10.411,-4.548 -3.852,-0.792 -10.27,-0.632 -13.981,0.349 -3.05,0.806 -5.964,2.151 -8.153,3.763 -1.813,1.335 -1.887,1.365 -1.887,0.78 z"), 
                Fill = new SolidColorBrush(Color.FromRgb(115, 150, 190)),
                Width = 80,
                Height = 80,
                Stretch = Stretch.Uniform,
                HorizontalAlignment = HorizontalAlignment.Center
            };

            // 4. Nombre de la App y Versión
            var appName = new TextBlock
            {
                Text = Idioma.Instance.Main_AboutAppName,
                FontSize = 28,
                FontWeight = FontWeights.Bold,
                Foreground = new SolidColorBrush(Color.FromRgb(115, 150, 190)),
                HorizontalAlignment = HorizontalAlignment.Center,
                Margin = new Thickness(0, 15, 0, 2)
            };

            var appVersion = new TextBlock
            {
                Text = $"v{versionProyecto}",
                FontSize = 15,
                Foreground = new SolidColorBrush(Color.FromRgb(100, 100, 100)),
                HorizontalAlignment = HorizontalAlignment.Center,
                Margin = new Thickness(0, 5, 0, 25)
            };

            // 5. EL "BOTÓN" (Usando un Border como Panel interactivo)
            var btnGitHub = new Border
            {
                Background = new SolidColorBrush(Color.FromRgb(85, 119, 161)),
                CornerRadius = new CornerRadius(5),
                Padding = new Thickness(30, 10, 30, 10),
                HorizontalAlignment = HorizontalAlignment.Center,
                Cursor = Cursors.Hand,
                Margin = new Thickness(0, 0, 0, 20),
                Child = new TextBlock
                {
                    Text = Idioma.Instance.About_Github_button,
                    Foreground = Brushes.White,
                    FontWeight = FontWeights.Regular,
                    FontSize = 13
                }
            };

            btnGitHub.MouseDown += (s, ev) =>
            {
                AbrirUrl("https://github.com/multiAppDev/MultiNavigator");
            };

            btnGitHub.MouseEnter += (s, ev) => btnGitHub.Opacity = 0.8;
            btnGitHub.MouseLeave += (s, ev) => btnGitHub.Opacity = 1.0;

            // 6. Enlace GitHub (Soporte)
            var linkGithub = new TextBlock
            {
                Text = Idioma.Instance.About_Github, // ver código en GitHub
                FontSize = 12,
                Foreground = Brushes.SteelBlue,
                TextDecorations = TextDecorations.Underline,
                HorizontalAlignment = HorizontalAlignment.Center,
                Cursor = Cursors.Hand,
                Margin = new Thickness(0, 0, 0, 20)
            };
            linkGithub.MouseDown += (s, ev) => AbrirUrl("https://github.com/tu-usuario/tu-repo");

            // 7. Pie de página (Separador + Info técnica)
            var separator = new System.Windows.Shapes.Rectangle
            {
                Height = 1,
                Fill = new SolidColorBrush(Color.FromRgb(200, 200, 200)),
                Margin = new Thickness(50, 0, 50, 15),
                SnapsToDevicePixels = true
            };


            var btnKofi = new Border
            {
                Background = new SolidColorBrush(Color.FromRgb(80, 40, 5)), // Rojo-naranja Ko-fi oficial
                CornerRadius = new CornerRadius(5),
                Padding = new Thickness(30, 10, 30, 10),
                HorizontalAlignment = HorizontalAlignment.Center,
                Cursor = Cursors.Hand,
                Margin = new Thickness(0, 0, 0, 20),
                Child = new TextBlock
                {
                    Foreground = Brushes.White,
                    FontWeight = FontWeights.Regular,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Inlines =
                    {
                        new Run("☕ ") { FontSize = 18 },
                        new Run("No ads. Just coffee.") { FontSize = 13 }
                    }
                }
            };

            btnKofi.MouseDown += (s, ev) => AbrirUrl("https://ko-fi.com/multinavigator");
            btnKofi.MouseEnter += (s, ev) => btnKofi.Opacity = 0.8;
            btnKofi.MouseLeave += (s, ev) => btnKofi.Opacity = 1.0;

            // Copyright
            var copyright = new TextBlock
            {
                Text = "© 2001–2026 MultiNavigator",
                FontSize = 11,
                Foreground = new SolidColorBrush(Color.FromRgb(170, 170, 170)),
                HorizontalAlignment = HorizontalAlignment.Center,
                Margin = new Thickness(0, 0, 0, 2)
            };

            // Licencia MPL v2 — enlace clickable
            var linkLicense = new TextBlock
            {
                Text = "Mozilla Public License 2.0",
                FontSize = 11,
                Foreground = Brushes.SteelBlue,
                TextDecorations = TextDecorations.Underline,
                HorizontalAlignment = HorizontalAlignment.Center,
                Cursor = Cursors.Hand,
                Margin = new Thickness(0, 0, 0, 8)
            };
            linkLicense.MouseDown += (s, ev) =>
                AbrirUrl("https://mozilla.org/MPL/2.0/");

            var engineInfo = new TextBlock
            {
                Text = $"Motor: Chromium {wv2Version}",
                FontSize = 12,
                Foreground = new SolidColorBrush(Color.FromRgb(170, 170, 170)),
                HorizontalAlignment = HorizontalAlignment.Center
            };

            // Construcción del panel
            panel.Children.Add(logo);
            panel.Children.Add(appName);
            panel.Children.Add(appVersion);
            panel.Children.Add(btnGitHub);
            panel.Children.Add(linkGithub);
            panel.Children.Add(separator);
            panel.Children.Add(btnKofi);
            panel.Children.Add(copyright);
            panel.Children.Add(linkLicense);
            panel.Children.Add(engineInfo);

            win.Content = panel;
            win.ShowDialog();
        }

        // Método auxiliar para abrir enlaces externos
        private void AbrirUrl(string url)
        {
            try
            {
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(url) { UseShellExecute = true });
            }
            catch { /* Evita crashes si el protocolo falla */ }
        }

        private ObservableCollection<TabInfo> GetTabsList(int panelId)
        {
            switch (panelId)
            {
                case 1: return Tabs1;
                case 2: return Tabs2;
                case 3: return Tabs3;
                case 4: return Tabs4;
                default: return Tabs1;
            }
        }


        private ItemsControl GetTabsPanel(int panelId) => panelId switch
        {
            1 => TabsPanel1,
            2 => TabsPanel2,
            3 => TabsPanel3,
            4 => TabsPanel4,
            _ => TabsPanel1
        };

        public WebView2? GetActiveWebView(int panelId)
        {
            var list = GetTabsList(panelId);
            if (list.Count == 0) return null;

            var active = list.FirstOrDefault(t => t.IsActive) ?? list[0];
            return active.WebView;
        }


        private int _currentFavoritesPanel = 0;

        private TextBox? GetUrlBox(int id)
        {
            ContentControl? control = id switch
            {
                1 => NavBar1,
                2 => NavBar2,
                3 => NavBar3,
                4 => NavBar4,
                _ => null
            };

            if (control == null) return null;

            // ⭐ FORZAR que se aplique el template
            control.ApplyTemplate();
            control.UpdateLayout();

            return FindVisualChild<TextBox>(control, "UrlTemplate");
        }

        private Grid? GetWebHost(int id) => id switch
        {
            1 => WebHost1,
            2 => WebHost2,
            3 => WebHost3,
            4 => WebHost4,
            _ => null
        };



        private void ConfigureWebView(WebView2 wv, TabInfo tab)
        {
            wv.Tag = new WebViewState { Incognito = false };

            wv.NavigationStarting += (_, e) =>
            {
                tab.IsLoading = true;
                tab.Url = e.Uri;

                int panelId = DeterminePanelIdForWebView(wv);
                UpdateSecureIcon(panelId, tab.Url);

                // ⭐ Actualizar textbox inmediatamente
                Dispatcher.Invoke(() =>
                {
                    var urlBox = GetUrlBox(panelId);
                    if (urlBox != null && !urlBox.IsFocused)
                        urlBox.Text = e.Uri;
                });
            };


            wv.NavigationCompleted += (_, e) =>
            {
                string url = wv.Source?.ToString() ?? "";
                tab.Url = url;

                int panelId = DeterminePanelIdForWebView(wv);
                UpdateSecureIcon(panelId, url);

                // ⭐ No guardar historial en modo incógnito
                if (!tab.IsIncognito)
                {
                    string title = wv.CoreWebView2?.DocumentTitle ?? "";
                    History.Instance.Add(url, title);
                }

                // ⭐ Actualizar textbox
                Dispatcher.Invoke(() =>
                {
                    var urlBox = GetUrlBox(panelId);
                    if (urlBox != null && !urlBox.IsFocused)
                        urlBox.Text = url;
                });
            };

            wv.CoreWebView2InitializationCompleted += (_, e) =>
            {
                if (!e.IsSuccess) return;
                ConfigureCoreWebView2(wv, tab, wv.CoreWebView2);
            };
        }


        private void ConfigureCoreWebView2(WebView2 wv, TabInfo tab, CoreWebView2 core)
        {
            core.ContextMenuRequested += (s, args) =>
            {
                if (args.ContextMenuTarget.LinkUri == null) return;
                string linkUrl = args.ContextMenuTarget.LinkUri;

                var menuList = args.MenuItems;

                var sep1 = wv.CoreWebView2.Environment.CreateContextMenuItem(
                    "", null, CoreWebView2ContextMenuItemKind.Separator);
                menuList.Add(sep1);

                for (int i = 1; i <= 4; i++)
                {
                    int panelCapturado = i;
                    var item = wv.CoreWebView2.Environment.CreateContextMenuItem(
                        $"{Idioma.Instance.Fav_CtxOpen} {panelCapturado}",
                        null,
                        CoreWebView2ContextMenuItemKind.Command);
                    item.CustomItemSelected += (_, __) =>
                    {
                        Dispatcher.Invoke(() => CreateTabWithUrl(panelCapturado, linkUrl));
                    };
                    menuList.Add(item);
                }

                menuList.Add(sep1);

                var itemIncognito = wv.CoreWebView2.Environment.CreateContextMenuItem(
                    $"🕵️ {Idioma.Instance.Main_CtxNewIncognito}",
                    null,
                    CoreWebView2ContextMenuItemKind.Command);
                itemIncognito.CustomItemSelected += (_, __) =>
                {
                    Dispatcher.Invoke(() =>
                    {
                        int panelId = DeterminePanelIdForWebView(wv);
                        AddTab(panelId, null, true, "end");
                        var list = GetTabsList(panelId);
                        var newTab = list.LastOrDefault();
                        if (newTab?.WebView != null)
                            newTab.WebView.Source = new Uri(linkUrl);
                    });
                };
                menuList.Add(itemIncognito);

                var sep2 = wv.CoreWebView2.Environment.CreateContextMenuItem(
                    "", null, CoreWebView2ContextMenuItemKind.Separator);
                menuList.Add(sep2);

                var browsers = GetInstalledBrowsers();
                foreach (var (nombreCapturado, exeCapturado, iconCapturado) in browsers)
                {
                    System.IO.Stream? iconStream = null;
                    try
                    {
                        if (iconCapturado != null)
                            iconStream = ConvertImageSourceToStream(iconCapturado);
                    }
                    catch { }

                    var itemBrowser = wv.CoreWebView2.Environment.CreateContextMenuItem(
                        $"{Idioma.Instance.Main_CtxOpenWith} {nombreCapturado}",
                        iconStream,
                        CoreWebView2ContextMenuItemKind.Command);

                    itemBrowser.CustomItemSelected += (_, __) =>
                    {
                        Dispatcher.Invoke(() =>
                        {
                            try
                            {
                                Process.Start(new ProcessStartInfo
                                {
                                    FileName = exeCapturado,
                                    Arguments = linkUrl,
                                    UseShellExecute = false
                                });
                            }
                            catch { }
                        });
                    };
                    menuList.Add(itemBrowser);
                }
            };

            core.NewWindowRequested += (sender, args) =>
            {
                args.Handled = true;
                string url = args.Uri;
                if (string.IsNullOrEmpty(url)) return;

                Dispatcher.Invoke(() =>
                {
                    int panelId = DeterminePanelIdForWebView(wv);
                    var activeTab = GetTabsList(panelId).FirstOrDefault(t => t.IsActive);
                    AddTab(panelId, activeTab, tab.IsIncognito, "right");
                    var list = GetTabsList(panelId);
                    var newTab = list.FirstOrDefault(t => t.IsActive);
                    if (newTab?.WebView != null)
                        newTab.WebView.Source = new Uri(url);
                });
            };

            core.Settings.AreDefaultContextMenusEnabled = true;
            core.Settings.AreDevToolsEnabled = !tab.IsIncognito;
            core.Settings.IsStatusBarEnabled = false;
            core.Settings.IsWebMessageEnabled = true;
            core.Settings.IsScriptEnabled = true;
            core.Settings.AreBrowserAcceleratorKeysEnabled = true;
            core.Settings.IsPasswordAutosaveEnabled = !tab.IsIncognito;
            core.Settings.IsGeneralAutofillEnabled = !tab.IsIncognito;
            core.Settings.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/120.0.0.0 Safari/537.36";

            core.AddScriptToExecuteOnDocumentCreatedAsync(@"
                Object.defineProperty(navigator, 'webdriver', { get: () => undefined });");
            core.AddScriptToExecuteOnDocumentCreatedAsync(DropScript);

            core.WebMessageReceived += (sender, args) =>
            {
                try
                {
                    var json = JsonDocument.Parse(args.WebMessageAsJson);
                    if (json.RootElement.TryGetProperty("type", out var type) &&
                        type.GetString() == "drop")
                    {
                        if (json.RootElement.TryGetProperty("url", out var urlProp))
                        {
                            string droppedUrl = ExtractUrlFromText(urlProp.GetString() ?? "");
                            if (!string.IsNullOrEmpty(droppedUrl) &&
                                Uri.TryCreate(droppedUrl, UriKind.Absolute, out Uri? uri) &&
                                (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps))
                            {
                                int panelId = DeterminePanelIdForWebView(wv);
                                Dispatcher.Invoke(() => { CreateTabWithUrl(panelId, droppedUrl); });
                            }
                        }
                    }
                }
                catch { }
            };

            core.DOMContentLoaded += async (_, __) =>
            {
                tab.IsLoading = false;
                string result = await wv.ExecuteScriptAsync(FaviconScript);
                string? raw = CleanJsString(result);
                string faviconUrl = NormalizeFaviconUrl(raw, tab.Url);
                if (!string.IsNullOrEmpty(faviconUrl))
                    tab.FaviconUrl = faviconUrl;
                else if (string.IsNullOrEmpty(tab.FaviconUrl))
                    tab.FaviconUrl = tab.Url;
            };

            core.DocumentTitleChanged += (_, __) =>
            {
                tab.Title = core.DocumentTitle;
            };
        }

        private List<(string Nombre, string Ejecutable, ImageSource? Icono)> GetInstalledBrowsers()
        {
            var browsers = new List<(string Nombre, string Ejecutable)>();

            string[] knownBrowserExeNames =
            {
        "chrome.exe", "chromium.exe", "msedge.exe", "brave.exe",
        "vivaldi.exe", "opera.exe", "opera_gx.exe", "operagx.exe",
        "thorium.exe", "slimjet.exe", "dragon.exe", "iridium.exe",
        "iron.exe", "yandex.exe", "browser.exe", "epic.exe",
        "maxthon.exe", "ucbrowser.exe",

        "firefox.exe", "waterfox.exe", "librewolf.exe",
        "palemoon.exe", "basilisk.exe", "seamonkey.exe"
    };

            // --- StartMenuInternet ---
            string[] keys =
            {
        @"SOFTWARE\Clients\StartMenuInternet",
        @"SOFTWARE\WOW6432Node\Clients\StartMenuInternet"
    };

            foreach (var key in keys)
            {
                using var reg = Registry.LocalMachine.OpenSubKey(key);
                if (reg == null) continue;

                foreach (var browserName in reg.GetSubKeyNames())
                {
                    using var mainKey = reg.OpenSubKey(browserName);
                    using var browserKey = reg.OpenSubKey($@"{browserName}\shell\open\command");

                    var exe = browserKey?.GetValue("")?.ToString()?.Trim('"');
                    if (string.IsNullOrEmpty(exe) || !File.Exists(exe))
                        continue;

                    string? displayName = mainKey?.GetValue("DisplayName")?.ToString();
                    string? localized = mainKey?.GetValue("LocalizedString")?.ToString();

                    string nombreReal =
                        displayName ??
                        localized ??
                        Regex.Replace(browserName, @"[\.\-_].*$", "");

                    nombreReal = CleanBrowserName(nombreReal);

                    // Detectar motor
                    string engine = DetectBrowserEngine(exe);
                    nombreReal = $"{nombreReal} ({engine})";

                    browsers.Add((nombreReal, exe));
                }
            }

            // --- App Paths ---
            string[] appKeys =
            {
        @"SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths",
        @"SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\App Paths"
    };

            foreach (var key in appKeys)
            {
                using var reg = Registry.LocalMachine.OpenSubKey(key);
                if (reg == null) continue;

                foreach (var sub in reg.GetSubKeyNames())
                {
                    if (!knownBrowserExeNames.Contains(sub.ToLowerInvariant()))
                        continue;

                    using var appKey = reg.OpenSubKey(sub);
                    var exe = appKey?.GetValue("")?.ToString()?.Trim('"');
                    if (!string.IsNullOrEmpty(exe) && File.Exists(exe))
                    {
                        string nombre = System.IO.Path.GetFileNameWithoutExtension(exe);
                        nombre = CleanBrowserName(nombre);

                        string engine = DetectBrowserEngine(exe);
                        nombre = $"{nombre} ({engine})";

                        browsers.Add((nombre, exe));
                    }
                }
            }

            // --- HKCU App Paths ---
            using var userAppReg = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths");
            if (userAppReg != null)
            {
                foreach (var sub in userAppReg.GetSubKeyNames())
                {
                    if (!knownBrowserExeNames.Contains(sub.ToLowerInvariant()))
                        continue;

                    using var appKey = userAppReg.OpenSubKey(sub);
                    var exe = appKey?.GetValue("")?.ToString()?.Trim('"');
                    if (!string.IsNullOrEmpty(exe) && File.Exists(exe))
                    {
                        string nombre = System.IO.Path.GetFileNameWithoutExtension(exe);
                        nombre = CleanBrowserName(nombre);

                        string engine = DetectBrowserEngine(exe);
                        nombre = $"{nombre} ({engine})";
                        nombre = $"{nombre} ({engine})";

                        browsers.Add((nombre, exe));
                    }
                }
            }

            // --- Extraer iconos ---
            return browsers
                .DistinctBy(b => b.Ejecutable)
                .Where(b => !b.Ejecutable.Contains("iexplore", StringComparison.OrdinalIgnoreCase))
                .Select(b =>
                {
                    ImageSource? icon = null;
                    try
                    {
                        var ico = System.Drawing.Icon.ExtractAssociatedIcon(b.Ejecutable);
                        if (ico != null)
                        {
                            icon = Imaging.CreateBitmapSourceFromHIcon(
                                ico.Handle,
                                Int32Rect.Empty,
                                BitmapSizeOptions.FromWidthAndHeight(16, 16));
                        }
                    }
                    catch { }
                    return (b.Nombre, b.Ejecutable, icon);
                })
                .ToList();
        }



        private string CleanBrowserName(string rawName)
        {
            if (string.IsNullOrWhiteSpace(rawName))
                return "Unknown";

            // 1. Quitar sufijos AppX, GUIDs, hashes, etc.
            rawName = Regex.Replace(rawName, @"[\.\-_].*$", "", RegexOptions.IgnoreCase);

            // 2. Quitar espacios sobrantes
            rawName = rawName.Trim();

            // 3. Capitalizar primera letra
            if (rawName.Length > 1)
                return char.ToUpper(rawName[0]) + rawName[1..];

            return rawName.ToUpper();
        }




        private System.IO.Stream? ConvertImageSourceToStream(ImageSource imageSource)
        {
            try
            {
                if (imageSource is BitmapSource bitmapSource)
                {
                    var encoder = new System.Windows.Media.Imaging.PngBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(bitmapSource));
                    var stream = new System.IO.MemoryStream();
                    encoder.Save(stream);
                    stream.Position = 0;
                    return stream;
                }
            }
            catch { }
            return null;
        }


        private WebView2 CreateWebViewForTab(TabInfo tab)
        {
            var wv = new WebView2();
            ConfigureWebView(wv, tab);

            // Si no es incógnito, usamos el motor compartido que YA debería estar listo
            if (!tab.IsIncognito)
            {
                // ⭐ LA CLAVE: No bloquees con InvokeAsync si ya tenemos el entorno.
                // EnsureCoreWebView2Async es lo suficientemente inteligente para 
                // ejecutarse en segundo plano sin congelar la UI.
                if (_sharedEnvironment != null)
                {
                    _ = wv.EnsureCoreWebView2Async(_sharedEnvironment);
                    if (!string.IsNullOrEmpty(tab.Url))
                        wv.Source = new Uri(tab.Url);
                }
                else
                {
                    // Caso de emergencia: si por algún motivo el motor no cargó a tiempo
                    _ = InitWebViewFallback(wv, tab.Url);
                }
            }
            else
            {
                _ = InitIncognitoEnvironment(wv, tab);
            }

            return wv;
        }

        private async Task InitIncognitoEnvironment(WebView2 wv, TabInfo tab)
        {
            string tempProfile = System.IO.Path.Combine(
                System.IO.Path.GetTempPath(),
                "mn7_incog_" + Guid.NewGuid()
            );
            Directory.CreateDirectory(tempProfile);

            var incognitoEnv = await CoreWebView2Environment.CreateAsync(
                browserExecutableFolder: null,
                userDataFolder: tempProfile,
                options: _envOptions
            );

            var controllerOptions = incognitoEnv.CreateCoreWebView2ControllerOptions();
            controllerOptions.IsInPrivateModeEnabled = true;
            controllerOptions.ProfileName = Guid.NewGuid().ToString("N");

            await wv.EnsureCoreWebView2Async(incognitoEnv, controllerOptions);

            tab.TempProfilePath = tempProfile;

            if (!string.IsNullOrEmpty(tab.Url))
                wv.Source = new Uri(tab.Url);
        }


        // Método auxiliar separado para no ensuciar la creación de la pestaña
        private async Task InitWebViewFallback(WebView2 wv, string url)
        {
            // Solo llegamos aquí si _sharedEnvironment es null (muy raro con tu flujo)
            var env = await CoreWebView2Environment.CreateAsync(null, UserDataFolder, _envOptions);
            await wv.EnsureCoreWebView2Async(env);
            if (!string.IsNullOrEmpty(url)) wv.Source = new Uri(url);
        }


        private const string DropScript = @"
(function() {
    document.addEventListener('dragover', function(e) {
        var types = Array.from(e.dataTransfer.types);
        // ⭐ Solo interceptar si NO hay archivos
        if (!types.includes('Files')) {
            e.preventDefault();
            e.dataTransfer.dropEffect = 'copy';
        }
    }, true);
    
    document.addEventListener('drop', function(e) {
        var types = Array.from(e.dataTransfer.types);
        
        // ⭐ Si hay archivos, dejar que la página lo maneje normalmente
        if (types.includes('Files')) return;
        
        var url = e.dataTransfer.getData('text/uri-list') || 
                  e.dataTransfer.getData('text/plain') ||
                  e.dataTransfer.getData('text/html');
        
        // ⭐ Solo interceptar si es una URL
        if (url && url.startsWith('http')) {
            e.preventDefault();
            e.stopPropagation();
            window.chrome.webview.postMessage({ type: 'drop', url: url });
        }
    }, true);
})();
";

        private const string FaviconScript = @"
(() => {
    let selectors = [
        'link[rel=""icon"" ]',
        'link[rel=""shortcut icon"" ]',
        'link[rel=""mask-icon"" ]',
        'link[rel=""apple-touch-icon"" ]',
        'link[rel=""apple-touch-icon-precomposed"" ]'
    ];
    for (let sel of selectors) {
        let icon = document.querySelector(sel);
        if (icon && icon.href)
            return icon.href;
    }
    return null;
})();
";

        private string? CleanJsString(string? raw)
        {
            if (raw == null) return null;
            raw = raw.Trim();
            if (raw == "null" || raw == "\"\"" || raw == "undefined")
                return null;
            if (raw.StartsWith("\"") && raw.EndsWith("\""))
                return raw.Substring(1, raw.Length - 2);
            return raw;
        }

        private string ExtractUrlFromText(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return "";

            // Si viene HTML, intenta extraer href
            int hrefIndex = text.IndexOf("http");
            if (hrefIndex >= 0)
            {
                string possible = text.Substring(hrefIndex);
                int end = possible.IndexOfAny(new[] { '"', '\'', '<', ' ' });
                if (end > 0)
                    possible = possible.Substring(0, end);
                return possible.Trim();
            }

            return text.Trim();
        }

        private int DeterminePanelIdForWebView(WebView2 wv)
        {
            DependencyObject parent = wv;

            while (parent != null)
            {
                if (parent == WebHost1) return 1;
                if (parent == WebHost2) return 2;
                if (parent == WebHost3) return 3;
                if (parent == WebHost4) return 4;

                parent = VisualTreeHelper.GetParent(parent);
            }

            return 1;
        }


        private void CreateTabWithUrl(int panelId, string url)
        {
            PopupSugerencias.IsOpen = false;
            try
            {

                var list = GetTabsList(panelId);

                foreach (var t in list)
                    t.IsActive = false;

                Uri uri = new Uri(url);
                string domain = uri.Host;

                var newTab = new TabInfo
                {
                    Title = Idioma.Instance.Main_Loading,
                    Url = url,
                    FaviconUrl = $"https://www.google.com/s2/favicons?domain={domain}",
                    PanelId = panelId,
                    IsActive = true
                };

                newTab.WebView = CreateWebViewForTab(newTab);
                list.Add(newTab);

                ForceTabsRemeasure();
                ApplyActiveTabToWeb(panelId);
                UpdateScrollButtonsFor(panelId);

                Dispatcher.BeginInvoke(() =>
                {
                    ScrollViewer? target = panelId switch
                    {
                        1 => TabsScroll1,
                        2 => TabsScroll2,
                        3 => TabsScroll3,
                        4 => TabsScroll4,
                        _ => null
                    };
                    if (target != null)
                        SmoothScrollToEnd(target, toRight: true, durationMs: 500);
                }, System.Windows.Threading.DispatcherPriority.Background);

                SaveCurrentSession();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"💀 Error creando pestaña: {ex.Message}\n{ex.StackTrace}");
            }

            
        }

        private string DetectBrowserEngine(string exePath)
        {
            try
            {
                string folder = System.IO.Path.GetDirectoryName(exePath)!;

                // Archivos típicos de Chromium
                string[] chromiumFiles =
                {
            "chrome.dll", "chrome_elf.dll", "libcef.dll",
            "v8.dll", "blink.dll", "icudtl.dat", "resources.pak"
        };

                // Archivos típicos de Firefox
                string[] firefoxFiles =
                {
            "xul.dll", "mozglue.dll", "nss3.dll", "omni.ja"
        };

                var files = Directory.GetFiles(folder, "*", SearchOption.AllDirectories)
                                     .Select(f => System.IO.Path.GetFileName(f).ToLowerInvariant())
                                     .ToHashSet();

                if (chromiumFiles.Any(f => files.Contains(f)))
                    return "Chromium";

                if (firefoxFiles.Any(f => files.Contains(f)))
                    return "Firefox";

                return "Unknown";
            }
            catch
            {
                return "Unknown";
            }
        }


        private string NormalizeFaviconUrl(string? favicon, string pageUrl)
        {
            if (string.IsNullOrWhiteSpace(favicon))
                return "";

            // Si ya es absoluta → devolver tal cual
            if (Uri.IsWellFormedUriString(favicon, UriKind.Absolute))
                return favicon;

            // Si empieza por // → protocolo relativo
            if (favicon.StartsWith("//"))
                return "https:" + favicon;

            // Si es relativa → combinar con la URL de la página
            try
            {
                var baseUri = new Uri(pageUrl);
                var finalUri = new Uri(baseUri, favicon);
                return finalUri.ToString();
            }
            catch
            {
                return "";
            }
        }


        private void BtnStar_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && int.TryParse(btn.Tag?.ToString(), out int panelId))
            {
                _currentFavoritesPanel = panelId;
                PopupFavorites.PlacementTarget = btn;
                PopupFavorites.Placement = System.Windows.Controls.Primitives.PlacementMode.Bottom;
                PopupFavorites.IsOpen = !PopupFavorites.IsOpen;
            }
        }



        private System.Windows.Shapes.Path? GetSecureIcon(int id)
        {
            ContentControl? control = id switch
            {
                1 => NavBar1,
                2 => NavBar2,
                3 => NavBar3,
                4 => NavBar4,
                _ => null
            };

            if (control == null) return null;

            // ⭐ FORZAR que se aplique el template
            control.ApplyTemplate();
            control.UpdateLayout();

            return FindVisualChild<System.Windows.Shapes.Path>(control, "SecureIconTemplate");
        }

        private void LockIcon_Click(object sender, MouseButtonEventArgs e)
        {
            string helpUrl = "https://support.google.com/chrome/answer/95617";

            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = helpUrl,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(Idioma.Instance.Main_ErrOpenBrowser + ex.Message);
            }
        }



        // Helper para buscar controles por nombre en el árbol visual
        private T? FindVisualChild<T>(DependencyObject parent, string name) where T : FrameworkElement
        {
            if (parent == null) return null;

            int childCount = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);

                if (child is T typedChild && typedChild.Name == name)
                    return typedChild;

                var result = FindVisualChild<T>(child, name);
                if (result != null)
                    return result;
            }

            return null;
        }




        private void ApplyActiveTabToWeb(int panelId)
        {
            var list = GetTabsList(panelId);
            var host = GetWebHost(panelId);
            var urlBox = GetUrlBox(panelId);
            var secureIcon = GetSecureIcon(panelId);
            if (host == null || urlBox == null)
                return;
            host.Children.Clear();
            if (list.Count == 0)
                return;
            var active = list.FirstOrDefault(t => t.IsActive);
            if (active == null || active.WebView == null)
                return;

            // ⭐ MOSTRAR EL WEBVIEW ACTIVO
            host.Children.Add(active.WebView);
            urlBox.Text = active.Url;

            // ⭐ ACTUALIZAR ICONO DE SEGURIDAD
            UpdateSecureIcon(panelId, active.Url);

            // ⭐ ACTUALIZAR TÍTULO DE LA VENTANA
            this.Title = active.Title;

            // ⭐⭐ ASIGNAR EL TABINFO ACTIVO A LA BARRA DE NAVEGACIÓN
            switch (panelId)
            {
                case 1:
                    NavBar1.Content = active;
                    break;
                case 2:
                    NavBar2.Content = active;
                    break;
                case 3:
                    NavBar3.Content = active;
                    break;
                case 4:
                    NavBar4.Content = active;
                    break;
            }
        }



        private void UpdateSecureIcon(int panelId, string url)
        {
            var secureIcon = GetSecureIcon(panelId);
            if (secureIcon == null)
                return;

            // Obtener el tooltip real del Path
            var tooltip = secureIcon.ToolTip as ToolTip;
            if (tooltip == null)
                return;

            if (string.IsNullOrWhiteSpace(url))
            {
                secureIcon.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#e0cc4c"));
                tooltip.Content = Idioma.Instance.Main_SecureNoInfo;
                return;
            }

            if (url.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
            {
                secureIcon.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#2bcc57"));
                tooltip.Content = Idioma.Instance.Main_SecureYes;
            }
            else
            {
                secureIcon.Fill = Brushes.Red;
                tooltip.Content = Idioma.Instance.Main_SecureNo;
            }
        }


        private int GetPanelIdFromTabsPanel(FrameworkElement fe)
        {
            DependencyObject current = fe;

            while (current != null && current is not ItemsControl)
                current = VisualTreeHelper.GetParent(current);

            if (current == TabsPanel1) return 1;
            if (current == TabsPanel2) return 2;
            if (current == TabsPanel3) return 3;
            if (current == TabsPanel4) return 4;

            return 0;
        }

        private int GetPanelIdFromPoint(Point pos)
        {
            // PANEL 1
            var p1 = Panel1Grid.TranslatePoint(new Point(0, 0), this);
            var r1 = new Rect(p1.X, p1.Y, Panel1Grid.ActualWidth, Panel1Grid.ActualHeight);
            if (r1.Contains(pos)) return 1;

            // PANEL 2
            var p2 = Panel2Grid.TranslatePoint(new Point(0, 0), this);
            var r2 = new Rect(p2.X, p2.Y, Panel2Grid.ActualWidth, Panel2Grid.ActualHeight);
            if (r2.Contains(pos)) return 2;

            // PANEL 3
            var p3 = Panel3Grid.TranslatePoint(new Point(0, 0), this);
            var r3 = new Rect(p3.X, p3.Y, Panel3Grid.ActualWidth, Panel3Grid.ActualHeight);
            if (r3.Contains(pos)) return 3;

            // PANEL 4
            var p4 = Panel4Grid.TranslatePoint(new Point(0, 0), this);
            var r4 = new Rect(p4.X, p4.Y, Panel4Grid.ActualWidth, Panel4Grid.ActualHeight);
            if (r4.Contains(pos)) return 4;

            return 0;
        }



        private void TabItem_MouseMove(object sender, MouseEventArgs e)
        {
            // vacío
        }

        private void ResetDragState()
        {
            _tabDragging = false;
            isDraggingTab = false;
            draggedTab = null;

            // ⭐ FORZAR limpieza del placeholder siempre
            CleanupPlaceholder();

            placeholderTab = null;
            currentTargetPanel = null;
            currentDropIndex = -1;
            lastDropIndex = -1;

            HideGhost();
            Mouse.Capture(null);
        }


        private void CleanupPlaceholder()
        {
            if (placeholderTab != null)
            {
                Tabs1.Remove(placeholderTab);
                Tabs2.Remove(placeholderTab);
                Tabs3.Remove(placeholderTab);
                Tabs4.Remove(placeholderTab);
                placeholderTab = null;
            }
        }

        private void TabItem_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Mouse.Capture(null);

            // ⭐ CLICK NORMAL
            if (!isDraggingTab && sender is FrameworkElement fe && fe.DataContext is TabInfo tab)
            {
                int panelId = GetPanelIdFromTabsPanel(fe);
                var list = GetTabsList(panelId);

                foreach (var t in list)
                    t.IsActive = false;

                tab.IsActive = true;
                ApplyActiveTabToWeb(panelId);
            }

            _tabDragging = false;
            isDraggingTab = false;
            draggedTab = null;
        }




        // =========================
        //  DRAG & DROP EN WEBHOST
        // =========================


        //============================ COSAS DEL LINK EXTERNO ==================================================


        private bool isExternalDragging = false;


        private void Panel_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
                e.Effects = DragDropEffects.Copy;
            else
                e.Effects = DragDropEffects.None;

            isExternalDragging = true;
            e.Handled = true;
        }

        private void Panel_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
                e.Effects = DragDropEffects.Copy;
            else
                e.Effects = DragDropEffects.None;

            ItemsControl tabsPanel = sender as ItemsControl;
            if (tabsPanel == null)
            {
                e.Handled = true;
                return;
            }

            int panelId = GetPanelIdFromItemsControl(tabsPanel);
            Point posInPanel = e.GetPosition(tabsPanel);
            int dropIndex = GetDropIndexByPosition(tabsPanel, posInPanel);

            // SI EL PLACEHOLDER YA ESTÁ EN ESA POSICIÓN, NO HACER NADA
            var targetList = GetTabsList(panelId);

            if (externalPlaceholderTab != null)
            {
                int currentIndex = targetList.IndexOf(externalPlaceholderTab);

                if (panelId == lastExternalDropPanel &&
                    (dropIndex == currentIndex || dropIndex == currentIndex + 1))
                {
                    lastExternalDropIndex = dropIndex;
                    e.Handled = true;
                    return;
                }
            }

            lastExternalDropIndex = dropIndex;
            lastExternalDropPanel = panelId;

            // ⭐ OBTENER LA URL DEL DRAG
            string url = "";
            if (e.Data.GetDataPresent(DataFormats.Text))
            {
                url = e.Data.GetData(DataFormats.Text) as string;
            }

            // Crear placeholder si no existe
            if (externalPlaceholderTab == null)
            {
                externalPlaceholderTab = new TabInfo
                {
                    Title = !string.IsNullOrWhiteSpace(url) ? GetDomainFromUrl(url) : Idioma.Instance.Main_NewTab,
                    Url = url ?? "",
                    FaviconUrl = !string.IsNullOrWhiteSpace(url) ? GetFaviconUrl(url) : "https://www.google.com/s2/favicons?domain=google.com",
                    IsPlaceholder = true
                };
            }
            else
            {
                // ⭐ ACTUALIZAR título e icono si cambió la URL
                if (!string.IsNullOrWhiteSpace(url))
                {
                    externalPlaceholderTab.Title = GetDomainFromUrl(url);
                    externalPlaceholderTab.Url = url;
                    externalPlaceholderTab.FaviconUrl = GetFaviconUrl(url);
                }
            }

            // Remover de todos los paneles
            Tabs1.Remove(externalPlaceholderTab);
            Tabs2.Remove(externalPlaceholderTab);
            Tabs3.Remove(externalPlaceholderTab);
            Tabs4.Remove(externalPlaceholderTab);

            // Insertar en la nueva posición
            if (dropIndex >= 0 && dropIndex <= targetList.Count)
            {
                targetList.Insert(dropIndex, externalPlaceholderTab);
            }

            e.Handled = true;
        }

        // ⭐ MÉTODOS AUXILIARES
        private string GetDomainFromUrl(string url)
        {
            try
            {
                var uri = new Uri(url);
                return uri.Host;
            }
            catch
            {
                return url.Length > 30 ? url.Substring(0, 27) + "..." : url;
            }
        }

        private string GetFaviconUrl(string url)
        {
            try
            {
                var uri = new Uri(url);
                return $"https://www.google.com/s2/favicons?domain={uri.Host}&sz=64";
            }
            catch
            {
                return "https://www.google.com/s2/favicons?domain=google.com";
            }
        }

        private void Panel_DragLeave(object sender, DragEventArgs e)
        {
            ItemsControl tabsPanel = sender as ItemsControl;
            if (tabsPanel != null)
            {
                Point pos = e.GetPosition(tabsPanel);
                Rect bounds = new Rect(0, 0, tabsPanel.ActualWidth, tabsPanel.ActualHeight);

                if (bounds.Contains(pos))
                {
                    e.Handled = true;
                    return;
                }
            }

            isExternalDragging = false;
            lastExternalDropIndex = -1;
            lastExternalDropPanel = 0;

            if (externalPlaceholderTab != null)
            {
                Tabs1.Remove(externalPlaceholderTab);
                Tabs2.Remove(externalPlaceholderTab);
                Tabs3.Remove(externalPlaceholderTab);
                Tabs4.Remove(externalPlaceholderTab);
                externalPlaceholderTab = null;
            }

            e.Handled = true;
        }

        private void Panel_Drop(object sender, DragEventArgs e)
        {
            isExternalDragging = false;

            int insertIndex = lastExternalDropIndex;
            int panelId = lastExternalDropPanel;

            if (externalPlaceholderTab != null)
            {
                Tabs1.Remove(externalPlaceholderTab);
                Tabs2.Remove(externalPlaceholderTab);
                Tabs3.Remove(externalPlaceholderTab);
                Tabs4.Remove(externalPlaceholderTab);
                externalPlaceholderTab = null;
            }

            lastExternalDropIndex = -1;
            lastExternalDropPanel = 0;

            if (!e.Data.GetDataPresent(DataFormats.Text))
                return;

            string url = e.Data.GetData(DataFormats.Text) as string;
            if (string.IsNullOrWhiteSpace(url))
                return;

            if (panelId == 0)
                panelId = GetPanelIdFromItemsControl(sender as ItemsControl);

            if (panelId == 0)
                return;

            var targetList = GetTabsList(panelId);

            if (insertIndex < 0 || insertIndex > targetList.Count)
                insertIndex = targetList.Count;

            foreach (var t in targetList)
                t.IsActive = false;

            var newTab = new TabInfo
            {
                Title = url,
                Url = url,
                FaviconUrl = "https://www.google.com/s2/favicons?domain=" + new Uri(url).Host,
                PanelId = panelId,
                IsActive = true
            };

            newTab.WebView = CreateWebViewForTab(newTab);

            targetList.Insert(insertIndex, newTab);

            ApplyActiveTabToWeb(panelId);
            SaveCurrentSession();
        }


        //============================ FIN COSAS DEL LINK EXTERNO ==================================================



        private void MoveGhost(Point pos)
        {
            if (!ghostVisible) return;
            Point screenPos = PointToScreen(pos);
            Dispatcher.BeginInvoke(() =>
            {
                GhostPopup.HorizontalOffset = screenPos.X - ghostOffset.X;
                GhostPopup.VerticalOffset = screenPos.Y - ghostOffset.Y;
            }, System.Windows.Threading.DispatcherPriority.Render);
        }

        private void ShowGhost(string title, string iconUrl, Point pos)
        {
            Ghost.SetText(title);
            Ghost.SetIcon(iconUrl);
            Point screenPos = PointToScreen(pos);
            GhostPopup.HorizontalOffset = screenPos.X - ghostOffset.X;
            GhostPopup.VerticalOffset = screenPos.Y - ghostOffset.Y;
            GhostPopup.IsOpen = true;
            ghostVisible = true;
        }

        private void TabItem_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is not FrameworkElement fe || fe.DataContext is not TabInfo tab)
                return;

            if (e.OriginalSource is DependencyObject src)
            {
                var btn = FindParent<Button>(src);
                if (btn != null && btn.Content?.ToString() == "×")
                    return;
            }

            _tabDragStart = e.GetPosition(this);
            _tabDragging = true;

            _draggedTabElement = fe;
            draggedTab = tab;
            draggedFromPanel = GetPanelIdFromTabsPanel(fe);

            // ⭐ SOLUCIÓN: Calcular offset relativo al click dentro de la pestaña (-96)
            //Point clickInTab = e.GetPosition(fe);
            //ghostOffset = new Point(clickInTab.X-180, clickInTab.Y);

            Point clickInTab = e.GetPosition(fe);
            double offsetCorrection = SystemParameters.VirtualScreenWidth > SystemParameters.PrimaryScreenWidth ? -180 : 0;
            ghostOffset = new Point(clickInTab.X + offsetCorrection, clickInTab.Y);

            isDraggingTab = false;
            lastDropIndex = -1;

            Mouse.Capture(this);
        }


        private Point GetCenteredGhostOffset()
        {
            // Obtener el DPI de la ventana actual
            var source = PresentationSource.FromVisual(this);
            double dpiX = 1.0;

            if (source != null)
            {
                dpiX = source.CompositionTarget.TransformToDevice.M11;
            }

            // Calcular el offset considerando el DPI
            double offsetX = (Ghost.Width / 2) * dpiX;
            double offsetY = (Ghost.Height / 2) * dpiX;

            return new Point(offsetX, offsetY);
        }


        private T? FindParent<T>(DependencyObject child) where T : DependencyObject
        {
            DependencyObject parent = VisualTreeHelper.GetParent(child);

            while (parent != null)
            {
                if (parent is T correctlyTyped)
                    return correctlyTyped;

                parent = VisualTreeHelper.GetParent(parent);
            }

            return null;
        }

        private void CleanupIncognitoTab(TabInfo tab)
        {
            if (!tab.IsIncognito) return;

            // Dispose del WebView
            if (tab.WebView != null)
            {
                try { tab.WebView.Dispose(); } catch { }
                tab.WebView = null;
            }

            // Borrar carpeta temporal
            if (!string.IsNullOrEmpty(tab.TempProfilePath) &&
                Directory.Exists(tab.TempProfilePath))
            {
                Task.Run(() =>
                {
                    try { Directory.Delete(tab.TempProfilePath, recursive: true); }
                    catch { /* Si falla, el SO lo limpiará eventualmente */ }
                });
            }

            tab.TempProfilePath = null;
        }

        private void CloseTabInternal(int panelId, TabInfo tab)
        {
            _closedTabs.Push((panelId, tab.Url, tab.Title, tab.FaviconUrl, tab.MarkColor));

            // limpiar si es incógnito
            CleanupIncognitoTab(tab);

            var list = GetTabsList(panelId);

            bool wasActive = tab.IsActive;
            int index = list.IndexOf(tab);

            list.Remove(tab);
            ForceTabsRemeasure();

            // ⭐ Si quedó vacío → crear pestaña nueva
            if (list.Count == 0)
            {
                var newTab = new TabInfo
                {
                    Title = Idioma.Instance.Main_NewTab,
                    Url = "https://www.google.com",
                    FaviconUrl = "https://www.google.com/s2/favicons?domain=google.com",
                    PanelId = panelId,
                    IsActive = true
                };

                newTab.WebView = CreateWebViewForTab(newTab);
                list.Add(newTab);

                ForceTabsRemeasure();
                ApplyActiveTabToWeb(panelId);
                return;
            }

            // ⭐ Si la pestaña cerrada era activa → activar la más cercana
            if (wasActive)
            {
                int newIndex = index;
                if (newIndex >= list.Count)
                    newIndex = list.Count - 1;

                foreach (var t in list)
                    t.IsActive = false;

                list[newIndex].IsActive = true;
            }

            ApplyActiveTabToWeb(panelId);
            SaveCurrentSession();
        }

        private void Menu_AbrirCon_SubmenuOpened(object sender, RoutedEventArgs e)
        {
            if (sender is not MenuItem mi || mi.Tag is not TabInfo tab) return;
            if (string.IsNullOrEmpty(tab.Url)) return;

            mi.Items.Clear();

            var browsers = GetInstalledBrowsers();
            foreach (var (nombre, exe, icono) in browsers)
            {
                var item = new MenuItem { Header = nombre };

                if (icono != null)
                    item.Icon = new System.Windows.Controls.Image
                    {
                        Source = icono,
                        Width = 16,
                        Height = 16
                    };

                string exeCapturado = exe;
                string urlCapturada = tab.Url;

                item.Click += (s, ev) =>
                {
                    try
                    {
                        Process.Start(new ProcessStartInfo
                        {
                            FileName = exeCapturado,
                            Arguments = urlCapturada,
                            UseShellExecute = false
                        });
                    }
                    catch { }
                };

                mi.Items.Add(item);
            }
        }


        private void ReopenLastClosedTab()
        {
            if (_closedTabs.Count == 0) return;

            var (panelId, url, title, faviconUrl, markColor) = _closedTabs.Pop();

            var list = GetTabsList(panelId);
            foreach (var t in list) t.IsActive = false;

            var newTab = new TabInfo
            {
                Title = title,
                Url = url,
                FaviconUrl = faviconUrl,
                MarkColor = markColor,
                PanelId = panelId,
                IsActive = true
            };
            newTab.WebView = CreateWebViewForTab(newTab);
            list.Add(newTab);

            ApplyActiveTabToWeb(panelId);
            SaveCurrentSession();
        }


        private void CloseTab_Click(object sender, RoutedEventArgs e)
        {
            if (sender is not FrameworkElement fe || fe.DataContext is not TabInfo tab)
                return;

            var panel = FindParent<ItemsControl>(fe);
            if (panel == null)
                return;

            int panelId = GetPanelIdFromItemsControl(panel);
            if (panelId == 0)
                return;

            CloseTabInternal(panelId, tab);
        }

        private void CloseTabs(TabInfo tab, string mode)
        {
            var list = GetTabsList(tab.PanelId);
            int index = list.IndexOf(tab);

            switch (mode)
            {
                case "this":
                    list.Remove(tab);
                    break;

                case "right":
                    while (list.Count > index + 1)
                        list.RemoveAt(index + 1);
                    break;

                case "left":
                    for (int i = index - 1; i >= 0; i--)
                        list.RemoveAt(i);
                    break;

                case "others":
                    var keep = tab;
                    list.Clear();
                    list.Add(keep);
                    keep.IsActive = true;
                    break;
            }

            // Si no queda ninguna pestaña, crear una nueva
            if (list.Count == 0)
            {
                AddTab(tab.PanelId);
                return;
            }

            // Asegurar que alguna pestaña quede activa
            if (!list.Any(t => t.IsActive))
            {
                list[Math.Min(index, list.Count - 1)].IsActive = true;
            }

            ApplyActiveTabToWeb(tab.PanelId);
        }




        private void AddTab1_Click(object sender, RoutedEventArgs e) => AddTab(1);
        private void AddTab2_Click(object sender, RoutedEventArgs e) => AddTab(2);
        private void AddTab3_Click(object sender, RoutedEventArgs e) => AddTab(3);
        private void AddTab4_Click(object sender, RoutedEventArgs e) => AddTab(4);

        private void AddTab(
                            int panelId,
                            TabInfo? reference = null,
                            bool incognito = false,
                            string position = "end",
                            bool duplicate = false)
        {
            var list = GetTabsList(panelId);

            // Si no hay referencia, usamos la pestaña activa
            if (reference == null)
                reference = list.FirstOrDefault(t => t.IsActive);

            // Si sigue sin haber referencia (panel vacío), creamos al final
            int insertIndex = list.Count;

            if (reference != null)
            {
                int idx = list.IndexOf(reference);

                insertIndex = position switch
                {
                    "right" => idx + 1,
                    "left" => idx,
                    "start" => 0,
                    "end" => list.Count,
                    _ => list.Count
                };
            }

            // Crear nueva pestaña
            var newTab = new TabInfo
            {
                Title = duplicate ? reference?.Title ?? Idioma.Instance.Main_NewTab : Idioma.Instance.Main_NewTab,
                Url = duplicate ? reference?.Url ?? "https://www.google.com" : "https://www.google.com",
                FaviconUrl = "https://www.google.com/s2/favicons?domain=google.com",
                IsIncognito = incognito,
                PanelId = panelId,
                IsActive = true
            };

            newTab.WebView = CreateWebViewForTab(newTab);

            // Insertar en la posición correcta
            list.Insert(insertIndex, newTab);

            // Desactivar todas las demás
            foreach (var t in list)
                t.IsActive = false;

            newTab.IsActive = true;

            // Si es duplicado, cargar la URL original
            if (duplicate && reference != null)
                newTab.WebView.Source = new Uri(reference.Url);

            // Mantener tu comportamiento original
            ForceTabsRemeasure();
            ApplyActiveTabToWeb(panelId);
            UpdateScrollButtonsFor(panelId);

            // Scroll suave solo si la pestaña va al final
            if (position == "end")
            {
                Dispatcher.BeginInvoke(() =>
                {
                    ScrollViewer? target = panelId switch
                    {
                        1 => TabsScroll1,
                        2 => TabsScroll2,
                        3 => TabsScroll3,
                        4 => TabsScroll4,
                        _ => null
                    };

                    if (target != null)
                        SmoothScrollToEnd(target, toRight: true, durationMs: 500);

                }, System.Windows.Threading.DispatcherPriority.Background);
            }

            SaveCurrentSession();
        }

        private void Menu_TabAction(object sender, RoutedEventArgs e)
        {
            var mi = sender as MenuItem;
            var tab = (mi?.DataContext as TabInfo);
            if (tab == null) return;

            // LOG TEMPORAL — quitar después
            #if DEBUG
                System.Diagnostics.Debug.WriteLine($"[TabAction] PanelId={tab.PanelId} Title={tab.Title} Tag={mi.Tag}");
            #endif

            string[] parts = (mi.Tag as string)?.Split(',') ?? Array.Empty<string>();
            if (parts.Length < 2) return;

            string action = parts[0];
            string pos = parts[1];

            switch (action)
            {
                case "new":
                    AddTab(tab.PanelId, tab, false, pos);
                    break;

                case "newincognito":
                    AddTab(tab.PanelId, tab, true, pos);
                    break;

                case "dup":
                    AddTab(tab.PanelId, tab, tab.IsIncognito, pos, duplicate: true);
                    break;

                case "close":
                    CloseTabs(tab, pos);
                    break;

                case "reload":
                    tab.WebView?.Reload();
                    break;
            }
        }
            


        private void BtnScrollLeft_Click(object sender, MouseButtonEventArgs e)
        {
            var sv = GetScrollFromTag(sender);
            if (sv != null)
                SmoothScrollToEnd(sv, toRight: false);
        }


        private void BtnScrollRight_Click(object sender, MouseButtonEventArgs e)
        {
            var sv = GetScrollFromTag(sender);
            if (sv != null)
                SmoothScrollToEnd(sv, toRight: true);
        }



        private ScrollViewer? GetScrollFromTag(object sender)
        {
            if (sender is Border b && int.TryParse(b.Tag?.ToString(), out int id))
            {
                return id switch
                {
                    1 => TabsScroll1,
                    2 => TabsScroll2,
                    3 => TabsScroll3,
                    4 => TabsScroll4,
                    _ => null
                };
            }

            return null;
        }


        public class DoubleAnimator : Animatable
        {
            public static readonly DependencyProperty ValueProperty =
                DependencyProperty.Register("Value", typeof(double), typeof(DoubleAnimator));

            public double Value { get => (double)GetValue(ValueProperty); set => SetValue(ValueProperty, value); }

            protected override Freezable CreateInstanceCore() => new DoubleAnimator();
        }


        private void SmoothScrollToEnd(ScrollViewer sv, bool toRight, int durationMs = 500)
        {
            sv.Dispatcher.InvokeAsync(() =>
            {
                double from = sv.HorizontalOffset;
                double to = toRight
                    ? Math.Max(0, sv.ExtentWidth - sv.ViewportWidth)
                    : 0;

                var animator = new DoubleAnimator();

                DependencyPropertyDescriptor
                    .FromProperty(DoubleAnimator.ValueProperty, typeof(DoubleAnimator))
                    .AddValueChanged(animator, (s, e) =>
                        sv.ScrollToHorizontalOffset(animator.Value));

                animator.BeginAnimation(
                    DoubleAnimator.ValueProperty,
                    new DoubleAnimation(from, to, TimeSpan.FromMilliseconds(durationMs))
                    {
                        EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseOut }
                    });

            }, System.Windows.Threading.DispatcherPriority.Loaded);
        }


        private bool _scrollingLeft = false;
        private bool _scrollingRight = false;

        private void ScrollLeft_MouseEnter(object sender, MouseEventArgs e)
        {
            if (sender is FrameworkElement fe && int.TryParse(fe.Tag?.ToString(), out int id))
            {
                _scrollingLeft = true;
                _scrollingRight = false;
                StartScrolling(id);
            }
        }


        private void ScrollRight_MouseEnter(object sender, MouseEventArgs e)
        {
            if (sender is FrameworkElement fe && int.TryParse(fe.Tag?.ToString(), out int id))
            {
                _scrollingRight = true;
                _scrollingLeft = false;
                StartScrolling(id);
            }
        }

        private async void StartScrolling(int panelId)
        {
            ScrollViewer sv = panelId switch
            {
                1 => TabsScroll1,
                2 => TabsScroll2,
                3 => TabsScroll3,
                4 => TabsScroll4,
                _ => TabsScroll1
            };

            double speed = 0;
            double accel = 1.2;
            double maxSpeed = 30;
            double friction = 0.85;

            while (_scrollingLeft || _scrollingRight)
            {
                sv.UpdateLayout();

                if (_scrollingLeft)
                    speed = Math.Min(maxSpeed, speed + accel);

                if (_scrollingRight)
                    speed = Math.Max(-maxSpeed, speed - accel);

                sv.ScrollToHorizontalOffset(
                    sv.HorizontalOffset - speed * 0.2
                );

                await Task.Delay(16);
            }

            while (Math.Abs(speed) > 0.5)
            {
                speed *= friction;

                sv.ScrollToHorizontalOffset(
                    sv.HorizontalOffset - speed * 0.2
                );

                await Task.Delay(16);
            }
        }


        private void ScrollButtons_MouseLeave(object sender, MouseEventArgs e)
        {
            _scrollingLeft = false;
            _scrollingRight = false;
        }


        private async void StartScrolling()
        {
            double speed = 0;
            double accel = 1.2;
            double maxSpeed = 30;
            double friction = 0.85;

            while (_scrollingLeft || _scrollingRight)
            {
                TabsScroll1.UpdateLayout();

                if (_scrollingLeft)
                    speed = Math.Min(maxSpeed, speed + accel);

                if (_scrollingRight)
                    speed = Math.Max(-maxSpeed, speed - accel);

                if (!_scrollingLeft && !_scrollingRight)
                    speed *= friction;

                TabsScroll1.ScrollToHorizontalOffset(
                    TabsScroll1.HorizontalOffset - speed * 0.2
                );

                await Task.Delay(16);
            }

            while (Math.Abs(speed) > 0.5)
            {
                speed *= friction;

                TabsScroll1.ScrollToHorizontalOffset(
                    TabsScroll1.HorizontalOffset - speed * 0.2
                );

                await Task.Delay(16);
            }
        }




        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            botonCentral.MouseDown += BotonCentral_MouseDown;
            botonCentral.MouseMove += BotonCentral_MouseMove;
            botonCentral.MouseUp += BotonCentral_MouseUp;

            Dispatcher.BeginInvoke(new Action(() =>
            {
                PopupSugerencias.IsOpen = false;

                var urlBox1 = GetUrlBox(1);
                var urlBox2 = GetUrlBox(2);
                var urlBox3 = GetUrlBox(3);
                var urlBox4 = GetUrlBox(4);

                urlBox1?.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
                urlBox2?.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
                urlBox3?.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
                urlBox4?.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));

                GetActiveWebView(1)?.Focus();

                // ⭐ Esperar antes de permitir el popup
                Task.Delay(500).ContinueWith(_ =>
                {
                    Dispatcher.Invoke(() => _appLoading = false);
                });

            }), System.Windows.Threading.DispatcherPriority.ApplicationIdle);
        }

        private void CentrarBoton()
        {
            double x = (gridPrincipal.ActualWidth - botonCentral.Width) / 2;
            double y = (gridPrincipal.ActualHeight - botonCentral.Height) / 2;

            Canvas.SetLeft(botonCentral, x);
            Canvas.SetTop(botonCentral, y);

            currentPropX = 0.5;
            currentPropY = 0.5;
        }

        private void BotonCentral_MouseDown(object sender, MouseButtonEventArgs e)
        {
            dragging = true;

            Point pos = e.GetPosition(canvasBoton);

            offset = new Point(
                pos.X - Canvas.GetLeft(botonCentral),
                pos.Y - Canvas.GetTop(botonCentral)
            );

            botonCentral.CaptureMouse();
        }

        private void BotonCentral_MouseMove(object sender, MouseEventArgs e)
        {
            if (!dragging) return;

            Point pos = e.GetPosition(canvasBoton);

            double x = pos.X - offset.X;
            double y = pos.Y - offset.Y;

            double margen = botonCentral.Width / 2;

            double minX = -margen;
            double maxX = gridPrincipal.ActualWidth - botonCentral.Width + margen;

            double minY = -margen;
            double maxY = gridPrincipal.ActualHeight - botonCentral.Height + margen;

            x = Math.Max(minX, Math.Min(maxX, x));
            y = Math.Max(minY, Math.Min(maxY, y));

            Canvas.SetLeft(botonCentral, x);
            Canvas.SetTop(botonCentral, y);

            double centroX = x + botonCentral.Width / 2;
            double centroY = y + botonCentral.Height / 2;

            double ancho = gridPrincipal.ActualWidth;
            double alto = gridPrincipal.ActualHeight;

            gridPrincipal.ColumnDefinitions[0].Width = new GridLength(centroX, GridUnitType.Pixel);
            gridPrincipal.ColumnDefinitions[1].Width = new GridLength(ancho - centroX, GridUnitType.Pixel);

            gridPrincipal.RowDefinitions[0].Height = new GridLength(centroY, GridUnitType.Pixel);
            gridPrincipal.RowDefinitions[1].Height = new GridLength(alto - centroY, GridUnitType.Pixel);

            currentPropX = centroX / ancho;
            currentPropY = centroY / alto;
        }


        private void BotonCentral_MouseUp(object sender, MouseButtonEventArgs e)
        {
            dragging = false;
            botonCentral.ReleaseMouseCapture();

            var s = GeneralSettingsManager.Instance;
            s.BotonCentralX = currentPropX;
            s.BotonCentralY = currentPropY;
            s.Save();
        }

        private void BotonCentral_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Verificamos si es doble clic
            if (e.ClickCount == 2)
            {
                ResetearBotonCentral();
            }
        }

        private void ResetearBotonCentral()
        {
            double anchoTotal = gridPrincipal.ActualWidth;
            double altoTotal = gridPrincipal.ActualHeight;

            // Calculamos el centro exacto para las proporciones del Grid
            double centroX = anchoTotal / 2;
            double centroY = altoTotal / 2;

            // Calculamos la posición del botón en el Canvas (esquina superior izquierda del botón)
            // Para que el centro del botón coincida con el centro del Grid:
            double xCanvas = centroX - (botonCentral.Width / 2);
            double yCanvas = centroY - (botonCentral.Height / 2);

            // 1. Reposicionar el botón visualmente en el Canvas
            Canvas.SetLeft(botonCentral, xCanvas);
            Canvas.SetTop(botonCentral, yCanvas);

            // 2. Ajustar las columnas y filas del Grid al 50% exacto
            gridPrincipal.ColumnDefinitions[0].Width = new GridLength(1, GridUnitType.Star);
            gridPrincipal.ColumnDefinitions[1].Width = new GridLength(1, GridUnitType.Star);

            gridPrincipal.RowDefinitions[0].Height = new GridLength(1, GridUnitType.Star);
            gridPrincipal.RowDefinitions[1].Height = new GridLength(1, GridUnitType.Star);

            // 3. Actualizar tus variables de proporción para que el próximo movimiento sea suave
            currentPropX = 0.5;
            currentPropY = 0.5;

            // Soltamos la captura del ratón por si acaso el primer clic la activó
            dragging = false;
            botonCentral.ReleaseMouseCapture();
        }

        private void MainWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            // Si hay un panel maximizado no tocar las filas/columnas
            if (_panelMaximizado != 0)
            {
                double ancho = gridPrincipal.ActualWidth;
                double alto = gridPrincipal.ActualHeight;
                Canvas.SetLeft(botonCentral, ancho / 2 - botonCentral.Width / 2);
                Canvas.SetTop(botonCentral, alto / 2 - botonCentral.Height / 2);
                return;
            }

            double anchoG = gridPrincipal.ActualWidth;
            double altoG = gridPrincipal.ActualHeight;
            if (anchoG > 0 && altoG > 0)
            {
                double centroX = currentPropX * anchoG;
                double centroY = currentPropY * altoG;
                centroX = Math.Max(50, Math.Min(anchoG - 50, centroX));
                centroY = Math.Max(50, Math.Min(altoG - 50, centroY));
                gridPrincipal.ColumnDefinitions[0].Width = new GridLength(currentPropX, GridUnitType.Star);
                gridPrincipal.ColumnDefinitions[1].Width = new GridLength(1 - currentPropX, GridUnitType.Star);
                gridPrincipal.RowDefinitions[0].Height = new GridLength(currentPropY, GridUnitType.Star);
                gridPrincipal.RowDefinitions[1].Height = new GridLength(1 - currentPropY, GridUnitType.Star);
                double botonX = centroX - botonCentral.Width / 2;
                double botonY = centroY - botonCentral.Height / 2;
                Canvas.SetLeft(botonCentral, botonX);
                Canvas.SetTop(botonCentral, botonY);
            }
        }


        //================================ NAVEGACION =================================================================
        private bool TryGetId(object sender, out int id)
        {
            id = 0;

            if (sender is not FrameworkElement fe)
                return false;

            // Buscar el ContentControl padre
            ContentControl? navbar = FindParent<ContentControl>(fe);

            if (navbar == null)
                return false;

            // Identificar qué NavBar es
            if (navbar == NavBar1) { id = 1; return true; }
            if (navbar == NavBar2) { id = 2; return true; }
            if (navbar == NavBar3) { id = 3; return true; }
            if (navbar == NavBar4) { id = 4; return true; }

            return false;
        }

        private void Url_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (sender is not TextBox tb)
                return;

            // ⭐ NAVEGACIÓN CON FLECHAS EN EL POPUP
            if (PopupSugerencias.IsOpen && ListaSugerencias.Items.Count > 0)
            {
                if (e.Key == Key.Down)
                {
                    e.Handled = true;
                    _navigatingWithKeyboard = true;

                    if (ListaSugerencias.SelectedIndex < ListaSugerencias.Items.Count - 1)
                        ListaSugerencias.SelectedIndex++;
                    else
                        ListaSugerencias.SelectedIndex = 0;

                    ListaSugerencias.ScrollIntoView(ListaSugerencias.SelectedItem);

                    if (ListaSugerencias.SelectedItem is HistoryEntry entry)
                    {
                        bool esHistorial = History.Instance.Search(entry.Title).Any(h => h.Url == entry.Url);
                        tb.Text = esHistorial ? entry.Url : entry.Title;
                        tb.SelectionStart = tb.Text.Length;
                    }

                    _navigatingWithKeyboard = false;
                    return;
                }
                else if (e.Key == Key.Up)
                {
                    e.Handled = true;
                    _navigatingWithKeyboard = true;

                    if (ListaSugerencias.SelectedIndex > 0)
                    {
                        ListaSugerencias.SelectedIndex--;
                        ListaSugerencias.ScrollIntoView(ListaSugerencias.SelectedItem);

                        if (ListaSugerencias.SelectedItem is HistoryEntry entry)
                        {
                            bool esHistorial = History.Instance.Search(entry.Title).Any(h => h.Url == entry.Url);
                            tb.Text = esHistorial ? entry.Url : entry.Title;
                            tb.SelectionStart = tb.Text.Length;
                        }
                    }

                    _navigatingWithKeyboard = false;
                    return;
                }
            }

            // Cerrar popup con Escape
            if (e.Key == Key.Escape)
            {
                PopupSugerencias.IsOpen = false;
                ListaSugerencias.SelectedIndex = -1;
                return;
            }

            if (e.Key != Key.Enter)
                return;

            // ⭐ ENTER
            e.Handled = true;
            PopupSugerencias.IsOpen = false;

            var web = FindWebViewForTextbox(tb);
            if (web == null)
                return;

            string text = tb.Text.Trim();
            if (string.IsNullOrWhiteSpace(text))
                return;

            if (!text.Contains("://"))
            {
                if (text.Contains(".") && !text.Contains(" "))
                    text = "https://" + text;
                else
                    text = "https://www.google.com/search?q=" + Uri.EscapeDataString(text);
            }

            Uri uri;
            try { uri = new Uri(text); }
            catch { uri = new Uri("https://www.google.com/search?q=" + Uri.EscapeDataString(tb.Text)); }

            web.Source = uri;
            web.Focus();

            ListaSugerencias.SelectedIndex = -1;
        }

        private static readonly HttpClient _httpClient = new HttpClient { Timeout = TimeSpan.FromSeconds(2) };

        private CancellationTokenSource _suggestionCts;

        private async void Url_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is not TextBox tb) return;
            if (_navigatingWithKeyboard) return;
            if (_appLoading) return;
            if (!tb.IsKeyboardFocused) return;

            string query = tb.Text.Trim();
            if (!TryGetId(tb, out int panelId)) return;
            _currentSuggestionPanel = panelId;

            if (string.IsNullOrWhiteSpace(query) || query.Length < 2)
            {
                PopupSugerencias.IsOpen = false;
                return;
            }
            
            // Historial inmediato - solo mostrar si hay resultados
            var historial = History.Instance.Search(query).Take(4).ToList();
            if (historial.Any())
                MostrarSugerencias(tb, historial);

            // Cancelar petición anterior
            _suggestionCts?.Cancel();
            _suggestionCts = new CancellationTokenSource();
            var token = _suggestionCts.Token;

            // ⭐ Debounce 200ms
            try { await Task.Delay(200, token); }
            catch (TaskCanceledException) { return; }

            if (token.IsCancellationRequested) return;

            try
            {
                var sugerencias = await ObtenerSugerenciasMotor(query, token);
                if (token.IsCancellationRequested) return;

                var combined = historial.ToList();
                foreach (var s in sugerencias.Take(5))
                {
                    if (!historial.Any(h => h.Url.Contains(s) || h.Title.Contains(s)))
                    {
                        combined.Add(new HistoryEntry
                        {
                            Title = s,
                            Url = GeneralSettingsManager.Instance.Buscador + Uri.EscapeDataString(s),
                            Timestamp = DateTime.Now
                        });
                    }
                }

                if (!token.IsCancellationRequested && combined.Any())
                    MostrarSugerencias(tb, combined);
            }
            catch { }
        }

        private void MostrarSugerencias(TextBox tb, List<HistoryEntry> items)
        {
            if (!items.Any()) return;

            _updatingSugerencias = true;

            for (int i = _sugerencias.Count - 1; i >= items.Count; i--)
                _sugerencias.RemoveAt(i);

            for (int i = 0; i < items.Count; i++)
            {
                if (i < _sugerencias.Count)
                    _sugerencias[i] = items[i];
                else
                    _sugerencias.Add(items[i]);
            }

            _updatingSugerencias = false;

            if (!PopupSugerencias.IsOpen)
            {
                PopupSugerencias.PlacementTarget = tb;
                PopupSugerencias.Width = tb.ActualWidth;
                PopupSugerencias.IsOpen = true;
            }
        }

        private async Task<List<string>> ObtenerSugerenciasMotor(string query, CancellationToken token)
        {
            try
            {
                var buscador = GeneralSettingsManager.Instance.Buscador;
                string url = buscador switch
                {
                    var b when b.Contains("bing.com") => $"https://api.bing.com/qsonhs.aspx?q={Uri.EscapeDataString(query)}",
                    var b when b.Contains("duckduckgo.com") => $"https://duckduckgo.com/ac/?q={Uri.EscapeDataString(query)}",
                    var b when b.Contains("ecosia.org") => $"https://ac.ecosia.org/autocomplete?q={Uri.EscapeDataString(query)}",
                    var b when b.Contains("brave.com") => $"https://search.brave.com/api/suggest?q={Uri.EscapeDataString(query)}",
                    var b when b.Contains("qwant.com") => $"https://api.qwant.com/v3/suggest?q={Uri.EscapeDataString(query)}",
                    _ => $"https://suggestqueries.google.com/complete/search?client=firefox&q={Uri.EscapeDataString(query)}"
                };

                var response = await _httpClient.GetStringAsync(url, token);

                if (buscador.Contains("duckduckgo.com"))
                {
                    var arr = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(response);
                    return arr?.Select(d => d["phrase"]).ToList() ?? new();
                }
                else if (buscador.Contains("ecosia.org"))
                {
                    var obj = JsonConvert.DeserializeObject<dynamic>(response);
                    var suggestions = new List<string>();
                    foreach (var item in obj.suggestions)
                        suggestions.Add((string)item.value);
                    return suggestions;
                }
                else if (buscador.Contains("qwant.com"))
                {
                    var obj = JsonConvert.DeserializeObject<dynamic>(response);
                    var suggestions = new List<string>();
                    foreach (var item in obj.data.items)
                        suggestions.Add((string)item.value);
                    return suggestions;
                }
                else
                {
                    var arr = JsonConvert.DeserializeObject<dynamic>(response);
                    var suggestions = new List<string>();
                    foreach (var item in arr[1])
                        suggestions.Add((string)item);
                    return suggestions;
                }
            }
            catch { return new List<string>(); }
        }

        private void ListaSugerencias_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is not ListBox lb) return;
            if (_updatingSugerencias) return;
            if (lb.SelectedItem is not HistoryEntry entry) return;
            if (_navigatingWithKeyboard) return;
            var web = GetActiveWebView(_currentSuggestionPanel);
            if (web != null)
                web.Source = new Uri(entry.Url);
            var urlBox = GetUrlBox(_currentSuggestionPanel);
            if (urlBox != null)
                urlBox.Text = entry.Url;
            PopupSugerencias.IsOpen = false;
            lb.SelectedIndex = -1;
        }


        private IEnumerable<DependencyObject> GetDescendants(DependencyObject parent)
        {
            int count = VisualTreeHelper.GetChildrenCount(parent);

            for (int i = 0; i < count; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                yield return child;

                foreach (var grandChild in GetDescendants(child))
                    yield return grandChild;
            }
        }


        private WebView2 FindWebViewForTextbox(DependencyObject start)
        {
            var parent = start;

            while (parent != null)
            {
                var web = GetDescendants(parent)
                    .OfType<WebView2>()
                    .FirstOrDefault();

                if (web != null)
                    return web;

                parent = VisualTreeHelper.GetParent(parent);
            }

            return null;
        }


        private void Back_Click(object sender, RoutedEventArgs e)
        {
            if (!TryGetId(sender, out int id))
                return;

            var web = GetActiveWebView(id);

            if (web?.CanGoBack == true)
            {
                web.GoBack();
                web.Focus();
            }
        }

        private void Forward_Click(object sender, RoutedEventArgs e)
        {
            if (!TryGetId(sender, out int id))
                return;

            var web = GetActiveWebView(id);

            if (web?.CanGoForward == true)
            {
                web.GoForward();
                web.Focus();
            }
        }


        private void Reload_Click(object sender, RoutedEventArgs e)
        {
            if (!TryGetId(sender, out int id))
                return;

            var web = GetActiveWebView(id);

            web?.Reload();
        }


        //================================ FIN NAVEGACION =================================================================

        private int GetPanelIdFromItemsControl(ItemsControl panel)
        {
            if (panel == TabsPanel1) return 1;
            if (panel == TabsPanel2) return 2;
            if (panel == TabsPanel3) return 3;
            if (panel == TabsPanel4) return 4;
            return 0;
        }

        private int GetDropIndexByPosition(ItemsControl panel, Point mousePos)
        {
            double x = 0;

            for (int i = 0; i < panel.Items.Count; i++)
            {
                var c = panel.ItemContainerGenerator.ContainerFromIndex(i) as FrameworkElement;
                if (c == null) continue;

                double w = c.ActualWidth;
                if (mousePos.X < x + w / 2)
                    return i;

                x += w;
            }

            return panel.Items.Count;
        }


        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            // Si no hay drag potencial → salir
            if (!_tabDragging || draggedTab == null)
                return;

            Point posWindow = e.GetPosition(this);

            // ⭐ UMBRAL DE INICIO DE DRAG REAL
            if (!isDraggingTab)
            {
                double dx = Math.Abs(posWindow.X - _tabDragStart.X);
                double dy = Math.Abs(posWindow.Y - _tabDragStart.Y);

                if (dx < 5 && dy < 5)
                    return;

                if (Mouse.LeftButton != MouseButtonState.Pressed)
                    return;

                // ⭐ Aquí empieza el drag REAL
                isDraggingTab = true;

                // --- NUEVO: activar GhostControl ---
                ShowGhost(draggedTab.Title, draggedTab.FaviconUrl, posWindow);
            }

            // ⭐ MOVER GHOST (nuevo)
            MoveGhost(posWindow);

            // ⭐ PLACEHOLDER + DROP INDICATOR
            ItemsControl[] panels = { TabsPanel1, TabsPanel2, TabsPanel3, TabsPanel4 };

            currentTargetPanel = null;
            currentDropIndex = -1;

            foreach (var panel in panels)
            {
                Point panelPos = panel.TranslatePoint(new Point(0, 0), this);
                Rect rect = new Rect(panelPos.X, panelPos.Y, panel.ActualWidth, panel.ActualHeight);

                if (!rect.Contains(posWindow))
                    continue;

                currentTargetPanel = panel;

                // Ocultar ghost cuando está sobre un panel
                GhostPopup.IsOpen = false;

                int panelId = GetPanelIdFromItemsControl(panel);
                var list = GetTabsList(panelId);

                // ⭐ POSICIÓN DEL RATÓN RELATIVA AL PANEL
                Point posInPanel = e.GetPosition(panel);

                // ⭐ CÁLCULO CHROME-LIKE DEL DROP INDEX
                currentDropIndex = GetDropIndexByPosition(panel, posInPanel);

                // --- CREAR O MOVER PLACEHOLDER ---
                if (placeholderTab == null)
                {
                    placeholderTab = new TabInfo
                    {
                        Title = draggedTab.Title,
                        Url = draggedTab.Url,
                        FaviconUrl = draggedTab.FaviconUrl,
                        IsPlaceholder = true
                    };

                    list.Insert(currentDropIndex, placeholderTab);

                    // Eliminar pestaña original
                    var originList = GetTabsList(draggedFromPanel);
                    if (originList.Contains(draggedTab))
                        originList.Remove(draggedTab);
                }
                else
                {
                    int oldIndex = list.IndexOf(placeholderTab);

                    if (currentDropIndex < 0) currentDropIndex = 0;
                    if (currentDropIndex > list.Count) currentDropIndex = list.Count;

                    if (oldIndex == -1)
                    {
                        list.Insert(currentDropIndex, placeholderTab);
                    }
                    else if (oldIndex != currentDropIndex)
                    {
                        if (oldIndex >= 0 && oldIndex < list.Count &&
                            currentDropIndex >= 0 && currentDropIndex < list.Count)
                        {
                            list.Move(oldIndex, currentDropIndex);
                        }
                    }
                }

                // --- DROP INDICATOR ESTABLE ---
                if (currentDropIndex != lastDropIndex)
                {
                    UpdateDropIndicatorPosition(panel, currentDropIndex);
                    lastDropIndex = currentDropIndex;
                }

                // ⭐ AUTO-SCROLL DURANTE DRAG
                if (isDraggingTab)
                {
                    if (panel == TabsPanel1)
                        AutoScrollDuringDrag(TabsScroll1, e.GetPosition(TabsScroll1));
                    else if (panel == TabsPanel2)
                        AutoScrollDuringDrag(TabsScroll2, e.GetPosition(TabsScroll2));
                    else if (panel == TabsPanel3)
                        AutoScrollDuringDrag(TabsScroll3, e.GetPosition(TabsScroll3));
                    else if (panel == TabsPanel4)
                        AutoScrollDuringDrag(TabsScroll4, e.GetPosition(TabsScroll4));
                }
                ghostVisible = false;
                return;
            }

            // ⭐ SI NO ESTAMOS SOBRE NINGÚN PANEL → ELIMINAR PLACEHOLDER
            if (placeholderTab != null)
            {
                foreach (var list in new[] { Tabs1, Tabs2, Tabs3, Tabs4 })
                    list.Remove(placeholderTab);

                placeholderTab = null;
                currentDropIndex = -1;
                currentTargetPanel = null;
            }

            // Mostrar ghost si no está sobre panel
            if (!ghostVisible)
            {
                GhostPopup.IsOpen = true;
                ghostVisible = true;
            }
        }


        private void UpdateDropIndicatorPosition(ItemsControl panel, int index)
        {
            var container = panel.ItemContainerGenerator.ContainerFromIndex(index) as FrameworkElement;
            if (container == null) return;

            var posCanvas = container.TranslatePoint(new Point(0, 0), canvasBoton);

            Canvas.SetLeft(dropIndicator, posCanvas.X - 1);
            Canvas.SetTop(dropIndicator, posCanvas.Y);
        }


        private void FixOriginPanelAfterDrag()
        {
            var originList = GetTabsList(draggedFromPanel);

            // Si quedó vacío → crear pestaña nueva
            if (originList.Count == 0)
            {
                var newTab = new TabInfo
                {
                    Title = Idioma.Instance.Main_NewTab,
                    Url = "https://www.google.com",
                    FaviconUrl = "https://www.google.com/s2/favicons?domain=google.com",
                    IsActive = true
                };

                newTab.WebView = CreateWebViewForTab(newTab);

                originList.Add(newTab);
                ForceTabsRemeasure();
                ApplyActiveTabToWeb(draggedFromPanel);
                return;
            }

            // Si ya hay activa, no tocar
            if (originList.Any(t => t.IsActive))
                return;

            // Activar primera pestaña real
            var firstReal = originList.FirstOrDefault(t => !t.IsPlaceholder && t.WebView != null);

            if (firstReal != null)
            {
                foreach (var t in originList)
                    t.IsActive = false;

                firstReal.IsActive = true;
            }

            ApplyActiveTabToWeb(draggedFromPanel);
        }




        private void Window_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Mouse.Capture(null);

            // ⭐ CLICK NORMAL (sin drag real)
            if (!isDraggingTab && draggedTab != null)
            {
                int panelId = draggedFromPanel;
                var list = GetTabsList(panelId);

                foreach (var t in list)
                    t.IsActive = false;

                draggedTab.IsActive = true;
                ApplyActiveTabToWeb(panelId);

                ResetDragState();
                return;
            }

            // ⭐ SI NO HAY GHOST Y NO HAY TARGET PANEL → NO HACER DROP
            if ((!ghostVisible) && currentTargetPanel == null)
            {
                FixOriginPanelAfterDrag();
                ResetDragState();
                return;
            }

            // ⭐ Cerrar ghost (nuevo)
            HideGhost();

            Point pos = e.GetPosition(this);

            // ⭐ 1) DROP SOBRE PANEL DE PESTAÑAS (ItemsControl)
            if (currentTargetPanel != null)
            {
                int panelId = GetPanelIdFromItemsControl(currentTargetPanel);
                var list = GetTabsList(panelId);

                int index = currentDropIndex;

                // ⭐ AJUSTAR índice ANTES de eliminar placeholder
                if (placeholderTab != null && list.Contains(placeholderTab))
                {
                    int placeholderIndex = list.IndexOf(placeholderTab);
                    if (index > placeholderIndex)
                        index--;
                }

                // Eliminar placeholder de todos los paneles
                if (placeholderTab != null)
                {
                    foreach (var l in new[] { Tabs1, Tabs2, Tabs3, Tabs4 })
                        l.Remove(placeholderTab);
                    placeholderTab = null;
                }

                // ⭐ Validar índice
                if (index < 0) index = 0;
                if (index > list.Count) index = list.Count;

                // Insertar pestaña real
                if (draggedTab != null)
                {
                    list.Insert(index, draggedTab);

                    FixOriginPanelAfterDrag();

                    foreach (var t in list)
                        t.IsActive = false;

                    draggedTab.IsActive = true;
                    ApplyActiveTabToWeb(panelId);
                }

                ResetDragState();
                return;
            }

            // ⭐ 2) DROP SOBRE CUALQUIER PARTE DEL PANEL (Grid completo)
            int dropPanelId = GetPanelIdFromPoint(pos);
            if (dropPanelId != 0)
            {
                var targetList = GetTabsList(dropPanelId);

                // Eliminar placeholder
                if (placeholderTab != null)
                {
                    foreach (var l in new[] { Tabs1, Tabs2, Tabs3, Tabs4 })
                        l.Remove(placeholderTab);

                    placeholderTab = null;
                }

                if (draggedTab != null)
                {
                    targetList.Add(draggedTab);

                    foreach (var t in targetList)
                        t.IsActive = false;

                    FixOriginPanelAfterDrag();

                    draggedTab.IsActive = true;
                    ApplyActiveTabToWeb(dropPanelId);
                }

                ResetDragState();
                return;
            }

            // ⭐ 3) DROP FUERA DE LA VENTANA → abrir en navegador externo o volver al origen
            bool fuera =
                pos.X < 0 ||
                pos.Y < 0 ||
                pos.X > ActualWidth ||
                pos.Y > ActualHeight;

            // Eliminar placeholder
            if (placeholderTab != null)
            {
                foreach (var l in new[] { Tabs1, Tabs2, Tabs3, Tabs4 })
                    l.Remove(placeholderTab);

                placeholderTab = null;
            }

            var originListFinal = GetTabsList(draggedFromPanel);

            if (fuera && draggedTab?.Url != null)
            {
                try
                {
                    System.Diagnostics.Process.Start(
                        new System.Diagnostics.ProcessStartInfo(draggedTab.Url)
                        {
                            UseShellExecute = true
                        }
                    );
                }
                catch { }
            }

            if (draggedTab != null)
            {
                originListFinal.Add(draggedTab);

                FixOriginPanelAfterDrag();

                foreach (var t in originListFinal)
                    t.IsActive = false;

                draggedTab.IsActive = true;
                ApplyActiveTabToWeb(draggedFromPanel);
            }

            ResetDragState();
        }


        private void Web_CoreWebView2InitializationCompleted(object? sender, Microsoft.Web.WebView2.Core.CoreWebView2InitializationCompletedEventArgs e)
        {
            if (!e.IsSuccess)
                return;

            var web = sender as WebView2;
            if (web == null)
                return;

            web.CoreWebView2.NewWindowRequested += (s, args) =>
            {
                try
                {
                    string url = args.Uri;

                    if (!string.IsNullOrWhiteSpace(url))
                    {
                        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(url)
                        {
                            UseShellExecute = true
                        });
                    }

                    args.Handled = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(Idioma.Instance.Main_ErrExternalLink + ex.Message);
                }
            };
        }


        private void OpenCookies_Click(object sender, RoutedEventArgs e)
        {
            // 1. Si ya está abierta, la traemos al frente y listo
            var win = Application.Current.Windows.OfType<CookieManagerWindow>().FirstOrDefault();
            if (win != null)
            {
                if (win.WindowState == WindowState.Minimized) win.WindowState = WindowState.Normal;
                win.Activate();
                return;
            }

            // 2. Buscamos el WebView activo
            var web = GetActiveWebView(1) ?? GetActiveWebView(2) ??
                      GetActiveWebView(3) ?? GetActiveWebView(4);

            if (web?.CoreWebView2 == null)
            {
                MessageBox.Show(Idioma.Instance.Main_NoTabInit);
                return;
            }

            // 3. Si no existe, la creamos usando el CookieManager del WebView activo
            new CookieManagerWindow(web.CoreWebView2.CookieManager)
            {
                Owner = this
            }.Show();
        }




    }


    // PANEL PERSONALIZADO PARA PESTAÑAS
    public class StableCompressingTabPanel : Panel
    {
        public double IdealTabWidth { get; set; } = 180.0;
        public double MinTabWidth { get; set; } = 60.0;

        private readonly Dictionary<UIElement, double> _lastPositions = new();

        public StableCompressingTabPanel()
        {
            Loaded += (s, e) =>
            {
                var scroll = FindAncestor<ScrollViewer>(this);
                var dock = FindAncestor<DockPanel>(this);

                if (scroll != null && dock != null)
                {
                    scroll.SizeChanged += (s2, e2) =>
                    {
                        dock.Width = scroll.ActualWidth;
                        InvalidateMeasure();
                        InvalidateArrange();
                    };
                }
            };
        }

        protected override void OnVisualChildrenChanged(DependencyObject visualAdded, DependencyObject visualRemoved)
        {
            base.OnVisualChildrenChanged(visualAdded, visualRemoved);
            InvalidateMeasure();
        }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);
            InvalidateMeasure();
            InvalidateArrange();
        }


        protected override Size MeasureOverride(Size availableSize)
        {
            int count = InternalChildren.Count;
            if (count == 0)
                return new Size(0, 0);

            const double buttonWidth = 28;
            const double buttonMarginLeft = 4;
            double reservedWidth = buttonWidth + buttonMarginLeft;

            double viewportWidth = GetContainerWidth();

            if (viewportWidth <= 0 || double.IsInfinity(viewportWidth))
                viewportWidth = this.ActualWidth > 0 ? this.ActualWidth : 1;

            if (!double.IsInfinity(viewportWidth))
                viewportWidth = Math.Max(0, viewportWidth - reservedWidth);

            double idealTotalWidth = IdealTabWidth * count;
            double compressedTotalWidth = MinTabWidth * count;

            double actualWidth;

            if (idealTotalWidth <= viewportWidth)
            {
                actualWidth = idealTotalWidth;
            }
            else if (compressedTotalWidth <= viewportWidth)
            {
                actualWidth = viewportWidth;
            }
            else
            {
                actualWidth = compressedTotalWidth;
            }

            double tabWidth = actualWidth / count;
            tabWidth = Math.Max(MinTabWidth, Math.Min(tabWidth, IdealTabWidth));

            foreach (UIElement child in InternalChildren)
                child.Measure(new Size(tabWidth, availableSize.Height));

            double height = InternalChildren
                .Cast<UIElement>()
                .Max(c => c.DesiredSize.Height);

            return new Size(actualWidth, height);
        }


        private double GetContainerWidth()
        {
            Grid? container = FindAncestor<Grid>(this);
            return container?.ActualWidth ?? 0;
        }

        private T? FindAncestor<T>(DependencyObject child) where T : DependencyObject
        {
            DependencyObject current = child;

            while (current != null)
            {
                if (current is T typed)
                    return typed;

                current = VisualTreeHelper.GetParent(current);
            }

            return null;
        }


        protected override Size ArrangeOverride(Size finalSize)
        {
            int count = InternalChildren.Count;
            if (count == 0)
                return finalSize;

            double totalWidth = finalSize.Width;
            double tabWidth = IdealTabWidth;

            if (totalWidth < IdealTabWidth * count)
                tabWidth = Math.Max(MinTabWidth, totalWidth / count);

            double x = 0;

            foreach (UIElement child in InternalChildren)
            {
                Rect targetRect = new Rect(x, 0, tabWidth, finalSize.Height);
                child.Arrange(targetRect);

                if (child.RenderTransform is not TransformGroup)
                {
                    child.RenderTransform = new TransformGroup
                    {
                        Children = new TransformCollection { new TranslateTransform() }
                    };
                }

                var transformGroup = (TransformGroup)child.RenderTransform;
                var translate = (TranslateTransform)transformGroup.Children[0];

                if (!_lastPositions.ContainsKey(child))
                {
                    _lastPositions[child] = x;
                    translate.X = 0;
                }
                else
                {
                    double lastX = _lastPositions[child];
                    double delta = lastX - x;

                    if (Math.Abs(delta) > 0.1)
                    {
                        translate.BeginAnimation(TranslateTransform.XProperty, null);
                        translate.X = delta;

                        var anim = new DoubleAnimation
                        {
                            To = 0,
                            Duration = TimeSpan.FromMilliseconds(200),
                            EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
                        };

                        translate.BeginAnimation(TranslateTransform.XProperty, anim);
                    }

                    _lastPositions[child] = x;
                }

                x += tabWidth;
            }

            // Limpiar pestañas eliminadas
            var toRemove = _lastPositions.Keys.Where(k => !InternalChildren.Contains(k)).ToList();
            foreach (var key in toRemove)
                _lastPositions.Remove(key);

            return finalSize;
        }
    }

    public class WebViewState
    {
        public bool Incognito { get; set; }
    }

    public class UrlToFaviconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string url && !string.IsNullOrEmpty(url))
            {
                try
                {
                    var uri = new Uri(url);
                    return $"https://www.google.com/s2/favicons?domain={uri.Host}&sz=64";
                }
                catch
                {
                    return "https://www.google.com/s2/favicons?domain=google.com";
                }
            }
            return "https://www.google.com/s2/favicons?domain=google.com";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }



    public static class AppPaths
    {
        public static readonly string AppFolder = System.IO.Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "Multinavigator");

        public static readonly string BrowserData = System.IO.Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "Multinavigator", "BrowserData");

        public static readonly string GeneralSettings = System.IO.Path.Combine(AppFolder, "general.json");
        public static readonly string Favorites = System.IO.Path.Combine(AppFolder, "favorites.json");
        public static readonly string Permissions = System.IO.Path.Combine(AppFolder, "permissions.json");
        public static readonly string Themes = System.IO.Path.Combine(AppFolder, "custom_themes.json");
        public static readonly string ThemeSettings = System.IO.Path.Combine(AppFolder, "theme_setting.json");
        public static readonly string History = System.IO.Path.Combine(AppFolder, "history.json");
    }


}
