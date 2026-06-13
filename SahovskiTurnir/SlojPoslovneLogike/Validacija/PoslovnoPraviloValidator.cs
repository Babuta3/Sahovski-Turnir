using SlojPodataka.KlasePodataka;
using SlojPoslovneLogike.Ogranicenja;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlojPoslovneLogike.Validacija
{
    public class PoslovnoPraviloValidator
    {
        private readonly CitacPravila _citacPravila;

        public PoslovnoPraviloValidator(CitacPravila citacPravila)
        {
            _citacPravila = citacPravila;
        }

        public async Task<List<PlasmanIgraca>> IzracunajNagrade(
            List<PlasmanIgraca> plasmani, decimal nagradniFond)
        {
            int brojNagradjenih = await _citacPravila.DohvatiBrojNagradjeniхMesta();

            var nagradjeni = plasmani
                .Where(p => p.Mesto <= brojNagradjenih)
                .OrderBy(p => p.Mesto)
                .ToList();

            if (nagradjeni.Count == 0)
                return plasmani;

            int ukupnoDelova = nagradjeni.Count * (nagradjeni.Count + 1) / 2;

            foreach (var plasman in nagradjeni)
            {
                int koeficijent = brojNagradjenih - plasman.Mesto + 1;
                plasman.Nagrada = Math.Round(nagradniFond * koeficijent / ukupnoDelova, 2);
            }

            foreach (var plasman in plasmani.Where(p => p.Mesto > brojNagradjenih))
            {
                plasman.Nagrada = 0;
            }

            return plasmani;
        }
    }
}
