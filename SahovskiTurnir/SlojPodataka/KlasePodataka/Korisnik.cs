using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlojPodataka.KlasePodataka
{
    [Table("Korisnik")]
    public class Korisnik : OsnovniEntitet
    {
        [Key]
        public int KorisnikID { get; set; }

        [Required]
        [StringLength(50)]
        public string KorisnickoIme { get; set; } = string.Empty;

        [Required]
        [StringLength(256)]
        public string LozinkaHash { get; set; } = string.Empty;
    }
}
