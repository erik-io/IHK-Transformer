using IHK_Transform.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IHK_Transform;
using System.Diagnostics;
using IHK_Transform.Utilities;

namespace IHK_Transform.Services
{
    internal class SqlDataService : DataHandler
    {
        // private List<Azubi> _azubis = new List<Azubi>();
        // private List<Ausbilder> _ausbilder = new List<Ausbilder>();
        // private List<Ausbildung> _ausbildungen = new List<Ausbildung>();
        private readonly string _connectionString;

        public SqlDataService(IniReader iniReader)
        {
            string server = iniReader.GetData("SQL", "server");
            string port = iniReader.GetData("SQL", "port");
            string database = iniReader.GetData("SQL", "database");
            string user = iniReader.GetData("SQL", "user");
            string password = iniReader.GetData("SQL", "password");
            _connectionString = $"Server={server},Port={port};Database={database};User Id={user};Password={password};";
        }

        public void SetFilePath(string filePath)
        {
            throw new NotImplementedException();
        }

        public override void LoadData()
        {
            LoadAzubiData();
            LoadAusbilderData();
            LoadAusbildungData();
        }

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(_connectionString);
        }

        private void LoadAzubiData()
        {
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
                            int azubiID = reader.GetInt32("azubi_id");
                            string vorname = reader.GetString("vorname");
                            string nachname = reader.GetString("nachname");
                            DateTime ausbildungsbeginn = reader.GetDateTime("ausbildungsbeginn");
                            string ausbildungID = reader.GetString("ausbildung_id");
                            int ausbilderID = reader.GetInt32("ausbilder_id");

                            _azubis.Add(new Azubi(azubiID, vorname, nachname, ausbildungsbeginn, ausbildungID,
                                ausbilderID));
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine($"Fehler beim Verarbeiten eines Azubi-Eintrags: {ex.Message}");
                        }
                    }
                }
            }
        }

        private void LoadAusbilderData()
        {
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
                            int AusbilderID = reader.GetInt32("ausbilder_id");
                            string vorname = reader.GetString("vorname");
                            string nachname = reader.GetString("nachname");

                            _ausbilder.Add(new Ausbilder(AusbilderID, vorname, nachname));
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine($"Fehler beim Verarbeiten eines Ausbilder-Eintrags: {ex.Message}");
                        }
                    }
                }
            }
        }

        private void LoadAusbildungData()
        {
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
                            string ausbildungID = reader.GetString("ausbildung_id");
                            string bezeichnung = reader.GetString("bezeichnung");
                            _ausbildungen.Add(new Ausbildung(ausbildungID, bezeichnung));
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine($"Fehler beim Verarbeiten eines Ausbildungs-Eintrags: {ex.Message}");
                        }
                    }
                }
            }
        }

        // public List<Azubi> GetAzubiData() => _azubis;
        // public List<Ausbilder> GetAusbilderData() => _ausbilder;
        // public List<Ausbildung> GetAusbildungData() => _ausbildungen;
    }
}
