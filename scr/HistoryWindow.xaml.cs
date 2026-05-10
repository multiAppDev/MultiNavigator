using Multinavigator.Idiomas;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Multinavigator
{
    public class HistoryGroup
    {
        public Idioma Trad => Idioma.Instance;
        public string GroupName { get; set; }
        public int Count => Entries.Count;
        public bool IsExpanded { get; set; }
        public List<HistoryEntry> Entries { get; set; } = new();
    }

    public partial class HistoryWindow : Window
    {
        public HistoryWindow()
        {
            this.DataContext = this;
            InitializeComponent();
            LoadHistory();
        }

        private void LoadHistory()
        {
            var entries = History.Instance.GetAll().ToList();
            GroupsPanel.ItemsSource = BuildGroups(entries);
        }

        private List<HistoryGroup> BuildGroups(List<HistoryEntry> entries)
        {
            return entries
                .GroupBy(e => e.DateGroup)
                .Select((g, i) => new HistoryGroup
                {
                    GroupName  = g.Key,
                    IsExpanded = i == 0,
                    Entries    = g.ToList()
                })
                .ToList();
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string query = SearchBox.Text.Trim();

            if (string.IsNullOrEmpty(query))
            {
                LoadHistory();
            }
            else
            {
                var results = History.Instance.Search(query).ToList();
                GroupsPanel.ItemsSource = new List<HistoryGroup>
                {
                    new HistoryGroup
                    {
                        GroupName  = string.Format(Idioma.Instance.History_SearchResults, query),
                        IsExpanded = true,
                        Entries    = results
                    }
                };
            }
        }

        private void DeleteEntry_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is HistoryEntry entry)
            {
                History.Instance.Remove(entry);
                LoadHistory();
            }
        }

        private void DeleteAll_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show(Idioma.Instance.History_Msg_ConfirmDelete,
                    Idioma.Instance.History_Msg_Confirmation,
                    MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                History.Instance.Clear();
                LoadHistory();
            }
        }
    }

    public class HistoryEntry
    {
        public DateTime Timestamp { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }

        public string DateGroup
        {
            get
            {
                var today = DateTime.Today;
                var diff  = today - Timestamp.Date;

                if (diff.Days == 0)  return Idioma.Instance.History_Today;
                if (diff.Days == 1)  return Idioma.Instance.History_Yesterday;
                if (diff.Days < 7)   return Idioma.Instance.History_ThisWeek;
                if (diff.Days < 30)  return Idioma.Instance.History_ThisMonth;
                return Timestamp.ToString("MMMM yyyy");
            }
        }
    }

    public class HistoryManager
    {
        private readonly List<HistoryEntry> _entries = new();
        private static string FilePath => AppPaths.History;

        public HistoryManager() { Load(); }

        public void Add(string url, string title)
        {
            var last = _entries.OrderByDescending(e => e.Timestamp).FirstOrDefault();
            if (last != null && last.Url == url &&
                (DateTime.Now - last.Timestamp).TotalSeconds < 30)
                return;

            _entries.Add(new HistoryEntry { Timestamp = DateTime.Now, Url = url, Title = title });
            Save();
        }

        public void Remove(HistoryEntry entry) { _entries.Remove(entry); Save(); }
        public void Clear()                    { _entries.Clear();       Save(); }

        public IEnumerable<HistoryEntry> GetAll() =>
            _entries.OrderByDescending(e => e.Timestamp);

        public IEnumerable<HistoryEntry> Search(string query)
        {
            query = query.ToLower();
            return _entries.Where(e =>
                e.Url.ToLower().Contains(query) ||
                (e.Title != null && e.Title.ToLower().Contains(query))
            ).OrderByDescending(e => e.Timestamp);
        }

        private void Save()
        {
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(FilePath));
                File.WriteAllText(FilePath,
                    System.Text.Json.JsonSerializer.Serialize(_entries,
                        new System.Text.Json.JsonSerializerOptions { WriteIndented = true }));
            }
            catch { }
        }

        private void Load()
        {
            try
            {
                if (File.Exists(FilePath))
                {
                    var loaded = System.Text.Json.JsonSerializer.Deserialize<List<HistoryEntry>>(
                        File.ReadAllText(FilePath));
                    if (loaded != null) _entries.AddRange(loaded);
                }
            }
            catch { }
        }
    }

    public static class History
    {
        public static HistoryManager Instance { get; } = new HistoryManager();
    }
}
