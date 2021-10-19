using System.Collections.Generic;

namespace SisClientes.Data.Dtos
{
    public class LerTodasCidadesDto
    {
        public string Nome { get; set; }
        public string Estado { get; set; }
        public IList<LerClienteSemCidadeDto> ClientesQueHabitam { get; set; }
        public LerTodasCidadesDto()
        {
            ClientesQueHabitam = new List<LerClienteSemCidadeDto>();
        }
    }
}
