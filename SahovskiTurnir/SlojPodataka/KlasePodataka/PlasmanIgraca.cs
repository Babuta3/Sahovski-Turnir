using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlojPodataka.KlasePodataka
{
    [Table("PlasmanIgraca")]
    public class PlasmanIgraca : OsnovniEntitet
    {
        [Key]
        public int PlasmanID { get; set; }

        [ForeignKey("Turnir")]
        [Required]
        public int TurnirID { get; set; }
        public Turnir Turnir { get; set; } = null!;

        [ForeignKey("Igrac")]
        [Required]
        public int IgracID { get; set; }
        public Igrac Igrac { get; set; } = null!;

        [Required]
        public int Mesto { get; set; }

        [Required]
        public decimal Bodovi { get; set; }

        public decimal Nagrada { get; set; } = 0;
    }
}
