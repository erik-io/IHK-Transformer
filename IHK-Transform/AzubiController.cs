using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHK_Transform
{
    internal class AzubiController
    {
        private readonly AzubiService _azubiService;

        public AzubiController(AzubiService azubiService)
        {
            _azubiService = azubiService;
        }

        public List<Azubi> GetAzubi()
        {
            return _azubiService.GetAzubi();
        }
    }
}
