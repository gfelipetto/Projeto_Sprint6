using System;

namespace SisProdutos.Models
{
    public class Categorias
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public Guid ProdutoId { get; set; }

        public Produtos Produto { get; set; }
    }
}
