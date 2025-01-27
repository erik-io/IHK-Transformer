using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IHK_Transform.Models.EventArgs;
using IHK_Transform.Services;

namespace IHK_Transform.Controllers.Interfaces
{
    public interface IDataController
    {
        // Ereignisbehandlung
        event EventHandler DataLoaded;
        event EventHandler<string> ErrorOccurred;

        // Datenoperationen
        void LoadData(string sourceType);
        List<object> GetDisplayData();

        // Initialisierung
        void InitializeDataSources();
    }
}
