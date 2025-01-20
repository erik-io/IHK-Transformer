using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IHK_Transform.Models;

namespace IHK_Transform.Services
{
    internal class CsvDataService
    {
        private List<Azubi> _azubis = new List<Azubi>();
        private List<Ausbilder> _ausbilder = new List<Ausbilder>();
        private List<Ausbildung> _ausbildungen = new List<Ausbildung>();
        private string _filePath;


        public void SetFilePath(string filePath)
        {
            _filePath = filePath ?? throw new ArgumentNullException(nameof(filePath));
        }

        public void LoadData()
        {
            if (string.IsNullOrWhiteSpace(_filePath))
                throw new InvalidOperationException("Die Datei wurde nicht gefunden oder der Pfad ist leer.");

            LoadAzubiData();
            LoadAusbilderData();
            LoadAusbildungData();
        }

        private void LoadAzubiData()
        {
            string[] lines = File.ReadAllLines(_filePath);
            _azubis.Clear();
            int startIndex = Array.FindIndex(lines, line => line.StartsWith("[Azubis]"));
            if (startIndex == -1)
                throw new InvalidOperationException("Die Datei enthält keine Azubis.");

            for (int i = startIndex + 2; i < lines.Length && !lines[i].StartsWith("["); i++)
            {
                string[] data = lines[i].Split(';');
                if (data.Length >= 6)
                {
                    var azubi = new Azubi(
                        int.Parse(data[0]),
                        data[1],
                        data[2],
                        DateTime.Parse(data[3]),
                        data[4],
                        int.Parse(data[5])
                    );
                    _azubis.Add(azubi);
                }
            }
        }

        private void LoadAusbilderData()
        {
            string[] lines = File.ReadAllLines(_filePath);
            _ausbilder.Clear();
            int startIndex = Array.FindIndex(lines, line => line.StartsWith("[Ausbilder]"));
            if (startIndex == -1)
                throw new InvalidOperationException("Die Datei enthält keine Ausbilder.");

            for (int i = startIndex + 2; i < lines.Length && !lines[i].StartsWith("["); i++)
            {
                string[] data = lines[i].Split(';');
                if (data.Length >= 3)
                {
                    var ausbilder = new Ausbilder(
                        int.Parse(data[0]),
                        data[1],
                        data[2]
                    );
                    _ausbilder.Add(ausbilder);
                }
            }
        }

        private void LoadAusbildungData()
        {
            string[] lines = File.ReadAllLines(_filePath);
            _ausbildungen.Clear();
            int startIndex = Array.FindIndex(lines, line => line.StartsWith("[Ausbildungen]"));
            if (startIndex == -1)
                throw new InvalidOperationException("Die Datei enthält keine Ausbildungen.");

            for (int i = startIndex + 2; i < lines.Length && !lines[i].StartsWith("["); i++)
            {
                string[] data = lines[i].Split(';');
                if (data.Length >= 2)
                {
                    var ausbildung = new Ausbildung(
                        data[0],
                        data[1]
                    );
                    _ausbildungen.Add(ausbildung);
                }
            }
        }

        public List<Azubi> GetAzubiData() => _azubis;
        public List<Ausbilder> GetAusbilderData() => _ausbilder;
        public List<Ausbildung> GetAusbildungData() => _ausbildungen;
    }
}