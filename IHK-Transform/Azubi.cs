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

        public void setAusbildungID(int ausbildungID)
        {
            _ausbildung_id = ausbildungID;
        }

        public int getAusbildungID()
        {
            return _ausbildung_id;
        }

        public int getAzubiID()
        {
            return _azubi_id;
        }

        public void setAzubiID(int azubiID)
        {
            _azubi_id = azubiID;
        }

        private int _azubi_id { get; set; }
        public string _vorname { get; set; }
        public string _nachname { get; set; }
        private string _ausbildungsberuf { get; set; }
        private int _ausbildungsbeginn { get; set; }
        private int _ausbildung_id { get; set; }
        private int _ausbilder_id { get; set; }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}