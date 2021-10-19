using SisClientes.Data.Dtos.Cliente;
using System.Collections.Generic;

namespace SisClientes.Data.Dtos.Cidade
{
    public class LerTodasCidadesDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Estado { get; set; }
        public IList<LerClienteSemCidadeDto> ClientesQueHabitam { get; set; }
        public LerTodasCidadesDto()
        {
            ClientesQueHabitam = new List<LerClienteSemCidadeDto>();
        }
    }
}
