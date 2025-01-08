using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        public void DisplayAzubis()
        {
            var azubis = _azubiService.GetAzubi();
            var ausbilder = _azubiService.GetAusbilder();
            var ausbildung = _azubiService.GetAusbildung();

            foreach (var azubi in azubis)
            {
                var ausbilderName = ausbilder.FirstOrDefault(a => a.getAusbilderID() == azubi.getAzubiID());
                var beruf = ausbildung.FirstOrDefault(b => b.getAusbildungID() == azubi.getAusbildungID());

                Debug.WriteLine($"Azubi: {azubi.getVorname()} {azubi.getNachname()} - Ausbilder: {ausbilderName} - Beruf: {beruf}");
            }
        }
    }
}