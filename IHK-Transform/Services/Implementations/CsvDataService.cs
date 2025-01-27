using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using IHK_Transform.Models.Entities;
using IHK_Transform.Services.Interfaces;

namespace IHK_Transform.Services.Implementations
{
    public class CsvDataService : IDataProvider
    {
        // Felder für Datenhaltung
        private List<Azubi> _azubis = new List<Azubi>();
        private List<Ausbilder> _ausbilder = new List<Ausbilder>();
        private List<Ausbildung> _ausbildungen = new List<Ausbildung>();

        private string _filePath;
        private char _delimiter = ';';
        private string _lastError = string.Empty;

        // Interface-Implementierungen
        public bool IsConnected { get; private set; }
        public string LastError => _lastError;

        public void SetFilePath(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentNullException(nameof(filePath));
            if (!File.Exists(filePath))
                throw new FileNotFoundException("CSV-Datei nicht gefunden", filePath);
            _filePath = filePath;
        }

        public void SetDelimiter(char delimiter) => _delimiter = delimiter;

        // --- IDataProvider-Implementierung ---
        public void Connect() => IsConnected = true; // CSV-Datei benötigt keine Verbindung
        public void Disconnect() => IsConnected = false;

        public void SetSource(string source) => SetFilePath(source);

        public void LoadData()
        {
            Debug.WriteLine($"CSV Datei wird geladen: {_filePath}");
            var lines = File.ReadAllLines(_filePath);
            var section = "";

            // Listen vor dem Laden leeren
            _azubis.Clear();
            _ausbilder.Clear();
            _ausbildungen.Clear();

            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                if (line.StartsWith("["))
                {
                    section = line;
                    continue;
                }

                if (line.Contains(_delimiter)) // Header-Zeilen überspringen
                {
                    if (line.StartsWith("ausbilder_id") ||
                        line.StartsWith("ausbildung_id") ||
                        line.StartsWith("azubi_id")) continue;

                    var values = line.Split(_delimiter);

                    switch (section)
                    {
                        case "[Ausbilder]":
                            _ausbilder.Add(new Ausbilder(
                                int.Parse(values[0]),
                                values[1],
                                values[2]
                            ));
                            break;

                        case "[Ausbildungen]":
                            _ausbildungen.Add(new Ausbildung(
                                values[0],
                                values[1]
                            ));
                            break;

                        case "[Azubis]":
                            _azubis.Add(new Azubi(
                                int.Parse(values[0]),
                                values[1],
                                values[2],
                                DateTime.Parse(values[3]),
                                values[4],
                                int.Parse(values[5])
                            ));
                            break;
                    }
                }
            }
            Debug.WriteLine($"Geladene Daten - Azubis: {_azubis.Count}, Ausbilder: {_ausbilder.Count}");
        }

        // --- IDataHandler-Implementierung ---
        public void SaveData()
        {
            throw new NotImplementedException("CSV-Speicherung nicht implementiert.");
        }

        // --- Datenabfragen ---
        public List<Azubi> GetAzubiData() => _azubis;
        public List<Ausbilder> GetAusbilderData() => _ausbilder;
        public List<Ausbildung> GetAusbildungData() => _ausbildungen;

        // --- Private Hilfsmethoden ---
        private void LoadAzubiData()
        {
            string[] lines = File.ReadAllLines(_filePath);
            _azubis.Clear();
            int startIndex = Array.FindIndex(lines, line => line.StartsWith("[Azubis]"));
            if (startIndex == -1)
                throw new InvalidOperationException("Fehlender [Azubis]-Header.");

            for (int i = startIndex + 1; i < lines.Length && !lines[i].StartsWith("["); i++)
            {
                string[] data = lines[i].Split(_delimiter);
                if (data.Length < 6) 
                    continue;
                
                _azubis.Add(new Azubi
                (
                    int.Parse(data[0]),
                    data[1],
                    data[2],
                    DateTime.Parse(data[3]),
                    data[4],
                    int.Parse(data[5])
                ));
            }
        }
        private void LoadAusbilderData()
        {
            string[] lines = File.ReadAllLines(_filePath);
            _ausbilder.Clear();
            int startIndex = Array.FindIndex(lines, line => line.StartsWith("[Ausbilder]"));
            if (startIndex == -1) 
                throw new InvalidOperationException("Fehlender [Ausbilder]-Header.");

            for (int i = startIndex + 1; i < lines.Length && !lines[i].StartsWith("["); i++)
            {
                string[] data = lines[i].Split(_delimiter);
                if (data.Length < 3)
                    continue;

                _ausbilder.Add(new Ausbilder(
                    int.Parse(data[0]),
                    data[1],
                    data[2]
                ));

            }
        }
        private void LoadAusbildungData()
        {
            string[] lines = File.ReadAllLines(_filePath);
            _ausbildungen.Clear();
            int startIndex = Array.FindIndex(lines, line => line.StartsWith("[Ausbildungen]"));
            if (startIndex == -1)
                throw new InvalidOperationException("Die Datei enthält keine Ausbildungen.");

            for (int i = startIndex + 1; i < lines.Length && !lines[i].StartsWith("["); i++)
            {
                string[] data = lines[i].Split(_delimiter);
                if (data.Length < 2)
                    continue;

                _ausbildungen.Add(new Ausbildung
                (
                    data[0],
                    data[1]
                ));
            }
        }
    }
}