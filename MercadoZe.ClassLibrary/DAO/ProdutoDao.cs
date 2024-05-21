using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace MercadoZe.ConsoleApp.Descricao
{
    public class ProdutoDao
    {
        private const string _connectionString = @"Data Source=.\SQLexpress;initial catalog=MercadoDoSeuZeDB;uid=sa;pwd=asd;";
        public ProdutoDao()
        {

        }

        public void AdicionarProduto(Produto novoProduto)
        {
            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();

                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;

                    string sql = @"INSERT ALUNO VALUES (@NOME, @DESCRICAO, @VALIDADE, @PRECO, @UNIDADE, @QTDESTOQUE);";

                    ConverteObjetoParaSql(novoProduto, comando);

                    comando.CommandText = sql;

                    comando.ExecuteNonQuery();
                }
            }
        }

        private void ConverteObjetoParaSql(Produto novoProduto, SqlCommand comando)
        {
            comando.Parameters.AddWithValue("@NOME", novoProduto.nome);
            comando.Parameters.AddWithValue("@DESCRICAO", novoProduto.descricao);
            comando.Parameters.AddWithValue("@VALIDADE", novoProduto.validade);
            comando.Parameters.AddWithValue("@PRECO", novoProduto.preco);
            comando.Parameters.AddWithValue("@UNIDADE", novoProduto.unidade);
            comando.Parameters.AddWithValue("@QTDESTOQUE", novoProduto.qtdEstoque);
        }

        private Produto ConverterSqlParaObejto(SqlDataReader leitor)
        {
            Produto produtoBuscado = new Aluno();


            var ID_PRODUTO = reader["ID_PRODUTO"];
            var nome = reader["NOME"];
            var descricao = reader["DESCRICAO"];
            var DATA_VENCIMENTO = reader["DATA_VENCIMENTO"];
            var PRECO_UNITARIO = reader["PRECO_UNITARIO"];
            var unidade = reader["UNIDADE"];
            var QUANTIDADE_ESTOQUE = reader["QUANTIDADE_ESTOQUE"];


            alunoBuscado.Nome = leitor["NOME"].ToString();
            alunoBuscado.Matricula = int.Parse(leitor["MATRICULA"].ToString());
            alunoBuscado.Idade = int.Parse(leitor["IDADE"].ToString());

            return alunoBuscado;
        }

    }
}