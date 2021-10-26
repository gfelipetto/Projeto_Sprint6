using SisProdutos.Data.Dtos.Categorias;
using SisProdutos.Data.Dtos.Cidade;
using SisProdutos.Data.Dtos.PalavrasChave;
using System;
using System.Collections.Generic;

namespace SisProdutos.Data.Dtos.Produto
{
    public class LerProdutoDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal Preco { get; set; }

        public IList<LerCidadeDto> Cidades { get; set; }
        public IList<LerPalavrasChaveDto> PalavrasChave { get; set; }
        public IList<LerCategoriasDto> Categorias { get; set; }
        public LerProdutoDto()
        {
            Cidades = new List<LerCidadeDto>();
            PalavrasChave = new List<LerPalavrasChaveDto>();
            Categorias = new List<LerCategoriasDto>();
        }
    }
}
