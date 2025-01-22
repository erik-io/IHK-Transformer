namespace IHK_Transform.Models
{
    internal class Ausbilder
    {
        public Ausbilder()
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
    }
}