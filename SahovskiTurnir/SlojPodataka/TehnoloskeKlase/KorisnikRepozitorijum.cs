using SlojPodataka.KlasePodataka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlojPodataka.TehnoloskeKlase
{
    public class KorisnikRepozitorijum
    {
        private readonly TurnirDbContext _kontekst;

        public KorisnikRepozitorijum(TurnirDbContext kontekst)
        {
            _kontekst = kontekst;
        }

        public Korisnik? DohvatiPoKorisnickomImenu(string korisnickoIme)
        {
            return _kontekst.Korisnici
                .FirstOrDefault(k => k.KorisnickoIme == korisnickoIme);
        }

        public void Dodaj(Korisnik korisnik)
        {
            _kontekst.Korisnici.Add(korisnik);
            _kontekst.SaveChanges();
        }

        public bool PostojiKorisnik(string korisnickoIme)
        {
            return _kontekst.Korisnici
                .Any(k => k.KorisnickoIme == korisnickoIme);
        }
    }
}
