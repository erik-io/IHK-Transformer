using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using IHK_Transform.Models.Entities;
using IHK_Transform.Services.Interfaces;

namespace IHK_Transform.Services.Implementations
{
    public class CsvDataService : IDataProvider
    {
        // Felder für Datenhaltung
        private readonly List<Azubi> _azubis = new List<Azubi>();
        private readonly List<Ausbilder> _ausbilder = new List<Ausbilder>();
        private readonly List<Ausbildung> _ausbildungen = new List<Ausbildung>();

        private string _filePath;
        private char _delimiter = ';';

        // Interface-Implementierungen
        public bool IsConnected { get; private set; }
        public string LastError { get; } = string.Empty;

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

        public void LoadData()
        {
            // Prüfen ob Dateipfad gesetzt ist
            if (string.IsNullOrWhiteSpace(_filePath))
            {
                throw new InvalidOperationException("Es wurde kein Datenpfad für die CSV-Datei gesetzt.");
            }

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
    }
}