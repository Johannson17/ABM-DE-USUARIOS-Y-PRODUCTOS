using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Backend.Api.Models;
using Backend.Api.Data;

namespace Backend.Api.Repositories
{
    public class ArticuloRepository : IArticuloRepository
    {
        private readonly AppDbContext _context;
        public ArticuloRepository(AppDbContext context) => _context = context;

        public async Task<IEnumerable<Articulo>> GetAllAsync() =>
            await _context.Articulos.ToListAsync();

        public async Task<Articulo> GetByIdAsync(int id) =>
            await _context.Articulos.FindAsync(id);

        // (Opcional) Buscar por SKU si lo necesitás
        public async Task<Articulo> GetBySkuAsync(string sku) =>
            await _context.Articulos.FirstOrDefaultAsync(a => a.SKU == sku);

        public async Task AddAsync(Articulo articulo)
        {
            _context.Articulos.Add(articulo);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Articulo articulo)
        {
            _context.Articulos.Update(articulo);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var articulo = await GetByIdAsync(id);
            if (articulo != null)
            {
                _context.Articulos.Remove(articulo);
                await _context.SaveChangesAsync();
            }
        }
    }
}
