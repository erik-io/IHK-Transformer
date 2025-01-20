using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IHK_Transform.Models;
using IHK_Transform.Services;

namespace IHK_Transform
{
    internal class AzubiController
    {
        private List<Azubi> _azubis;
        private List<Ausbilder> _ausbilder;
        private List<Ausbildung> _ausbildung;

        public AzubiController(AzubiService azubiService)
        {
            _azubis = new List<Azubi>();
            _ausbilder = new List<Ausbilder>();
            _ausbildung = new List<Ausbildung>();
        }

        public void LoadDataFromSQL(ReadWrite_SQL sqlHelper)
        {
            _azubis = sqlHelper.GetAzubi();
            _ausbilder = sqlHelper.GetAusbilder();
            _ausbildung = sqlHelper.GetAusbildung();
        }

        public void LoadDataFromCSV(CsvDataService csvDataService)
        {
            _azubis = csvDataService.GetAzubi();
            _ausbilder = csvDataService.GetAusbilder();
            _ausbildung = csvDataService.GetAusbildung();
        }

        public void LoadDataFromXml(XmlDataService xmlDataService)
        {
            _azubis = xmlDataService.GetAzubi();
            _ausbilder = xmlDataService.GetAusbilder();
            _ausbildung = xmlDataService.GetAusbildung();
        }

        public List<object> DisplayAzubis()
        {

            var data = new List<object>();

            foreach (var azubi in _azubis)
            {
                var ausbilderName = _ausbilder.FirstOrDefault(a => a.getAusbilderID() == azubi.getAusbilderID());
                var beruf = _ausbildung.FirstOrDefault(b => b.getAusbildungID() == azubi.getAusbildungID());

                var ausbildungsberuf = beruf != null
                    ? $"{beruf.getAusbildungID()}{azubi.getAusbildungsbeginn().ToString("yy")}"
                    : "NULL";

                var ausbilderFullName = ausbilderName != null
                    ? $"{ausbilderName.getVorname()} {ausbilderName.getNachname()}"
                    : "NULL";

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