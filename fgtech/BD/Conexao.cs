using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fgtech.BD
{
        public class Conexao
        {

            private static string stringConexao = $"server=localhost;uid=root;pwd=root;port=3306;database=fg_tech";

            public static MySqlConnection ObterConexao()
            {
                return new MySqlConnection(stringConexao);
            }
        }
}