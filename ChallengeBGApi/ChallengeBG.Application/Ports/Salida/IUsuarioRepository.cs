using ChallengeBG.Domain.Entities;

namespace ChallengeBG.Application.Ports.Salida
{
    public interface IUsuarioRepository
    {
        Task<Usuario?> GetByNombreUsuarioAsync(string nombreUsuario);
    }
}