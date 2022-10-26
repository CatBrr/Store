namespace Store.Models
{
    public class master
    {
        public int ID { get; set; }
        public string Nimi { get; set; }
        public string Perenimi { get; set; }
        public string telefon { get; set; }
        public string epost { get; set; }
        public loom loom { get; set; }
        public teenust[] teenused { get; set; }
        public DateTime bron { get; set; }
        public klient[] klientid { get; set; }
        public keel[] keelid { get; set; }

    }
}
