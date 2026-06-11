using Microsoft.EntityFrameworkCore;
using SlojPodataka.KlasePodataka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlojPodataka.TehnoloskeKlase
{
    public class TurnirRepozitorijum
    {
        private readonly TurnirDbContext _kontekst;

        public TurnirRepozitorijum(TurnirDbContext kontekst)
        {
            _kontekst = kontekst;
        }

        public List<Turnir> DohvatiSve()
        {
            return _kontekst.Turniri
                .AsNoTracking()
                .Include(t => t.Plasmani)
                    .ThenInclude(p => p.Igrac)
                .ToList();
        }

        public Turnir? DohvatiPoId(int id)
        {
            return _kontekst.Turniri
                .Include(t => t.Plasmani)
                    .ThenInclude(p => p.Igrac)
                .FirstOrDefault(t => t.TurnirID == id);
        }

        public void Dodaj(Turnir turnir)
        {
            using var transakcija = _kontekst.Database.BeginTransaction();
            try
            {
                _kontekst.Turniri.Add(turnir);
                _kontekst.SaveChanges();
                transakcija.Commit();
            }
            catch
            {
                transakcija.Rollback();
                throw;
            }
        }

        public void Izmeni(Turnir turnir)
        {
            using var transakcija = _kontekst.Database.BeginTransaction();
            try
            {
                var stariPlasmani = _kontekst.PlasmaniIgraca
                    .Where(p => p.TurnirID == turnir.TurnirID)
                    .ToList();
                _kontekst.PlasmaniIgraca.RemoveRange(stariPlasmani);
                _kontekst.SaveChanges();

                var postojeci = _kontekst.Turniri
                    .FirstOrDefault(t => t.TurnirID == turnir.TurnirID);

                if (postojeci == null) throw new Exception("Turnir nije pronađen.");

                postojeci.NazivTurnira = turnir.NazivTurnira;
                postojeci.Mesto = turnir.Mesto;
                postojeci.Datum = turnir.Datum;
                postojeci.Organizator = turnir.Organizator;
                postojeci.GlavniArbitar = turnir.GlavniArbitar;
                postojeci.NagradniFond = turnir.NagradniFond;
                postojeci.TipTurnira = turnir.TipTurnira;
                postojeci.FormatTakmicenja = turnir.FormatTakmicenja;
                postojeci.BrojRundi = turnir.BrojRundi;
                postojeci.BrojUcesnika = turnir.BrojUcesnika;
                postojeci.VremenskaKontrola = turnir.VremenskaKontrola;
                postojeci.TiebreakKriterijum = turnir.TiebreakKriterijum;

                foreach (var plasman in turnir.Plasmani)
                {
                    plasman.TurnirID = postojeci.TurnirID;
                    plasman.PlasmanID = 0;
                    _kontekst.PlasmaniIgraca.Add(plasman);
                }

                _kontekst.SaveChanges();
                transakcija.Commit();
            }
            catch
            {
                transakcija.Rollback();
                throw;
            }
        }

        public void Obrisi(int id)
        {
            using var transakcija = _kontekst.Database.BeginTransaction();
            try
            {
                var turnir = _kontekst.Turniri
                    .Include(t => t.Plasmani)
                    .FirstOrDefault(t => t.TurnirID == id);

                if (turnir == null) throw new Exception("Turnir nije pronađen.");

                _kontekst.PlasmaniIgraca.RemoveRange(turnir.Plasmani);
                _kontekst.Turniri.Remove(turnir);
                _kontekst.SaveChanges();
                transakcija.Commit();
            }
            catch
            {
                transakcija.Rollback();
                throw;
            }
        }

        public List<Turnir> Filtriraj(DateTime? datumOd, DateTime? datumDo, string? mesto)
        {
            var upit = _kontekst.Turniri
                .Include(t => t.Plasmani)
                    .ThenInclude(p => p.Igrac)
                .AsQueryable();

            if (datumOd.HasValue)
                upit = upit.Where(t => t.Datum >= datumOd.Value);

            if (datumDo.HasValue)
                upit = upit.Where(t => t.Datum <= datumDo.Value);

            if (!string.IsNullOrEmpty(mesto))
                upit = upit.Where(t => t.Mesto.Contains(mesto));

            return upit.ToList();
        }
    }
}
