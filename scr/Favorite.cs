using Newtonsoft.Json;
using System.Windows.Media.Imaging;
using System.ComponentModel;
using System.Runtime.CompilerServices;

public class Favorite : INotifyPropertyChanged
{
    public string Title { get; set; } = "";
    public string Url { get; set; } = "";
    public string Folder { get; set; } = "";
    public string FaviconUrl { get; set; } = "";

    [JsonIgnore]  // ← ESTO es lo que faltaba — no serializar la imagen
    private BitmapImage? _faviconImage;

    [JsonIgnore]
    public BitmapImage? FaviconImage
    {
        get => _faviconImage;
        set { _faviconImage = value; OnPropertyChanged(); }
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string? name = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}