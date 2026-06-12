using Microsoft.AspNetCore.Mvc;
using PrezentacioniSloj.PrezentacionaLogika.ViewModels;
using SlojServisa.DTO;
using System.Text;
using System.Text.Json;

namespace PrezentacioniSloj.PrezentacionaLogika.Controllers
{
    public class NalogController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public NalogController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        private HttpClient KreirajKlijenta() =>
            _httpClientFactory.CreateClient("SahovskiApi");

        [HttpGet]
        public IActionResult Prijava()
        {
            return View(new PrijavaViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Prijava(PrijavaViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var dto = new PrijavaDTO
            {
                KorisnickoIme = model.KorisnickoIme,
                Lozinka = model.Lozinka
            };

            var klijent = KreirajKlijenta();
            var json = JsonSerializer.Serialize(dto);
            var sadrzaj = new StringContent(json, Encoding.UTF8, "application/json");
            var odgovor = await klijent.PostAsync("api/KorisnikRest/prijava", sadrzaj);

            if (!odgovor.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Pogrešno korisničko ime ili lozinka.");
                return View(model);
            }

            HttpContext.Session.SetString("KorisnickoIme", model.KorisnickoIme);
            return RedirectToAction("Spisak", "Turnir");
        }

        public IActionResult Odjava()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Prijava");
        }
    }
}
