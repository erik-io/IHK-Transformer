using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IHK_Transform
{
    internal class ReadWrite_CSV
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

        public void LoadAzubiData()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "CSV-Dateien (*.csv)|*.csv|Alle Dateien (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.Title = "Wählen Sie die Azubi CSV-Datei aus";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string[] lines = File.ReadAllLines(openFileDialog.FileName);

                    for (int i = 1; i < lines.Length; i++)
                    {
                        string[] data = lines[i].Split(';');
                        if (data.Length >= 6)
                        {
                            var azubi = new Azubi(
                                int.Parse(data[0]),     // AzubiID
                                data[1],                // Vorname
                                data[2],        // Nachname
                                int.Parse(data[4]), // Lehrjahr
                                int.Parse(data[3]),                   // AusbildungID (wird später zugeordnet)
                                int.Parse(data[5])             // AusbilderID
                            );
                            _azubis.Add(azubi);
                        }
                    }
                }
            }
        }

        public void LoadAusbilderData()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "CSV-Dateien (*.csv)|*.csv|Alle Dateien (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.Title = "Wählen Sie die Ausbilder CSV-Datei aus";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string[] lines = File.ReadAllLines(openFileDialog.FileName);

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
            }
        }

        public void LoadAusbildungData()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "CSV-Dateien (*.csv)|*.csv|Alle Dateien (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.Title = "Wählen Sie die Ausbildung CSV-Datei aus";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string[] lines = File.ReadAllLines(openFileDialog.FileName);

                    for (int i = 1; i < lines.Length; i++)
                    {
                        string[] data = lines[i].Split(';');
                        if (data.Length >= 3)
                        {
                            var ausbildung = new Ausbildung(
                                int.Parse(data[0]),     // AusbildungID
                                data[1],                // Berufsbezeichnung
                                data[2]                 // Kurzbezeichnung
                            );
                            _ausbildungen.Add(ausbildung);
                        }
                    }
                }
            }
        }

        public List<Azubi> GetAzubi()
        {
            return _azubis;
        }

        public List<Ausbilder> GeAusbilder()
        {
            return _ausbilder;
        }

        public List<Ausbildung> GetAusbildung()
        {
            return _ausbildungen;
        }
    }
}
