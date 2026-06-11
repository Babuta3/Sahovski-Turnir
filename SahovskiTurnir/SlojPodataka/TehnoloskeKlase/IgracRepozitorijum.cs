using Microsoft.EntityFrameworkCore;
using SlojPodataka.KlasePodataka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlojPodataka.TehnoloskeKlase
{
    public class IgracRepozitorijum
    {
        private readonly TurnirDbContext _kontekst;

        public IgracRepozitorijum(TurnirDbContext kontekst)
        {
            _kontekst = kontekst;
        }

        public List<Igrac> DohvatiSve()
        {
            return _kontekst.Igraci
                .AsNoTracking()
                .ToList();
        }

        public Igrac? DohvatiPoId(int id)
        {
            return _kontekst.Igraci
                .FirstOrDefault(i => i.IgracID == id);
        }

        public void Dodaj(Igrac igrac)
        {
            _kontekst.Igraci.Add(igrac);
            _kontekst.SaveChanges();
        }

        public void Izmeni(Igrac igrac)
        {
            var postojeci = _kontekst.Igraci
                .FirstOrDefault(i => i.IgracID == igrac.IgracID);

            if (postojeci == null) throw new Exception("Igrač nije pronađen.");

            postojeci.Ime = igrac.Ime;
            postojeci.Prezime = igrac.Prezime;
            postojeci.Klub = igrac.Klub;
            postojeci.Titula = igrac.Titula;
            postojeci.ELO = igrac.ELO;

            _kontekst.SaveChanges();
        }

        public void Obrisi(int id)
        {
            var igrac = _kontekst.Igraci
                .FirstOrDefault(i => i.IgracID == id);

            if (igrac == null) throw new Exception("Igrač nije pronađen.");

            _kontekst.Igraci.Remove(igrac);
            _kontekst.SaveChanges();
        }

        public int DohvatiUkupanBrojIgracaPrekoSP()
        {
            using var konekcija = new Microsoft.Data.SqlClient.SqlConnection(Konekcija.NizKonekcije);
            var komanda = new Microsoft.Data.SqlClient.SqlCommand("sp_DajUkupanBrojIgraca", konekcija);
            komanda.CommandType = System.Data.CommandType.StoredProcedure;
            konekcija.Open();
            var rezultat = komanda.ExecuteScalar();
            return (int)rezultat!;
        }
    }
}
