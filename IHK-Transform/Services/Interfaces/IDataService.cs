using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IHK_Transform.Models;
using IHK_Transform.Models.Entities;

namespace IHK_Transform.Services.Interfaces
{
    internal interface IDataService
    {
        // Grundlegende CRUD-Operationen
        List<Azubi> GetAzubiData();
        List<Ausbilder> GetAusbilderData();
        List<Ausbildung> GetAusbildungData();

        // Spezifische Datenzugriffsmethoden
        Azubi GetAzubiById(int id);
        Ausbilder GetAusbilderById(int id);
        Ausbildung GetAusbildungById(string id);

        // Datenladefunktionen
        void LoadData();
        void SetFilePath(string path);
    }
}
