using ChallengeBG.Application.DTOs;
using ChallengeBG.Application.Ports.Entrada;
using Microsoft.AspNetCore.Mvc;

namespace ChallengeBG.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController(IAuthService _authService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> login(LoginRequest req)
        {
            var respuesta = await _authService.LoginAsync(req);

            return Ok(respuesta);
        }
    }
}