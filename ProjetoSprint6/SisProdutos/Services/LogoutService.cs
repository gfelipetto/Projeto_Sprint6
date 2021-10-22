using FluentResults;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace SisProdutos.Services
{
    public class LogoutService
    {
        private SignInManager<IdentityUser<Guid>> _signManager;

        public LogoutService(SignInManager<IdentityUser<Guid>> signManager)
        {
            _signManager = signManager;
        }

        public Result DeslogarUsuario()
        {
            Task resultadoIdentity = _signManager.SignOutAsync();
            if (resultadoIdentity.IsCompletedSuccessfully) return Result.Ok();
            return Result.Fail("Logout falhou");
        }
    }
}
