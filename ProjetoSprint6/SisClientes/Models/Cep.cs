using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SisClientes.Models
{
    public class Cep
    {
        public string CEP { get; set; }
        public string UF { get; set; }
        public string Localidade { get; set; }
        public string Logradouro { get; set; }
        public string Bairro { get; set; }
    }
}
