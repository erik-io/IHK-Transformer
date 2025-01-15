using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace IHK_Transform
{
    internal class ReadWrite_Hierarchie : DataHandler
    {
        private List<Azubi> _azubis;
        private List<Ausbilder> _ausbilder;
        private List<Ausbildung> _ausbildungen;

        public ReadWrite_Hierarchie()
        {
            _azubis = new List<Azubi>();
            _ausbilder = new List<Ausbilder>();
            _ausbildungen = new List<Ausbildung>();
        }

        private void LoadAzubiData(string filePath)
        {
            XDocument doc = XDocument.Load(filePath);
            foreach (var a in doc.Descendants("row"))
            {
                int azubiID = int.Parse(a.Element("azubi_id").Value ?? "0");
                string vorname = a.Element("vorname").Value ?? "NULL";
                string nachname = a.Element("nachname").Value ?? "NULL";
                DateTime ausbildungsbeginn = DateTime.Parse(a.Element("ausbildungsbeginn").Value ?? "2000-01-01");
                string ausbildungID = a.Element("ausbildung_id").Value ?? "0";
                int ausbilderID = int.Parse(a.Element("ausbilder_id").Value ?? "0");

                _azubis.Add(new Azubi(azubiID, vorname, nachname, ausbildungsbeginn, ausbildungID, ausbilderID));
            }
        }

        private void LoadAusbilderData(string filePath)
        {
            XDocument doc = XDocument.Load(filePath);
            foreach (var a in doc.Descendants("row"))
            {
                int ausbilderID = int.Parse(a.Element("ausbilder_id").Value ?? "0");
                string vorname = a.Element("vorname").Value ?? "NULL";
                string nachname = a.Element("nachname").Value ?? "NULL";

                _ausbilder.Add(new Ausbilder(ausbilderID, vorname, nachname));
            }
        }

        private void LoadAusbildungData(string filePath)
        {
            XDocument doc = XDocument.Load(filePath);
            foreach (var a in doc.Descendants("row"))
            {
                string ausbildungID = a.Element("ausbildung_id").Value ?? "0";
                string bezeichnung = a.Element("bezeichnung").Value;

                _ausbildungen.Add(new Ausbildung(ausbildungID, bezeichnung));
            }
        }

        public override object ReadData()
        {
            string roothPath = AppDomain.CurrentDomain.BaseDirectory;

            string azubiFile = Path.Combine(roothPath, "Azubis.xml");
            string ausbilderFile = Path.Combine(roothPath, "Ausbilder.xml");
            string ausbildungFile = Path.Combine(roothPath, "Ausbildungen.xml");

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
                    openFileDialog.Filter = "XML-Dateien (*.xml)|*.xml|Alle Dateien (*.*)|*.*";
                    openFileDialog.FilterIndex = 1;
                    openFileDialog.Multiselect = true;
                    openFileDialog.Title = "XML-Dateien auswählen";

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string[] selectedFiles = openFileDialog.FileNames;

                        string selectedAzubiFile = selectedFiles.FirstOrDefault(f =>
                            Path.GetFileName(f).Equals("Azubis.xml", StringComparison.OrdinalIgnoreCase));

                        string selectedAusbilderFile = selectedFiles.FirstOrDefault(f =>
                            Path.GetFileName(f).Equals("Ausbilder.xml", StringComparison.OrdinalIgnoreCase));

                        string selectedAusbildungFile = selectedFiles.FirstOrDefault(f =>
                            Path.GetFileName(f).Equals("Ausbildungen.xml", StringComparison.OrdinalIgnoreCase));

                        if (!string.IsNullOrEmpty(selectedAzubiFile) && File.Exists(selectedAzubiFile))
                        {
                            LoadAzubiData(selectedAzubiFile);
                        }
                        else
                        {
                            MessageBox.Show("Die Datei 'Azubis.xml' wurde nicht gefunden oder hat den falschen Namen.",
                                "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        if (!string.IsNullOrEmpty(selectedAusbilderFile) && File.Exists(selectedAusbilderFile))
                        {
                            LoadAusbilderData(selectedAusbilderFile);
                        }
                        else
                        {
                            MessageBox.Show(
                                "Die Datei 'Ausbilder.xml' wurde nicht gefunden oder hat den falschen Namen.", "Fehler",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        if (!string.IsNullOrEmpty(selectedAusbildungFile) && File.Exists(selectedAusbildungFile))
                        {
                            LoadAusbildungData(selectedAusbildungFile);
                        }
                        else
                        {
                            MessageBox.Show(
                                "Die Datei 'Ausbildungen.xml' wurde nicht gefunden oder hat den falschen Namen.",
                                "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        public List<Azubi> GetAzubi() => _azubis;
        public List<Ausbilder> GetAusbilder() => _ausbilder;
        public List<Ausbildung> GetAusbildung() => _ausbildungen;
    }
}
