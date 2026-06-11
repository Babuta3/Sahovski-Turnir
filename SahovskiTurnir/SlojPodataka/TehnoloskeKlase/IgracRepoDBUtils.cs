using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlojPodataka.TehnoloskeKlase
{
    public class IgracRepoDBUtils : Tabela
    {
        public int IzbrojIgrace()
        {
            var tabela = IzvrsiUpit("SELECT COUNT(*) FROM Igrac");
            return (int)tabela.Rows[0][0];
        }

        public List<string> DohvatiImenaIgraca()
        {
            var tabela = IzvrsiUpit("SELECT Ime + ' ' + Prezime FROM Igrac ORDER BY Prezime");
            var rezultat = new List<string>();
            foreach (DataRow red in tabela.Rows)
                rezultat.Add(red[0].ToString()!);
            return rezultat;
        }
    }
}
