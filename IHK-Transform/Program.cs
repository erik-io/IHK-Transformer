using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using IHK_Transform.Controllers;
using IHK_Transform.Controllers.Interfaces;
using IHK_Transform.Services;
using IHK_Transform.Services.Implementations;
using IHK_Transform.Services.Interfaces;
using IHK_Transform.Utilities;
using IHK_Transform.Views.Forms;

namespace IHK_Transform
{
    internal static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Dependency Injection Setup
            var iniReader = new IniReader("Config/config.ini");

            // Datenprovider erstellen (implementiert IDataProvider)
            IDataProvider dataProvider = new CsvDataService(); // Explizit CSV-Provider verwenden

            // Controller initialisieren
            IDataController dataController = new DataController(dataProvider);

            // MainForm mit Dependency Injection erstellen
            var mainForm = new MainForm(dataController);

            // MainController verbindet View und Controller
            var mainController = new MainController(mainForm, dataController, dataProvider);

            // Anwendung starten
            Application.Run(mainForm);
        }
    }
}
