using ChallengeBG.Application.DTOs;
using ChallengeBG.Application.Ports.Salida;
using ChallengeBG.Domain.Entities;
using ChallengeBG.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ChallengeBG.Infrastructure.Repositories
{
    public class ProductoRepository(ChallengeBGContext _db) : IProductoRepository
    {
        private static IQueryable<GetProductoResponse> MapResponse(IQueryable<Producto> productos)
        {
            return productos
                .Select(p => new GetProductoResponse(
                    p.ProductoId,
                    p.Nombre,
                    p.Descripcion,
                    p.ProductoProveedor.Select(pp => new GetProductoProveedorResponse(
                        pp.ProveedorId,
                        pp.Proveedor.Nombre,
                        pp.NumeroLote,
                        pp.Precio,
                        pp.Stock
                    )).ToList()
                ));
        }

        public async Task<List<GetProductoResponse>> GetAllAsync(string? buscar)
        {
            var productos = _db.Producto.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(buscar))
                productos = productos.Where(p => p.Nombre.Contains(buscar));

            return await MapResponse(productos.OrderBy(p => p.Nombre)).ToListAsync();
        }

        public async Task<GetProductoResponse?> GetByIdAsync(int id)
        {
            var productos = _db.Producto.AsNoTracking().Where(p => p.ProductoId == id);

            return await MapResponse(productos).FirstOrDefaultAsync();
        }

        public async Task<Producto?> FindAsync(int id)
        {
            return await _db.Producto
                .Include(p => p.ProductoProveedor)
                .FirstOrDefaultAsync(p => p.ProductoId == id);
        }

        public async Task<bool> ExistsNameAsync(string nombre, int idExcluido)
        {
            return await _db.Producto.AnyAsync(p => p.Nombre == nombre && p.ProductoId != idExcluido);
        }

        public async Task AddAsync(Producto producto)
        {
            await _db.Producto.AddAsync(producto);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Producto producto)
        {
            _db.Producto.Update(producto);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(Producto producto)
        {
            _db.Producto.Remove(producto);
            await _db.SaveChangesAsync();
        }
    }
}