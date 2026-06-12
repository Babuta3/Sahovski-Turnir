using SlojPodataka.KlasePodataka;
using SlojServisa.DTO;

namespace SlojServisa.KlaseMapiranja
{
    public class IgracMapper
    {
        public IgracDTO UDTO(Igrac igrac)
        {
            return new IgracDTO
            {
                IgracID = igrac.IgracID,
                Ime = igrac.Ime,
                Prezime = igrac.Prezime,
                Klub = igrac.Klub,
                Titula = igrac.Titula,
                ELO = igrac.ELO
            };
        }

        public List<IgracDTO> UListuDTO(List<Igrac> igraci)
        {
            return igraci.Select(i => UDTO(i)).ToList();
        }

        public Igrac UEntitet(IgracDTO dto)
        {
            return new Igrac
            {
                IgracID = dto.IgracID,
                Ime = dto.Ime,
                Prezime = dto.Prezime,
                Klub = dto.Klub,
                Titula = dto.Titula,
                ELO = dto.ELO
            };
        }
    }
}
