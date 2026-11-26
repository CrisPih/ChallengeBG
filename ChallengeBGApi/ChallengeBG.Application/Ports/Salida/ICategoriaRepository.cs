using ChallengeBG.Application.DTOs;

namespace ChallengeBG.Application.Ports.Salida
{
    public interface ICategoriaRepository
    {
        Task<List<GetCategoriaResponse>> GetAllAsync();

        Task<bool> ExistsAsync(int id);
    }
}