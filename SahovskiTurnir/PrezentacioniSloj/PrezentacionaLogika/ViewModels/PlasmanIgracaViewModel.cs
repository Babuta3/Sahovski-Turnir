using System.ComponentModel.DataAnnotations;

namespace PrezentacioniSloj.PrezentacionaLogika.ViewModels
{
    public class PlasmanIgracaViewModel
    {
        public int PlasmanID { get; set; }

        [Required(ErrorMessage = "Igrač je obavezan.")]
        [Display(Name = "Igrač")]
        public int IgracID { get; set; }

        public string ImeIgraca { get; set; } = string.Empty;
        public string PrezimeIgraca { get; set; } = string.Empty;
        public string Klub { get; set; } = string.Empty;
        public string Titula { get; set; } = string.Empty;
        public int? ELO { get; set; }

        [Required(ErrorMessage = "Mesto je obavezno.")]
        [Range(1, 500, ErrorMessage = "Mesto mora biti između 1 i 500.")]
        [Display(Name = "Mesto")]
        public int Mesto { get; set; }

        [Required(ErrorMessage = "Bodovi su obavezni.")]
        [Range(0, 30, ErrorMessage = "Bodovi moraju biti između 0 i 30.")]
        [Display(Name = "Bodovi")]
        public decimal Bodovi { get; set; }

        [Display(Name = "Nagrada (RSD)")]
        public decimal Nagrada { get; set; }
    }
}
