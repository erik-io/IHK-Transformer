using System;

namespace IHK_Transform.Models
{
    internal class Azubi
    {
        public Azubi()
        {
        }

        public Azubi(int azubiID, string vorname, string nachname, string ausbildung, DateTime ausbildungsbeginn, string ausbildungID, int ausbilderID)
        {
            _azubi_id = azubiID;
            _vorname = vorname;
            _nachname = nachname;
            _ausbildung = ausbildung;
            _ausbildungsbeginn = ausbildungsbeginn;
            _ausbildung_id = ausbildungID;
            _ausbilder_id = ausbilderID;
        }

        public Azubi(int azubiID, string vorname, string nachname, DateTime ausbildungsbeginn, string ausbildungID, int ausbilderID)
        {
            _azubi_id = azubiID;
            _vorname = vorname;
            _nachname = nachname;
            _ausbildungsbeginn = ausbildungsbeginn;
            _ausbildung_id = ausbildungID;
            _ausbilder_id = ausbilderID;
        }

        private int _azubi_id { get; set; }
        private string _vorname { get; set; }
        private string _nachname { get; set; }
        private string _ausbildung { get; set; }
        private DateTime _ausbildungsbeginn { get; set; }
        private string _ausbildung_id { get; set; }
        private int _ausbilder_id { get; set; }

        public int getAzubiID()
        {
            return _azubi_id;
        }

        public void setAzubiID(int azubiID)
        {
            _azubi_id = azubiID;
        }

        public string getVorname()
        {
            return _vorname;
        }

        public void setVorname(string vorname)
        {
            _vorname = vorname;
        }

        public string getNachname()
        {
            return _nachname;
        }

        public void setNachname(string nachname)
        {
            _nachname = nachname;
        }

        public string getAusbildung()
        {
            return _ausbildung;
        }

        public void setAusbildung(string ausbildung)
        {
            _ausbildung = ausbildung;
        }

        public DateTime getAusbildungsbeginn()
        {
            return _ausbildungsbeginn;
        }

        public void setAusbildungsbeginn(DateTime ausbildungsbeginn)
        {
            _ausbildungsbeginn = ausbildungsbeginn;
        }

        public string getAusbildungID()
        {
            return _ausbildung_id;
        }

        public void setAusbildungID(string ausbildungID)
        {
            _ausbildung_id = ausbildungID;
        }

        public int getAusbilderID()
        {
            return _ausbilder_id;
        }

        public void setAusbilderID(int ausbilderID)
        {
            _ausbilder_id = ausbilderID;
        }

        public override string ToString()
        {
            return $"{_vorname} {_nachname}";
        }
    }
}