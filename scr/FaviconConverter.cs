using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace Multinavigator
{
    public class FaviconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not string input || string.IsNullOrWhiteSpace(input))
                return null;

            try
            {
                // Si ya es URL completa → úsala tal cual
                if (input.StartsWith("http://") || input.StartsWith("https://"))
                    return new Uri(input);

                // Si es dominio o URL sin esquema → normalizar
                string domain = input.Replace("https://", "")
                                     .Replace("http://", "")
                                     .Replace("www.", "")
                                     .Trim()
                                     .TrimEnd('/');

                var parts = domain.Split('.');
                if (parts.Length > 2)
                    domain = string.Join(".", parts.Skip(parts.Length - 2));

                string googleUrl = $"https://www.google.com/s2/favicons?domain={domain}&sz=32";
                string directUrl = $"https://{domain}/favicon.ico";

                // Devolvemos Google; el fragmento es solo informativo
                return new Uri(googleUrl);
            }
            catch
            {
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}
