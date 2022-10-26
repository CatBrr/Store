using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Store.Models
{
    public class sugu
    {
        public int Id { get; set; }
        public string nimetus { get; set; }
    }
    public class viilatuup
    {
        public int Id { get; set; }
        public string nimetus { get; set; }
        //sirgepikk, sirgelühike, sirgekeskmine, lokkispikk, lokkislühike, lokkiskeskmine 
    }

    public class loom
    {
        public int Id { get; set; }
        public string Nimi { get; set; }
        [Required]
        [Display(Name = "sugu")]
        public int suguId { get; set; }
        public  sugu sugu{ get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "tervis (1/10)")]
        public int tervis { get; set; }
        public int viilatuupId { get; set; }
        public viilatuup viilatüüp { get; set; }
        public string iseloomu { get; set; }
        public int vanus { get; set; }
    }

}
