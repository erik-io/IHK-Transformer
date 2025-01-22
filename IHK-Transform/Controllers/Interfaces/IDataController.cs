using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IHK_Transform.Services;

namespace IHK_Transform.Controllers.Interfaces
{
    internal interface IDataController
    {
        // Grundlegende Controller-Operationen
        void Initialize();
        List<object> GetDisplayData();

        // Daten-Lade-Operationen
        void LoadDataFromSql(SqlDataService sqlDataService);
        void LoadDataFromCsv(CsvDataService csvDataService);
        void LoadDataFromXml(XmlDataService xmlDataService);
    }
}
