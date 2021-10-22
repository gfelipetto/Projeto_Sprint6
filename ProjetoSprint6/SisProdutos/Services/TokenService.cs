using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using SisProdutos.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SisProdutos.Services
{
    public class TokenService
    {
        public Token CriarToken(IdentityUser<Guid> usuario)
        {
            Claim[] direitosUsuario = new Claim[]
            {
                new Claim("userName", usuario.UserName),
                new Claim("id", usuario.Id.ToString())
            };

            var chave = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("8JD8ssahfuds-s9dsfjm=324ijsfdg8uGabriel23034dsfdsfji3210i09123jmkdfijokasdf")
                );
            var credenciais = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: direitosUsuario,
                signingCredentials: credenciais,
                expires: DateTime.UtcNow.AddHours(1)
                );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return new Token(tokenString);
        }
    }
}
