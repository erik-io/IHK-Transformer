using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace IHK_Transform.Models.Entities
{
    /// <summary>
    /// Repräsentiert einen Auszubildenden im System.
    /// Implementiert INotifyPropertyChanged, für die GUI-Aktualisierung.
    /// </summary>
    public class Azubi : INotifyPropertyChanged
    {
        private int _azubiId;
        private string _vorname;
        private string _nachname;
        private DateTime _ausbildungsbeginn;
        private string _ausbildungId;
        private int _ausbilderId;

        // Event für Property-Änderungen
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Standardkonstruktor für einen neuen Auszubildenden
        /// </summary>
        public Azubi()
        {
            // Standardwerte setzen
            Ausbildungsbeginn = DateTime.Now;
        }

        /// <summary>
        /// Konstruktor mit allen erforderlichen Daten
        /// </summary>
        public Azubi(int azubiId, string vorname, string nachname,
            DateTime ausbildungsbeginn, string ausbildungId, int ausbilderId)
        {
            AzubiId = azubiId;
            Vorname = vorname;
            Nachname = nachname;
            Ausbildungsbeginn = ausbildungsbeginn;
            AusbildungId = ausbildungId;
            AusbilderId = ausbilderId;
        }

        // Properties mit Validierung und Benachrichtigung
        public int AzubiId
        {
            get => _azubiId;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Azubi-ID muss größer als 0 sein.");
                SetProperty(ref _azubiId, value);
            }
        }

        public string Vorname
        {
            get => _vorname;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Vorname darf nicht leer sein.");
                SetProperty(ref _vorname, value.Trim());
            }
        }

        public string Nachname
        {
            get => _nachname;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Nachname darf nicht leer sein.");
                SetProperty(ref _nachname, value.Trim());
            }
        }

        public DateTime Ausbildungsbeginn
        {
            get => _ausbildungsbeginn;
            set => SetProperty(ref _ausbildungsbeginn, value);
        }

        public string AusbildungId
        {
            get => _ausbildungId;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Ausbildungs-ID darf nicht leer sein.");
                SetProperty(ref _ausbildungId, value);
            }
        }

        public int AusbilderId
        {
            get => _ausbilderId;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Ausbilder-ID muss größer als 0 sein.");
                SetProperty(ref _ausbilderId, value);
            }
        }

        /// <summary>
        /// Hilfsmethode zum Setzen von Properties mit PropertyChanged-Benachrichtigung
        /// </summary>
        protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        /// <summary>
        /// Löst das PropertyChanged-Event aus
        /// </summary>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Gibt eine lesbare String-Repräsentation des Auszubildenden zurück
        /// </summary>
        public override string ToString()
        {
            return $"{Vorname} {Nachname}";
        }

        // Übergangsmethoden für Kompatibilität
        public int getAzubiID() => AzubiId;
        public string getVorname() => Vorname;
        public string getNachname() => Nachname;
        public DateTime getAusbildungsbeginn() => Ausbildungsbeginn;
        public string getAusbildungID() => AusbildungId;
        public int getAusbilderID() => AusbilderId;
    }
}


/*public Azubi()
{
}

public Azubi(int azubiID, string vorname, string nachname, string ausbildung, DateTime ausbildungsbeginn, string ausbildungID, int ausbilderID)
{
    _azubi_id = azubiID;
    _vorname = vorname;
    _nachname = nachname;
    _ausbildung = ausbildung;
    _ausbildungsbeginn = ausbildungsbeginn;
    _ausbildung_id = ausbildungID;
    _ausbilder_id = ausbilderID;
}

public Azubi(int azubiID, string vorname, string nachname, DateTime ausbildungsbeginn, string ausbildungID, int ausbilderID)
{
    _azubi_id = azubiID;
    _vorname = vorname;
    _nachname = nachname;
    _ausbildungsbeginn = ausbildungsbeginn;
    _ausbildung_id = ausbildungID;
    _ausbilder_id = ausbilderID;
}

private int _azubi_id { get; set; }
private string _vorname { get; set; }
private string _nachname { get; set; }
private string _ausbildung { get; set; }
private DateTime _ausbildungsbeginn { get; set; }
private string _ausbildung_id { get; set; }
private int _ausbilder_id { get; set; }

public int getAzubiID()
{
    return _azubi_id;
}

public void setAzubiID(int azubiID)
{
    _azubi_id = azubiID;
}

public string getVorname()
{
    return _vorname;
}

public void setVorname(string vorname)
{
    _vorname = vorname;
}

public string getNachname()
{
    return _nachname;
}

public void setNachname(string nachname)
{
    _nachname = nachname;
}

public string getAusbildung()
{
    return _ausbildung;
}

public void setAusbildung(string ausbildung)
{
    _ausbildung = ausbildung;
}

public DateTime getAusbildungsbeginn()
{
    return _ausbildungsbeginn;
}

public void setAusbildungsbeginn(DateTime ausbildungsbeginn)
{
    _ausbildungsbeginn = ausbildungsbeginn;
}

public string getAusbildungID()
{
    return _ausbildung_id;
}

public void setAusbildungID(string ausbildungID)
{
    _ausbildung_id = ausbildungID;
}

public int getAusbilderID()
{
    return _ausbilder_id;
}

public void setAusbilderID(int ausbilderID)
{
    _ausbilder_id = ausbilderID;
}

public override string ToString()
{
    return $"{_vorname} {_nachname}";
}
}*/