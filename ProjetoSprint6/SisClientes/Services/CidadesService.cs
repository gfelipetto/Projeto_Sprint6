using AutoMapper;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using SisClientes.Data;
using SisClientes.Data.Dtos;
using SisClientes.HttpClients;
using SisClientes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SisClientes.Services
{
    public class CidadesService
    {
        private readonly SisClientesDbContext _scContext;
        private readonly IMapper _mapper;
        private readonly CepApiClient _cepApiCliet;

        public CidadesService(SisClientesDbContext sisClienteContext, IMapper mapper, CepApiClient cepClient)
        {
            _scContext = sisClienteContext;
            _mapper = mapper;
            _cepApiCliet = cepClient;
        }
        public async Task<Cidades> CadastrarNovaCidadeAsync(string cep)
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
                return novaCidade;
            }
            return null;
        }
        public async Task<List<LerTodasCidadesDto>> GetTodasCidadesAsync()
        {
            var cidadesDto = new List<LerTodasCidadesDto>();

            var listaCidades = await _scContext.Cidades.ToListAsync();
            foreach (var cidades in listaCidades)
            {
                cidades.ClientesQueHabitam = await _scContext.Clientes.Where(c => c.CidadeId == cidades.Id).ToListAsync();
                cidadesDto.Add(_mapper.Map<LerTodasCidadesDto>(cidades));
            }

            return cidadesDto;
        }
        public async Task<LerTodasCidadesDto> GetCidadePorIdAsync(Guid id)
        {
            var cidade = await _scContext.Cidades.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
            if (cidade == null) return null;

            cidade.ClientesQueHabitam = await _scContext.Clientes.Where(c => c.CidadeId == cidade.Id).ToListAsync();
            var cidadeDto = _mapper.Map<LerTodasCidadesDto>(cidade);
            return cidadeDto;
        }
        public async Task<Result> AtualizarCidadeAsync(Guid id, AtualizarCidadeDto cidadeAtualizadaDto)
        {
            var cidade = await _scContext.Cidades.FirstOrDefaultAsync(c => c.Id == id);
            if (cidade == null) return Result.Fail("Cidade não encontrada");

            _mapper.Map(cidadeAtualizadaDto, cidade);
            await _scContext.SaveChangesAsync();
            return Result.Ok();
        }
        public async Task<Result> DeletarClienteAsync(Guid id)
        {
            var cidade = await _scContext.Cidades.FirstOrDefaultAsync(c => c.Id == id);
            if (cidade == null) return Result.Fail("Cidade não encontrada");

            var cliente = await _scContext.Clientes.FirstOrDefaultAsync(c => c.CidadeId == cidade.Id);
            if (cliente != null) return Result.Fail("Existe um cliente associado a essa cidade");

            try
            {
                _scContext.Cidades.Remove(cidade);
                await _scContext.SaveChangesAsync();
                return Result.Ok();

            }
            catch (Exception)
            {
                return Result.Fail("Ocorreu um erro ao salvar informações");
            }
        }
    }
}
