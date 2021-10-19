using System;
using System.Collections.Generic;

namespace SisClientes.Data.Dtos
{
    public class LerTodosClientesDto
    {
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public LerCidadeSemClientesDto ResidenteDe { get; set; }
        public IList<LerTodosEnderecosDto> Enderecos { get; set; }
        public LerTodosClientesDto()
        {
            Enderecos = new List<LerTodosEnderecosDto>();
        }
    }
}
