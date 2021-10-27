using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SisProdutos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SisProdutos.Data.Configurations
{
    public class ComprasConfiguration : IEntityTypeConfiguration<Compras>
    {
        public void Configure(EntityTypeBuilder<Compras> builder)
        {
            builder.ToTable("Compras");

            builder.Property(c => c.Id)
                .HasColumnName("Id");

            builder.Property(c => c.Nome)
                .HasColumnName("Nome")
                .IsRequired();

            builder.Property(c => c.Descricao)
                .HasColumnName("Descricao")
                .IsRequired();

            builder.Property(c => c.Preco)
                .HasColumnName("Preco")
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(c => c.IdUsuario)
                .HasColumnName("IdUsuario")
                .IsRequired();
        }
    }
}
