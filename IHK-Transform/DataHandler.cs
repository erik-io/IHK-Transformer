using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHK_Transform
{
    /// <summary>
    /// diese Klasse soll dynamisch auslesen welche Datenhaltung vorhanden ist und entsprechend reagieren
    /// Erweiterungen dieser Klasse sollen vorab nur als Spezialisierung in geerbten Klasse vorhanden sein
    /// Die Dynamisierung wird deswegen als virtual deklariert
    /// </summary>
    abstract class DataHandler
    {
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
