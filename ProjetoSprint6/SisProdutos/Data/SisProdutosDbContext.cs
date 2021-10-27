using Microsoft.EntityFrameworkCore;
using SisProdutos.Data.Configurations;
using SisProdutos.Models;

namespace SisProdutos.Data
{
    public class SisProdutosDbContext : DbContext
    {
        public SisProdutosDbContext(DbContextOptions<SisProdutosDbContext> options) : base(options)
        { }

        public DbSet<Produtos> Produtos { get; set; }
        public DbSet<ProdutoCidades> ProdutoCidades { get; set; }
        public DbSet<Categorias> Categorias { get; set; }
        public DbSet<PalavrasChave> PalavrasChave { get; set; }
        public DbSet<Compras> ProdutoCarrinho { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProdutosConfiguration());
            modelBuilder.ApplyConfiguration(new ProdutoCidadesConfiguration());
            modelBuilder.ApplyConfiguration(new CategoriasConfiguration());
            modelBuilder.ApplyConfiguration(new PalavrasChaveConfiguration());
            modelBuilder.ApplyConfiguration(new ComprasConfiguration());
        }
    }
}
