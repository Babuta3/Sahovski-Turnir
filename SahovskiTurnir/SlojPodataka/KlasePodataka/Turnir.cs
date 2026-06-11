using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlojPodataka.KlasePodataka
{
    [Table("Turnir")]
    public class Turnir : OsnovniEntitet
    {
        [Key]
        public int TurnirID { get; set; }

        [Required]
        [StringLength(100)]
        public string NazivTurnira { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Mesto { get; set; } = string.Empty;

        [Required]
        public DateTime Datum { get; set; }

        [Required]
        [StringLength(100)]
        public string Organizator { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string GlavniArbitar { get; set; } = string.Empty;

        [Required]
        public decimal NagradniFond { get; set; }

        [Required]
        [StringLength(50)]
        public string TipTurnira { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string FormatTakmicenja { get; set; } = string.Empty;

        [Required]
        public int BrojRundi { get; set; }

        [Required]
        public int BrojUcesnika { get; set; }

        [Required]
        [StringLength(50)]
        public string VremenskaKontrola { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string TiebreakKriterijum { get; set; } = string.Empty;

        public ICollection<PlasmanIgraca> Plasmani { get; set; } = new List<PlasmanIgraca>();
    }
}
