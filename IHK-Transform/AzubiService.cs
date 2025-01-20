using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IHK_Transform.Models;

namespace IHK_Transform
{
    internal class AzubiService
    {
        public AzubiService(ReadWrite_SQL sqlHelper)
        {
            
        }

        public List<Azubi> GetAzubi()
        {
            return new List<Azubi>
            {
                 
            };
        }

        public List<Ausbilder> GetAusbilder()
        {
            return new List<Ausbilder>
            {
                
            };
        }

        public List<Ausbildung> GetAusbildung()
        {
            return new List<Ausbildung>
            {
                
            };
        }
    }
}