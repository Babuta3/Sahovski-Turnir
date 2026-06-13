using System.ComponentModel.DataAnnotations;

namespace PrezentacioniSloj.PrezentacionaLogika.ViewModels
{
    public class IgracViewModel
    {
        public int IgracID { get; set; }

        [Required(ErrorMessage = "Ime je obavezno.")]
        [StringLength(50)]
        [Display(Name = "Ime")]
        public string Ime { get; set; } = string.Empty;

        [Required(ErrorMessage = "Prezime je obavezno.")]
        [StringLength(50)]
        [Display(Name = "Prezime")]
        public string Prezime { get; set; } = string.Empty;

        [StringLength(100)]
        [Display(Name = "Klub")]
        public string Klub { get; set; } = "Bez kluba";

        [Required(ErrorMessage = "Titula je obavezna.")]
        [Display(Name = "Titula")]
        public string Titula { get; set; } = string.Empty;

        [Display(Name = "ELO rejting")]
        [Range(100, 3000, ErrorMessage = "ELO mora biti između 100 i 3000.")]
        public int? ELO { get; set; }
    }
}
