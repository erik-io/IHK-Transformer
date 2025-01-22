using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHK_Transform.Views.Interfaces
{
    internal interface IMainView
    {
        // Events für Button-Klicks
        event EventHandler LoadSqlDataRequested;
        event EventHandler LoadCsvDataRequested;
        event EventHandler LoadXmlDataRequested;

        // Methoden zur Anzeige
        void DisplayData(List<object> data);
        void ShowError(string message);
        void ShowSuccess(string message);
    }
}
