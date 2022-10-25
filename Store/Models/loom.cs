namespace Store.Models
{
    public class loom
    {
        int ID { get; set; }
        string Nimi { get; set; }
        string sugu { get; set; }
        string tervis { get; set; }
        string viilatüüp { get; set; }
        bool iseloomu { get; set; }
        int vanus { get; set; }
        klient omanik { get; set; }
    }

}
