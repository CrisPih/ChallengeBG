using ChallengeBG.Application.Ports.Entrada;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace ChallengeBG.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class ProveedorController(IProveedorService _proveedorService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> getAll()
        {
            var proveedores = await _proveedorService.GetAllAsync();

            return Ok(proveedores);
        }
    }
}