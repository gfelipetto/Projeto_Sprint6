using System;

namespace Auditoria.Data.Dtos
{
    public class IncluirExcecaoDto
    {
        public Guid UsuarioId { get; set; }
        public string Excecao { get; set; }
    }
}
