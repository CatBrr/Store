using Microsoft.Extensions.Hosting;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Store.Models
{
    public class bron
    {
        public int Id { get; set; }
        public int klientId { get; set; }
        public klient klient { get; set; }
        [Required]
        [Display(Name = "loom")]
        public int loomId { get; set; }
        public loom loomad { get; set; }
        [Required]
        [Display(Name = "vali master")]
        public int masterId { get; set; }
        public master master { get; set; }
        [Required]
        [Display(Name = "broneerida eag")]
        public DateTime aeg { get; set; }
        [Required]
        [Display(Name = "vali teenused")]
        public int teenustId { get; set; }
        public teenust teenused { get; set; }
    }
    public class master
    {
        public int Id { get; set; }
        public string Nimi { get; set; }
        public string Perenimi { get; set; }
        public string telefon { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "e-post")]
        public string epost { get; set; }
        public int teenustId { get; set; }
        public teenust teenused { get; set; }
        public int keelId { get; set; }
        public keel keelid { get; set; }


    }
}
