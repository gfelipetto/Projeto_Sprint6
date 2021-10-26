using System;

namespace SisProdutos.Data.Dtos.Usuario
{
    public class CadastrarUsuarioViaClientDto
    {
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Cep { get; set; }
    }
}
