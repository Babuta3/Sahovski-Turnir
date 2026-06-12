using Microsoft.AspNetCore.Mvc;
using SlojPodataka.TehnoloskeKlase;
using SlojPoslovneLogike.Ogranicenja;
using SlojPoslovneLogike.Stanje;
using SlojPoslovneLogike.Validacija;
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
        private readonly PoslovnoPraviloValidator _validator;
        private readonly PrikupljanjeStanja _stanje;

        public TurnirRestKontroler(TurnirRepozitorijum repozitorijum, CitacPravila citacPravila)
        {
            _repozitorijum = repozitorijum;
            _maper = new TurnirMapper();
            _validator = new PoslovnoPraviloValidator(citacPravila);
            _stanje = new PrikupljanjeStanja(repozitorijum);
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

        [HttpGet("statistika")]
        public ActionResult<int> DohvatiStatistiku()
        {
            int ukupno = _repozitorijum.DohvatiUkupanBrojTurniraPrekoSP();
            return Ok(ukupno);
        }

        [HttpPost]
        public async Task<ActionResult> Dodaj([FromBody] TurnirDTO dto)
        {
            var turnir = _maper.UEntitet(dto);
            turnir.Plasmani = await _validator.IzracunajNagrade(
                turnir.Plasmani.ToList(), dto.NagradniFond);
            _repozitorijum.Dodaj(turnir);
            return Ok("Turnir je uspešno dodat.");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Izmeni(int id, [FromBody] TurnirDTO dto)
        {
            dto.TurnirID = id;
            var turnir = _maper.UEntitet(dto);
            turnir.Plasmani = await _validator.IzracunajNagrade(
                turnir.Plasmani.ToList(), dto.NagradniFond);
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
