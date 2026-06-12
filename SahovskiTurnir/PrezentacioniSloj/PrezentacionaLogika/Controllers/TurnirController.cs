using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PrezentacioniSloj.PrezentacionaLogika.ViewModels;
using SlojServisa.DTO;
using System.Text;
using System.Text.Json;

namespace PrezentacioniSloj.PrezentacionaLogika.Controllers
{
    public class TurnirController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        private static readonly List<string> _tipovi = new()
        {
            "Otvoreni", "Zatvoreni"
        };

        private static readonly List<string> _formati = new()
        {
            "Švajcarac", "Berger", "Žreb Eliminacija"
        };

        private static readonly List<string> _vremeKontrole = new()
        {
            "Klasik (90+30)", "Rapid (15+10)", "Blitz (5+0)", "Blitz (3+2)", "Bullet (2+0)", "Bullet (1+1)"
        };

        private static readonly List<string> _tiebreakovi = new()
        {
            "Sonneborn-Berger", "Buchholz", "Direktan rezultat", "Progres"
        };

        public TurnirController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        private HttpClient KreirajKlijenta() =>
            _httpClientFactory.CreateClient("SahovskiApi");

        private async Task<List<IgracDTO>> DohvatiIgrace()
        {
            var klijent = KreirajKlijenta();
            var odgovor = await klijent.GetAsync("api/IgracRest");
            if (!odgovor.IsSuccessStatusCode) return new List<IgracDTO>();
            var json = await odgovor.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<IgracDTO>>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
                ?? new List<IgracDTO>();
        }

        private void PopuniDropdowne(TurnirViewModel model, List<IgracDTO> igraci)
        {
            ViewBag.Tipovi = _tipovi.Select(t => new SelectListItem { Value = t, Text = t }).ToList();
            ViewBag.Formati = _formati.Select(f => new SelectListItem { Value = f, Text = f }).ToList();
            ViewBag.VremenskaKontrola = _vremeKontrole.Select(v => new SelectListItem { Value = v, Text = v }).ToList();
            ViewBag.Tiebreakovi = _tiebreakovi.Select(t => new SelectListItem { Value = t, Text = t }).ToList();
            ViewBag.Igraci = igraci.Select(i => new SelectListItem
            {
                Value = i.IgracID.ToString(),
                Text = $"{i.Prezime} {i.Ime} ({i.Klub})"
            }).ToList();
        }

        public async Task<IActionResult> Spisak(TurnirViewModel filter)
        {
            if (HttpContext.Session.GetString("KorisnickoIme") == null)
                return RedirectToAction("Prijava", "Nalog");

            var klijent = KreirajKlijenta();
            var url = "api/TurnirRest/filter?";

            if (filter.DatumOd.HasValue)
                url += $"datumOd={filter.DatumOd:yyyy-MM-dd}&";
            if (filter.DatumDo.HasValue)
                url += $"datumDo={filter.DatumDo:yyyy-MM-dd}&";
            if (!string.IsNullOrEmpty(filter.FilterMesto))
                url += $"mesto={filter.FilterMesto}";

            var odgovor = await klijent.GetAsync(url);
            var json = await odgovor.Content.ReadAsStringAsync();
            var turniri = JsonSerializer.Deserialize<List<TurnirDTO>>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
                ?? new List<TurnirDTO>();

            ViewBag.Turniri = turniri;
            ViewBag.KorisnickoIme = HttpContext.Session.GetString("KorisnickoIme");
            return View(filter);
        }

        public async Task<IActionResult> Unos()
        {
            if (HttpContext.Session.GetString("KorisnickoIme") == null)
                return RedirectToAction("Prijava", "Nalog");

            var igraci = await DohvatiIgrace();
            var model = new TurnirViewModel
            {
                Datum = DateTime.Today,
                Plasmani = new List<PlasmanIgracaViewModel>()
            };
            PopuniDropdowne(model, igraci);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Unos(TurnirViewModel model)
        {
            if (HttpContext.Session.GetString("KorisnickoIme") == null)
                return RedirectToAction("Prijava", "Nalog");

            if (!ModelState.IsValid)
            {
                PopuniDropdowne(model, await DohvatiIgrace());
                return View(model);
            }

            var dto = new TurnirDTO
            {
                NazivTurnira = model.NazivTurnira,
                Mesto = model.Mesto,
                Datum = model.Datum,
                Organizator = model.Organizator,
                GlavniArbitar = model.GlavniArbitar,
                NagradniFond = model.NagradniFond,
                TipTurnira = model.TipTurnira,
                FormatTakmicenja = model.FormatTakmicenja,
                BrojRundi = model.BrojRundi,
                BrojUcesnika = model.BrojUcesnika,
                VremenskaKontrola = model.VremenskaKontrola,
                TiebreakKriterijum = model.TiebreakKriterijum,
                Plasmani = model.Plasmani.Select(p => new PlasmanIgracaDTO
                {
                    IgracID = p.IgracID,
                    Mesto = p.Mesto,
                    Bodovi = p.Bodovi
                }).ToList()
            };

            var klijent = KreirajKlijenta();
            var json = JsonSerializer.Serialize(dto);
            var sadrzaj = new StringContent(json, Encoding.UTF8, "application/json");
            var odgovor = await klijent.PostAsync("api/TurnirRest", sadrzaj);

            if (!odgovor.IsSuccessStatusCode)
            {
                var greska = await odgovor.Content.ReadAsStringAsync();
                ModelState.AddModelError("", greska);
                PopuniDropdowne(model, await DohvatiIgrace());
                return View(model);
            }

            return RedirectToAction("Spisak");
        }

        public async Task<IActionResult> Detalji(int id)
        {
            if (HttpContext.Session.GetString("KorisnickoIme") == null)
                return RedirectToAction("Prijava", "Nalog");

            var klijent = KreirajKlijenta();
            var odgovor = await klijent.GetAsync($"api/TurnirRest/{id}");
            if (!odgovor.IsSuccessStatusCode)
                return NotFound();

            var json = await odgovor.Content.ReadAsStringAsync();
            var turnir = JsonSerializer.Deserialize<TurnirDTO>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return View(turnir);
        }

        public async Task<IActionResult> Izmena(int id)
        {
            if (HttpContext.Session.GetString("KorisnickoIme") == null)
                return RedirectToAction("Prijava", "Nalog");

            var klijent = KreirajKlijenta();
            var odgovor = await klijent.GetAsync($"api/TurnirRest/{id}");
            if (!odgovor.IsSuccessStatusCode)
                return NotFound();

            var json = await odgovor.Content.ReadAsStringAsync();
            var dto = JsonSerializer.Deserialize<TurnirDTO>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            var igraci = await DohvatiIgrace();
            var model = new TurnirViewModel
            {
                TurnirID = dto!.TurnirID,
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
                Plasmani = dto.Plasmani.Select(p => new PlasmanIgracaViewModel
                {
                    PlasmanID = p.PlasmanID,
                    IgracID = p.IgracID,
                    ImeIgraca = p.ImeIgraca,
                    PrezimeIgraca = p.PrezimeIgraca,
                    Klub = p.Klub,
                    Titula = p.Titula,
                    ELO = p.ELO,
                    Mesto = p.Mesto,
                    Bodovi = p.Bodovi,
                    Nagrada = p.Nagrada
                }).ToList()
            };

            PopuniDropdowne(model, igraci);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Izmena(int id, TurnirViewModel model)
        {
            if (HttpContext.Session.GetString("KorisnickoIme") == null)
                return RedirectToAction("Prijava", "Nalog");

            if (!ModelState.IsValid)
            {
                PopuniDropdowne(model, await DohvatiIgrace());
                return View(model);
            }

            var dto = new TurnirDTO
            {
                TurnirID = id,
                NazivTurnira = model.NazivTurnira,
                Mesto = model.Mesto,
                Datum = model.Datum,
                Organizator = model.Organizator,
                GlavniArbitar = model.GlavniArbitar,
                NagradniFond = model.NagradniFond,
                TipTurnira = model.TipTurnira,
                FormatTakmicenja = model.FormatTakmicenja,
                BrojRundi = model.BrojRundi,
                BrojUcesnika = model.BrojUcesnika,
                VremenskaKontrola = model.VremenskaKontrola,
                TiebreakKriterijum = model.TiebreakKriterijum,
                Plasmani = model.Plasmani.Select(p => new PlasmanIgracaDTO
                {
                    PlasmanID = p.PlasmanID,
                    IgracID = p.IgracID,
                    Mesto = p.Mesto,
                    Bodovi = p.Bodovi
                }).ToList()
            };

            var klijent = KreirajKlijenta();
            var json = JsonSerializer.Serialize(dto);
            var sadrzaj = new StringContent(json, Encoding.UTF8, "application/json");
            var odgovor = await klijent.PutAsync($"api/TurnirRest/{id}", sadrzaj);

            if (!odgovor.IsSuccessStatusCode)
            {
                var greska = await odgovor.Content.ReadAsStringAsync();
                ModelState.AddModelError("", greska);
                PopuniDropdowne(model, await DohvatiIgrace());
                return View(model);
            }

            return RedirectToAction("Spisak");
        }

        [HttpPost]
        public async Task<IActionResult> Obrisi(int id)
        {
            if (HttpContext.Session.GetString("KorisnickoIme") == null)
                return RedirectToAction("Prijava", "Nalog");

            var klijent = KreirajKlijenta();
            await klijent.DeleteAsync($"api/TurnirRest/{id}");
            return RedirectToAction("Spisak");
        }

        public async Task<IActionResult> Stampa(int? id, TurnirViewModel filter)
        {
            if (HttpContext.Session.GetString("KorisnickoIme") == null)
                return RedirectToAction("Prijava", "Nalog");

            var klijent = KreirajKlijenta();

            if (id.HasValue)
            {
                var odgovor = await klijent.GetAsync($"api/TurnirRest/{id}");
                var json = await odgovor.Content.ReadAsStringAsync();
                var turnir = JsonSerializer.Deserialize<TurnirDTO>(json,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                ViewBag.JedanTurnir = true;
                return View(new List<TurnirDTO> { turnir! });
            }
            else
            {
                var url = "api/TurnirRest/filter?";
                if (filter.DatumOd.HasValue)
                    url += $"datumOd={filter.DatumOd:yyyy-MM-dd}&";
                if (filter.DatumDo.HasValue)
                    url += $"datumDo={filter.DatumDo:yyyy-MM-dd}&";
                if (!string.IsNullOrEmpty(filter.FilterMesto))
                    url += $"mesto={filter.FilterMesto}";

                var odgovor = await klijent.GetAsync(url);
                var json = await odgovor.Content.ReadAsStringAsync();
                var turniri = JsonSerializer.Deserialize<List<TurnirDTO>>(json,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
                    ?? new List<TurnirDTO>();

                ViewBag.JedanTurnir = false;
                return View(turniri);
            }
        }
    }
}
