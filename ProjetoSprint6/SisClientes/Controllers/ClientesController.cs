using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SisClientes.Data;
using SisClientes.Data.Dtos;
using SisClientes.HttpClients;
using SisClientes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SisClientes.Controllers
{
    [ApiController]
    [Route(template: "v1")]
    public class ClientesController : ControllerBase
    {
        private readonly SisClientesDbContext _scContext;
        private readonly IMapper _mapper;
        private readonly CepApiClient _cepApiCliet;
        public ClientesController(SisClientesDbContext sisClienteContext, IMapper mapper, CepApiClient cepClient)
        {
            _scContext = sisClienteContext;
            _mapper = mapper;
            _cepApiCliet = cepClient;
        }

        [HttpGet(template: "clientes")]
        public async Task<IActionResult> GetTodosClientesAsync()
        {
            var clientesDto = new List<LerTodosClientesDto>();

            var listaClientes = await _scContext.Clientes.ToListAsync();
            foreach (var cliente in listaClientes)
            {
                cliente.ResidenteDe = await _scContext.Cidades.FirstOrDefaultAsync(c => c.Id == cliente.CidadeId);
                cliente.Enderecos = await _scContext.Enderecos.Where(e => e.ClienteId == cliente.Id).ToListAsync();
                clientesDto.Add(_mapper.Map<LerTodosClientesDto>(cliente));
            }

            return Ok(clientesDto);
        }

        [HttpGet(template: "clientes/{id}")]
        public async Task<IActionResult> GetClientePorIdAsync(Guid id)
        {
            var cliente = await _scContext.Clientes.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
            if (cliente == null) return NotFound();

            cliente.ResidenteDe = await _scContext.Cidades.FirstOrDefaultAsync(c => c.Id == cliente.CidadeId);
            cliente.Enderecos = await _scContext.Enderecos.Where(e => e.ClienteId == cliente.Id).ToListAsync();
            var clienteDto = _mapper.Map<LerTodosClientesDto>(cliente);
            return Ok(clienteDto);

        }

        [HttpPost(template: "clientes")]
        public async Task<IActionResult> CadastrarNovoClienteAsync([FromBody] CadastrarClienteDto clienteNovoDto)
        {
            if (!ModelState.IsValid) return BadRequest(clienteNovoDto);

            var cepCliente = await _cepApiCliet.GetCepAsync(clienteNovoDto.Cep);
            var cidadeCliente = await _scContext.Cidades.FirstOrDefaultAsync(c => c.Nome == cepCliente.Localidade && c.Estado == cepCliente.UF);
            if (cidadeCliente == null)
            {
                cidadeCliente = new Cidades
                {
                    Nome = cepCliente.Localidade,
                    Estado = cepCliente.UF,
                };
                _scContext.Cidades.Add(cidadeCliente);
            }

            var novoCliente = new Clientes
            {
                Nome = clienteNovoDto.Nome,
                DataNascimento = clienteNovoDto.DataNascimento,
                CidadeId = cidadeCliente.Id,
            };

            _scContext.Clientes.Add(novoCliente);
            await _scContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetClientePorIdAsync), new { id = novoCliente.Id }, novoCliente);
        }

        [HttpPut(template: "clientes/{id}")]
        public async Task<IActionResult> AtualizarClienteAsync(Guid id, [FromBody] AtualizarClienteDto clienteAtualizadoDto)
        {
            if (!ModelState.IsValid) return BadRequest(clienteAtualizadoDto);

            var cliente = await _scContext.Clientes.FirstOrDefaultAsync(c => c.Id == id);
            if (cliente == null) return NotFound();

            var cepCliente = await _cepApiCliet.GetCepAsync(clienteAtualizadoDto.Cep);
            var cidadeCliente = await _scContext.Cidades.FirstOrDefaultAsync(c => c.Nome == cepCliente.Localidade && c.Estado == cepCliente.UF);
            if (cidadeCliente == null)
            {
                cidadeCliente = new Cidades
                {
                    Nome = cepCliente.Localidade,
                    Estado = cepCliente.UF,
                };
                _scContext.Cidades.Add(cidadeCliente);
            }

            cliente.CidadeId = cidadeCliente.Id;
            _mapper.Map(clienteAtualizadoDto, cliente);
            await _scContext.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete(template: "clientes/{id}")]
        public async Task<IActionResult> DeletarClienteAsync(Guid id)
        {
            var cliente = await _scContext.Clientes.FirstOrDefaultAsync(c => c.Id == id);
            if (cliente == null) return NotFound();

            try
            {
                _scContext.Clientes.Remove(cliente);
                await _scContext.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
