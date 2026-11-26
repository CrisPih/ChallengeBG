using ChallengeBG.Application.Ports.Salida;
using ChallengeBG.Domain.Entities;
using ChallengeBG.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ChallengeBG.Infrastructure.Repositories
{
    public class UsuarioRepository(ChallengeBGContext _db) : IUsuarioRepository
    {
        public async Task<Usuario?> GetByNombreUsuarioAsync(string nombreUsuario)
        {
            return await _db.Usuario.AsNoTracking()
                .FirstOrDefaultAsync(u => u.NombreUsuario == nombreUsuario);
        }
    }
}