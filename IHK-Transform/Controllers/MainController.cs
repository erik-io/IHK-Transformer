using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IHK_Transform.Controllers.Interfaces;
using IHK_Transform.Infrastructure.Configuration;
using IHK_Transform.Services;
using IHK_Transform.Services.Implementations;
using IHK_Transform.Services.Interfaces;
using IHK_Transform.Views.Interfaces;

namespace IHK_Transform.Controllers
{
    internal class MainController
    {
        private readonly IMainView _view;
        private IDataController _dataController;
        private readonly IDataProvider _dataProvider;

        public MainController(IMainView view, IDataController dataController, IDataProvider dataProvider)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _dataController = dataController ?? throw new ArgumentNullException(nameof(dataController));
            _dataProvider = dataProvider ?? throw new ArgumentNullException(nameof(dataProvider));

            _view.LoadCsvDataRequested += OnLoadCsvRequested;
            _view.LoadXmlDataRequested += OnLoadXmlRequested;
            _view.LoadSqlDataRequested += OnLoadSqlRequested;
        }

        private void OnLoadSqlRequested(object sender, EventArgs e)
        {
            try
            {
                // SQL-Provider erstellen
                var sqlProvider = DataProviderFactory.CreateProvider("sql", new IniReader("Config/config.ini"));

                // Provider konfigurieren und verbinden
                sqlProvider.Connect();

                // Neuen DataController mit SQL-Provider erstellen
                _dataController = new DataController(sqlProvider);
                _dataController.InitializeDataSources();
                _dataController.LoadData("sql");

                Debug.WriteLine("SQL-Daten werden geladen.");

                // Daten im Grid anzeigen
                _view.DisplayData(_dataController.GetDisplayData());
            }
            catch (Exception exception)
            {
                Debug.WriteLine($"Fehler beim SQL-Laden: {exception.Message}");
                _view.ShowMessage($"Fehler beim SQL-Laden: {exception.Message}");
            }
        }

        private void OnLoadXmlRequested(object sender, EventArgs e)
        {
        try
        {
            string filePath = ShowFileDialog("XML-Dateien (*.xml)|*.xml", "XML-Datei auswählen");
            if (string.IsNullOrEmpty(filePath)) return;

            // Provider erstellen und konfigurieren
            var xmlProvider = DataProviderFactory.CreateProvider("xml");
            xmlProvider.SetFilePath(filePath);
            xmlProvider.Connect();

            // Neuen DataController mit XML-Provider erstellen
            _dataController = new DataController(xmlProvider);
            _dataController.InitializeDataSources();
            _dataController.LoadData("xml");
            
            Debug.WriteLine($"XML-Datei wird geladen: {filePath}");

            // DataController aktualisieren
            _view.DisplayData(_dataController.GetDisplayData());
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Fehler beim XML-Laden: {ex.Message}");
            _view.ShowMessage($"Fehler beim XML-Laden: {ex.Message}");
        }
        }

        private void OnLoadCsvRequested(object sender, EventArgs e)
        {
            try
            {
                string filePath = ShowFileDialog("CSV-Dateien (*.csv)|*.csv", "CSV-Datei auswählen");
                if (string.IsNullOrEmpty(filePath)) return;

                // CSV-Provider aus dem bestehenden Provider casten
                var csvProvider = DataProviderFactory.CreateProvider("csv");
                if (!(csvProvider is CsvDataService))
                {
                    _view.ShowMessage("Fehler beim Erstellen des CSV-Providers.");
                    return;
                }

                Debug.WriteLine($"CSV-Datei wird geladen: {filePath}");

                // Provider konfigurieren und verbinden
                csvProvider.SetFilePath(filePath);
                csvProvider.Connect();

                // Neuen DataController mit CSV-Provider erstellen
                _dataController = new DataController(csvProvider);
                _dataController.InitializeDataSources();
                _dataController.LoadData("csv");

                _view.DisplayData(_dataController.GetDisplayData());
            }
            catch (Exception exception)
            {
                Debug.WriteLine($"Fehler beim CSV-Laden: {exception.Message}");
                _view.ShowMessage($"Fehler beim CSV-Laden: {exception.Message}");
            }
        }

        private string ShowFileDialog(string filter, string title)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = filter;
                openFileDialog.Title = title;
                openFileDialog.InitialDirectory = Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory, "Data"); // Startpfad für Dateiauswahl TODO: Config
                
                return openFileDialog.ShowDialog() == DialogResult.OK
                    ? openFileDialog.FileName
                    : null;
            }
        }
    }
}
