namespace Store.Models
{
    public class loom
    {
        public int ID { get; set; }
        public string Nimi { get; set; }
        public string sugu { get; set; }
        public string tervis { get; set; }
        public string viilatüüp { get; set; }
        public bool iseloomu { get; set; }
        public int vanus { get; set; }
        public klient omanik { get; set; }
    }

}
