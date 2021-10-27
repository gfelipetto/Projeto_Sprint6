using System;

namespace Auditoria.Data.Dtos
{
    public class LerExcecaoDto
    {
        public Guid Id { get; set; }
        public Guid UsuarioId { get; set; }
        public string Excecao { get; set; }
    }
}
