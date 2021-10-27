using Auditoria.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auditoria.Data.Configuration
{
    public class RegistroDeAtividadesConfiguration : IEntityTypeConfiguration<RegistroDeAtividades>
    {
        public void Configure(EntityTypeBuilder<RegistroDeAtividades> builder)
        {
            builder.ToTable("RegistroDeAtividades");

            builder.Property(r => r.Id)
                .HasColumnName("Id");

            builder.Property(r => r.UsuarioId)
                .HasColumnName("UsuarioId")
                .IsRequired();

            builder.Property(r => r.Nome)
                .HasColumnName("Nome")
                .IsRequired();

            builder.Property(r => r.Descricao)
                .HasColumnName("Descricao")
                .IsRequired();

            builder.Property(e => e.Preco)
                .HasColumnName("Preco")
                .HasColumnType("decimal(18,2)")
                .IsRequired();
        }
    }
}
