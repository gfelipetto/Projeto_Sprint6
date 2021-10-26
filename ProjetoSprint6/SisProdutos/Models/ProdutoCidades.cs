using System;

namespace SisProdutos.Models
{
    public class ProdutoCidades
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Estado { get; set; }
        public Guid ProdutoId { get; set; }

        public Produtos Produto { get; set; }
    }
}
