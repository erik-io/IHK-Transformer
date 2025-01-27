using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IHK_Transform.Models;
using IHK_Transform.Models.Entities;

namespace IHK_Transform.Services.Interfaces
{
    public interface IDataProvider : IDataHandler
    {
        // Verbindungsverwaltung
        bool IsConnected { get; }
        void Connect();
        void Disconnect();

        // Datenquellensteuerung
        void SetSource(string source);
        new void LoadData();

        // Datenabfragen
        new List<Azubi> GetAzubiData();
        new List<Ausbilder> GetAusbilderData();
        new List<Ausbildung> GetAusbildungData();

        // Fehlerbehandlung
        string LastError { get; }
    }
}
