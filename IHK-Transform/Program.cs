using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using IHK_Transform.Controllers;
using IHK_Transform.Controllers.Interfaces;
using IHK_Transform.Infrastructure.Configuration;
using IHK_Transform.Services;
using IHK_Transform.Services.Implementations;
using IHK_Transform.Services.Interfaces;
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

            try
            {
                // Konfiguration aus INI-Datei laden
                var iniReader = new IniReader("Config/config.ini");
                string dataSource = iniReader.GetValue("General","default_source");

                // Debug-Ausgabe zur Überprüfung der gelesenen Konfiguration
                Debug.WriteLine($"Gelesene Datenquelle aus INI: {dataSource}");

                // Factory erstellt den passenden Provider
                IDataProvider dataProvider;
                try
                {
                    dataProvider = DataProviderFactory.CreateProvider(dataSource, iniReader);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Fehler beim Erstellen des DataProviders: {ex.Message}\n" +
                                    "Die Anwendung wird mit Standard-CSV-Provider gestartet.",
                        "Konfigurationsfehler",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    // Fallback auf CSV bei Fehler
                    dataProvider = DataProviderFactory.CreateProvider("csv");
                }

                // Controller initialisieren
                IDataController dataController = new DataController(dataProvider);

                // MainForm mit Dependency Injection erstellen
                var mainForm = new MainForm(dataController);

                // MainController verbindet View und Controller
                var mainController = new MainController(mainForm, dataController, dataProvider);

                // Anwendung starten
                Application.Run(mainForm);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Kritischer Fehler beim Starten der Anwendung:\n{ex.Message}",
                    "Fehler",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}
