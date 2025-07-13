using Backend.Api.Models;

public interface IPermisoRepository
{
    Task<IEnumerable<Permiso>> GetAllAsync();
    Task<Permiso> GetByIdAsync(int id);
    Task<Permiso> GetByNombreAsync(string nombre); // Opcional, útil para búsquedas
    Task AddAsync(Permiso permiso);
    Task DeleteAsync(int id);
}
