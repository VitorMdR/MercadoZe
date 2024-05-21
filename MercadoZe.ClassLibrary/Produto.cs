using System;
namespace MercadoZe.Classes
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public DateTime DataVencimento { get; set; }
        public decimal Preco { get; set; }
        public string Unidade { get; set; }
        public int QuantidadeEmEstoque { get; set; }

        public Produto()
        {

        }

        

    }
}