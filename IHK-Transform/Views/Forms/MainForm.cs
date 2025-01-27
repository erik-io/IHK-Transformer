using System;
using System.Collections.Generic;
using System.Windows.Forms;
using IHK_Transform.Controllers.Interfaces;
using IHK_Transform.Infrastructure.Configuration;
using IHK_Transform.Views.Interfaces;

namespace IHK_Transform.Views.Forms
{
    public partial class MainForm : Form, IMainView
    {
        // Implementierung von IMainView
        public DataGridView AzubiGrid => dgvAzubi;
        public event EventHandler LoadSqlDataRequested;
        public event EventHandler LoadCsvDataRequested;
        public event EventHandler LoadXmlDataRequested;
        
        private readonly IDataController _dataController;

        // private XmlDataService _xmlDataService;
        // private CsvDataService _csvDataService;
        // private readonly FileHandler _fileHandler;

        public MainForm(IDataController dataController)
        {
            _dataController = dataController;
            InitializeComponent();

            WireEvents();
        }

        private void WireEvents()
        {
           btnLoadSQL.Click += (s, e) => LoadSqlDataRequested.Invoke(this, EventArgs.Empty);
           btnLoadCSV.Click += (s, e) => LoadCsvDataRequested.Invoke(this, EventArgs.Empty);
           btnLoadXML.Click += (s, e) => LoadXmlDataRequested.Invoke(this, EventArgs.Empty);

           _dataController.DataLoaded += (s, e) => ShowMessage("Daten erfolgreich geladen!", false);
           _dataController.ErrorOccurred += (s, msg) => ShowMessage(msg);
        }

        public void InitializeHandlers()
        {
            try
            {
                var iniReader = new IniReader("Config/config.ini");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fehler beim Initialisieren: {ex.Message}", "Fehler", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void btnLoadSQL_Click(object sender, EventArgs e)
        {
            LoadSqlDataRequested?.Invoke(this, EventArgs.Empty);
        }

        private void btnLoadCSV_Click(object sender, EventArgs e)
        {
            LoadCsvDataRequested?.Invoke(this, EventArgs.Empty);
        }

        private void btnLoadXML_Click(object sender, EventArgs e)
        {
            LoadXmlDataRequested?.Invoke(this, EventArgs.Empty);
        }

        // UI-Update Methoden
        public void DisplayData(List<object> data)
        {
            dgvAzubi.DataSource = data; // DataGrid aktualisieren
        }

        public void ShowMessage(string message, bool isError = true)
        {
            MessageBox.Show(
                message,
                isError ? "Fehler" : "Info",
                MessageBoxButtons.OK,
                isError ? MessageBoxIcon.Error : MessageBoxIcon.Information);
        }

        public void ShowLoadingState(bool isLoading)
        {
            btnLoadCSV.Enabled = !isLoading;
            btnLoadSQL.Enabled = !isLoading;
            btnLoadXML.Enabled = !isLoading;
        }
    }
}
