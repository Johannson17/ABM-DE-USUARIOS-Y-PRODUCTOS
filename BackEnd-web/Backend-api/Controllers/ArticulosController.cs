using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Api.Models;
using Backend.Api.Services;

namespace Backend.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArticulosController : ControllerBase
    {
        private readonly ArticuloService _service;
        public ArticulosController(ArticuloService service) => _service = service;

        // GET: api/articulos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Articulo>>> GetAll() =>
            Ok(await _service.GetAllArticulosAsync());

        // GET: api/articulos/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Articulo>> GetById(int id)
        {
            var articulo = await _service.GetArticuloByIdAsync(id);
            if (articulo == null) return NotFound();
            return Ok(articulo);
        }

        // (Opcional) GET: api/articulos/sku/{sku}
        [HttpGet("sku/{sku}")]
        public async Task<ActionResult<Articulo>> GetBySku(string sku)
        {
            var articulo = await _service.GetArticuloBySkuAsync(sku);
            if (articulo == null) return NotFound();
            return Ok(articulo);
        }

        // POST: api/articulos
        [HttpPost]
        public async Task<ActionResult> Create(Articulo articulo)
        {
            await _service.AddArticuloAsync(articulo);
            return CreatedAtAction(nameof(GetById), new { id = articulo.Id }, articulo);
        }

        // PUT: api/articulos/{id}
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(int id, Articulo articulo)
        {
            if (id != articulo.Id) return BadRequest();
            await _service.UpdateArticuloAsync(articulo);
            return NoContent();
        }

        // DELETE: api/articulos/{id}
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _service.DeleteArticuloAsync(id);
            return NoContent();
        }
    }
}
