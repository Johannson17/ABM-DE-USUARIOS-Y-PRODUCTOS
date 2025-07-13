using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Api.Models;
using Backend.Api.Repositories;

namespace Backend.Api.Services
{
    public class PermisoService
    {
        private readonly IPermisoRepository _repo;
        public PermisoService(IPermisoRepository repo) => _repo = repo;

        public Task<IEnumerable<Permiso>> GetAllPermisosAsync() => _repo.GetAllAsync();
        public Task<Permiso> GetPermisoByIdAsync(int id) => _repo.GetByIdAsync(id);

        // (Opcional) Buscar por nombre
        public Task<Permiso> GetPermisoByNombreAsync(string nombre) => _repo.GetByNombreAsync(nombre);

        public Task AddPermisoAsync(Permiso permiso) => _repo.AddAsync(permiso);
        public Task DeletePermisoAsync(int id) => _repo.DeleteAsync(id);
    }
}
