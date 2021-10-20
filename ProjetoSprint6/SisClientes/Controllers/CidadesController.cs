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
    public class CidadesController : ControllerBase
    {
        private readonly SisClientesDbContext _scContext;
        private readonly IMapper _mapper;
        private readonly CepApiClient _cepApiCliet;
        public CidadesController(SisClientesDbContext sisClienteContext, IMapper mapper, CepApiClient cepClient)
        {
            _scContext = sisClienteContext;
            _mapper = mapper;
            _cepApiCliet = cepClient;
        }

        [HttpGet(template: "cidades")]
        public async Task<IActionResult> GetTodasCidadesAsync()
        {
            var cidadesDto = new List<LerTodasCidadesDto>();

            var listaCidades = await _scContext.Cidades.ToListAsync();
            foreach (var cidades in listaCidades)
            {
                cidades.ClientesQueHabitam = await _scContext.Clientes.Where(c => c.CidadeId == cidades.Id).ToListAsync();
                cidadesDto.Add(_mapper.Map<LerTodasCidadesDto>(cidades));
            }

            return Ok(cidadesDto);
        }

        [HttpGet(template: "cidades/{id}")]
        public async Task<IActionResult> GetCidadePorIdAsync(Guid id)
        {
            var cidade = await _scContext.Cidades.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
            if (cidade == null) return NotFound();

            cidade.ClientesQueHabitam = await _scContext.Clientes.Where(c => c.CidadeId == cidade.Id).ToListAsync();
            var cidadeDto = _mapper.Map<LerTodasCidadesDto>(cidade);
            return Ok(cidadeDto);
        }

        [HttpPost(template: "cidades/{cep}")]
        public async Task<IActionResult> CadastrarNovaCidadeAsync(string cep)
        {
            var cepCidade = await _cepApiCliet.GetCepAsync(cep);
            var cidade = await _scContext.Cidades.FirstOrDefaultAsync(c => c.Nome == cepCidade.Localidade && c.Estado == cepCidade.UF);
            if (cidade == null)
            {
                var novaCidade = new Cidades
                {
                    Nome = cepCidade.Localidade,
                    Estado = cepCidade.UF,
                };
                _scContext.Cidades.Add(novaCidade);
                await _scContext.SaveChangesAsync();
                return CreatedAtAction(nameof(GetCidadePorIdAsync), new { id = novaCidade.Id }, novaCidade);
            }
            return BadRequest();
        }

        [HttpPut(template: "cidades/{id}")]
        public async Task<IActionResult> AtualizarCidadeAsync(Guid id, [FromBody] AtualizarCidadeDto cidadeAtualizadaDto)
        {
            if (!ModelState.IsValid) return BadRequest(cidadeAtualizadaDto);

            var cidade = await _scContext.Cidades.FirstOrDefaultAsync(c => c.Id == id);
            if (cidade == null) return NotFound();

            _mapper.Map(cidadeAtualizadaDto, cidade);
            await _scContext.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete(template: "cidades/{id}")]
        public async Task<IActionResult> DeletarClienteAsync(Guid id)
        {
            var cidade = await _scContext.Cidades.FirstOrDefaultAsync(c => c.Id == id);
            if (cidade == null) return NotFound();

            var cliente = await _scContext.Clientes.FirstOrDefaultAsync(c => c.CidadeId == cidade.Id);
            if (cliente != null) return BadRequest();

            try
            {
                _scContext.Cidades.Remove(cidade);
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
