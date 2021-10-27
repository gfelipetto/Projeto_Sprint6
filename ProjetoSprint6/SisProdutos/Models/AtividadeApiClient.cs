using System;

namespace SisProdutos.Models
{
    public class AtividadeApiClient
    {
        public Guid UsuarioId { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal Preco { get; set; }
    }
}
