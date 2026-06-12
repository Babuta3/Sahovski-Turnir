using SlojPodataka.KlasePodataka;
using SlojServisa.DTO;

namespace SlojServisa.KlaseMapiranja
{
    public class TurnirMapper
    {
        public TurnirDTO UDTO(Turnir turnir)
        {
            return new TurnirDTO
            {
                TurnirID = turnir.TurnirID,
                NazivTurnira = turnir.NazivTurnira,
                Mesto = turnir.Mesto,
                Datum = turnir.Datum,
                Organizator = turnir.Organizator,
                GlavniArbitar = turnir.GlavniArbitar,
                NagradniFond = turnir.NagradniFond,
                TipTurnira = turnir.TipTurnira,
                FormatTakmicenja = turnir.FormatTakmicenja,
                BrojRundi = turnir.BrojRundi,
                BrojUcesnika = turnir.BrojUcesnika,
                VremenskaKontrola = turnir.VremenskaKontrola,
                TiebreakKriterijum = turnir.TiebreakKriterijum,
                Plasmani = turnir.Plasmani.Select(p => new PlasmanIgracaDTO
                {
                    PlasmanID = p.PlasmanID,
                    TurnirID = p.TurnirID,
                    IgracID = p.IgracID,
                    ImeIgraca = p.Igrac.Ime,
                    PrezimeIgraca = p.Igrac.Prezime,
                    Klub = p.Igrac.Klub,
                    Titula = p.Igrac.Titula,
                    ELO = p.Igrac.ELO,
                    Mesto = p.Mesto,
                    Bodovi = p.Bodovi,
                    Nagrada = p.Nagrada
                }).ToList()
            };
        }

        public List<TurnirDTO> UListuDTO(List<Turnir> turniri)
        {
            return turniri.Select(t => UDTO(t)).ToList();
        }

        public Turnir UEntitet(TurnirDTO dto)
        {
            return new Turnir
            {
                TurnirID = dto.TurnirID,
                NazivTurnira = dto.NazivTurnira,
                Mesto = dto.Mesto,
                Datum = dto.Datum,
                Organizator = dto.Organizator,
                GlavniArbitar = dto.GlavniArbitar,
                NagradniFond = dto.NagradniFond,
                TipTurnira = dto.TipTurnira,
                FormatTakmicenja = dto.FormatTakmicenja,
                BrojRundi = dto.BrojRundi,
                BrojUcesnika = dto.BrojUcesnika,
                VremenskaKontrola = dto.VremenskaKontrola,
                TiebreakKriterijum = dto.TiebreakKriterijum,
                Plasmani = dto.Plasmani.Select(p => new PlasmanIgraca
                {
                    PlasmanID = p.PlasmanID,
                    TurnirID = p.TurnirID,
                    IgracID = p.IgracID,
                    Mesto = p.Mesto,
                    Bodovi = p.Bodovi,
                    Nagrada = p.Nagrada
                }).ToList()
            };
        }
    }
}
