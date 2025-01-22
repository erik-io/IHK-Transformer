using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace IHK_Transform.Models.Entities
{
    /// <summary>
    /// Repräsentiert eine Ausbildung im System.
    /// Implementiert INotifyPropertyChanged für die GUI-Aktualisierung.
    /// </summary>
    public class Ausbildung : INotifyPropertyChanged
    {
        private string _ausbildungId;
        private string _bezeichnung;

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Standardkonstruktor für eine neue Ausbildung
        /// </summary>
        public Ausbildung()
        {
        }

        /// <summary>
        /// Konstruktor mit allen erforderlichen Daten
        /// </summary>
        public Ausbildung(string ausbildungId, string bezeichnung)
        {
            AusbildungId = ausbildungId;
            Bezeichnung = bezeichnung;
        }

        public string AusbildungId
        {
            get => _ausbildungId;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Ausbildungs-ID darf nicht leer sein.");
                if (value.Length > 4)
                    throw new ArgumentException("Ausbildungs-ID darf maximal 4 Zeichen lang sein.");
                SetProperty(ref _ausbildungId, value.ToUpper());
            }
        }

        public string Bezeichnung
        {
            get => _bezeichnung;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Bezeichnung darf nicht leer sein.");
                SetProperty(ref _bezeichnung, value.Trim());
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
            return $"{AusbildungId} - {Bezeichnung}";
        }

        // Übergangsmethode
        public string getAusbildungID() => AusbildungId;
    }
}

/*public Ausbildung()
{
}

public Ausbildung(string ausbildungID, string bezeichnung)
{
    _ausbildung_id = ausbildungID;
    _bezeichnung = bezeichnung;
}

private string _ausbildung_id { get; set; }
private string _bezeichnung { get; set; }

public string getAusbildungID()
{
    return _ausbildung_id;
}

public void setAusbildungID(string ausbildungID)
{
    _ausbildung_id = ausbildungID;
}

public string getAusbildung()
{
    return _bezeichnung;
}

public void setAusbildung(string bezeichnung)
{
    _bezeichnung = bezeichnung;
}

public override string ToString()
{
    return _ausbildung_id;
}*/