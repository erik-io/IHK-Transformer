namespace IHK_Transform.Models
{
    internal class Ausbildung
    {
        public Ausbildung()
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
        }
    }
}