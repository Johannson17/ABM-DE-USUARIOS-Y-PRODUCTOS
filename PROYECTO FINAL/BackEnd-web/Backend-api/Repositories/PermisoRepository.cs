using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Backend.Api.Models;
using Backend.Api.Data;

namespace Backend.Api.Repositories
{
    public class PermisoRepository : IPermisoRepository
    {
        private readonly AppDbContext _context;
        public PermisoRepository(AppDbContext context) => _context = context;

        public async Task<IEnumerable<Permiso>> GetAllAsync() =>
            await _context.Permisos.ToListAsync();

        public async Task<Permiso> GetByIdAsync(int id) =>
            await _context.Permisos.FindAsync(id);

        // (Opcional) Buscar por nombre
        public async Task<Permiso> GetByNombreAsync(string nombre) =>
            await _context.Permisos.FirstOrDefaultAsync(p => p.Nombre == nombre);

        public async Task AddAsync(Permiso permiso)
        {
            _context.Permisos.Add(permiso);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var permiso = await GetByIdAsync(id);
            if (permiso != null)
            {
                _context.Permisos.Remove(permiso);
                await _context.SaveChangesAsync();
            }
        }
    }
}
