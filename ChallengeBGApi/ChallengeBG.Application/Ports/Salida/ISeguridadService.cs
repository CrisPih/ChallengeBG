using ChallengeBG.Application.DTOs;
using ChallengeBG.Domain.Entities;

namespace ChallengeBG.Application.Ports.Salida
{
    public interface ISeguridadService
    {
        bool VerifyClave(string clave, string claveHash);

        LoginResponse GenerateToken(Usuario usuario);
    }
}