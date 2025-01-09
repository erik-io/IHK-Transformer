using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace IHK_Transform
{
    internal class ReadWrite_SQL
    {
        private readonly string _connectionString;

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
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM azubi", conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var azubi = new Azubi();
                        azubi.setAzubiID(reader.GetInt32("azubi_id"));
                        azubi.setVorname(reader.GetString("vorname"));
                        azubi.setNachname(reader.GetString("nachname"));
                        azubi.setAusbildungsbeginn(reader.GetInt32("ausbildungsbeginn"));
                        azubi.setAusbildungID(reader.GetInt32("ausbildung_id"));
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
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM ausbildung", conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var ausbildung = new Ausbildung();
                        ausbildung.setAusbildungID(reader.GetInt32("ausbildung_id"));
                        ausbildung.setBerufsbezeichnung(reader.GetString("berufsbezeichnung"));
                        ausbildung.setKurzbezeichnung(reader.GetString("kurzbezeichnung"));
                        list.Add(ausbildung);
                    }
                }
            }
            return list;
        }
    }
}
