using SlojPodataka.Repozitorijumi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlojPoslovneLogike.Stanje
{
    public class PrikupljanjeStanja
    {
        private readonly TurnirRepozitorijum _repozitorijum;

        public PrikupljanjeStanja(TurnirRepozitorijum repozitorijum)
        {
            _repozitorijum = repozitorijum;
        }

        public decimal DohvatiNagradniFond(int turnirId)
        {
            var turnir = _repozitorijum.DohvatiPoId(turnirId);
            if (turnir == null) throw new Exception("Turnir nije pronađen.");
            return turnir.NagradniFond;
        }
    }
}
