using ChallengeBG.Application.DTOs;

namespace ChallengeBG.Application.Ports.Entrada
{
    public interface IProductoService
    {
        Task<List<GetProductoResponse>> GetAllAsync(string? buscar);

        Task<GetProductoResponse> GetByIdAsync(int id);

        Task CreateAsync(CreateProductoRequest req);

        Task UpdateAsync(UpdateProductoRequest req);

        Task DeleteAsync(int id);
    }
}