using System;

namespace Auditoria.Models
{
    public class RegistroExcecoes
    {
        public Guid Id { get; set; }
        public Guid UsuarioId { get; set; }
        public string Excecao { get; set; }
    }
}
