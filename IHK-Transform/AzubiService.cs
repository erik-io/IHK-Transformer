using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHK_Transform
{
    internal class AzubiService
    {
        public List<Azubi> GetAzubi()
        {
            return new List<Azubi>
            {
                new Azubi(1, "Simon", "Schuber", "FI", 23 , 1, 1),
                new Azubi (2, "Max", "Schuber", "K", 23, 2, 2),
                new Azubi (3, "Peter", "Schuber", "IM", 23, 3, 3)
            };
        }

        public List<Ausbilder> GetAusbilder()
        {
            return new List<Ausbilder>
            {
                new Ausbilder (1, "Peter", "Schmidt"),
                new Ausbilder (2, "Hans", "Müller"),
                new Ausbilder (3, "Klaus", "Schulz")
            };
        }

        public List<Ausbildung> GetAusbildung()
        {
            return new List<Ausbildung>
            {
                new Ausbildung (1, "Fachinformatiker", "FI"),
                new Ausbildung (2, "Kaufmann", "K"),
                new Ausbildung (3, "Industriemechaniker", "IM" )
            };
        }
    }
}