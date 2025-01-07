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

        public Azubi(long azubiNr, string vorname, string nachname, string ausbildungsberuf, int ausbildungsbeginn)
        {
            _azubiNr = azubiNr;
            _vorname = vorname;
            _nachname = nachname;
            _ausbildungsberuf = ausbildungsberuf;
            _ausbildungsbeginn = ausbildungsbeginn;
        }

        public Azubi(string vorname, string nachname, string ausbildungsberuf, int ausbildungsbeginn)
        {
            _vorname = vorname;
            _nachname = nachname;
            _ausbildungsberuf = ausbildungsberuf;
            _ausbildungsbeginn = ausbildungsbeginn;
        }

        private long _azubiNr { get; set; }
        private string _vorname { get; set; }
        private string _nachname { get; set; }
        private string _ausbildungsberuf { get; set; }
        private int _ausbildungsbeginn { get; set; }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
