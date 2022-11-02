using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Store.Models
{
    public class klient
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "nimi")]
        public string Nimi { get; set; }
        [Required]
        [Display(Name = "perenimi")]
        public string Perenimi { get; set; }
        public string telefon { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "e-post")]
        public string epost { get; set; }
        [Required]
        [Display(Name = "muu loomad")]
        public int loomId { get; set; }
        public loom loomad  { get; set; }

    }
}
