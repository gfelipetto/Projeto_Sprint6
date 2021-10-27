using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SisProdutos.Models
{
    public class UsuarioClienteApiClient
    {
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public Guid CidadeId { get; set; }
    }
}
