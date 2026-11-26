using System.ComponentModel.DataAnnotations;

namespace ChallengeBG.Application.DTOs
{
    #region requests
    public record LoginRequest(
        [Required(ErrorMessage = "El usuario es obligatorio")] string nombreUsuario,
        [Required(ErrorMessage = "La clave es obligatoria")] string clave);
    #endregion

    #region responses
    public record LoginResponse(string token, string nombreCompleto, DateTime expira);
    #endregion
}