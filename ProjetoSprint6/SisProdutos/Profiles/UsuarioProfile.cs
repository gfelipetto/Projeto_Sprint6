using AutoMapper;
using Microsoft.AspNetCore.Identity;
using SisProdutos.Data.Dtos;
using SisProdutos.Models;
using System;

namespace SisProdutos.Profiles
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            CreateMap<CadastrarUsuarioDto, Usuario>();
            CreateMap<Usuario, IdentityUser<Guid>>();
        }
    }
}
