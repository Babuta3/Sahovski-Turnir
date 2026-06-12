namespace SlojServisa.DTO
{
    public class PlasmanIgracaDTO
    {
        public int PlasmanID { get; set; }
        public int TurnirID { get; set; }
        public int IgracID { get; set; }
        public string ImeIgraca { get; set; } = string.Empty;
        public string PrezimeIgraca { get; set; } = string.Empty;
        public string Klub { get; set; } = string.Empty;
        public string Titula { get; set; } = string.Empty;
        public int? ELO { get; set; }
        public int Mesto { get; set; }
        public decimal Bodovi { get; set; }
        public decimal Nagrada { get; set; }
    }
}
