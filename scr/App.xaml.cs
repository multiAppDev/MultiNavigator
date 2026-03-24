using Multinavigator.Idiomas;
using System.Threading;
using System.Windows;

namespace Multinavigator
{
    public partial class App : Application
    {
        private Mutex? _mutex;

        protected override void OnStartup(StartupEventArgs e)
        {
            _mutex = new Mutex(true, "Multinavigator_SingleInstance", out bool isNewInstance);
            if (!isNewInstance)
            {
                var existing = System.Diagnostics.Process.GetProcessesByName(
                    System.Diagnostics.Process.GetCurrentProcess().ProcessName)
                    .FirstOrDefault(p => p.Id != System.Diagnostics.Process.GetCurrentProcess().Id);
                if (existing != null)
                {
                    ShowWindow(existing.MainWindowHandle, 9);
                    SetForegroundWindow(existing.MainWindowHandle);
                }
                Shutdown();
                return;
            }

            // Cargar ajustes
            var settings = GeneralSettingsManager.Instance;

            // Primera ejecución: detectar idioma del SO
            if (settings.FirstRun)
            {
                string systemLang = System.Globalization.CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
                var supported = new[]
                    {
                        "es", "en", "zh", "hi", "nl", "pt", "fr", "de", "ja", "ru",
                        "ko", "tr", "id", "it", "bn", "vi", "pl", "th", "sw", "tl",
                        "uk", "cs", "ro", "ms", "ur"
                    };
                settings.Language = supported.Contains(systemLang) ? systemLang : "en";
            }

            // Aplicar idioma ANTES de abrir cualquier ventana
            Idioma.Instance.SetLanguage(settings.Language ?? "es");

            base.OnStartup(e);

            ThemeManager.Instance.ApplyTheme(ThemeManager.Instance.CurrentTheme);
        }

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);
    }
}