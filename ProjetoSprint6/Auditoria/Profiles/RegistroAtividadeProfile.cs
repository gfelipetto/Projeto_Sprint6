using Auditoria.Data.Dtos;
using Auditoria.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auditoria.Profiles
{
    public class RegistroAtividadeProfile : Profile
    {
        public RegistroAtividadeProfile()
        {
            CreateMap<RegistroDeAtividades, LerAtividadeDto>();
            CreateMap<IncluirAtividadeDto, RegistroDeAtividades>();
        }
    }
}
