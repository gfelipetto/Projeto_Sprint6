using Microsoft.AspNetCore.Mvc;
using SisProdutos.Data.Dtos;
using SisProdutos.Services;
using System.Threading.Tasks;

namespace SisProdutos.Controllers
{
    [ApiController]
    [Route(template: "v1/SisProdutos/usuario")]
    public class CadastraController : ControllerBase
    {
        private CadastraService _cadastraService;

        public CadastraController(CadastraService cadastraService)
        {
            _cadastraService = cadastraService;
        }

        [HttpPost(template: "cadastrar")]
        public async Task<IActionResult> CadastraUsuarioAsync([FromBody]CadastrarUsuarioDto novoUsuario)
        {
            var resultado = await _cadastraService.CadastraUsuarioAsync(novoUsuario);
            if (resultado.IsFailed) return BadRequest(resultado.Errors);
            return Ok();
        }
    }
}
