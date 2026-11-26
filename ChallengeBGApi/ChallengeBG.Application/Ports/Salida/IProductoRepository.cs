using ChallengeBG.Application.DTOs;
using ChallengeBG.Domain.Entities;

namespace ChallengeBG.Application.Ports.Salida
{
    public interface IProductoRepository
    {
        Task<List<GetProductoResponse>> GetAllAsync(string? buscar);

        Task<GetProductoResponse?> GetByIdAsync(int id);

        Task<Producto?> FindAsync(int id);

        Task<bool> ExistsNameAsync(string nombre, int idExcluido);

        Task AddAsync(Producto producto);

        Task UpdateAsync(Producto producto);

        Task DeleteAsync(Producto producto);
    }
}