using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace IHK_Transform.Models.Entities
{
    /// <summary>
    /// Repräsentiert einen Ausbilder im System.
    /// Implementiert INotifyPropertyChanged, für die GUI-Aktualisierung.
    /// </summary>
    internal class Ausbilder : INotifyPropertyChanged
    {
        private int _ausbilderId;
        private string _vorname;
        private string _nachname;

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Standardkonstruktor für einen neuen Ausbilder
        /// </summary>
        public Ausbilder()
        {
        }

        /// <summary>
        /// Konstruktor mit allen erforderlichen Daten
        /// </summary>
        public Ausbilder(int ausbilderId, string vorname, string nachname)
        {
            AusbilderId = ausbilderId;
            Vorname = vorname;
            Nachname = nachname;
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

        protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public override string ToString()
        {
            return $"{Vorname} {Nachname}";
        }

        // Übergangsmethoden
        public int getAusbilderID() => AusbilderId;
        public string getVorname() => Vorname;
        public string getNachname() => Nachname;
    }

    /*public Ausbilder()
    {
    }

    public Ausbilder(int ausbilderID, string vorname, string nachname)
    {
        _ausbilder_id = ausbilderID;
        _vorname = vorname;
        _nachname = nachname;
    }

    private int _ausbilder_id { get; set; }
    private string _vorname { get; set; }
    private string _nachname { get; set; }

    public int getAusbilderID()
    {
        return _ausbilder_id;
    }

    public void setAusbilderID(int ausbilderID)
    {
        _ausbilder_id = ausbilderID;
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

    public override string ToString()
    {
        return _vorname + " " + _nachname;
    }
}*/
    }