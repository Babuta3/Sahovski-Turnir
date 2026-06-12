using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace SlojServisa.WebServis
{
    [ApiController]
    [Route("api/[controller]")]
    public class PravilaRestKontroler : ControllerBase
    {
        [HttpGet]
        public IActionResult DohvatiPravila()
        {
            try
            {
                var putanja = Path.Combine(AppContext.BaseDirectory, "Ogranicenja", "pravila_nagrade.xml");
                var xml = XDocument.Load(putanja);

                return Ok(new
                {
                    BrojNagradjeniхMesta = int.Parse(xml.Root!.Element("BrojNagradjeniхMesta")!.Value),
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Greška pri čitanju XML-a: {ex.Message}");
            }
        }
    }
}
