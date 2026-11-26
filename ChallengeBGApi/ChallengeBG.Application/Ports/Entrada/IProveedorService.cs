using ChallengeBG.Application.DTOs;

namespace ChallengeBG.Application.Ports.Entrada
{
    public interface IProveedorService
    {
        Task<List<GetProveedorResponse>> GetAllAsync();
    }
}