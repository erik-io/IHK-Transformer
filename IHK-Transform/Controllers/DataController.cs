using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using IHK_Transform.Controllers.Interfaces;
using IHK_Transform.Models;
using IHK_Transform.Models.Entities;
using IHK_Transform.Services;
using IHK_Transform.Services.Implementations;
using IHK_Transform.Services.Interfaces;
using IHK_Transform.Views.Interfaces;

namespace IHK_Transform.Controllers
{
    internal class DataController : IDataController
    {
        private readonly IDataProvider _dataProvider;
        private List<Azubi> _azubis = new List<Azubi>();
        private List<Ausbilder> _ausbilder = new List<Ausbilder>();
        private List<Ausbildung> _ausbildungen = new List<Ausbildung>();

        // Ereignisse
        public event EventHandler DataLoaded;
        public event EventHandler<string> ErrorOccurred;

        // Konstruktor mit Dependency Injection
        public DataController(IDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }

        // Initialisierung der Datenquelle
        public void InitializeDataSources()
        {
            try
            {
                _dataProvider.Connect();
            }
            catch (Exception ex)
            {
                OnErrorOccurred($"Verbindungsfehler: {ex.Message}");
            }
        }

        // Methode für CSV-Laden (nutzt den gesetzten Source-Typ)
        public void LoadData(string sourceType)
        {
            try
            {
                // _dataProvider.SetSource(sourceType);
                _dataProvider.LoadData();

                // Daten aus dem Provider holen
                _azubis = _dataProvider.GetAzubiData();
                _ausbilder = _dataProvider.GetAusbilderData();
                _ausbildungen = _dataProvider.GetAusbildungData();

                DataLoaded?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                OnErrorOccurred($"Fehler beim Laden: {ex.Message}");
            }
        }

        // Transformiert Daten für die Anzeige
        public List<object> GetDisplayData()
        {
            Debug.WriteLine($"DataController - Provider Type: {_dataProvider.GetType().Name}");
            var azubis = _dataProvider.GetAzubiData();
            Debug.WriteLine($"Geladene Azubis: {azubis?.Count ?? 0}");
            return DisplayAzubis();
        }

        public List<object> DisplayAzubis()
        {
            Debug.WriteLine($"DisplayAzubis - Listengrößen vor Verarbeitung:");
            Debug.WriteLine($"Azubis: {_azubis?.Count ?? 0}");
            Debug.WriteLine($"Ausbilder: {_ausbilder?.Count ?? 0}");
            Debug.WriteLine($"Ausbildungen: {_ausbildungen?.Count ?? 0}");

            var data = new List<object>();

            foreach (var azubi in _azubis)
            {
                var ausbilder = _ausbilder.FirstOrDefault(a => a.AusbilderId == azubi.AusbilderId);
                var ausbildung = _ausbildungen.FirstOrDefault(b => b.AusbildungId == azubi.AusbildungId);

                data.Add(new
                {
                    AzubiID = azubi.AzubiId,
                    Vorname = azubi.Vorname,
                    Nachname = azubi.Nachname,
                    Ausbildung = $"{ausbildung?.AusbildungId}{azubi.Ausbildungsbeginn:yy}", // Kurzformat generieren
                    Ausbilder = $"{ausbilder?.Vorname} {ausbilder?.Nachname}" ?? "NULL"
                });
            }
            Debug.WriteLine($"Erstellte Datensätze: {data.Count}");
            return data;
        }

        private void OnErrorOccurred(string message)
        {
            ErrorOccurred?.Invoke(this, message);
        }
    }
}