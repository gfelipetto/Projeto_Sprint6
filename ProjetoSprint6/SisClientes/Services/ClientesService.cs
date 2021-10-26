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
    public class ClientesService
    {
        private readonly SisClientesDbContext _scContext;
        private readonly IMapper _mapper;
        private readonly CepApiClient _cepApiCliet;

        public ClientesService(SisClientesDbContext sisClienteContext, IMapper mapper, CepApiClient cepClient)
        {
            _scContext = sisClienteContext;
            _mapper = mapper;
            _cepApiCliet = cepClient;
        }
        public async Task<List<LerTodosClientesDto>> GetTodosClientesAsync()
        {
            var clientesDto = new List<LerTodosClientesDto>();

            var listaClientes = await _scContext.Clientes.ToListAsync();
            foreach (var cliente in listaClientes)
            {
                cliente.ResidenteDe = await _scContext.Cidades.FirstOrDefaultAsync(c => c.Id == cliente.CidadeId);
                cliente.Enderecos = await _scContext.Enderecos.Where(e => e.ClienteId == cliente.Id).ToListAsync();
                clientesDto.Add(_mapper.Map<LerTodosClientesDto>(cliente));
            }

            return clientesDto;
        }
        public async Task<Clientes> CadastrarNovoClienteViaHttpAsync(CadastrarClienteDto clienteNovoDto, Guid id)
        {
            var cepCliente = await _cepApiCliet.GetCepAsync(clienteNovoDto.Cep);
            if (cepCliente == null) return null;

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
                Id = id,
                Nome = clienteNovoDto.Nome,
                DataNascimento = clienteNovoDto.DataNascimento,
                CidadeId = cidadeCliente.Id,
            };

            _scContext.Clientes.Add(novoCliente);
            await _scContext.SaveChangesAsync();
            return novoCliente;
        }
        public async Task<LerTodosClientesDto> GetClientePorIdAsync(Guid id)
        {
            var cliente = await _scContext.Clientes.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
            if (cliente == null) return null;

            cliente.ResidenteDe = await _scContext.Cidades.FirstOrDefaultAsync(c => c.Id == cliente.CidadeId);
            cliente.Enderecos = await _scContext.Enderecos.Where(e => e.ClienteId == cliente.Id).ToListAsync();
            var clienteDto = _mapper.Map<LerTodosClientesDto>(cliente);
            return clienteDto;
        }
        public async Task<Clientes> CadastrarNovoClienteAsync(CadastrarClienteDto clienteNovoDto)
        {
            var cepCliente = await _cepApiCliet.GetCepAsync(clienteNovoDto.Cep);
            if (cepCliente == null) return null;

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
            return novoCliente;
        }
        public async Task<Result> DeletarClienteAsync(Guid id)
        {
            var cliente = await _scContext.Clientes.FirstOrDefaultAsync(c => c.Id == id);
            if (cliente == null) return Result.Fail("Cliente não encontrado");

            try
            {
                _scContext.Clientes.Remove(cliente);
                await _scContext.SaveChangesAsync();
                return Result.Ok();
            }
            catch (Exception)
            {
                return Result.Fail("Ocorreu um erro ao salvar os dados");
            }
        }
        public async Task<Result> AtualizarClienteAsync(Guid id, AtualizarClienteDto clienteAtualizadoDto)
        {
            var cliente = await _scContext.Clientes.FirstOrDefaultAsync(c => c.Id == id);
            if (cliente == null) return Result.Fail("Cliente não encontrado");

            var cepCliente = await _cepApiCliet.GetCepAsync(clienteAtualizadoDto.Cep);
            if (cepCliente == null) return Result.Fail("Cep não encontrado");

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
            return Result.Ok();
        }
    }
}
