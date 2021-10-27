using AutoMapper;
using SisProdutos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SisProdutos.Profiles
{
    public class ComprasProfile : Profile
    {
        public ComprasProfile()
        {
            CreateMap<Produtos, Compras>();
        }
    }
}
