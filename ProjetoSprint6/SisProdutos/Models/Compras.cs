using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SisProdutos.Models
{
    public class Compras
    {
        public Guid Id { get; set; }
        public Guid IdUsuario { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal Preco { get; set; }
    }
}
