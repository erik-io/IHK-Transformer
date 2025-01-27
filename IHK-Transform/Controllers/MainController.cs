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
using IHK_Transform.Services.Implementations;
using IHK_Transform.Services.Interfaces;
using IHK_Transform.Views.Interfaces;

namespace IHK_Transform.Controllers
{
    internal class MainController
    {
        private readonly IMainView _view;
        private readonly IDataController _dataController;
        private readonly IDataProvider _dataProvider;

        public MainController(IMainView view, IDataController dataController, IDataProvider dataProvider)
        {
            _view = view;
            _dataController = dataController;
            _dataProvider = dataProvider;

            _view.LoadCsvDataRequested += OnLoadCsvRequested;
        }

        private void OnLoadCsvRequested(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Filter = "CSV-Dateien (*.csv)|*.csv";
                    openFileDialog.Title = "CSV-Datei auswählen";

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string filePath = openFileDialog.FileName;

                        var csvProvider = _dataProvider as CsvDataService;
                        // Pfad setzen und Daten laden
                        csvProvider.SetFilePath(filePath);
                        csvProvider.LoadData();

                        _dataController.LoadData("csv");

                        Debug.WriteLine($"Daten geladen: {csvProvider.GetAzubiData().Count} Azubis");
                        _view.DisplayData(_dataController.GetDisplayData());
                    }
                }
            }
            catch (Exception exception)
            {
                _view.ShowMessage($"Fehler beim CSV-Laden: {exception.Message}");
            }
        }
    }
}
