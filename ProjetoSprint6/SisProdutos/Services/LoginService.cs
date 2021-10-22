using FluentResults;
using Microsoft.AspNetCore.Identity;
using SisProdutos.Data.Resquests;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SisProdutos.Services
{
    public class LoginService
    {
        private SignInManager<IdentityUser<Guid>> _signManager;
        private TokenService _tokenService;

        public LoginService(SignInManager<IdentityUser<Guid>> signManager, TokenService tokenService)
        {
            _signManager = signManager;
            _tokenService = tokenService;
        }

        public async Task<Result> LogarUsuarioAsync(LoginRequest request)
        {
            SignInResult resultado = await _signManager.PasswordSignInAsync(request.UserName, request.Password, false, false);
            if (resultado.Succeeded)
            {
                var usuarioIdentity = _signManager.UserManager.Users.FirstOrDefault(u => u.NormalizedUserName == request.UserName.ToUpper());
                var token = _tokenService.CriarToken(usuarioIdentity);
                return Result.Ok().WithSuccess(token.Valor);
            }
            return Result.Fail("O login falhou");
        }
    }
}
