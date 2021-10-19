using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SisClientes.Data;
using SisClientes.Data.Dtos;
using SisClientes.Models;
using System;
using System.Threading.Tasks;

namespace SisClientes.Controllers
{
    [ApiController]
    [Route(template: "v1")]
    public class EnderecosController : ControllerBase
    {
        private readonly SisClientesDbContext _scContext;
        private readonly IMapper _mapper;
        public EnderecosController(SisClientesDbContext sisContext, IMapper mapper)
        {
            _scContext = sisContext;
            _mapper = mapper;
        }

        [HttpGet(template: "enderecos/{id}")]
        public async Task<IActionResult> GetEnderecoPorIdAsync(Guid id)
        {
            var endereco = await _scContext.Enderecos.FirstOrDefaultAsync(e => e.Id == id);
            if (endereco == null) return NotFound();

            var enderecoDto = _mapper.Map<LerTodosEnderecosDto>(endereco);
            return Ok(enderecoDto);
        }


        [HttpPost(template: "enderecos/{id}")]
        public async Task<IActionResult> CadastrarNovoEnderecoAsync(Guid id, [FromBody] CadastrarNovoEnderecoDto novoEnderecoDto)
        {
            if (!ModelState.IsValid) return BadRequest();

            var cliente = await _scContext.Clientes.FirstOrDefaultAsync(c => c.Id == id);
            if (cliente == null) return NotFound();

            var novoEndereco = new Enderecos
            {
                ClienteId = cliente.Id,
                Cep = novoEnderecoDto.Cep,
                Logradouro = novoEnderecoDto.Logradouro,
                Bairro = novoEnderecoDto.Bairro,
                NumeroCasa = novoEnderecoDto.NumeroCasa,
                Complemento = novoEnderecoDto.Complemento
            };
            _scContext.Enderecos.Add(novoEndereco);
            await _scContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetEnderecoPorIdAsync), new { id = novoEndereco.Id}, novoEndereco);
        }

        [HttpPut(template: "enderecos/{id}")]
        public async Task<IActionResult> AtualizarEnderecoAsync(Guid id, [FromBody] AtualizarEnderecoDto enderecoAtualizado)
        {
            if (!ModelState.IsValid) return BadRequest();

            var endereco = await _scContext.Enderecos.FirstOrDefaultAsync(e => e.Id == id);
            if (endereco == null) return NotFound();

            _mapper.Map(enderecoAtualizado, endereco);
            await _scContext.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete(template:"enderecos/{id}")]
        public async Task<IActionResult> DeletarEnderecoAsync(Guid id)
        {
            var endereco = await _scContext.Enderecos.FirstOrDefaultAsync(e => e.Id == id);
            if (endereco == null) return NotFound();

            _scContext.Remove(endereco);
            await _scContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
