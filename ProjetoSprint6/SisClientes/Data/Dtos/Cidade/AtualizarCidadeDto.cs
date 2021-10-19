using System.ComponentModel.DataAnnotations;

namespace SisClientes.Data.Dtos
{
    public class AtualizarCidadeDto
    {
        [Required(ErrorMessage = "O campo Nome é obrigatório")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "O campo Estado é obrigatório")]
        public string Estado { get; set; }
    }
}
