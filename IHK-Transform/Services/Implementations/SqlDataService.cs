using IHK_Transform.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IHK_Transform;
using System.Diagnostics;
using IHK_Transform.Infrastructure.Configuration;
using IHK_Transform.Models.Entities;
using IHK_Transform.Services.Interfaces;

namespace IHK_Transform.Services
{
    internal class SqlDataService : IDataProvider
    {
        private readonly List<Azubi> _azubis = new List<Azubi>();
        private readonly List<Ausbilder> _ausbilder = new List<Ausbilder>();
        private readonly List<Ausbildung> _ausbildungen = new List<Ausbildung>();
        private readonly string _connectionString;
        private string _lastError = string.Empty;

        
        // Interface-Implementierungen
        public bool IsConnected { get; private set; }
        public string LastError => _lastError;

        public SqlDataService(IniReader iniReader)
        {
            // Verbindungsdaten aus der INI-Datei lesen
            string server = iniReader.GetValue("SQL", "server") ?? "localhost";
            string port = iniReader.GetValue("SQL", "port") ?? "3306";
            string database = iniReader.GetValue("SQL", "database") ?? "ihk_transformer";
            string user = iniReader.GetValue("SQL", "user") ?? "root";
            string password = iniReader.GetValue("SQL", "password") ?? "";

            _connectionString = $"Server={server},Port={port};Database={database};User Id={user};Password={password};";
        }

        // --- IDataProvider-Implementierung ---
        public void Connect()
        {
            try
            {
                using (var conn = GetConnection())
                {
                    conn.Open();
                    IsConnected = true;
                }
            }
            catch (Exception e)
            {
                _lastError = e.Message;
                IsConnected = false;
            }
        }

        public void Disconnect() => IsConnected = false;

        public void SetFilePath(string filePath)
        {
            throw new NotImplementedException("SetFilePath wird für SQL-Datenquellen nicht unterstützt");
        }


        public void LoadData()
        {
            if(!IsConnected)
            {
                throw new InvalidOperationException("Datenbankverbindung wurde noch nicht hergestellt.");
            }

            try
            {
                LoadAzubiData();
                LoadAusbilderData();
                LoadAusbildungData();
            }
            catch (Exception e)
            {
                _lastError = $"Datenladefehler : {e.Message}";
                Debug.WriteLine(_lastError);
            }
        }

        // --- DataHandler-Implementierung (geerbt) ---
        public void SaveData()
        {
            // Optional: Logik zum Speichern von Änderungen in der Datenbank
            throw new NotImplementedException("SaveData ist für SQL noch nicht implementiert");
        }

        // --- Datenabfragen ---
        public List<Azubi> GetAzubiData() => _azubis;
        public List<Ausbilder> GetAusbilderData() => _ausbilder;
        public List<Ausbildung> GetAusbildungData() => _ausbildungen;

        private MySqlConnection GetConnection() => new MySqlConnection(_connectionString);

        private void LoadAzubiData()
        {
            _azubis.Clear();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM Azubis", conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        try
                        {
                            _azubis.Add(new Azubi(
                                reader.GetInt32("azubi_id"),
                                reader.GetString("vorname"),
                                reader.GetString("nachname"),
                                reader.GetDateTime("geburtsdatum"),
                                reader.GetString("ausbildung_id"),
                                reader.GetInt32("ausbilder_id")
                            ));
                        }
                        catch (Exception ex)
                        {
                            _lastError = $"Fehler beim Verarbeiten eines Azubi-Eintrags: {ex.Message}";
                            Debug.WriteLine(_lastError);
                        }
                    }
                }
            }
        }

        private void LoadAusbilderData()
        {
            _ausbilder.Clear();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM ausbilder", conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        try
                        {
                            _ausbilder.Add(new Ausbilder(
                                reader.GetInt32("ausbilder_id"),
                                reader.GetString("vorname"),
                                reader.GetString("nachname")
                            ));
                        }
                        catch (Exception ex)
                        {
                            _lastError = $"Fehler beim Verarbeiten eines Ausbilder-Eintrags: {ex.Message}";
                            Debug.WriteLine(_lastError);
                        }
                    }
                }
            }
        }

        private void LoadAusbildungData()
        {
            _ausbildungen.Clear();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM ausbildungen", conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        try
                        {
                            _ausbildungen.Add(new Ausbildung(
                                reader.GetString("ausbildung_id"),
                                reader.GetString("bezeichnung")
                            ));
                        }
                        catch (Exception ex)
                        {
                            _lastError = $"Fehler beim Verarbeiten eines Ausbildung-Eintrags: {ex.Message}";
                            Debug.WriteLine(_lastError);
                        }
                    }
                }
            }
        }
    }
}
