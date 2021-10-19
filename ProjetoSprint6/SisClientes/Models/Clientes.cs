using System;
using System.Collections.Generic;

namespace SisClientes.Models
{
    public class Clientes
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public Guid CidadeId { get; set; }

        public Cidades ResidenteDe { get; set; }
        public IList<Enderecos> Enderecos { get; set; }
        public Clientes()
        {
            Enderecos = new List<Enderecos>();
        }
    }
}
