using AutoMapper;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using SisClientes.Data;
using SisClientes.Data.Dtos;
using SisClientes.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SisClientes.Services
{
    public class EnderecosService
    {
        private readonly SisClientesDbContext _scContext;
        private readonly IMapper _mapper;

        public EnderecosService(SisClientesDbContext sisContext, IMapper mapper)
        {
            _scContext = sisContext;
            _mapper = mapper;
        }
        public async Task<List<LerTodosEnderecosDto>> GetTodosEnderecosAsync()
        {
            var enderecosDto = new List<LerTodosEnderecosDto>();

            var listaEnderecos = await _scContext.Enderecos.ToListAsync();
            foreach (var endereco in listaEnderecos)
            {
                enderecosDto.Add(_mapper.Map<LerTodosEnderecosDto>(endereco));
            }
            return enderecosDto;
        }
        public async Task<LerTodosEnderecosDto> GetEnderecoPorIdAsync(Guid id)
        {
            var endereco = await _scContext.Enderecos.FirstOrDefaultAsync(e => e.Id == id);
            if (endereco == null) return null;

            var enderecoDto = _mapper.Map<LerTodosEnderecosDto>(endereco);
            return enderecoDto;
        }
        public async Task<Enderecos> CadastrarNovoEnderecoAsync(Guid id, CadastrarNovoEnderecoDto novoEnderecoDto)
        {
            var cliente = await _scContext.Clientes.FirstOrDefaultAsync(c => c.Id == id);
            if (cliente == null) return null;

            var novoEndereco = new Enderecos
            {
                ClienteId = cliente.Id,
                Cep = novoEnderecoDto.Cep,
                Logradouro = novoEnderecoDto.Logradouro,
                Bairro = novoEnderecoDto.Bairro,
                NumeroCasa = novoEnderecoDto.NumeroCasa,
                Complemento = novoEnderecoDto.Complemento,
                EhPrincipal = novoEnderecoDto.EhPrincipal
            };
            _scContext.Enderecos.Add(novoEndereco);
            await _scContext.SaveChangesAsync();
            return novoEndereco;
        }
        public async Task<Result> AtualizarEnderecoAsync(Guid id, AtualizarEnderecoDto enderecoAtualizado)
        {
            var endereco = await _scContext.Enderecos.FirstOrDefaultAsync(e => e.Id == id);
            if (endereco == null) return Result.Fail("O endereço não foi encontrado");

            _mapper.Map(enderecoAtualizado, endereco);
            await _scContext.SaveChangesAsync();
            return Result.Ok();
        }
        public async Task<Result> DeletarEnderecoAsync(Guid id)
        {
            var endereco = await _scContext.Enderecos.FirstOrDefaultAsync(e => e.Id == id);
            if (endereco == null) return Result.Fail("O endereço não foi encontrado");

            _scContext.Remove(endereco);
            await _scContext.SaveChangesAsync();
            return Result.Ok();
        }
    }
}
