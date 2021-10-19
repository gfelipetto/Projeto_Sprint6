using System;
using System.Collections.Generic;

namespace SisClientes.Models
{
    public class Cidades
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string  Estado { get; set; }

        public IList<LertodosClientesSemCidadeDtO> ClientesQueHabitam { get; set; }
        public Cidades()
        {
            ClientesQueHabitam = new List<LertodosClientesSemCidadeDtO>();
        }
    }
}
