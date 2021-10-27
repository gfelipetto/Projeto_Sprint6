using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SisProdutos.Data.Resquests
{
    public class LoginResponse
    {
        public string TokenValor { get; set; }
        public Guid UsuarioId { get; set; }

        public override string ToString()
        {
            return $"{TokenValor}-{UsuarioId}";
        }
    }
}
