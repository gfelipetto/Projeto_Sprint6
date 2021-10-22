using Microsoft.AspNetCore.Mvc;
using SisProdutos.Data.Dtos;
using SisProdutos.Services;
using System.Threading.Tasks;

namespace SisProdutos.Controllers
{
    [ApiController]
    [Route(template: "v1/SisProdutos")]
    public class CadastraController : ControllerBase
    {
        private CadastraService _cadastraService;

        public CadastraController(CadastraService cadastraService)
        {
            _cadastraService = cadastraService;
        }

        [HttpPost(template: "cadastrar")]
        public async Task<IActionResult> CadastraUsuarioAsync(CadastrarUsuarioDto novoUsuario)
        {
            var resultado = await _cadastraService.CadastraUsuarioAsync(novoUsuario);
            if (resultado.IsFailed) return StatusCode(500);
            return Ok();
        }
    }
}
