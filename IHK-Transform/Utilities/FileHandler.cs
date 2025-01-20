using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IHK_Transform.Services;

namespace IHK_Transform.Utilities
{
    internal class FileHandler
    {
        private readonly XmlDataService _xmlDataService;
        private readonly CsvDataService _csvDataService;

        public FileHandler(XmlDataService xmlDataService, CsvDataService csvDataService)
        {
            _xmlDataService = xmlDataService ?? throw new ArgumentNullException(nameof(xmlDataService));
            _csvDataService = csvDataService ?? throw new ArgumentNullException(nameof(csvDataService));
        }

        public void LoadData(string fileType)
        {
            switch (fileType.ToLower())
            {
                case "xml":
                    LoadXML();
                    break;
                case "csv":
                    LoadCSV();
                    break;
                default:
                    throw new ArgumentException("Nicht unterstütztes Dateiformat.");
            }
        }

        private void LoadXML()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "XML-Dateien (*.xml)|*.xml|Alle Dateien (*.*)|*.*";
                openFileDialog.Title = "XML-Datei auswählen";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        _xmlDataService.SetFilePath(openFileDialog.FileName);
                        _xmlDataService.LoadData();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Fehler beim Laden der XML-Datei: {ex.Message}", "Fehler",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void LoadCSV()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "CSV-Dateien (*.csv)|*.csv|Alle Dateien (*.*)|*.*";
                openFileDialog.Title = "CSV-Datei auswählen";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        _csvDataService.SetFilePath(openFileDialog.FileName);
                        _csvDataService.LoadData();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Fehler beim Laden der CSV-Datei: {ex.Message}", "Fehler",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
