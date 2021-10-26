using System;
using System.Collections.Generic;

namespace SisProdutos.Models
{
    public class Produtos
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal Preco { get; set; }


        public IList<ProdutoCidades> Cidades { get; set; }
        public IList<PalavrasChave> PalavrasChave { get; set; }
        public IList<Categorias> Categorias { get; set; }
        public Produtos()
        {
            Cidades = new List<ProdutoCidades>();
            PalavrasChave = new List<PalavrasChave>();
            Categorias = new List<Categorias>();
        }
    }
}
