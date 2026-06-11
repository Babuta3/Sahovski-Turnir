using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlojPodataka.KlasePodataka
{
    [Table("Igrac")]
    public class Igrac : OsnovniEntitet
    {
        [Key]
        public int IgracID { get; set; }

        [Required]
        [StringLength(50)]
        public string Ime { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Prezime { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Klub { get; set; } = string.Empty;

        [Required]
        [StringLength(10)]
        public string Titula { get; set; } = "Bez titule";

        public int? ELO { get; set; }

        public ICollection<PlasmanIgraca> Plasmani { get; set; } = new List<PlasmanIgraca>();
    }
}
