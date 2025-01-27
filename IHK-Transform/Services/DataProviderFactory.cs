using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IHK_Transform.Infrastructure.Configuration;
using IHK_Transform.Services.Implementations;
using IHK_Transform.Services.Interfaces;

namespace IHK_Transform.Services
{
    internal class DataProviderFactory
    {
        public static IDataProvider CreateProvider(string sourceType, IniReader iniReader = null)
        {
            if (string.IsNullOrWhiteSpace(sourceType))
            {
                Debug.WriteLine("Keine Datenquelle angegeben, verwende Standard");
                sourceType = "CSV";
            }

            // Normalisiere den Input
            sourceType = sourceType.Trim().ToLower();

            Debug.WriteLine($"Erstelle DataProvider für Typ: {sourceType}");

            try
            {
                switch (sourceType.ToLower())
                {
                    case "csv":
                        return new CsvDataService();

                    case "xml":
                        return new XmlDataService();

                    case "sql":
                        if (iniReader == null)
                        {
                            throw new ArgumentNullException(nameof(iniReader), 
                                "INI-Reader benötigt für SQL-Verbindung");
                        }
                        return new SqlDataService(iniReader);


                    default:
                        throw new ArgumentException(
                            $"Ungültiger Datenquellentyp: '{sourceType}'. " +
                            "Erlaubte Werte sind: 'csv', 'xml', 'sql'.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Fehler beim Erstellen des DataProviders: {ex.Message}");
                throw;
            }
        }
    }
}