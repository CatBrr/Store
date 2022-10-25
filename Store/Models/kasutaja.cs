namespace Store.Models
{
    public class kasutaja
    {
        public int ID { get; set; }
        public string Nimi { get; set; }
        public string Perenimi { get; set; }
        public string telefon { get; set; }
        public string epost { get; set; }
        public bool istenindaja { get; set; }
        public string salasona { get; set; }
    }
}
