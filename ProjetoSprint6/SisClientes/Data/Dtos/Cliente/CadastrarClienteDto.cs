using System;
using System.ComponentModel.DataAnnotations;

namespace SisClientes.Data.Dtos
{
    public class CadastrarClienteDto
    {
        [Required(ErrorMessage = "O campo Nome é obrigatório")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "O campo DataNascimento é obrigatório")]
        public DateTime DataNascimento { get; set; }
        [Required(ErrorMessage = "O campo Cep é obrigatório")]
        public string Cep { get; set; }
    }
}
