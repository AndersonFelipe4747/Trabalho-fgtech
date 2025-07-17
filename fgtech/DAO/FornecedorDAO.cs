using fgtech.Modelo;
using fgtech.BD;
using fgtech.Utilitario;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fgtech.DAO
{
    internal class FornecedorDAO
    {
        public void Inserir(Fornecedor f)
        {
            using (var conn = Conexao.ObterConexao())
            {
                conn.Open();

                string sql = "INSERT INTO Fornecedor (nome_for, cnpj_for, telefone_for) VALUES (@nome, @cnpj, @tel)";
                var cmd = new MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@nome", f.Nome);
                cmd.Parameters.AddWithValue("@cnpj", f.CNPJ);
                cmd.Parameters.AddWithValue("@tel", f.Telefone);
                cmd.ExecuteNonQuery();
            }
        }

        public List<Fornecedor> ListarTodos()
        {
            var lista = new List<Fornecedor>();
            using (var conn = Conexao.ObterConexao())
            {
                conn.Open();
                string sql = "SELECT * FROM Fornecedor";
                var cmd = new MySqlCommand(sql, conn);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(new Fornecedor
                    {
                        Id = reader.GetInt32("id_fornecedor"),
                        Nome = reader.GetString("nome_for"),
                        CNPJ = reader.GetString("cnpj_for"),
                        Telefone = reader.GetString("telefone_for")
                    });
                }
            }
            return lista;
        }

        public void Atualizar(Fornecedor f)
        {
            using (var conn = Conexao.ObterConexao())
            {
                conn.Open();

                string sql = "UPDATE Fornecedor SET nome_for = @nome, cnpj_for = @cnpj, telefone_for = @tel WHERE id_fornecedor = @id";
                var cmd = new MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@nome", f.Nome);
                cmd.Parameters.AddWithValue("@cnpj", f.CNPJ);
                cmd.Parameters.AddWithValue("@tel", f.Telefone);
                cmd.Parameters.AddWithValue("@id", f.Id);
                cmd.ExecuteNonQuery();
            }
        }

        public void Deletar(int id)
        {
            using (var conn = Conexao.ObterConexao())
            {
                conn.Open();

                string sql = "DELETE FROM Fornecedor WHERE id_fornecedor = @id";
                var cmd = new MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }

        public Fornecedor BuscarPorId(int id)
        {
            using (var conn = Conexao.ObterConexao())
            {
                conn.Open();
                string sql = "SELECT * FROM Fornecedor WHERE id_fornecedor = @id";
                var cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);
                var reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    return new Fornecedor
                    {
                        Id = reader.GetInt32("id_fornecedor"),
                        Nome = reader.GetString("nome_for"),
                        CNPJ = reader.GetString("cnpj_for"),
                        Telefone = reader.GetString("telefone_for")
                    };
                }
            }
            return null; //não achou
        }
    }
}
