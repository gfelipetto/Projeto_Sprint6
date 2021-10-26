using Microsoft.AspNetCore.Mvc;
using SisClientes.Data.Dtos;
using SisClientes.Services;
using System;
using System.Threading.Tasks;

namespace SisClientes.Controllers
{
    [ApiController]
    [Route(template: "v1/SisClientes/cidade")]
    public class CidadesController : ControllerBase
    {
        private CidadesService _cidadeService;
        public CidadesController(CidadesService cs)
        {
            _cidadeService = cs;
        }

        [HttpGet(template: "mostrar")]
        public async Task<IActionResult> GetTodasCidadesAsync()
        {
            var resultado = await _cidadeService.GetTodasCidadesAsync();
            return Ok(resultado);
        }

        [HttpGet(template: "mostrar/{id}")]
        public async Task<IActionResult> GetCidadePorIdAsync(Guid id)
        {
            var resultado = await _cidadeService.GetCidadePorIdAsync(id);
            if (resultado == null) return NotFound();
            return Ok(resultado);
        }

        [HttpPost(template: "cadastrar/{cep}")]
        public async Task<IActionResult> CadastrarNovaCidadeAsync(string cep)
        {
            var resultado = await _cidadeService.CadastrarNovaCidadeAsync(cep);
            if (resultado == null) return BadRequest();
            return CreatedAtAction(nameof(GetCidadePorIdAsync), new { id = resultado.Id }, resultado);
        }

        [HttpPut(template: "atualizar/{id}")]
        public async Task<IActionResult> AtualizarCidadeAsync(Guid id, [FromBody] AtualizarCidadeDto cidadeAtualizadaDto)
        {
            if (!ModelState.IsValid) return BadRequest(cidadeAtualizadaDto);
            var resultado = await _cidadeService.AtualizarCidadeAsync(id, cidadeAtualizadaDto);
            if (resultado.IsFailed) return NotFound();
            return NoContent();
        }

        [HttpDelete(template: "remover/{id}")]
        public async Task<IActionResult> DeletarClienteAsync(Guid id)
        {
            var resultado = await _cidadeService.DeletarClienteAsync(id);
            if (resultado.IsFailed) return BadRequest(resultado.Errors);
            return NoContent();
        }
    }
}
