using Microsoft.AspNetCore.Mvc;
using SlojPodataka.TehnoloskeKlase;
using SlojServisa.DTO;

namespace SlojServisa.WebServis
{
    [ApiController]
    [Route("api/[controller]")]
    public class KorisnikRestKontroler : ControllerBase
    {
        private readonly KorisnikRepozitorijum _repozitorijum;

        public KorisnikRestKontroler(KorisnikRepozitorijum repozitorijum)
        {
            _repozitorijum = repozitorijum;
        }

        [HttpPost("prijava")]
        public ActionResult Prijava([FromBody] PrijavaDTO dto)
        {
            var korisnik = _repozitorijum.DohvatiPoKorisnickomImenu(dto.KorisnickoIme);

            if (korisnik == null)
                return Unauthorized("Korisničko ime nije pronađeno.");

            if (!FunkcijeLozinke.Proveri(dto.Lozinka, korisnik.LozinkaHash))
                return Unauthorized("Lozinka nije ispravna.");

            return Ok("Prijava uspešna.");
        }
    }
}
