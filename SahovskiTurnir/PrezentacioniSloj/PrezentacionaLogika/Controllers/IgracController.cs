using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PrezentacioniSloj.PrezentacionaLogika.ViewModels;
using SlojServisa.DTO;
using System.Text;
using System.Text.Json;

namespace PrezentacioniSloj.PrezentacionaLogika.Controllers
{
    public class IgracController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public IgracController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        private HttpClient KreirajKlijenta() =>
            _httpClientFactory.CreateClient("SahovskiApi");

        public async Task<IActionResult> Spisak()
        {
            if (HttpContext.Session.GetString("KorisnickoIme") == null)
                return RedirectToAction("Prijava", "Nalog");

            var klijent = KreirajKlijenta();
            var odgovor = await klijent.GetAsync("api/IgracRest");
            var json = await odgovor.Content.ReadAsStringAsync();
            var igraci = JsonSerializer.Deserialize<List<IgracDTO>>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
                ?? new List<IgracDTO>();

            ViewBag.KorisnickoIme = HttpContext.Session.GetString("KorisnickoIme");
            return View(igraci);
        }

        public IActionResult Unos()
        {
            if (HttpContext.Session.GetString("KorisnickoIme") == null)
                return RedirectToAction("Prijava", "Nalog");

            PopuniTitule();
            return View(new IgracViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Unos(IgracViewModel model)
        {
            if (HttpContext.Session.GetString("KorisnickoIme") == null)
                return RedirectToAction("Prijava", "Nalog");

            if (!ModelState.IsValid)
            {
                PopuniTitule();
                return View(model);
            }

            var dto = new IgracDTO
            {
                Ime = model.Ime,
                Prezime = model.Prezime,
                Klub = model.Klub,
                Titula = model.Titula,
                ELO = model.ELO
            };

            var klijent = KreirajKlijenta();
            var json = JsonSerializer.Serialize(dto);
            var sadrzaj = new StringContent(json, Encoding.UTF8, "application/json");
            var odgovor = await klijent.PostAsync("api/IgracRest", sadrzaj);

            if (!odgovor.IsSuccessStatusCode)
            {
                var greska = await odgovor.Content.ReadAsStringAsync();
                ModelState.AddModelError("", greska);
                PopuniTitule();
                return View(model);
            }

            return RedirectToAction("Spisak");
        }

        public async Task<IActionResult> Izmena(int id)
        {
            if (HttpContext.Session.GetString("KorisnickoIme") == null)
                return RedirectToAction("Prijava", "Nalog");

            var klijent = KreirajKlijenta();
            var odgovor = await klijent.GetAsync($"api/IgracRest/{id}");
            if (!odgovor.IsSuccessStatusCode)
                return NotFound();

            var json = await odgovor.Content.ReadAsStringAsync();
            var dto = JsonSerializer.Deserialize<IgracDTO>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            var model = new IgracViewModel
            {
                IgracID = dto!.IgracID,
                Ime = dto.Ime,
                Prezime = dto.Prezime,
                Klub = dto.Klub,
                Titula = dto.Titula,
                ELO = dto.ELO
            };

            PopuniTitule();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Izmena(int id, IgracViewModel model)
        {
            if (HttpContext.Session.GetString("KorisnickoIme") == null)
                return RedirectToAction("Prijava", "Nalog");

            if (!ModelState.IsValid)
            {
                PopuniTitule();
                return View(model);
            }

            var dto = new IgracDTO
            {
                IgracID = id,
                Ime = model.Ime,
                Prezime = model.Prezime,
                Klub = model.Klub,
                Titula = model.Titula,
                ELO = model.ELO
            };

            var klijent = KreirajKlijenta();
            var json = JsonSerializer.Serialize(dto);
            var sadrzaj = new StringContent(json, Encoding.UTF8, "application/json");
            var odgovor = await klijent.PutAsync($"api/IgracRest/{id}", sadrzaj);

            if (!odgovor.IsSuccessStatusCode)
            {
                var greska = await odgovor.Content.ReadAsStringAsync();
                ModelState.AddModelError("", greska);
                PopuniTitule();
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
            await klijent.DeleteAsync($"api/IgracRest/{id}");
            return RedirectToAction("Spisak");
        }

        private void PopuniTitule()
        {
            var titule = new List<string> { "Bez titule", "CM", "FM", "IM", "GM" };
            ViewBag.Titule = titule.Select(t => new SelectListItem { Value = t, Text = t }).ToList();
        }
    }
}
