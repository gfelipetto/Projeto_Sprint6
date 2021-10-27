using Auditoria.Data;
using Auditoria.Data.Dtos;
using Auditoria.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Auditoria.Services
{
    public class ExcecoesServices
    {
        private readonly AuditoriaDbContext _auditoriaDbContext;
        private readonly IMapper _mapper;
        public ExcecoesServices(AuditoriaDbContext auditoriaDbContext, IMapper mapper)
        {
            _auditoriaDbContext = auditoriaDbContext;
            _mapper = mapper;
        }
        public async Task<List<LerExcecaoDto>> GetTodasExcecoesAsync()
        {
            var listaExcecoes = await _auditoriaDbContext.RegistroExcecoes.ToListAsync();
            var listaExcecoesDto = _mapper.Map<List<RegistroExcecoes>, List<LerExcecaoDto>>(listaExcecoes);
            return listaExcecoesDto;
        }

        public async Task<LerExcecaoDto> GetExcecaoPorIdAsync(Guid id)
        {
            var excecao = await _auditoriaDbContext.RegistroExcecoes.FirstOrDefaultAsync(e => e.Id == id);
            var excecaoDto = _mapper.Map<LerExcecaoDto>(excecao);
            return excecaoDto;
        }

        public async Task<RegistroExcecoes> IncluirExcecao(IncluirExcecaoDto incluirExcecaoDto)
        {
            var novaExcecao = _mapper.Map<RegistroExcecoes>(incluirExcecaoDto);
            await _auditoriaDbContext.RegistroExcecoes.AddAsync(novaExcecao);
            await _auditoriaDbContext.SaveChangesAsync();
            return novaExcecao;
        }
    }
}
