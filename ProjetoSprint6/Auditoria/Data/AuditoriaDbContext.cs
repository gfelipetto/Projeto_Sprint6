using Auditoria.Data.Configuration;
using Auditoria.Models;
using Microsoft.EntityFrameworkCore;

namespace Auditoria.Data
{
    public class AuditoriaDbContext : DbContext
    {
        public AuditoriaDbContext(DbContextOptions options) : base(options)
        { }
        public DbSet<RegistroDeAtividades> RegistroDeAtividades { get; set; }
        public DbSet<RegistroExcecoes> RegistroExcecoes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new RegistroDeAtividadesConfiguration());
            modelBuilder.ApplyConfiguration(new RegistroExcecaoConfiguration());
        }
    }
}
