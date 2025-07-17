using fgtech.BD;
using fgtech.DAO;
using fgtech.Modelo;
using fgtech.Utilitario;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using System.Threading;

namespace FGTechConsole
{
    class Program
    {
        static FornecedorDAO dao = new FornecedorDAO();

        static void Main(string[] args)
        {
            int opcao;
            do
            {
                Console.Clear();
                Console.WriteLine("---- FG TECH ----");
                Console.WriteLine("\n--- MENU FORNECEDOR ---");
                Console.WriteLine("1. INSERIR FORNECEDOR");
                Console.WriteLine("2. LISTAR FORNECEDORES"); //assim que listar os fornecedores, terá acesso as informações (O cnpj tem validação de formato)
                Console.WriteLine("3. ATUALIZAR FORNECEDOR");
                Console.WriteLine("4. DELETAR FORNECEDOR");
                Console.WriteLine("0. SAIR");
                Console.Write("ESCOLHA: ");

                opcao = int.Parse(Console.ReadLine());

                switch (opcao)
                {
                    case 1: Inserir(); 
                        break;
                    case 2: Listar(); 
                        break;
                    case 3: Atualizar(); 
                        break;
                    case 4: Deletar();
                        break;

                        default:
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("FG_TECH - A CONFIANÇA ESTÁ AQUI!");
                        Console.WriteLine("ANDERSON FELIPE GARCIA LOPES");
                        Console.ResetColor();
                        Thread.Sleep(1500);
               break;
                }
            } 
            while (opcao != 0);
        }

        static void Inserir()
        {
            Console.Write("NOME: ");
            string nome = Console.ReadLine();

            Console.Write("CNPJ: "); //tipo de cnpj válido 05.570.714/0001-59 (peguei da kabum)
            string cnpj = Console.ReadLine();
            if (!Validacao.ValidarCNPJ(cnpj))
            {
                Console.ForegroundColor = ConsoleColor.Red; //aqui é a cor referente a validação do cnpj
                Console.WriteLine("CNPJ INVÁLIDO!");
                Console.ResetColor();
                Thread.Sleep(2000); //aqui também, 2 segundos
                return;
            }

            Console.Write("TELEFONE: "); // tipo de telefone 3421-9324
            string tel = Console.ReadLine();

            dao.Inserir(new Fornecedor { Nome = nome, CNPJ = cnpj, Telefone = tel });

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("FORNECEDOR INSERIDO COM SUCESSO!");
            Console.ResetColor();

            Thread.Sleep(2000); // Espera 2 segundos para o usuário ver a mensagem
            if (!Confirmar()) return;
        }

        static void Listar()
        {
            Console.Clear();
            List<Fornecedor> lista = dao.ListarTodos();
            Console.WriteLine("\n--- FORNECEDORES CADASTRADOS ---");
            foreach (var f in lista)
            {
                Console.WriteLine($"ID: {f.Id} | Nome: {f.Nome} | CNPJ: {f.CNPJ} | Telefone: {f.Telefone}");
            }

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\nTotal: {lista.Count} fornecedor(es) listado(s).");
            Console.ResetColor();

            Thread.Sleep(2000); // Espera 2 segundos para o usuário ver a mensagem
            if (!Confirmar()) return;
        }

        static void Atualizar()
        {
            Console.Clear();
            Console.Write("ID DO FORNECEDOR: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ID INVÁLIDO!"); //validar id 
                Console.ResetColor();
                Thread.Sleep(2000);
                return;
            }

            var fornecedor = dao.BuscarPorId(id); //busca pelo id, se o id não tiver no banco de dados, vai retornar a mensagem de erro.
            if (fornecedor == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("O FORNECEDOR COM ESSE ID NÃO FOI ENCONTRADO!");
                Console.ResetColor();
                Thread.Sleep(2000);
                return;
            }

            Console.Write("NOVO NOME: ");
            string nome = Console.ReadLine();

            Console.Write("NOVO CNPJ: ");
            string cnpj = Console.ReadLine();
            if (!Validacao.ValidarCNPJ(cnpj))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("CNPJ INVÁLIDO!");
                Console.ResetColor();
                Thread.Sleep(2000);
                return;
            }

            Console.Write("NOVO TELEFONE: ");
            string tel = Console.ReadLine();

            dao.Atualizar(new Fornecedor { Id = id, Nome = nome, CNPJ = cnpj, Telefone = tel });

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\nO fornecedor id ({id}) com nome ({nome}) foi atualizado com sucesso.");
            Console.ResetColor();

            Thread.Sleep(2500); //Espera 2,5 segundos para o usuário ver a mensagem
            if (!Confirmar()) return;
        }


        static void Deletar()
        {
            Console.Clear();
            Console.Write("ID DO FORNECEDOR A DELETAR:  ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ID INVÁLIDO!");
                Console.ResetColor();
                Thread.Sleep(2000);
                return;
            }

            var fornecedor = dao.BuscarPorId(id);
            if (fornecedor == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ESSE ID NÃO CONSTA NO BANCO DE DADOS!"); //busca pelo id, se o id não tiver no banco de dados, vai retornar a mensagem de erro.
                Console.ResetColor();
                Thread.Sleep(2000);
                return;
            }

            Console.WriteLine($"\nDeseja realmente deletar o fornecedor: {fornecedor.Nome} (ID: {fornecedor.Id})?");
            Console.Write("DIGITE S/N PARA CONFIRMAR: ");
            var confirm = Console.ReadKey().KeyChar.ToString().ToUpper();

            if (confirm != "S")
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nOperação cancelada."); //não apaga o fornecedor
                Console.ResetColor();
                Thread.Sleep(2000);
                return;
            }

            dao.Deletar(id);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nO fornecedor id ({id}) com nome ({fornecedor.Nome}) foi deletado."); //apaga o fornecedor
            Console.ResetColor();

            Thread.Sleep(2500); //Espera 2,5 segundos para o usuário ver a mensagem
            if (!Confirmar()) return;

        }
        static bool Confirmar()
        {
            Console.Write("\nDESEJA CONTINUAR? (S/N): ");
            var tecla = Console.ReadKey().KeyChar;
            return tecla.ToString().ToUpper() == "S";
        }
        public Fornecedor BuscarPorId(int id) //busca pelo id, se o id não tiver no banco de dados, vai retornar a mensagem de erro.
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
            return null;
        }
    }
}