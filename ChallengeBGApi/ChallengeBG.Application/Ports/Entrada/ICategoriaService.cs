using ChallengeBG.Application.DTOs;

namespace ChallengeBG.Application.Ports.Entrada
{
    public interface ICategoriaService
    {
        Task<List<GetCategoriaResponse>> GetAllAsync();
    }
}