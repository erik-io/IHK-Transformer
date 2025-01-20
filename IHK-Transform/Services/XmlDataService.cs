﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using IHK_Transform.Models;

namespace IHK_Transform.Services
{
    internal class XmlDataService
    {
        private List<Azubi> _azubis;
        private List<Ausbilder> _ausbilder;
        private List<Ausbildung> _ausbildungen; 
        private string _filePath;

        public XmlDataService()
        {
            _azubis = new List<Azubi>();
            _ausbilder = new List<Ausbilder>();
            _ausbildungen = new List<Ausbildung>();
        }

        public XmlDataService(string filePath)
        {
            _filePath = filePath ?? throw new ArgumentNullException(nameof(filePath));
            _azubis = new List<Azubi>();
            _ausbilder = new List<Ausbilder>();
            _ausbildungen = new List<Ausbildung>();
        }

        public void SetFilePath(string filePath)
        {
            _filePath = filePath ?? throw new ArgumentNullException(nameof(filePath));
        }

        public void LoadData()
        {
            if (string.IsNullOrWhiteSpace(_filePath))
                throw new InvalidOperationException("Die Datei wurde nicht gefunden oder der Pfad ist leer.");

            LoadAzubiData();
            LoadAusbilderData();
            LoadAusbildungData();
        }

        private void LoadAzubiData()
        {
            XDocument doc = XDocument.Load(_filePath);
            foreach (var a in doc.Descendants("Azubis").Elements("Eintrag"))
            {
                try
                {
                    int azubiID = int.Parse(a.Element("azubi_id")?.Value ?? "0");
                    string vorname = a.Element("vorname")?.Value ?? "NULL";
                    string nachname = a.Element("nachname")?.Value ?? "NULL";
                    DateTime ausbildungsbeginn = DateTime.Parse(a.Element("ausbildungsbeginn")?.Value ?? "2000-01-01");
                    string ausbildungID = a.Element("ausbildung_id")?.Value ?? "0";
                    int ausbilderID = int.Parse(a.Element("ausbilder_id")?.Value ?? "0");

                    _azubis.Add(new Azubi(azubiID, vorname, nachname, ausbildungsbeginn, ausbildungID, ausbilderID));
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Fehler beim Verarbeiten eines Azubi-Eintrags: {ex.Message}");
                }
            }
        }

        private void LoadAusbilderData()
        {
            XDocument doc = XDocument.Load(_filePath);
            foreach (var a in doc.Descendants("Ausbilder").Elements("Eintrag"))
            {
                try
                {
                    int ausbilderID = int.Parse(a.Element("ausbilder_id")?.Value ?? "0");
                    string vorname = a.Element("vorname")?.Value ?? "NULL";
                    string nachname = a.Element("nachname")?.Value ?? "NULL";

                    _ausbilder.Add(new Ausbilder(ausbilderID, vorname, nachname));
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Fehler beim Verarbeiten eines Ausbilder-Eintrags: {ex.Message}");
                }
            }
        }

        private void LoadAusbildungData()
        {
            XDocument doc = XDocument.Load(_filePath);
            foreach (var a in doc.Descendants("Ausbildungen").Elements("Eintrag"))
            {
                try
                {
                    string ausbildungID = a.Element("ausbildung_id")?.Value ?? "0";
                    string bezeichnung = a.Element("bezeichnung")?.Value ?? "NULL";

                    _ausbildungen.Add(new Ausbildung(ausbildungID, bezeichnung));
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Fehler beim Verarbeiten eines Ausbildungs-Eintrags: {ex.Message}");
                }
            }
        }

        public List<Azubi> GetAzubi() => _azubis;
        public List<Ausbilder> GetAusbilder() => _ausbilder;
        public List<Ausbildung> GetAusbildung() => _ausbildungen;
    }
}
