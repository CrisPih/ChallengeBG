using ChallengeBG.Application.DTOs;
using ChallengeBG.Application.Ports.Entrada;
using ChallengeBG.Application.Ports.Salida;

namespace ChallengeBG.Application.Services
{
    public class CategoriaService(ICategoriaRepository _categoriaRepository) : ICategoriaService
    {
        public async Task<List<GetCategoriaResponse>> GetAllAsync()
        {
            return await _categoriaRepository.GetAllAsync();
        }
    }
}