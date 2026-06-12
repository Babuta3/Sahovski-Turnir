namespace SlojServisa.DTO
{
    public class TurnirDTO
    {
        public int TurnirID { get; set; }
        public string NazivTurnira { get; set; } = string.Empty;
        public string Mesto { get; set; } = string.Empty;
        public DateTime Datum { get; set; }
        public string Organizator { get; set; } = string.Empty;
        public string GlavniArbitar { get; set; } = string.Empty;
        public decimal NagradniFond { get; set; }
        public string TipTurnira { get; set; } = string.Empty;
        public string FormatTakmicenja { get; set; } = string.Empty;
        public int BrojRundi { get; set; }
        public int BrojUcesnika { get; set; }
        public string VremenskaKontrola { get; set; } = string.Empty;
        public string TiebreakKriterijum { get; set; } = string.Empty;
    }
}
