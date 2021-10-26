using System;

namespace SisClientes.Models
{
    public class Enderecos
    {
        public Guid Id { get; set; }
        public Guid ClienteId { get; set; }
        public string Cep { get; set; }
        public string Logradouro { get; set; }
        public string Bairro { get; set; }
        public string NumeroCasa { get; set; }
        public string  Complemento { get; set; }
        public bool EhPrincipal { get; set; }
    }
}
