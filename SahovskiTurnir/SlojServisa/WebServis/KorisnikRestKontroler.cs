using Microsoft.AspNetCore.Mvc;
using SlojPodataka.KlasePodataka;
using SlojPodataka.Repozitorijumi;
using SlojPodataka.TehnoloskeKlase;
using SlojServisa.DTO;

namespace SlojServisa.WebServis
{
    [ApiController]
    [Route("api/KorisnikRest")]
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

        [HttpPost("registracija")]
        public ActionResult Registracija([FromBody] PrijavaDTO dto)
        {
            if (_repozitorijum.PostojiKorisnik(dto.KorisnickoIme))
                return BadRequest("Korisničko ime već postoji.");

            var korisnik = new Korisnik
            {
                KorisnickoIme = dto.KorisnickoIme,
                LozinkaHash = FunkcijeLozinke.Hashuj(dto.Lozinka)
            };

            _repozitorijum.Dodaj(korisnik);
            return Ok("Korisnik je uspešno kreiran.");
        }
    }
}
