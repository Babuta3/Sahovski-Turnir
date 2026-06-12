using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace SlojPoslovneLogike.Ogranicenja
{
    public class CitacPravila
    {
        private readonly HttpClient _httpKlijent;
        private const string _urlServisa = "http://localhost:5072/api/PravilaRest";

        public CitacPravila(HttpClient httpKlijent)
        {
            _httpKlijent = httpKlijent;
        }

        public async Task<int> DohvatiBrojNagradjeniхMesta()
        {
            var pravila = await _httpKlijent.GetFromJsonAsync<PravilaModel>(_urlServisa);
            return pravila?.BrojNagradjeniхMesta ?? 3;
        }
    }

    public class PravilaModel
    {
        public int BrojNagradjeniхMesta { get; set; }
    }
}
