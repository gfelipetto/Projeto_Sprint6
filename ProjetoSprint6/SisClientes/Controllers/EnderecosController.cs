using Microsoft.AspNetCore.Mvc;
using SisClientes.Data.Dtos;
using SisClientes.Services;
using System;
using System.Threading.Tasks;

namespace SisClientes.Controllers
{
    [ApiController]
    [Route(template: "v1/SisClientes")]
    public class EnderecosController : ControllerBase
    {
        private EnderecosService _enderecosService;
        public EnderecosController(EnderecosService es)
        {
            _enderecosService = es;
        }

        [HttpGet(template: "enderecos")]
        public async Task<IActionResult> GetTodosEnderecosAsync()
        {
            var resultado = await _enderecosService.GetTodosEnderecosAsync();
            return Ok(resultado);
        }

        [HttpGet(template: "enderecos/{id}")]
        public async Task<IActionResult> GetEnderecoPorIdAsync(Guid id)
        {
            var resultado = await _enderecosService.GetEnderecoPorIdAsync(id);
            if (resultado == null) return NotFound();
            return Ok(resultado);
        }


        [HttpPost(template: "enderecos/{id}")]
        public async Task<IActionResult> CadastrarNovoEnderecoAsync(Guid id, [FromBody] CadastrarNovoEnderecoDto novoEnderecoDto)
        {
            if (!ModelState.IsValid) return BadRequest();

            var resultado = await _enderecosService.CadastrarNovoEnderecoAsync(id, novoEnderecoDto);
            if (resultado == null) return NotFound();
            return CreatedAtAction(nameof(GetEnderecoPorIdAsync), new { id = resultado.Id }, resultado);
        }

        [HttpPut(template: "enderecos/{id}")]
        public async Task<IActionResult> AtualizarEnderecoAsync(Guid id, [FromBody] AtualizarEnderecoDto enderecoAtualizado)
        {
            if (!ModelState.IsValid) return BadRequest();

            var resultado = await _enderecosService.AtualizarEnderecoAsync(id, enderecoAtualizado);
            if (resultado.IsFailed) return NotFound(resultado.Errors);
            return NoContent();
        }

        [HttpDelete(template:"enderecos/{id}")]
        public async Task<IActionResult> DeletarEnderecoAsync(Guid id)
        {
            var resultado = await _enderecosService.DeletarEnderecoAsync(id);
            if (resultado.IsFailed) return NotFound(resultado.Errors);
            return NoContent();
        }
    }
}
