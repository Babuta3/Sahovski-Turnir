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
            byte[] salt = new byte[16];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(salt);

            var bajtovi = Encoding.UTF8.GetBytes(lozinka);
            var kombinovano = salt.Concat(bajtovi).ToArray();

            using var sha256 = SHA256.Create();
            var hash = sha256.ComputeHash(kombinovano);

            return Convert.ToBase64String(salt) + ":" + Convert.ToBase64String(hash);
        }

        public static bool Proveri(string lozinka, string sacuvaniHash)
        {
            var delovi = sacuvaniHash.Split(':');
            var salt = Convert.FromBase64String(delovi[0]);
            var originalniHash = delovi[1];

            var bajtovi = Encoding.UTF8.GetBytes(lozinka);
            var kombinovano = salt.Concat(bajtovi).ToArray();

            using var sha256 = SHA256.Create();
            var hash = Convert.ToBase64String(sha256.ComputeHash(kombinovano));

            return hash == originalniHash;
        }
    }
}
