using System.Linq;

namespace SisProdutos.Models
{
    public static class ProdutosOrdemExtensions
    {
        public static IQueryable<Produtos> AplicarOrdem(this IQueryable<Produtos> query, ProdutosOrdem ordem)
        {
            if (!string.IsNullOrEmpty(ordem.OrdenarPor))
            {
                if (ordem.OrdenarPor.Contains("Crescente"))
                {
                    query = query.OrderBy(q => q.Preco);
                }
                else if (ordem.OrdenarPor.Contains("Decrescente"))
                {
                    query = query.OrderByDescending(q => q.Preco);
                }
            }
            return query;
        }
    }
    public class ProdutosOrdem
    {
        public string OrdenarPor { get; set; }
    }
}
