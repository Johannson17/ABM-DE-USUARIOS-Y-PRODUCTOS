using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Backend.Api.Models;
using Backend.Api.Data;
using System.Linq;

namespace Backend.Api.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly AppDbContext _context;
        public UsuarioRepository(AppDbContext context) => _context = context;

        public async Task<IEnumerable<Usuario>> GetAllAsync() =>
            await _context.Usuarios
                .Include(u => u.UsuarioPermisos)
                .ThenInclude(up => up.Permiso)
                .ToListAsync();

        public async Task<Usuario> GetByIdAsync(int id) =>
            await _context.Usuarios
                .Include(u => u.UsuarioPermisos)
                .ThenInclude(up => up.Permiso)
                .FirstOrDefaultAsync(u => u.Id == id);

        // Opcional: buscar por nombre de usuario (no PK)
        public async Task<Usuario> GetByNombreUsuarioAsync(string nombreUsuario) =>
            await _context.Usuarios
                .Include(u => u.UsuarioPermisos)
                .ThenInclude(up => up.Permiso)
                .FirstOrDefaultAsync(u => u.NombreUsuario == nombreUsuario);

        // Opcional: buscar por email
        public async Task<Usuario> GetByEmailAsync(string email) =>
            await _context.Usuarios
                .Include(u => u.UsuarioPermisos)
                .ThenInclude(up => up.Permiso)
                .FirstOrDefaultAsync(u => u.Email == email);

        public async Task AddAsync(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Usuario usuario)
        {
            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var usuario = await GetByIdAsync(id);
            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
                await _context.SaveChangesAsync();
            }
        }

        // Métodos de permisos de usuario (por Id)
        public async Task<IEnumerable<Permiso>> GetPermisosAsync(int usuarioId)
        {
            var usuario = await GetByIdAsync(usuarioId);
            return usuario?.UsuarioPermisos.Select(up => up.Permiso) ?? Enumerable.Empty<Permiso>();
        }

        public async Task AsignarPermisoAsync(int usuarioId, int permisoId)
        {
            var usuario = await GetByIdAsync(usuarioId);
            var permiso = await _context.Permisos.FindAsync(permisoId);
            if (usuario != null && permiso != null &&
                !usuario.UsuarioPermisos.Any(up => up.PermisoId == permisoId))
            {
                usuario.UsuarioPermisos.Add(new UsuarioPermiso
                {
                    UsuarioId = usuarioId,
                    PermisoId = permisoId
                });
                await _context.SaveChangesAsync();
            }
        }

        public async Task QuitarPermisoAsync(int usuarioId, int permisoId)
        {
            var usuario = await GetByIdAsync(usuarioId);
            if (usuario != null)
            {
                var up = usuario.UsuarioPermisos.FirstOrDefault(up => up.PermisoId == permisoId);
                if (up != null)
                {
                    usuario.UsuarioPermisos.Remove(up);
                    await _context.SaveChangesAsync();
                }
            }
        }
    }
}
