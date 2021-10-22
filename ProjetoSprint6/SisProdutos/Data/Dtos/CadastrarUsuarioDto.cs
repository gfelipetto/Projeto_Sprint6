using System.ComponentModel.DataAnnotations;

namespace SisProdutos.Data.Dtos
{
    public class CadastrarUsuarioDto
    {
        [Required]
        public string UserName { get; set; }
        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Required]
        [Compare("Password")]
        public string RePassword { get; set; }
    }
}
