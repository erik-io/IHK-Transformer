using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHK_Transform
{
    internal class Ausbildung
    {
        public Ausbildung()
        {
        }

        public Ausbildung(string ausbildungID, string ausbildung)
        {
            _ausbildung_id = ausbildungID;
            _ausbildung = ausbildung;
        }

        private string _ausbildung_id { get; set; }
        private string _ausbildung { get; set; }

        public string getAusbildungID()
        {
            return _ausbildung_id;
        }

        public void setAusbildungID(string ausbildungID)
        {
            _ausbildung_id = ausbildungID;
        }

        public string getAusbildung()
        {
            return _ausbildung;
        }

        public void setAusbildung(string berufsbezeichnung)
        {
            _ausbildung = berufsbezeichnung;
        }

        public override string ToString()
        {
            return _ausbildung_id;
        }
    }
}