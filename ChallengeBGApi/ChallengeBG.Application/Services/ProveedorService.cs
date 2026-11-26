using ChallengeBG.Application.DTOs;
using ChallengeBG.Application.Ports.Entrada;
using ChallengeBG.Application.Ports.Salida;

namespace ChallengeBG.Application.Services
{
    public class ProveedorService(IProveedorRepository _proveedorRepository) : IProveedorService
    {
        public async Task<List<GetProveedorResponse>> GetAllAsync()
        {
            return await _proveedorRepository.GetAllAsync();
        }
    }
}