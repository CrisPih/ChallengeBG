using ChallengeBG.Application.DTOs;

namespace ChallengeBG.Application.Ports.Entrada
{
    public interface IAuthService
    {
        Task<LoginResponse> LoginAsync(LoginRequest req);
    }
}