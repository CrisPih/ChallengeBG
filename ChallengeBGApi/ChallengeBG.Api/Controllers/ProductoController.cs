using ChallengeBG.Application.DTOs;
using ChallengeBG.Application.Ports.Entrada;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace ChallengeBG.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class ProductoController(IProductoService _productoService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> getAll(string? buscar)
        {
            var productos = await _productoService.GetAllAsync(buscar);

            return Ok(productos);
        }

        [HttpGet]
        public async Task<IActionResult> getById(int id)
        {
            var producto = await _productoService.GetByIdAsync(id);

            return Ok(producto);
        }

        [HttpPost]
        public async Task<IActionResult> create(CreateProductoRequest req)
        {
            await _productoService.CreateAsync(req);

            return Ok(new { mensaje = "Producto creado correctamente" });
        }

        [HttpPut]
        public async Task<IActionResult> update(UpdateProductoRequest req)
        {
            await _productoService.UpdateAsync(req);

            return Ok(new { mensaje = "Producto actualizado correctamente" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> delete(int id)
        {
            await _productoService.DeleteAsync(id);

            return Ok(new { mensaje = "Producto eliminado correctamente" });
        }
    }
}