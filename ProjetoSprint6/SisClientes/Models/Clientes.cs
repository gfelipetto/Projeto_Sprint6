using System;
using System.Collections.Generic;

namespace SisClientes.Models
{
    public class LertodosClientesSemCidadeDtO
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public int CidadeId { get; set; }

        public Cidades ResidenteDe { get; set; }
        public IList<Enderecos> Enderecos { get; set; }
        public LertodosClientesSemCidadeDtO()
        {
            Enderecos = new List<Enderecos>();
        }
    }
}
