using System.ComponentModel.DataAnnotations;

namespace SisClientes.Data.Dtos

{
    public class CadastrarNovoEnderecoDto
    {
        [Required(ErrorMessage = "O campo Cep é obrigatório")]
        public string Cep { get; set; }
        [Required(ErrorMessage = "O campo Logradouro é obrigatório")]
        public string Logradouro { get; set; }
        [Required(ErrorMessage = "O campo Bairro é obrigatório")]
        public string Bairro { get; set; }
        [Required(ErrorMessage = "O campo NumeroCasa é obrigatório")]
        public string NumeroCasa { get; set; }
        public string Complemento { get; set; }
        [Required(ErrorMessage = "O EhPrincipal é obrigatório")]
        public bool EhPrincipal { get; set; }
    }
}
