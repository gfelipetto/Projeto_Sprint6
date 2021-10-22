using System.ComponentModel.DataAnnotations;

namespace SisProdutos.Data.Resquests
{
    public class LoginRequest
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
