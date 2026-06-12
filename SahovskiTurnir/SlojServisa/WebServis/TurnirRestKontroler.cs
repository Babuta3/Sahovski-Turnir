using Microsoft.AspNetCore.Mvc;
using SlojPodataka.TehnoloskeKlase;
using SlojServisa.DTO;
using SlojServisa.KlaseMapiranja;

namespace SlojServisa.WebServis
{
    [ApiController]
    [Route("api/[controller]")]
    public class TurnirRestKontroler : ControllerBase
    {
        private readonly TurnirRepozitorijum _repozitorijum;
        private readonly TurnirMapper _maper;

        public TurnirRestKontroler(TurnirRepozitorijum repozitorijum)
        {
            _repozitorijum = repozitorijum;
            _maper = new TurnirMapper();
        }

        [HttpGet]
        public ActionResult<List<TurnirDTO>> DohvatiSve()
        {
            var turniri = _repozitorijum.DohvatiSve();
            return Ok(_maper.UListuDTO(turniri));
        }

        [HttpGet("{id}")]
        public ActionResult<TurnirDTO> DohvatiPoId(int id)
        {
            var turnir = _repozitorijum.DohvatiPoId(id);
            if (turnir == null)
                return NotFound($"Turnir sa ID-em {id} nije pronađen.");
            return Ok(_maper.UDTO(turnir));
        }

        [HttpGet("filter")]
        public ActionResult<List<TurnirDTO>> Filtriraj(
            [FromQuery] DateTime? datumOd,
            [FromQuery] DateTime? datumDo,
            [FromQuery] string? mesto)
        {
            var turniri = _repozitorijum.Filtriraj(datumOd, datumDo, mesto);
            return Ok(_maper.UListuDTO(turniri));
        }

        [HttpPost]
        public ActionResult Dodaj([FromBody] TurnirDTO dto)
        {
            var turnir = _maper.UEntitet(dto);
            _repozitorijum.Dodaj(turnir);
            return Ok("Turnir je uspešno dodat.");
        }

        [HttpPut("{id}")]
        public ActionResult Izmeni(int id, [FromBody] TurnirDTO dto)
        {
            dto.TurnirID = id;
            var turnir = _maper.UEntitet(dto);
            _repozitorijum.Izmeni(turnir);
            return Ok("Turnir je uspešno izmenjen.");
        }

        [HttpDelete("{id}")]
        public ActionResult Obrisi(int id)
        {
            _repozitorijum.Obrisi(id);
            return Ok("Turnir je uspešno obrisan.");
        }
    }
}
