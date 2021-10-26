using SisProdutos.Data.Dtos.Categorias;
using SisProdutos.Data.Dtos.Cidade;
using SisProdutos.Data.Dtos.PalavrasChave;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SisProdutos.Data.Dtos.Produto
{
    public class CadastrarProdutosDto
    {
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Descricao { get; set; }
        [Required]
        public decimal Preco { get; set; }
        [Required]
        public IList<LerCidadeDto> Cidade { get; set; }
        [Required]
        public IList<LerPalavrasChaveDto> PalavrasChave { get; set; }
        [Required]
        public IList<LerCategoriasDto> Categorias { get; set; }

    }
}
