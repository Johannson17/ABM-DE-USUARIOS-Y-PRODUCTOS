using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Api.Models;

namespace Backend.Api.Repositories
{
    public interface IUsuarioRepository
    {
        Task<IEnumerable<Usuario>> GetAllAsync();
        Task<Usuario> GetByIdAsync(int id);

        // Opcional: buscar por nombre de usuario o email
        Task<Usuario> GetByNombreUsuarioAsync(string nombreUsuario);
        Task<Usuario> GetByEmailAsync(string email);

        Task AddAsync(Usuario usuario);
        Task UpdateAsync(Usuario usuario);
        Task DeleteAsync(int id);

        // Métodos extra para permisos (asignar/quitar, etc.) usando Ids
        Task<IEnumerable<Permiso>> GetPermisosAsync(int usuarioId);

        // ASIGNAR PERMISO a usuario (por id)
        Task AsignarPermisoAsync(int usuarioId, int permisoId);

        // QUITAR PERMISO a usuario (por id)
        Task QuitarPermisoAsync(int usuarioId, int permisoId);
    }
}
