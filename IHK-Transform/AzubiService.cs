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
                new Azubi(
                    1L,
                    "Simon", 
                    "Schubert", 
                    "FIAN", 
                    23)
            };
        }
    }
}
