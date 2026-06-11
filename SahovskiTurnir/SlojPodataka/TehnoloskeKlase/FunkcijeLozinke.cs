using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace SlojPodataka.TehnoloskeKlase
{
    public class FunkcijeLozinke
    {
        public static string Hashuj(string lozinka)
        {
            using var sha256 = SHA256.Create();
            var bajtovi = Encoding.UTF8.GetBytes(lozinka);
            var hash = sha256.ComputeHash(bajtovi);
            return Convert.ToBase64String(hash);
        }

        public static bool Proveri(string lozinka, string hash)
        {
            return Hashuj(lozinka) == hash;
        }
    }
}
