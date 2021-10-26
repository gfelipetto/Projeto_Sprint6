using AutoMapper;
using SisProdutos.Data.Dtos.PalavrasChave;
using SisProdutos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SisProdutos.Profiles
{
    public class PalavrasChaveCategoria : Profile
    {
        public PalavrasChaveCategoria()
        {
            CreateMap<PalavrasChave, LerPalavrasChaveDto>();
        }
    }
}
