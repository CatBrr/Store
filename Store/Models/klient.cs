namespace Store.Models
{
    public class klient
    {
        public int ID { get; set; }
        public string Nimi { get; set; }
        public string Perenimi { get; set; }
        public string telefon { get; set; }
        public string epost { get; set; }
        public loom[] loomad  { get; set; }
        public master master { get; set; }
        public DateTime aeg { get; set; }
        public teenust[] teenused { get; set; }
    }
}
