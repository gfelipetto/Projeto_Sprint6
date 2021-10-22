using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using SisProdutos.Data.Dtos;
using SisProdutos.Models;
using System;
using System.Threading.Tasks;

namespace SisProdutos.Services
{
    public class CadastraService
    {
        private IMapper _mapper;
        private UserManager<IdentityUser<Guid>> _userManager; 
        public CadastraService(IMapper mapper, UserManager<IdentityUser<Guid>> userManager)
        {
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<Result> CadastraUsuarioAsync(CadastrarUsuarioDto novoUsuario)
        {
            var usuario = _mapper.Map<Usuario>(novoUsuario);
            IdentityUser<Guid> usuarioIdentity = _mapper.Map<IdentityUser<Guid>>(usuario);
            IdentityResult resultadoIdentity = await _userManager.CreateAsync(usuarioIdentity, novoUsuario.Password);
            if (resultadoIdentity.Succeeded) return Result.Ok();
            return Result.Fail("Falha ao cadastrar usuário");
        }
    }
}
