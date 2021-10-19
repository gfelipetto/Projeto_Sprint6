﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SisClientes.Models;

namespace SisClientes.Data.Configurations
{
    public class ClientesConfiguration : IEntityTypeConfiguration<LertodosClientesSemCidadeDtO>
    {
        public void Configure(EntityTypeBuilder<LertodosClientesSemCidadeDtO> builder)
        {
            builder.ToTable("Clientes");

            builder.Property(c => c.Id)
                .HasColumnName("Id");

            builder.Property(c => c.Nome)
                .HasColumnName("Nome")
                .IsRequired();

            builder.Property(c => c.DataNascimento)
                .HasColumnName("DataNascimento")
                .IsRequired();

            builder.Property(c => c.CidadeId)
                .HasColumnName("CidadeId")
                .IsRequired();
        }
    }
}
