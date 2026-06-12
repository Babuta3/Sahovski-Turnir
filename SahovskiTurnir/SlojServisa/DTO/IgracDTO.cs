namespace SlojServisa.DTO
{
    public class IgracDTO
    {
        public int IgracID { get; set; }
        public string Ime { get; set; } = string.Empty;
        public string Prezime { get; set; } = string.Empty;
        public string Klub { get; set; } = string.Empty;
        public string Titula { get; set; } = string.Empty;
        public int? ELO { get; set; }
    }
}
