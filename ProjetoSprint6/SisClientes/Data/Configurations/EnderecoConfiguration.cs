using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SisClientes.Models;

namespace SisClientes.Data.Configurations
{
    public class EnderecoConfiguration : IEntityTypeConfiguration<Enderecos>
    {
        public void Configure(EntityTypeBuilder<Enderecos> builder)
        {
            builder.ToTable("Endereco");

            builder.Property(e => e.Id)
                .HasColumnName("Id");

            builder.Property(e => e.ClienteId)
                .HasColumnName("ClienteId")
                .IsRequired();

            builder.Property(e => e.Cep)
                .HasColumnName("Cep")
                .IsRequired();

            builder.Property(e => e.Logradouro)
                .HasColumnName("Logradouro")
                .IsRequired();

            builder.Property(e => e.Bairro)
                .HasColumnName("Bairro")
                .IsRequired();

            builder.Property(e => e.NumeroCasa)
                .HasColumnName("NumeroCasa")
                .IsRequired();

            builder.Property(e => e.Complemento)
                .HasColumnName("Complemento")
                .IsRequired();
        }
    }
}
