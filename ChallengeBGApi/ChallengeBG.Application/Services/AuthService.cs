using ChallengeBG.Application.DTOs;
using ChallengeBG.Application.Ports.Entrada;
using ChallengeBG.Application.Ports.Salida;

namespace ChallengeBG.Application.Services
{
    public class AuthService(IUsuarioRepository _usuarioRepository,
        ISeguridadService _seguridadService) : IAuthService
    {
        public async Task<LoginResponse> LoginAsync(LoginRequest req)
        {
            var usuario = await _usuarioRepository.GetByNombreUsuarioAsync(req.nombreUsuario);

            if (usuario == null || !_seguridadService.VerifyClave(req.clave, usuario.ClaveHash))
                throw new UnauthorizedAccessException("Usuario o clave incorrectos");

            return _seguridadService.GenerateToken(usuario);
        }
    }
}