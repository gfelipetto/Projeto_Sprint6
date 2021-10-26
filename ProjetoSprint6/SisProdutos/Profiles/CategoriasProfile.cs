using AutoMapper;
using SisProdutos.Data.Dtos.Categorias;
using SisProdutos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SisProdutos.Profiles
{
    public class CategoriasProfile : Profile
    {
        public CategoriasProfile()
        {
            CreateMap<Categorias, LerCategoriasDto>();
        }
    }
}
