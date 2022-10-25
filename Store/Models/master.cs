namespace Store.Models
{
    public class master
    {
        int ID { get; set; }
        string Nimi { get; set; }
        string Perenimi { get; set; }
        string telefon { get; set; }
        string epost { get; set; }
        loom loom { get; set; }
        public teenust[] teenused = new teenust[] { };
        public DateTime[] bron = new DateTime[] { };
        public klient[] klientid = new klient[] { };
        public keel[] keelid = new keel[] { };

    }
}
