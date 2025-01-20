using System.Collections.Generic;
using IHK_Transform.Models;

namespace IHK_Transform.Services
{
    /// <summary>
    /// diese Klasse soll dynamisch auslesen welche Datenhaltung vorhanden ist und entsprechend reagieren
    /// Erweiterungen dieser Klasse sollen vorab nur als Spezialisierung in geerbten Klasse vorhanden sein
    /// Die Dynamisierung wird deswegen als virtual deklariert
    /// </summary>
    public abstract class DataHandler
    {
        internal List<Azubi> _azubis = new List<Azubi>();
        internal List<Ausbilder> _ausbilder = new List<Ausbilder>();
        internal List<Ausbildung> _ausbildungen = new List<Ausbildung>();

        public abstract void LoadData();
        internal virtual List<Azubi> GetAzubiData() => _azubis;
        internal virtual List<Ausbilder> GetAusbilderData() => _ausbilder;
        internal virtual List<Ausbildung> GetAusbildungData() => _ausbildungen;


        public virtual object ReadData()
        {
            //zuerst auslesen aller Datanhalterunegn, dann aufruf der GetData
            //diese gibt dann die Datanhaltung als object zurück
            object t = null;

            return t;
        }

        object GetData()
        {
            object m = null;

            return m;
        }
    }
}
