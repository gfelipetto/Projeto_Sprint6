using System;

namespace Auditoria.Data.Dtos
{
    public class IncluirAtividadeDto
    {
        public Guid UsuarioId { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal Preco { get; set; }
    }
}
