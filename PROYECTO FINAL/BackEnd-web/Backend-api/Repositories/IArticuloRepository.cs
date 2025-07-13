using Backend.Api.Models;

public interface IArticuloRepository
{
    Task<IEnumerable<Articulo>> GetAllAsync();
    Task<Articulo> GetByIdAsync(int id);
    Task<Articulo> GetBySkuAsync(string sku); // Opcional, útil para búsquedas
    Task AddAsync(Articulo articulo);
    Task UpdateAsync(Articulo articulo);
    Task DeleteAsync(int id);
}
