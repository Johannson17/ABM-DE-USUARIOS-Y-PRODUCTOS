using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Api.Models;
using Backend.Api.Repositories;

namespace Backend.Api.Services
{
    public class ArticuloService
    {
        private readonly IArticuloRepository _repo;
        public ArticuloService(IArticuloRepository repo) => _repo = repo;

        public Task<IEnumerable<Articulo>> GetAllArticulosAsync() => _repo.GetAllAsync();
        public Task<Articulo> GetArticuloByIdAsync(int id) => _repo.GetByIdAsync(id);

        // (Opcional) Buscar por SKU si lo necesitás
        public Task<Articulo> GetArticuloBySkuAsync(string sku) => _repo.GetBySkuAsync(sku);

        public Task AddArticuloAsync(Articulo articulo) => _repo.AddAsync(articulo);
        public Task UpdateArticuloAsync(Articulo articulo) => _repo.UpdateAsync(articulo);
        public Task DeleteArticuloAsync(int id) => _repo.DeleteAsync(id);
    }
}
