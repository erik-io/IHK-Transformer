using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IHK_Transform
{
    internal class ReadWrite_CSV : DataHandler
    {
        private List<Azubi> _azubis;
        private List<Ausbilder> _ausbilder;
        private List<Ausbildung> _ausbildungen;

        public ReadWrite_CSV()
        {
            _azubis = new List<Azubi>();
            _ausbilder = new List<Ausbilder>();
            _ausbildungen = new List<Ausbildung>();
        }

        public void LoadAzubiData(string filePath)
        {
            string[] lines = File.ReadAllLines(filePath);

            for (int i = 1; i < lines.Length; i++)
            {
                string[] data = lines[i].Split(';');
                if (data.Length >= 6)
                {
                    var azubi = new Azubi(
                        int.Parse(data[0]), // AzubiID
                        data[1], // Vorname
                        data[2], // Nachname
                        DateTime.Parse(data[3]), // Ausbildungsbeginn (als Datum)
                        data[4], // AusbildungID 
                        int.Parse(data[5]) // AusbilderID
                    );
                    _azubis.Add(azubi);
                }
            }
        }

        public void LoadAusbilderData(string filePath)
        {
            string[] lines = File.ReadAllLines(filePath);

            for (int i = 1; i < lines.Length; i++)
            {
                string[] data = lines[i].Split(';');
                if (data.Length >= 2)
                {
                    var ausbilder = new Ausbilder(
                        int.Parse(data[0]),     // AusbilderID
                        data[1],                // Vorname
                        data[2]                         // Nachname
                    );
                    _ausbilder.Add(ausbilder);
                }
            }
                
            
        }

        public void LoadAusbildungData(string filePath)
        {
            string[] lines = File.ReadAllLines(filePath);

            for (int i = 1; i < lines.Length; i++)
            {
                string[] data = lines[i].Split(';');
                if (data.Length >= 2)
                {
                    var ausbildung = new Ausbildung(
                        data[0],     // AusbildungID
                        data[1]                // Berufsbezeichnung
                    );
                    _ausbildungen.Add(ausbildung);
                }
            }
        }

        public List<Azubi> GetAzubi() => _azubis;
        public List<Ausbilder> GeAusbilder() => _ausbilder;
        public List<Ausbildung> GetAusbildung() => _ausbildungen;

        public override object ReadData()
        {
            string rootPath = AppDomain.CurrentDomain.BaseDirectory;

            string azubiFile = Path.Combine(rootPath, "Azubis.csv");
            string ausbilderFile = Path.Combine(rootPath, "Ausbilder.csv");
            string ausbildungFile = Path.Combine(rootPath, "Ausbildungen.csv");

            if (File.Exists(azubiFile) && File.Exists(ausbilderFile) && File.Exists(ausbildungFile))
            {
                LoadAzubiData(azubiFile);
                LoadAusbilderData(ausbilderFile);
                LoadAusbildungData(ausbildungFile);
            }
            else
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Filter = "CSV-Dateien (*.csv)|*.csv|Alle Dateien (*.*)|*.*";
                    openFileDialog.FilterIndex = 1;
                    openFileDialog.Multiselect = true;
                    openFileDialog.Title = "CSV-Dateien auswählen";
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string[] selectedFiles = openFileDialog.FileNames;

                        string selectedAzubiFile = selectedFiles.FirstOrDefault(f =>
                           Path.GetFileName(f).Equals("Azubis.csv", StringComparison.OrdinalIgnoreCase));
                        string selectedAusbilderFile = selectedFiles.FirstOrDefault(f =>
                            Path.GetFileName(f).Equals("Ausbilder.csv", StringComparison.OrdinalIgnoreCase));
                        string selectedAusbildungFile = selectedFiles.FirstOrDefault(f =>
                            Path.GetFileName(f).Equals("Ausbildungen.csv", StringComparison.OrdinalIgnoreCase));

                        if (azubiFile != null && File.Exists(selectedAzubiFile))
                        {
                            LoadAzubiData(selectedAzubiFile);
                        }
                        else
                        {
                            MessageBox.Show("Die Datei 'Azubis.csv' wurde nicht gefunden oder hat den falschen Namen.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        if (ausbilderFile != null && File.Exists(selectedAusbilderFile)) {
                            LoadAusbilderData(selectedAusbilderFile);
                        }
                        else
                        {
                            MessageBox.Show("Die Datei 'Ausbilder.csv' wurde nicht gefunden oder hat den falschen Namen.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        if (ausbildungFile != null && File.Exists(selectedAusbildungFile))
                        {
                            LoadAusbildungData(selectedAusbildungFile);
                        }
                        else
                        {
                            MessageBox.Show("Die Datei 'Ausbildungen.csv' wurde nicht gefunden oder hat den falschen Namen.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }

            return new
            {
                Azubis = _azubis,
                Ausbilder = _ausbilder,
                Ausbildungen = _ausbildungen
            };
        }
    }
}
