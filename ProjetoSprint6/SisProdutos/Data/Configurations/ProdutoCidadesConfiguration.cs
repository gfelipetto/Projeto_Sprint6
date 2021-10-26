using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SisProdutos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SisProdutos.Data.Configurations
{
    public class ProdutoCidadesConfiguration : IEntityTypeConfiguration<ProdutoCidades>
    {
        public void Configure(EntityTypeBuilder<ProdutoCidades> builder)
        {
            builder.ToTable("ProdutoCidades");

            builder.Property(p => p.Id)
                .HasColumnName("Id");

            builder.Property(p => p.Nome)
                .HasColumnName("Nome")
                .IsRequired();

            builder.Property(p => p.Nome)
                .HasColumnName("Estado")
                .IsRequired();

            builder.Property(p => p.ProdutoId)
                .HasColumnName("ProdutoId");

            builder.HasOne(p => p.Produto)
                .WithMany(p => p.Cidades)
                .HasForeignKey("ProdutoId");
        }
    }
}
