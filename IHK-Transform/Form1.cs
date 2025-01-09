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
            
        }

        private void btnLoadSQL_Click(object sender, EventArgs e)
        {
            string connectionString = "Server=localhost;Database=ihk_transform;Uid=root;Pwd=;";
            var sqlHelper = new ReadWrite_SQL(connectionString);

            // Daten aus der Datenbank abrufen
            var azubis = sqlHelper.FetchAzubi();
            var ausbilder = sqlHelper.FetchAusbilder();
            var ausbildung = sqlHelper.FetchAusbildung();

            foreach (var azubi in azubis)
            {
                var ausbilderName = ausbilder.FirstOrDefault(a => a.getAusbilderID() == azubi.getAusbilderID());
                var beruf = ausbildung.FirstOrDefault(b => b.getAusbildungID() == azubi.getAusbildungID());

                Debug.WriteLine($"Azubi: {azubi.getVorname()} {azubi.getNachname()} - Ausbilder: {ausbilderName} - Beruf: {beruf}");
            }

            // Daten für das DataGridView aufbereiten
            var data = new List<object>();
            foreach (var azubi in azubis)
            {
                var ausbilderName = ausbilder.FirstOrDefault(a => a.getAusbilderID() == azubi.getAusbilderID());
                var beruf = ausbildung.FirstOrDefault(b => b.getAusbildungID() == azubi.getAusbildungID());

                var ausbildungsberuf = $"{beruf?.getKurzbezeichnung()+azubi.getAusbildungsbeginn()}";
                var ausbilderFullName = ausbilderName != null
                    ? $"{ausbilderName.getVorname()} {ausbilderName.getNachname()}"
                   : "Unbekannt";

                data.Add(new
                {
                    AzubiID = azubi.getAzubiID(),
                    Vorname = azubi.getVorname(),
                    Nachname = azubi.getNachname(),
                    Ausbildungsberuf = ausbildungsberuf,
                    Ausbilder = ausbilderFullName
                });

            }
            dgvAzubi.DataSource = data;
        }

        private void btnLoadCSV_Click(object sender, EventArgs e)
        {
            var csvHelper = new ReadWrite_CSV();

            csvHelper.LoadAzubiData();
            csvHelper.LoadAusbilderData();
            csvHelper.LoadAusbildungData();

            var azubis = csvHelper.FetchAzubi();
            var ausbilder = csvHelper.FetchAusbilder();
            var ausbildung = csvHelper.FetchAusbildung();

            var data = new List<object>();
            foreach (var azubi in azubis)
            {
                var ausbilderName = ausbilder.FirstOrDefault(a => a.getAusbilderID() == azubi.getAusbilderID());
                var beruf = ausbildung.FirstOrDefault(b => b.getAusbildungID() == azubi.getAusbildungID());

                var ausbildungsberuf = beruf != null
                    ? $"{beruf.getKurzbezeichnung()}{azubi.getAusbildungsbeginn()}"
                    : "Unbekannt";

                var ausbilderFullName = ausbilderName != null
                    ? $"{ausbilderName.getVorname()} {ausbilderName.getNachname()}"
                    : "Unbekannt";

                data.Add(new
                {
                    AzubiID = azubi.getAzubiID(),
                    Vorname = azubi.getVorname(),
                    Nachname = azubi.getNachname(),
                    Ausbildungsberuf = ausbildungsberuf,
                    Ausbilder = ausbilderFullName
                });
            }

            dgvAzubi.DataSource = data;
        }
    }
}
