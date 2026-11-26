using ChallengeBG.Application.DTOs;
using ChallengeBG.Application.Exceptions;
using ChallengeBG.Application.Ports.Entrada;
using ChallengeBG.Application.Ports.Salida;
using ChallengeBG.Domain.Entities;

namespace ChallengeBG.Application.Services
{
    public class ProductoService(IProductoRepository _productoRepository) : IProductoService
    {
        #region Helper Methods
        private async Task ValidateNameAsync(string nombre, int idExcluido)
        {
            if (await _productoRepository.ExistsNameAsync(nombre, idExcluido))
                throw new BusinessException("Ya existe un producto con ese nombre");
        }

        private static List<ProductoProveedor> MapPrecios(List<ProductoProveedorRequest> precios)
        {
            return precios.Select(p => new ProductoProveedor
            {
                ProveedorId = p.proveedorId,
                NumeroLote = p.numeroLote,
                Precio = p.precio,
                Stock = p.stock
            }).ToList();
        }

        private async Task<Producto> FindOrThrowAsync(int id)
        {
            return await _productoRepository.FindAsync(id)
                ?? throw new NotFoundException("El producto no existe");
        }
        #endregion

        public async Task<List<GetProductoResponse>> GetAllAsync(string? buscar)
        {
            return await _productoRepository.GetAllAsync(buscar);
        }

        public async Task<GetProductoResponse> GetByIdAsync(int id)
        {
            return await _productoRepository.GetByIdAsync(id)
                ?? throw new NotFoundException("El producto no existe");
        }

        public async Task CreateAsync(CreateProductoRequest req)
        {
            await ValidateNameAsync(req.nombre, 0);

            var producto = new Producto
            {
                Nombre = req.nombre,
                Descripcion = req.descripcion,
                ProductoProveedor = MapPrecios(req.precios)
            };

            await _productoRepository.AddAsync(producto);
        }

        public async Task UpdateAsync(UpdateProductoRequest req)
        {
            var producto = await FindOrThrowAsync(req.id);

            await ValidateNameAsync(req.nombre, req.id);

            producto.Nombre = req.nombre;
            producto.Descripcion = req.descripcion;
            producto.ProductoProveedor.Clear();

            foreach (var precio in MapPrecios(req.precios))
                producto.ProductoProveedor.Add(precio);

            await _productoRepository.UpdateAsync(producto);
        }

        public async Task DeleteAsync(int id)
        {
            var producto = await FindOrThrowAsync(id);

            await _productoRepository.DeleteAsync(producto);
        }
    }
}