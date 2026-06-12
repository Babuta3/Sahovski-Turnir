using System.ComponentModel.DataAnnotations;

namespace PrezentacioniSloj.PrezentacionaLogika.ViewModels
{
    public class TurnirViewModel
    {
        public int TurnirID { get; set; }

        [Required(ErrorMessage = "Naziv turnira je obavezan.")]
        [StringLength(100)]
        [Display(Name = "Naziv turnira")]
        public string NazivTurnira { get; set; } = string.Empty;

        [Required(ErrorMessage = "Mesto je obavezno.")]
        [StringLength(100)]
        [Display(Name = "Mesto održavanja")]
        public string Mesto { get; set; } = string.Empty;

        [Required(ErrorMessage = "Datum je obavezan.")]
        [Display(Name = "Datum")]
        public DateTime Datum { get; set; } = DateTime.Today;

        [Required(ErrorMessage = "Organizator je obavezan.")]
        [StringLength(100)]
        [Display(Name = "Organizator")]
        public string Organizator { get; set; } = string.Empty;

        [Required(ErrorMessage = "Glavni arbitar je obavezan.")]
        [StringLength(100)]
        [Display(Name = "Glavni arbitar")]
        public string GlavniArbitar { get; set; } = string.Empty;

        [Required(ErrorMessage = "Nagradni fond je obavezan.")]
        [Range(0, double.MaxValue, ErrorMessage = "Nagradni fond mora biti pozitivan broj.")]
        [Display(Name = "Nagradni fond (RSD)")]
        public decimal NagradniFond { get; set; }

        [Required(ErrorMessage = "Tip turnira je obavezan.")]
        [Display(Name = "Tip turnira")]
        public string TipTurnira { get; set; } = string.Empty;

        [Required(ErrorMessage = "Format takmičenja je obavezan.")]
        [Display(Name = "Format takmičenja")]
        public string FormatTakmicenja { get; set; } = string.Empty;

        [Required(ErrorMessage = "Broj rundi je obavezan.")]
        [Range(1, 30, ErrorMessage = "Broj rundi mora biti između 1 i 30.")]
        [Display(Name = "Broj rundi")]
        public int BrojRundi { get; set; }

        [Required(ErrorMessage = "Broj učesnika je obavezan.")]
        [Range(2, 500, ErrorMessage = "Broj učesnika mora biti između 2 i 500.")]
        [Display(Name = "Broj učesnika")]
        public int BrojUcesnika { get; set; }

        [Required(ErrorMessage = "Vremenska kontrola je obavezna.")]
        [Display(Name = "Vremenska kontrola")]
        public string VremenskaKontrola { get; set; } = string.Empty;

        [Required(ErrorMessage = "Tiebreak kriterijum je obavezan.")]
        [Display(Name = "Tiebreak kriterijum")]
        public string TiebreakKriterijum { get; set; } = string.Empty;

        public DateTime? DatumOd { get; set; }
        public DateTime? DatumDo { get; set; }
        public string? FilterMesto { get; set; }
    }
}
