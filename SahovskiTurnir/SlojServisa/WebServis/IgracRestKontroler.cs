using Microsoft.AspNetCore.Mvc;
using SlojPodataka.TehnoloskeKlase;
using SlojServisa.DTO;
using SlojServisa.KlaseMapiranja;

namespace SlojServisa.WebServis
{
    [ApiController]
    [Route("api/[controller]")]
    public class IgracRestKontroler : ControllerBase
    {
        private readonly IgracRepozitorijum _repozitorijum;
        private readonly IgracRepoDBUtils _dbUtils;
        private readonly IgracMapper _maper;

        public IgracRestKontroler(IgracRepozitorijum repozitorijum)
        {
            _repozitorijum = repozitorijum;
            _dbUtils = new IgracRepoDBUtils();
            _maper = new IgracMapper();
        }

        [HttpGet]
        public ActionResult<List<IgracDTO>> DohvatiSve()
        {
            var igraci = _repozitorijum.DohvatiSve();
            return Ok(_maper.UListuDTO(igraci));
        }

        [HttpGet("{id}")]
        public ActionResult<IgracDTO> DohvatiPoId(int id)
        {
            var igrac = _repozitorijum.DohvatiPoId(id);
            if (igrac == null)
                return NotFound($"Igrač sa ID-em {id} nije pronađen.");
            return Ok(_maper.UDTO(igrac));
        }

        [HttpGet("broj")]
        public ActionResult<int> DohvatiBroj()
        {
            int broj = _dbUtils.IzbrojIgrace();
            return Ok(broj);
        }

        [HttpGet("imena")]
        public ActionResult<List<string>> DohvatiImena()
        {
            var imena = _dbUtils.DohvatiImenaIgraca();
            return Ok(imena);
        }

        [HttpGet("statistika")]
        public ActionResult<int> DohvatiStatistiku()
        {
            int ukupno = _repozitorijum.DohvatiUkupanBrojIgracaPrekoSP();
            return Ok(ukupno);
        }

        [HttpPost]
        public ActionResult Dodaj([FromBody] IgracDTO dto)
        {
            var igrac = _maper.UEntitet(dto);
            _repozitorijum.Dodaj(igrac);
            return Ok("Igrač je uspešno dodat.");
        }

        [HttpPut("{id}")]
        public ActionResult Izmeni(int id, [FromBody] IgracDTO dto)
        {
            dto.IgracID = id;
            var igrac = _maper.UEntitet(dto);
            _repozitorijum.Izmeni(igrac);
            return Ok("Igrač je uspešno izmenjen.");
        }

        [HttpDelete("{id}")]
        public ActionResult Obrisi(int id)
        {
            _repozitorijum.Obrisi(id);
            return Ok("Igrač je uspešno obrisan.");
        }
    }
}
