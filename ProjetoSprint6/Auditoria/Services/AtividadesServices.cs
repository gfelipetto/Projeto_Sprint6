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
    public class AtividadesServices
    {
        private readonly AuditoriaDbContext _auditoriaDbContext;
        private readonly IMapper _mapper;

        public AtividadesServices(AuditoriaDbContext auditoriaDbContext, IMapper mapper)
        {
            _auditoriaDbContext = auditoriaDbContext;
            _mapper = mapper;
        }
        public async Task<List<LerAtividadeDto>> GetTodasAtividadesAsync()
        {
            var listaAtividades = await _auditoriaDbContext.RegistroDeAtividades.ToListAsync();
            var listaAtividadesDto = _mapper.Map<List<RegistroDeAtividades>, List<LerAtividadeDto>>(listaAtividades);
            return listaAtividadesDto;
        }
        public async Task<LerAtividadeDto> GetAtividadePorIdAsync(Guid id)
        {
            var atividade = await _auditoriaDbContext.RegistroDeAtividades.FirstOrDefaultAsync(a => a.Id == id);
            var atividadeDto = _mapper.Map<LerAtividadeDto>(atividade);
            return atividadeDto;
        }
        public async Task<RegistroDeAtividades> IncluirAtividadeAsync(IncluirAtividadeDto incluirAtividadeDto)
        {
            var novaAtividade = _mapper.Map<RegistroDeAtividades>(incluirAtividadeDto);
            await _auditoriaDbContext.RegistroDeAtividades.AddAsync(novaAtividade);
            await _auditoriaDbContext.SaveChangesAsync();
            return novaAtividade;
        }


    }
}
