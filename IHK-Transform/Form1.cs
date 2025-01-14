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

namespace IHK_Transform
{
    public partial class Form1 : Form
    {
        private readonly AzubiController _azubiController;
        private readonly string connectionString = "Server=localhost;Database=ihk_transformer;Uid=root;Pwd=;";

        public Form1()
        {
            InitializeComponent();
            var sqlHelper = new ReadWrite_SQL();
            var azubiService = new AzubiService(sqlHelper);
            _azubiController = new AzubiController(azubiService);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void btnLoadSQL_Click(object sender, EventArgs e)
        {
            var sqlHelper = new ReadWrite_SQL(connectionString);

            _azubiController.LoadDataFromSQL(sqlHelper);

            var data = _azubiController.DisplayAzubis();
            dgvAzubi.DataSource = data;
        }

        private void btnLoadCSV_Click(object sender, EventArgs e)
        {
            var csvHelper = new ReadWrite_CSV();

            _azubiController.LoadDataFromCSV(csvHelper);
            var data = _azubiController.DisplayAzubis();
            dgvAzubi.DataSource = data;
        }

        private void btnHierarchie_Click(object sender, EventArgs e)
        {
            var hierachicalHelper = new ReadWrite_Hierarchie();
            _azubiController.LoadDataFromHierarchical(hierachicalHelper);
            var data = _azubiController.DisplayAzubis();
            dgvAzubi.DataSource = data;
        }
    }
}
