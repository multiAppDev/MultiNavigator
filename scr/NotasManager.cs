using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using Newtonsoft.Json;

namespace Multinavigator
{
    public class Nota
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public DateTime Fecha { get; set; } = DateTime.Today;
        public string Texto { get; set; } = "";
        public bool AvisoActivo { get; set; } = false;
        public DateTime? FechaAviso { get; set; } = null;
    }

    public class NotasManager
    {
        private static NotasManager _instance;
        public static NotasManager Instance => _instance ??= Load();

        public ObservableCollection<Nota> Notas { get; set; } = new();

        private static string FilePath => System.IO.Path.Combine(AppPaths.AppFolder, "notas.json");

        public void Add(Nota nota)
        {
            Notas.Add(nota);
            Save();
        }

        public void Remove(Nota nota)
        {
            Notas.Remove(nota);
            Save();
        }

        public void Save()
        {
            try
            {
                Directory.CreateDirectory(System.IO.Path.GetDirectoryName(FilePath));
                File.WriteAllText(FilePath, JsonConvert.SerializeObject(Notas, Formatting.Indented));
            }
            catch { }
        }

        public List<Nota> GetAvisosHoy()
        {
            var hoy = DateTime.Today;
            var result = new List<Nota>();
            foreach (var n in Notas)
                if (n.AvisoActivo && n.FechaAviso.HasValue && n.FechaAviso.Value <= hoy)
                    result.Add(n);
            return result;
        }

        private static NotasManager Load()
        {
            var mgr = new NotasManager();
            try
            {
                if (File.Exists(FilePath))
                {
                    var list = JsonConvert.DeserializeObject<List<Nota>>(File.ReadAllText(FilePath));
                    if (list != null)
                        foreach (var n in list)
                            mgr.Notas.Add(n);
                }
            }
            catch { }
            return mgr;
        }
    }
}