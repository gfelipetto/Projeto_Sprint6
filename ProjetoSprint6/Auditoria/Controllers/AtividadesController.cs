using Auditoria.Data.Dtos;
using Auditoria.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Auditoria.Controllers
{
    [ApiController]
    [Route(template: "v1/Auditoria/atividades")]
    public class AtividadesController : ControllerBase
    {
        private readonly AtividadesServices _atividadesServices;

        public AtividadesController(AtividadesServices atividadesServices)
        {
            _atividadesServices = atividadesServices;
        }

        [HttpGet(template: "mostrar")]
        public async Task<IActionResult> GetTodasAtividadesAsync()
        {
            var atividades = await _atividadesServices.GetTodasAtividadesAsync();
            return Ok(atividades);
        }

        [HttpGet(template: "mostrar/{id}")]
        public async Task<IActionResult> GetAtividadePorIdAsync(Guid id)
        {
            var atividade = await _atividadesServices.GetAtividadePorIdAsync(id);
            if (atividade == null) return NotFound();
            return Ok(atividade);
        }

        [HttpPost(template: "incluir")]
        public async Task<IActionResult> IncluirAtividade([FromBody] IncluirAtividadeDto incluirAtividadeDto)
        {
            var atividade = await _atividadesServices.IncluirAtividade(incluirAtividadeDto);
            if (atividade == null) return BadRequest();
            return NoContent();
        }
    }
}
