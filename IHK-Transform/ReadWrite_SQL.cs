using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using IHK_Transform.Models;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace IHK_Transform
{
    internal class ReadWrite_SQL
    {
        private readonly string _connectionString;

        public ReadWrite_SQL()
        {
        }

        public ReadWrite_SQL(IniReader iniReader)
        {
            string server = iniReader.GetData("SQL", "server");
            string port = iniReader.GetData("SQL", "port");
            string database = iniReader.GetData("SQL", "database");
            string user = iniReader.GetData("SQL", "user");
            string password = iniReader.GetData("SQL", "password");

            _connectionString = $"Server={server},Port={port};Database={database};User Id={user};Password={password};";
        }

        public ReadWrite_SQL(string connectionString)
        {
             _connectionString = connectionString;
        }

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(_connectionString);
        }

        public List<Azubi> GetAzubi()
        {
            var list = new List<Azubi>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM Azubis", conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var azubi = new Azubi();
                        azubi.setAzubiID(reader.GetInt32("azubi_id"));
                        azubi.setVorname(reader.GetString("vorname"));
                        azubi.setNachname(reader.GetString("nachname"));
                        azubi.setAusbildungsbeginn(reader.GetDateTime("ausbildungsbeginn"));
                        azubi.setAusbildungID(reader.GetString("ausbildung_id"));
                        azubi.setAusbilderID(reader.GetInt32("ausbilder_id"));
                        list.Add(azubi);
                    }
                }
            }
            return list;
        }

        public List<Ausbilder> GetAusbilder()
        {
            var list = new List<Ausbilder>();
            
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM ausbilder", conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var ausbilder = new Ausbilder();
                        ausbilder.setAusbilderID(reader.GetInt32("ausbilder_id"));
                        ausbilder.setVorname(reader.GetString("vorname"));
                        ausbilder.setNachname(reader.GetString("nachname"));
                        list.Add(ausbilder);
                    }
                }
            }
            return list;
        }

        public List<Ausbildung> GetAusbildung()
        {
            var list = new List<Ausbildung>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM ausbildungen", conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var ausbildung = new Ausbildung();
                        ausbildung.setAusbildungID(reader.GetString("ausbildung_id"));
                        ausbildung.setAusbildung(reader.GetString("bezeichnung"));
                        list.Add(ausbildung);
                    }
                }
            }
            return list;
        }
    }
}
