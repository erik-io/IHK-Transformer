using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IHK_Transform.Models;

namespace IHK_Transform.Services.Interfaces
{
    internal interface IDataProvider : IDataHandler
    {
        // Grundlegende Provider-Operationen
        void SetFilePath(string filePath);
        void SetConfiguration(IDictionary<string, string> config);
        void LoadData();

        // Daten-Zugriffsmethoden
        List<Azubi> GetAzubiData();
        List<Ausbilder> GetAusbilderData();
        List<Ausbildung> GetAusbildungData();
    }
}
