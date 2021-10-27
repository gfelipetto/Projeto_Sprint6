using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using SisProdutos.Data.Dtos;
using SisProdutos.HttpClients;
using SisProdutos.Models;
using System;
using System.Threading.Tasks;

namespace SisProdutos.Services
{
    public class CadastraService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser<Guid>> _userManager;
        private readonly SisClientesApiClient _sisClientesApiClient;
        public CadastraService(IMapper mapper, UserManager<IdentityUser<Guid>> userManager, SisClientesApiClient sisClientesApiClient)
        {
            _mapper = mapper;
            _userManager = userManager;
            _sisClientesApiClient = sisClientesApiClient;
        }
        public async Task<Result> CadastraUsuarioAsync(CadastrarUsuarioDto novoUsuario)
        {
            try
            {
                novoUsuario.Id = Guid.NewGuid();

                var usuario = _mapper.Map<Usuario>(novoUsuario);
                IdentityUser<Guid> usuarioIdentity = _mapper.Map<IdentityUser<Guid>>(usuario);

                IdentityResult resultadoIdentity = await _userManager.CreateAsync(usuarioIdentity, novoUsuario.Password);
                if (resultadoIdentity.Succeeded)
                {
                    var resultado = await _sisClientesApiClient.CadastrarClienteApiAsync(novoUsuario);
                    if(resultado.IsSuccess)return Result.Ok();
                }
                return Result.Fail("Falha ao cadastrar usuário");
            }
            catch (Exception e)
            {
                return Result.Fail("Falha ao cadastrar usuário");
            }
        }
    }
}
