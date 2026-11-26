using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ChallengeBG.Application.DTOs;
using ChallengeBG.Application.Ports.Salida;
using ChallengeBG.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ChallengeBG.Infrastructure.Security
{
    public class SeguridadService(IConfiguration _config) : ISeguridadService
    {
        public bool VerifyClave(string clave, string claveHash)
        {
            return BCrypt.Net.BCrypt.Verify(clave, claveHash);
        }

        public LoginResponse GenerateToken(Usuario usuario)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expira = DateTime.UtcNow.AddMinutes(int.Parse(_config["Jwt:ExpiraMinutos"]!));

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.UsuarioId.ToString()),
                new Claim(ClaimTypes.Name, usuario.NombreUsuario)
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: expira,
                signingCredentials: credentials);

            string tokenTexto = new JwtSecurityTokenHandler().WriteToken(token);

            return new LoginResponse(tokenTexto, usuario.NombreCompleto, expira);
        }
    }
}