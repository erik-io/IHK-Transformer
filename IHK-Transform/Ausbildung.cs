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

        public Ausbildung(int ausbildungID, string berufsbezeichnung, string kurzbezeichnung)
        {
            _ausbildung_id = ausbildungID;
            _berufsbezeichnung = berufsbezeichnung;
            _kurzbezeichnung = kurzbezeichnung;
        }

        public Ausbildung(string berufsbezeichnung, string kurzbezeichnung)
        {
            _berufsbezeichnung = berufsbezeichnung;
            _kurzbezeichnung = kurzbezeichnung;
        }

        private int _ausbildung_id { get; set; }
        private string _berufsbezeichnung { get; set; }
        private string _kurzbezeichnung { get; set; }

        public int getAusbildungsID()
        {
            return _ausbildung_id;
        }

        public void setAusbildungsID(int ausbildungID)
        {
            _ausbildung_id = ausbildungID;
        }

        public string getBerufsbezeichnung()
        {
            return _berufsbezeichnung;
        }

        public void setBerufsbezeichnung(string berufsbezeichnung)
        {
            _berufsbezeichnung = berufsbezeichnung;
        }

        public string getKurzbezeichnung()
        {
            return _kurzbezeichnung;
        }

        public void setKurzbezeichnung(string kurzbezeichnung)
        {
            _kurzbezeichnung = kurzbezeichnung;
        }

        public override string ToString()
        {
            return _kurzbezeichnung;
        }
    }
}