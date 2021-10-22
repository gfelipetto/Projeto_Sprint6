using Microsoft.AspNetCore.Mvc;
using SisProdutos.Data.Resquests;
using SisProdutos.Services;
using System.Threading.Tasks;

namespace SisProdutos.Controllers
{
    [ApiController]
    [Route(template: "v1/SisProdutos")]
    public class LoginController : ControllerBase
    {
        private LoginService _loginService;

        public LoginController(LoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost(template: "login")]
        public async Task<IActionResult> LogarUsuarioAsync(LoginRequest request)
        {
            var resultado = await _loginService.LogarUsuarioAsync(request);
            if (resultado.IsFailed) return Unauthorized(resultado.Errors);
            return Ok(resultado.Successes);
        }
    }
}
