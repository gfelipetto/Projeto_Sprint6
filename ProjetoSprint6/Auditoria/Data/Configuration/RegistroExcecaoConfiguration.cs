using Auditoria.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auditoria.Data.Configuration
{
    public class RegistroExcecaoConfiguration : IEntityTypeConfiguration<RegistroExcecoes>
    {
        public void Configure(EntityTypeBuilder<RegistroExcecoes> builder)
        {
            builder.ToTable("RegistroExcecoes");

            builder.Property(r => r.Id)
                .HasColumnName("Id");

            builder.Property(r => r.UsuarioId)
                .HasColumnName("UsuarioId")
                .IsRequired();

            builder.Property(r => r.Excecao)
                .HasColumnName("Excecao")
                .IsRequired();
        }
    }
}
