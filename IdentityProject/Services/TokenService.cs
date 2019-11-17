using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using IdentityProject.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace IdentityProject.Services
{
    public class TokenService
    {
        private IConfiguration _config;
        public static string GenerateToken(User user)
        {

            var tokenHandler = new JwtSecurityTokenHandler();
            //transforma a chave criada em bytes
            var key = Encoding.ASCII.GetBytes(Settings.Secret);

            //O Token Descriptor é dividido em 3 partes: Subject, Expires e SigningCredentials
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                //Subject nada mais é do que as Claims 
                Subject = new ClaimsIdentity(new Claim[]{
                                                    new Claim(ClaimTypes.Name,user.UserName.ToString()),
                                                    new Claim(ClaimTypes.Role,user.Role.ToString())
                }),
                //Expires indica em quanto tempo o token irá expirar
                Expires = DateTime.UtcNow.AddHours(2),
                //SigningCredentials indica a chave criada pelo desenvolvedor para assegurar a confidencialidade das informações 
                //Essa parte é dividida entre a chave que foi criada e o tipo de criptofrafia que irá ser usada na mesma
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                                                                SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}