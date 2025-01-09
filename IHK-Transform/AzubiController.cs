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
        // private readonly AzubiService _azubiService;

        private List<Azubi> _azubis;
        private List<Ausbilder> _ausbilder;
        private List<Ausbildung> _ausbildung;

        public AzubiController(AzubiService azubiService)
        {
            _azubis = new List<Azubi>();
            _ausbilder = new List<Ausbilder>();
            _ausbildung = new List<Ausbildung>();
        }

        // public List<Azubi> GetAzubi()
        // {
        //     return _azubiService.GetAzubi();
        // }

        public void LoadDataFromSQL(ReadWrite_SQL sqlHelper)
        {
            _azubis = sqlHelper.GetAzubi();
            _ausbilder = sqlHelper.GetAusbilder();
            _ausbildung = sqlHelper.GetAusbildung();
        }

        public void LoadDataFromCSV(ReadWrite_CSV csvHelper)
        {
            csvHelper.LoadAzubiData();
            csvHelper.LoadAusbilderData();
            csvHelper.LoadAusbildungData();

            _azubis = csvHelper.GetAzubi();
            _ausbilder = csvHelper.GeAusbilder();
            _ausbildung = csvHelper.GetAusbildung();
        }

        /*public void DisplayAzubis()
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
        }*/

        public List<object> DisplayAzubis()
        {

            var data = new List<object>();

            foreach (var azubi in _azubis)
            {
                var ausbilderName = _ausbilder.FirstOrDefault(a => a.getAusbilderID() == azubi.getAusbilderID());
                var beruf = _ausbildung.FirstOrDefault(b => b.getAusbildungID() == azubi.getAusbildungID());

                var ausbildungsberuf = beruf != null
                    ? $"{beruf.getKurzbezeichnung()}{azubi.getAusbildungsbeginn()}"
                    : "Unbekannt";

                var ausbilderFullName = ausbilderName != null
                    ? $"{ausbilderName.getVorname()} {ausbilderName.getNachname()}"
                    : "Unbekannt";

                data.Add(new
                {
                    AzubiID = azubi.getAzubiID(),
                    Vorname = azubi.getVorname(),
                    Nachname = azubi.getNachname(),
                    Ausbildungsberuf = ausbildungsberuf,
                    Ausbilder = ausbilderFullName
                });
            }

            return data;
        }
    }
}