using Microsoft.AspNetCore.Mvc;
using SisProdutos.Services;
using System.Threading.Tasks;

namespace SisProdutos.Controllers
{
    [ApiController]
    [Route(template: "v1/SisProdutos/usuario")]
    public class LogoutController : ControllerBase
    {
        private LogoutService _logoutService;

        public LogoutController(LogoutService logoutService)
        {
            _logoutService = logoutService;
        }

        [HttpPost(template: "logout")]
        public IActionResult DeslogarUsuario()
        {
            var resultado = _logoutService.DeslogarUsuario();
            if (resultado.IsFailed) return Unauthorized(resultado.Errors);
            return Ok(resultado.Successes);

        }
    }
}
