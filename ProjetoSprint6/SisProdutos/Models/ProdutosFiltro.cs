using SisProdutos.Data;
using System.Linq;

namespace SisProdutos.Models
{
    public static class ProdutosFiltroExtensions
    {
        public static IQueryable<Produtos> AplicarFiltro(this IQueryable<Produtos> query, SisProdutosDbContext sisProdutosDbContext, ProdutosFiltro filtro)
        {
            if (filtro != null)
            {
                if (!string.IsNullOrEmpty(filtro.Nome))
                {
                    query = query.Where(q => q.Nome.Contains(filtro.Nome));
                }
                if (!string.IsNullOrEmpty(filtro.Descricao))
                {
                    query = query.Where(q => q.Descricao.Contains(filtro.Descricao));
                }
                if (!string.IsNullOrEmpty(filtro.PalavraChave))
                {
                    var listaPalvrasChaves = sisProdutosDbContext.PalavrasChave.Where(p => p.Nome.Contains(filtro.PalavraChave));
                    query = query.Where(q => listaPalvrasChaves.Any(l => l.ProdutoId == q.Id));
                }
                if (!string.IsNullOrEmpty(filtro.Categoria))
                {
                    var listaCategorias = sisProdutosDbContext.Categorias.Where(p => p.Nome.Contains(filtro.Categoria));
                    query = query.Where(q => listaCategorias.Any(l => l.ProdutoId == q.Id));
                }
                if (!string.IsNullOrEmpty(filtro.Cidade))
                {
                    var listaCidades = sisProdutosDbContext.ProdutoCidades.Where(p => p.Nome.Contains(filtro.Cidade));
                    query = query.Where(q => listaCidades.Any(l => l.ProdutoId == q.Id));
                }
            }
            return query;
        }
    }
    public class ProdutosFiltro
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string PalavraChave { get; set; }
        public string Categoria { get; set; }
        public string Cidade { get; set; }
    }
}
