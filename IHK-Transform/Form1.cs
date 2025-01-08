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
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string connectionString = "Server=localhost;Database=ihk_transform;Uid=root;Pwd=;";

            var sqlHelper = new ReadWrite_SQL(connectionString);

            var azubis = sqlHelper.FetchAzubi();
            var ausbilder = sqlHelper.FetchAusbilder();
            var ausbildung = sqlHelper.FetchAusbildung();

            foreach (var azubi in azubis)
            {
                var ausbilderName = ausbilder.FirstOrDefault(a => a.getAusbilderID() == azubi.getAusbilderID());
                var beruf = ausbildung.FirstOrDefault(b => b.getAusbildungsID() == azubi.getAusbildungID());

                Debug.WriteLine($"Azubi: {azubi.getVorname()} {azubi.getNachname()} - Ausbilder: {ausbilderName} - Beruf: {beruf}");
            }

        }
    }
}
