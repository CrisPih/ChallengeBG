using ChallengeBG.Application.DTOs;
using ChallengeBG.Application.Ports.Salida;
using ChallengeBG.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ChallengeBG.Infrastructure.Repositories
{
    public class ProveedorRepository(ChallengeBGContext _db) : IProveedorRepository
    {
        public async Task<List<GetProveedorResponse>> GetAllAsync()
        {
            return await _db.Proveedor.AsNoTracking()
                .Select(p => new GetProveedorResponse(p.ProveedorId, p.Nombre))
                .ToListAsync();
        }
    }
}