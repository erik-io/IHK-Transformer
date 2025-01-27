using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IHK_Transform.Views.Interfaces
{
    internal interface IMainView
    {
        // DataGrid-Zugriff
        DataGridView AzubiGrid { get; }

        // Methode zur Datenanzeige hinzufügen
        void DisplayData(List<object> data);

        // Ereignisse
        event EventHandler LoadSqlDataRequested;
        event EventHandler LoadCsvDataRequested;
        event EventHandler LoadXmlDataRequested;

        // Statusupdates
        void ShowLoadingState(bool isLoading);
        void ShowMessage(string message, bool isError = true);
    }
}
