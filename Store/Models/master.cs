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
        public int keelId { get; set; }
        public keel keelid { get; set; }


    }
}
