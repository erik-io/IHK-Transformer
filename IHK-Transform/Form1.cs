using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IHK_Transform.Services;
using IHK_Transform.Utilities;

namespace IHK_Transform
{
    public partial class Form1 : Form
    {
        private readonly AzubiController _azubiController;
        private XmlDataService _xmlDataService;
        private CsvDataService _csvDataService;
        private readonly FileHandler _fileHandler;

        public Form1()
        {
            _xmlDataService = new XmlDataService();
            _csvDataService = new CsvDataService();
            _fileHandler = new FileHandler(_xmlDataService, _csvDataService);

            var azubiService = new AzubiService();
            _azubiController = new AzubiController(azubiService);
            InitializeComponent();
            InitializeHandlers();
        }

        public void InitializeHandlers()
        {
            try
            {
                var iniReader = new IniReader("config.ini");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fehler beim Initialisieren: {ex.Message}", "Fehler", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnLoadSQL_Click(object sender, EventArgs e)
        {
            try
            {
                var iniReader = new IniReader("config.ini");
                var sqlDataService = new SqlDataService(iniReader);

                sqlDataService.LoadData();

                _azubiController.LoadDataFromSQL(sqlDataService);

                var data = _azubiController.DisplayAzubis();
                dgvAzubi.DataSource = data;

            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }

        private void btnLoadCSV_Click(object sender, EventArgs e)
        {
            _fileHandler.LoadData("csv");
            _azubiController.LoadDataFromCSV(_csvDataService);
            var data = _azubiController.DisplayAzubis();
            dgvAzubi.DataSource = data;
        }

        private void btnLoadXML_Click(object sender, EventArgs e)
        {
            _fileHandler.LoadData("xml");
            _azubiController.LoadDataFromXml(_xmlDataService);
            var data = _azubiController.DisplayAzubis();
            dgvAzubi.DataSource = data;
        }
    }
}
