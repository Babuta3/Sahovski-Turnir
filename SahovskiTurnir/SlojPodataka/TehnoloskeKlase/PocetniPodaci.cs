using SlojPodataka.KlasePodataka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlojPodataka.TehnoloskeKlase
{
    public class PocetniPodaci
    {
        public static void Inicijalizuj(TurnirDbContext kontekst)
        {
            if (!kontekst.Korisnici.Any())
            {
                kontekst.Korisnici.Add(new Korisnik
                {
                    KorisnickoIme = "admin",
                    LozinkaHash = FunkcijeLozinke.Hashuj("admin123")
                });
                kontekst.SaveChanges();
            }
        }
    }
}
