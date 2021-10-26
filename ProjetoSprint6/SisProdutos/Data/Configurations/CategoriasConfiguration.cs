using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SisProdutos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SisProdutos.Data.Configurations
{
    public class CategoriasConfiguration : IEntityTypeConfiguration<Categorias>
    {
        public void Configure(EntityTypeBuilder<Categorias> builder)
        {
            builder.ToTable("Categorias");

            builder.Property(c => c.Id)
                .HasColumnName("Id");

            builder.Property(c => c.Nome)
                .HasColumnName("Nome")
                .IsRequired();

            builder.Property(c => c.ProdutoId)
                .HasColumnName("ProdutoId");

            builder.HasOne(c => c.Produto)
                .WithMany(p => p.Categorias)
                .HasForeignKey("ProdutoId");
        }
    }
}
