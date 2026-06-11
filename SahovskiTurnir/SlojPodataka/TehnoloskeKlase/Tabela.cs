using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlojPodataka.TehnoloskeKlase
{
    public abstract class Tabela
    {
        protected string _nizKonekcije = Konekcija.NizKonekcije;

        public DataTable IzvrsiUpit(string sql)
        {
            using (SqlConnection konekcija = new SqlConnection(_nizKonekcije))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(sql, konekcija);
                DataTable tabela = new DataTable();
                adapter.Fill(tabela);
                return tabela;
            }
        }
    }
}
