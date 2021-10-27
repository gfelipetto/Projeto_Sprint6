using Auditoria.Data.Dtos;
using Auditoria.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Auditoria.Controllers
{
    [ApiController]
    [Route(template: "v1/Auditoria/excecoes")]
    public class ExcecoesController : ControllerBase
    {
        private readonly ExcecoesServices _excecoesServices;

        public ExcecoesController(ExcecoesServices excecoesServices)
        {
            _excecoesServices = excecoesServices;
        }

        [HttpGet(template: "mostrar")]
        public async Task<IActionResult> GetTodasExcecoesAsync()
        {
            var exececoes = await _excecoesServices.GetTodasExcecoesAsync();
            return Ok(exececoes);
        }

        [HttpGet(template: "mostrar/{id}")]
        public async Task<IActionResult> GetExcecaoPorIdAsync(Guid id)
        {
            var exececoes = await _excecoesServices.GetExcecaoPorIdAsync(id);
            if (exececoes == null) return NotFound();
            return Ok(exececoes);
        }

        [HttpPost(template: "incluir")]
        public async Task<IActionResult> IncluirExcecao([FromBody] IncluirExcecaoDto incluirExcecaoDto)
        {
            var exececoes = await _excecoesServices.IncluirExcecao(incluirExcecaoDto);
            if (exececoes == null) return BadRequest();
            return NoContent();
        }
    }
}
