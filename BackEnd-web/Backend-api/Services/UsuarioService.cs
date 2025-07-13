using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Api.Models;
using Backend.Api.Repositories;

namespace Backend.Api.Services
{
    public class UsuarioService
    {
        private readonly IUsuarioRepository _repo;
        public UsuarioService(IUsuarioRepository repo) => _repo = repo;

        public Task<IEnumerable<Usuario>> GetAllUsuariosAsync() => _repo.GetAllAsync();

        public Task<Usuario> GetUsuarioByIdAsync(int id) => _repo.GetByIdAsync(id);

        // (Opcional) Buscar por nombre de usuario
        public Task<Usuario> GetUsuarioByNombreUsuarioAsync(string nombreUsuario) => _repo.GetByNombreUsuarioAsync(nombreUsuario);

        // (Opcional) Buscar por email
        public Task<Usuario> GetUsuarioByEmailAsync(string email) => _repo.GetByEmailAsync(email);

        public Task AddUsuarioAsync(Usuario usuario) => _repo.AddAsync(usuario);
        public Task UpdateUsuarioAsync(Usuario usuario) => _repo.UpdateAsync(usuario);
        public Task DeleteUsuarioAsync(int id) => _repo.DeleteAsync(id);
        public Task AsignarPermisoAsync(int usuarioId, int permisoId) => _repo.AsignarPermisoAsync(usuarioId, permisoId);

    }
}
