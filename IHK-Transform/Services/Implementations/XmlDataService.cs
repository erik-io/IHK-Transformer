using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Management.Instrumentation;
using System.Xml.Linq;
using IHK_Transform.Models.Entities;
using IHK_Transform.Services.Interfaces;

namespace IHK_Transform.Services.Implementations
{
    public class XmlDataService : IDataProvider
    {
        // Datenfelder für die Datenhaltung
        private readonly List<Azubi> _azubis = new List<Azubi>();
        private readonly List<Ausbilder> _ausbilder = new List<Ausbilder>();
        private readonly List<Ausbildung> _ausbildungen = new List<Ausbildung>(); 
        private string _filePath;
        private string _lastError;
        private bool _isConnected;

        // // Interface-Implementierungen
        // public bool IsConnected { get; private set; }
        // public string LastError { get; private set; } = string.Empty;

        // --- IDataProvider-Implementierung ---
        // public void Connect() => IsConnected = true; // XML-Datei benötigt keine Verbindung
        // public void Disconnect() => IsConnected = false;

        public bool IsConnected => _isConnected;
        public string LastError => _lastError;

        public void Connect()
        {
            if (string.IsNullOrWhiteSpace(_filePath))
            {
                _lastError = "Kein Dateipfad angegeben.";
                _isConnected = false;
                return;
            }

            try
            {
                // Prüfen ob die XML-Datei existiert und lesbar ist
                if (System.IO.File.Exists(_filePath))
                {
                    // Testweise die XML-Datei laden um Syntax zu prüfen
                    XDocument.Load(_filePath);
                    _isConnected = true;
                    Debug.WriteLine($"XML-Verbindung hergestellt: {_filePath}");
                }
                else
                {
                    _lastError = "Die Datei wurde nicht gefunden oder ist nicht lesbar.";
                    _isConnected = false;
                }
            }
            catch (Exception ex)
            {
                _lastError = $"XML-Verbindungsfehler: {ex.Message}";
                _isConnected = false;
                throw;
            }
        }

        public void Disconnect()
        {
            _isConnected = false;
        }

        // Datenquellensteuerung
        public void SetFilePath(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentNullException(nameof(filePath));

            _filePath = filePath;
            Debug.WriteLine($"XML-Dateipfad gesetzt: {filePath}");
        }

        public void LoadData()
        {
            if (!_isConnected)
            {
                throw new InvalidOperationException("Keine Verbindung zur XML-Datei hergestellt.");
            }

            if (string.IsNullOrWhiteSpace(_filePath))
            {
                throw new InvalidOperationException("Kein XML-Dateipfad angegeben.");
            }

            try
            {
                LoadAzubiData();
                LoadAusbilderData();
                LoadAusbildungData();
                Debug.WriteLine($"XML-Daten geladen - Azubis: {_azubis.Count}, Ausbilder: {_ausbilder.Count}, Ausbildungen: {_ausbildungen.Count}");
            }
            catch (Exception ex)
            {
                _lastError = $"Fehler beim Laden der XML-Daten: {ex.Message}";
                Debug.WriteLine(_lastError);
                throw;
            }
        }

        private void LoadAzubiData()
        {
            XDocument doc = XDocument.Load(_filePath);
            foreach (var a in doc.Descendants("Azubis").Elements("Eintrag"))
            {
                try
                {
                    int azubiID = int.Parse(a.Element("azubi_id")?.Value ?? "0");
                    string vorname = a.Element("vorname")?.Value ?? string.Empty;
                    string nachname = a.Element("nachname")?.Value ?? string.Empty;
                    DateTime ausbildungsbeginn = DateTime.Parse(a.Element("ausbildungsbeginn")?.Value ?? DateTime.Now.ToString());
                    string ausbildungID = a.Element("ausbildung_id")?.Value ?? string.Empty;
                    int ausbilderID = int.Parse(a.Element("ausbilder_id")?.Value ?? "0");

                    _azubis.Add(new Azubi(azubiID, vorname, nachname, ausbildungsbeginn, ausbildungID, ausbilderID));
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Fehler beim Verarbeiten eines Azubi-Eintrags: {ex.Message}");
                }
            }
        }

        private void LoadAusbilderData()
        {
            XDocument doc = XDocument.Load(_filePath);
            foreach (var a in doc.Descendants("Ausbilder").Elements("Eintrag"))
            {
                try
                {
                    int ausbilderID = int.Parse(a.Element("ausbilder_id")?.Value ?? "0");
                    string vorname = a.Element("vorname")?.Value ?? string.Empty;
                    string nachname = a.Element("nachname")?.Value ?? string.Empty;

                    _ausbilder.Add(new Ausbilder(ausbilderID, vorname, nachname));
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Fehler beim Verarbeiten eines Ausbilder-Eintrags: {ex.Message}");
                }
            }
        }

        private void LoadAusbildungData()
        {
            XDocument doc = XDocument.Load(_filePath);
            foreach (var a in doc.Descendants("Ausbildungen").Elements("Eintrag"))
            {
                try
                {
                    string ausbildungID = a.Element("ausbildung_id")?.Value ?? string.Empty;
                    string bezeichnung = a.Element("bezeichnung")?.Value ?? string.Empty;

                    _ausbildungen.Add(new Ausbildung(ausbildungID, bezeichnung));
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Fehler beim Verarbeiten eines Ausbildungs-Eintrags: {ex.Message}");
                }
            }
        }

        // Datenabrufmethoden
        public List<Azubi> GetAzubiData() => _azubis;
        public List<Ausbilder> GetAusbilderData() => _ausbilder;
        public List<Ausbildung> GetAusbildungData() => _ausbildungen;

        // Speichern der Daten (nicht implementiert)
        public void SaveData()
        {
            throw new NotImplementedException("XML-Speicherung nicht implementiert.");
        }
    }
}
