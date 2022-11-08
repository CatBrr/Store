using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Store.Models
{
    public class bron
    {
        public int Id { get; set; }
        public int klientId { get; set; }
        public klient klient { get; set; }
        [Display(Name = "loom")]
        public int loomId { get; set; }
        public loom loomad { get; set; }
        [Display(Name = "vali master")]
        public int masterId { get; set; }
        public master master { get; set; }
        [Required]
        [Display(Name = "broneerida eag")]
        public DateTime aeg { get; set; }
        [Display(Name = "vali teenused")]
        public int teenustId { get; set; }
        public teenust teenused { get; set; }
    }
}
