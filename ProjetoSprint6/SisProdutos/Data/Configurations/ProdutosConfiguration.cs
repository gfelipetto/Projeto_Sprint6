using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SisProdutos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SisProdutos.Data.Configurations
{
    public class ProdutosConfiguration : IEntityTypeConfiguration<Produtos>
    {
        public void Configure(EntityTypeBuilder<Produtos> builder)
        {
            builder.ToTable("Produtos");

            builder.Property(p => p.Id)
                .HasColumnName("Id");

            builder.Property(p => p.Nome)
                .HasColumnName("Nome")
                .IsRequired();

            builder.Property(p => p.Descricao)
                .HasColumnName("Descricao")
                .IsRequired();

            builder.Property(e => e.Preco)
                .HasColumnName("Preco")
                .HasColumnType("decimal(18,2)")
                .IsRequired();
        }
    }
}
