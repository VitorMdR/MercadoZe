using System;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace MercadoZe.ConsoleApp
{
    class Program
    {
        static SqlConnection _connection = new SqlConnection();
        static Produto _produtoDao = new ProdutoDao();
        static List<Produto> _listaProduto = new List<Produto>();
        static Produto _produtoBuscado = new Produto();

        static void Main(string[] args)
        {
            MenuPrincipal();
        }
        public static void MenuPrincipal()
        {
            Console.Clear();
            System.Console.WriteLine("====================== MERCADO DO SEU ZÉ =================");
            System.Console.WriteLine("==========================================================");
            System.Console.WriteLine("============== [1] - ADICIONAR PRODUTOS ==================");
            System.Console.WriteLine("============== [2] - EDITAR PRODUTOS =====================");
            System.Console.WriteLine("============== [3] - DELETAR PRODUTOS ====================");
            System.Console.WriteLine("============== [4] - CONSULTAR REGISTROS =================");
            System.Console.WriteLine("============== [5] - BUSCAR PRODUTO PELA IDENTIFICAÇÃO ===");
            System.Console.WriteLine("============== [6] - BUSCAR PRODUTO PELA DESCRIÇÃO =======");
            System.Console.WriteLine("============== [7] - SAIR ================================");

            string escolha = Console.ReadLine();

            switch (escolha)
            {
                case "1":
                    Console.Clear();
                    AdicionaProduto();
                    MenuPrincipal();
                    break;

                case "2":
                    Console.Clear();
                    AtualizarRegistro();
                    MenuPrincipal();
                    break;
                case "3":
                    Console.Clear();
                    DeletarRegistro();
                    MenuPrincipal();
                    break;
                case "4":
                    Console.Clear();
                    ConsultarRegistros();
                    MenuPrincipal();
                    break;
                case "5":
                    Console.Clear();
                    BuscaIdentificador();
                    MenuPrincipal();
                    break;
                case "6":
                    Console.Clear();
                    BuscaDescricao();
                    MenuPrincipal();
                    break;
                case "7":
                    Console.Clear();
                    System.Console.WriteLine("APERTE QUALQUER TECLA PARA SAIR");
                    Console.ReadKey();
                    Environment.Exit(1);
                    break;

                default:
                    Console.Clear();
                    System.Console.WriteLine("Opção inválida, aperte qualquer tecla para voltar.");
                    Console.ReadKey();
                    MenuPrincipal();
                    break;
            }
        }
        public static void DeletarRegistro()
        {
            Console.WriteLine("Digite o ID do produto que deseja deletar");
            var idProduto = Convert.ToInt32(Console.ReadLine());

            System.Console.WriteLine("================================================================");
            System.Console.WriteLine("=============== O PRODUTO SERÁ DELETADO: =======================");
            System.Console.WriteLine("================================================================");

            SqlCommand comando = new SqlCommand();
            comando.Connection = _connection;

            comando.CommandText = @"DELETE FROM Produto WHERE ID_PRODUTO = @ID_PRODUTO";
            comando.Parameters.AddWithValue("@ID_PRODUTO", idProduto);


            Connect();
            SqlDataReader reader = comando.ExecuteReader();


            while (reader.Read())
            {
                var ID_PRODUTO = reader["ID_PRODUTO"];
                var nome = reader["NOME"];
                var descricao = reader["DESCRICAO"];
                var DATA_VENCIMENTO = reader["DATA_VENCIMENTO"];
                var PRECO_UNITARIO = reader["PRECO_UNITARIO"];
                var unidade = reader["UNIDADE"];
                var QUANTIDADE_ESTOQUE = reader["QUANTIDADE_ESTOQUE"];
                System.Console.WriteLine($"ID - {ID_PRODUTO} | PRODUTO - {nome} | DESCRICAO - {descricao} | VENCIMENTO - {DATA_VENCIMENTO} | VALOR - {PRECO_UNITARIO} | UNIDADE - {unidade} | ESTOQUE = {QUANTIDADE_ESTOQUE}");
                System.Console.WriteLine("========================================================================================================================================================");
            }
            comando.ExecuteNonQuery();

            Disconnect();

        }
        public static void BuscaIdentificador()
        {
            Console.Clear();
            System.Console.WriteLine("================================================================");
            System.Console.WriteLine("==================== DIGITE O ID DO PRODUTO ====================");
            System.Console.WriteLine("================================================================");
            int typedID = int.Parse(Console.ReadLine());



            SqlCommand searchCommand = new SqlCommand();
            searchCommand.Connection = _connection;
            searchCommand.CommandText = @"SELECT * FROM PRODUTO WHERE ID = @IdProduto";
            searchCommand.Parameters.AddWithValue("@IdProduto", typedID);

            Connect();

            SqlDataReader reader = searchCommand.ExecuteReader();

            System.Console.WriteLine("================================================================");
            System.Console.WriteLine("=============== TODOS OS PRODUTOS CADASTRADOS ==================");
            System.Console.WriteLine("================================================================");
            while (reader.Read())
            {
                var ID_PRODUTO = reader["ID_PRODUTO"];
                var nome = reader["NOME"];
                var descricao = reader["DESCRICAO"];
                var DATA_VENCIMENTO = reader["DATA_VENCIMENTO"];
                var PRECO_UNITARIO = reader["PRECO_UNITARIO"];
                var unidade = reader["UNIDADE"];
                var QUANTIDADE_ESTOQUE = reader["QUANTIDADE_ESTOQUE"];
                System.Console.WriteLine($"ID - {ID_PRODUTO} | PRODUTO - {nome} | DESCRICAO - {descricao} | VENCIMENTO - {DATA_VENCIMENTO} | VALOR - {PRECO_UNITARIO} | UNIDADE - {unidade} | ESTOQUE = {QUANTIDADE_ESTOQUE}");
                System.Console.WriteLine("========================================================================================================================================================");
            }

            Disconnect();
            Console.ReadKey();
        }
        public static void BuscaDescricao()
        {
            Console.Clear();

            System.Console.WriteLine("================================================================");
            System.Console.WriteLine("==================== DIGITE A DESCRIÇÃO DO PRODUTO ====================");
            System.Console.WriteLine("================================================================");
            var Descricao = Console.ReadLine();



            SqlCommand searchCommand = new SqlCommand();
            searchCommand.Connection = _connection;
            searchCommand.CommandText = @"SELECT * FROM PRODUTO WHERE DESCRICAO LIKE '%@DESCRICAO%'";
            searchCommand.Parameters.AddWithValue("@DESCRICAO", Descricao);

            Connect();

            SqlDataReader reader = searchCommand.ExecuteReader();

            System.Console.WriteLine("================================================================");
            System.Console.WriteLine("=============== TODOS OS PRODUTOS CADASTRADOS ==================");
            System.Console.WriteLine("================================================================");
            while (reader.Read())
            {
                var ID_PRODUTO = reader["ID_PRODUTO"];
                var nome = reader["NOME"];
                var descricao = reader["DESCRICAO"];
                var DATA_VENCIMENTO = reader["DATA_VENCIMENTO"];
                var PRECO_UNITARIO = reader["PRECO_UNITARIO"];
                var unidade = reader["UNIDADE"];
                var QUANTIDADE_ESTOQUE = reader["QUANTIDADE_ESTOQUE"];
                System.Console.WriteLine($"ID - {ID_PRODUTO} | PRODUTO - {nome} | DESCRICAO - {descricao} | VENCIMENTO - {DATA_VENCIMENTO} | VALOR - {PRECO_UNITARIO} | UNIDADE - {unidade} | ESTOQUE = {QUANTIDADE_ESTOQUE}");
                System.Console.WriteLine("========================================================================================================================================================");
            }

            Disconnect();
        }
        public static void AtualizarRegistro()
        {
            System.Console.WriteLine("================================================================");
            System.Console.WriteLine("==================== ATUALIZAR PRODUTO ====================");
            System.Console.WriteLine("================================================================");

            Console.WriteLine("Digite o ID do produto que deseja alterar:");
            var idProduto = Console.ReadLine();

            Console.WriteLine("Digite o nome do produto:");
            var nome = Console.ReadLine();

            Console.WriteLine("Digite a descrião do produto:");
            var descricao = Console.ReadLine();

            Console.WriteLine("Digite a validade do produto:");
            var validade = Console.ReadLine();

            Console.WriteLine("Digite o valor do produto:");
            var preco = Console.ReadLine();

            Console.WriteLine("Digite unidade do produto:");
            var unidade = Console.ReadLine();

            Console.WriteLine("Digite a quantidade em estoque:");
            var qtdEstoque = Console.ReadLine();

            SqlCommand updateCommand = new SqlCommand();
            updateCommand.Connection = _connection;

            updateCommand.Parameters.AddWithValue("@ID", idProduto);
            updateCommand.Parameters.AddWithValue("@NOME", nome);
            updateCommand.Parameters.AddWithValue("@DESCRICAO", descricao);
            updateCommand.Parameters.AddWithValue("@DATAVENCIMENTO", validade);
            updateCommand.Parameters.AddWithValue("@PRECO", preco);
            updateCommand.Parameters.AddWithValue("@UNIDADE", unidade);
            updateCommand.Parameters.AddWithValue("@ESTOQUE", qtdEstoque);
            updateCommand.CommandText = @"UPDATE PRODUTO SET NOME = @NOME, DESCRICAO = @DESCRICAO, DATA_VENCIMENTO = @DATAVENCIMENTO, PRECO = @PRECO, UNIDADE = @UNIDADE, QUANTIDADE_ESTOQUE = @ESTOQUE WHERE ID_PRODUTO = @ID";

            Connect();
            updateCommand.ExecuteNonQuery();
            Disconnect();
        }
        public static void ConsultarRegistros()
        {
            Console.Clear();



            SqlCommand readCommand = new SqlCommand();
            readCommand.Connection = _connection;
            readCommand.CommandText = @"SELECT * FROM PRODUTO ";

            Connect();

            SqlDataReader reader = readCommand.ExecuteReader();

            while (reader.Read())
            {
                var ID_PRODUTO = reader["ID_PRODUTO"];
                var nome = reader["NOME"];
                var descricao = reader["DESCRICAO"];
                var DATA_VENCIMENTO = reader["DATA_VENCIMENTO"];
                var PRECO_UNITARIO = reader["PRECO_UNITARIO"];
                var unidade = reader["UNIDADE"];
                var QUANTIDADE_ESTOQUE = reader["QUANTIDADE_ESTOQUE"];
                System.Console.WriteLine($"ID - {ID_PRODUTO} | PRODUTO - {nome} | DESCRICAO - {descricao} | VENCIMENTO - {DATA_VENCIMENTO} | VALOR - {PRECO_UNITARIO} | UNIDADE - {unidade} | ESTOQUE = {QUANTIDADE_ESTOQUE}");
                System.Console.WriteLine("========================================================================================================================================================");
            }

            Disconnect();

            Console.ReadKey();
        }

        private static void AdicionaProduto()
        {
            System.Console.WriteLine("================================================================");
            System.Console.WriteLine("==================== ADICIONAR PRODUTO ====================");
            System.Console.WriteLine("================================================================");

            var novoProduto = new Produto();
            Console.WriteLine("Digite o nome do produto:");
            novoProduto.nome = Console.ReadLine();

            Console.WriteLine("Digite a descrião do produto:");
            novoProduto.descricao = Console.ReadLine();

            Console.WriteLine("Digite a validade do produto:");
            novoProduto.validade = Console.ReadLine();

            Console.WriteLine("Digite o valor do produto:");
            novoProduto.preco = Console.ReadLine();

            Console.WriteLine("Digite unidade do produto:");
            novoProduto.unidade = Console.ReadLine();

            Console.WriteLine("Digite a quantidade em estoque:");
            novoProduto.qtdEstoque = Console.ReadLine();
            
            
            _produtoDao.AdicionaProduto(novoProduto)

            
        }


        public static void Connect()
        {
            var conectionString = @"Data Source=.\SQLexpress;initial catalog=MercadoDoSeuZeDB;uid=sa;pwd=asd;";

            _connection.ConnectionString = conectionString;

            _connection.Open();
        }
        public static void Disconnect()
        {
            _connection.Close();
        }
    }
}
