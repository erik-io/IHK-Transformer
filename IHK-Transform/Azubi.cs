using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHK_Transform
{
    internal class Azubi
    {
        public Azubi()
        {
        }

        public Azubi(int azubiID, string vorname, string nachname, string ausbildungsberuf, int ausbildungsbeginn, int ausbildungID, int ausbilderID)
        {
            _azubi_id = azubiID;
            _vorname = vorname;
            _nachname = nachname;
            _ausbildungsberuf = ausbildungsberuf;
            _ausbildungsbeginn = ausbildungsbeginn;
            _ausbildung_id = ausbildungID;
            _ausbilder_id = ausbilderID;
        }

        public Azubi(string vorname, string nachname, string ausbildungsberuf, int ausbildungsbeginn, int ausbildungID, int ausbilderID)
        {
            _vorname = vorname;
            _nachname = nachname;
            _ausbildungsberuf = ausbildungsberuf;
            _ausbildungsbeginn = ausbildungsbeginn;
            _ausbildung_id = ausbildungID;
            _ausbilder_id = ausbilderID;
        }

        private int _azubi_id { get; set; }
        private string _vorname { get; set; }
        private string _nachname { get; set; }
        private string _ausbildungsberuf { get; set; }
        private int _ausbildungsbeginn { get; set; }
        private int _ausbildung_id { get; set; }
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

        public string getAusbildungsberuf()
        {
            return _ausbildungsberuf;
        }

        public void setAusbildungsberuf(string ausbildungsberuf)
        {
            _ausbildungsberuf = ausbildungsberuf;
        }

        public int getAusbildungsbeginn()
        {
            return _ausbildungsbeginn;
        }

        public void setAusbildungsbeginn(int ausbildungsbeginn)
        {
            _ausbildungsbeginn = ausbildungsbeginn;
        }

        public int getAusbildungID()
        {
            return _ausbildung_id;
        }

        public void setAusbildungID(int ausbildungID)
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
            return base.ToString();
        }
    }
}