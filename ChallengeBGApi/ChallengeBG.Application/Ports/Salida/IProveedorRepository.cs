using ChallengeBG.Application.DTOs;

namespace ChallengeBG.Application.Ports.Salida
{
    public interface IProveedorRepository
    {
        Task<List<GetProveedorResponse>> GetAllAsync();
    }
}