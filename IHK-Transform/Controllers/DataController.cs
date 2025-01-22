using System.Collections.Generic;
using System.Linq;
using IHK_Transform.Models;
using IHK_Transform.Models.Entities;
using IHK_Transform.Services;

namespace IHK_Transform.Controllers
{
    internal class DataController
    {
        private List<Azubi> _azubis;
        private List<Ausbilder> _ausbilder;
        private List<Ausbildung> _ausbildung;

        public DataController(DataService dataService)
        {
            _azubis = new List<Azubi>();
            _ausbilder = new List<Ausbilder>();
            _ausbildung = new List<Ausbildung>();
        }

        public void LoadDataFromSQL(SqlDataService sqlDataService)
        {
            _azubis = sqlDataService.GetAzubiData();
            _ausbilder = sqlDataService.GetAusbilderData();
            _ausbildung = sqlDataService.GetAusbildungData();
        }

        public void LoadDataFromCSV(CsvDataService csvDataService)
        {
            _azubis = csvDataService.GetAzubiData();
            _ausbilder = csvDataService.GetAusbilderData();
            _ausbildung = csvDataService.GetAusbildungData();
        }

        public void LoadDataFromXml(XmlDataService xmlDataService)
        {
            _azubis = xmlDataService.GetAzubiData();
            _ausbilder = xmlDataService.GetAusbilderData();
            _ausbildung = xmlDataService.GetAusbildungData();
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