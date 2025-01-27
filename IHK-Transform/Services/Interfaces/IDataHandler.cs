using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IHK_Transform.Models;
using IHK_Transform.Models.Entities;

namespace IHK_Transform.Services.Interfaces
{
    public interface IDataHandler
    {
        // Basismethoden für Datenverwaltung
        void LoadData();
        void SaveData();

        // Datenbereitstellung
        List<Azubi> GetAzubiData();
        List<Ausbilder> GetAusbilderData();
        List<Ausbildung> GetAusbildungData();
    }
}
