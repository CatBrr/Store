using Microsoft.Extensions.Hosting;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Store.Models
{
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
        [Required]
        [Display(Name = "loom")]
        public loom loom { get; set; }
        [Required]
        [PasswordPropertyText]
        [Display(Name = "salasõna")]
        public string salasona { get; set; }
        public int teenustId { get; set; }
        public List<teenust> teenused { get; set; }
        public DateTime bron { get; set; }
        public int klientId { get; set; }
        public List<klient> klientid { get; set; }
        public int keeltId { get; set; }
        public List<keel> keelid { get; set; }


    }
}
