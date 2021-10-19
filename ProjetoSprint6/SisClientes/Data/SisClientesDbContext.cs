using Microsoft.EntityFrameworkCore;
using SisClientes.Data.Configurations;
using SisClientes.Models;

namespace SisClientes.Data
{
    public class SisClientesDbContext : DbContext
    {
        public SisClientesDbContext(DbContextOptions<SisClientesDbContext> options) : base(options)
        {

        }
        public DbSet<Cidades> Cidades { get; set; }
        public DbSet<LertodosClientesSemCidadeDtO> Clientes { get; set; }
        public DbSet<Enderecos> Enderecos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CidadesConfiguration());
            modelBuilder.ApplyConfiguration(new ClientesConfiguration());
            modelBuilder.ApplyConfiguration(new EnderecoConfiguration());
        }
    }
}
