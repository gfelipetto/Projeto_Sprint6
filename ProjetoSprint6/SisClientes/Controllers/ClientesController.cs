using Microsoft.AspNetCore.Mvc;
using SisClientes.Data.Dtos;
using SisClientes.Services;
using System;
using System.Threading.Tasks;

namespace SisClientes.Controllers
{
    [ApiController]
    [Route(template: "v1/SisClientes/cliente")]
    public class ClientesController : ControllerBase
    {
        private ClientesService _clientesService;
        public ClientesController(ClientesService cs)
        {
            _clientesService = cs;
        }

        [HttpGet(template: "mostrar")]
        public async Task<IActionResult> GetTodosClientesAsync()
        {
            var resultado = await _clientesService.GetTodosClientesAsync();
            return Ok(resultado);
        }

        [HttpGet(template: "mostrar/{id}")]
        public async Task<IActionResult> GetClientePorIdAsync(Guid id)
        {
            var resultado = await _clientesService.GetClientePorIdAsync(id);
            if (resultado == null) return NotFound();
            return Ok(resultado);
        }
        [HttpPost(template: "cadastrar/{id}")]
        public async Task<IActionResult> CadastrarNovoClienteAsync(Guid id, [FromBody] CadastrarClienteDto clienteNovoDto)
        {
            if (!ModelState.IsValid) return BadRequest(clienteNovoDto);

            var resultado = await _clientesService.CadastrarNovoClienteViaHttpAsync(clienteNovoDto, id);
            if (resultado == null) return BadRequest();
            return CreatedAtAction(nameof(GetClientePorIdAsync), new { id = resultado.Id }, resultado);
        }

        [HttpPost(template: "cadastrar")]
        public async Task<IActionResult> CadastrarNovoClienteAsync([FromBody] CadastrarClienteDto clienteNovoDto)
        {
            if (!ModelState.IsValid) return BadRequest(clienteNovoDto);

            var resultado = await _clientesService.CadastrarNovoClienteAsync(clienteNovoDto);
            if (resultado == null) return BadRequest();
            return CreatedAtAction(nameof(GetClientePorIdAsync), new { id = resultado.Id }, resultado);
        }

        [HttpPut(template: "atualizar/{id}")]
        public async Task<IActionResult> AtualizarClienteAsync(Guid id, [FromBody] AtualizarClienteDto clienteAtualizadoDto)
        {
            if (!ModelState.IsValid) return BadRequest(clienteAtualizadoDto);

            var resultado = await _clientesService.AtualizarClienteAsync(id, clienteAtualizadoDto);
            if (resultado.IsFailed) return BadRequest(resultado.Errors);
            return NoContent();
        }

        [HttpDelete(template: "remover/{id}")]
        public async Task<IActionResult> DeletarClienteAsync(Guid id)
        {
            var resultado = await _clientesService.DeletarClienteAsync(id);
            if (resultado.IsFailed) return BadRequest(resultado.Errors);
            return NoContent();
        }
    }
}
