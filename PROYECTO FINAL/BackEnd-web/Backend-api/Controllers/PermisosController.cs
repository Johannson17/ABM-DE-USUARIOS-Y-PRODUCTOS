using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Api.Models;
using Backend.Api.Services;

namespace Backend.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PermisosController : ControllerBase
    {
        private readonly PermisoService _service;

        public PermisosController(PermisoService service)
        {
            _service = service;
        }

        // GET: api/permisos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Permiso>>> GetAll()
            => Ok(await _service.GetAllPermisosAsync());

        // GET: api/permisos/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Permiso>> GetById(int id)
        {
            var permiso = await _service.GetPermisoByIdAsync(id);
            if (permiso == null) return NotFound();
            return Ok(permiso);
        }

        // (Opcional) GET: api/permisos/nombre/{nombre}
        [HttpGet("nombre/{nombre}")]
        public async Task<ActionResult<Permiso>> GetByNombre(string nombre)
        {
            var permiso = await _service.GetPermisoByNombreAsync(nombre);
            if (permiso == null) return NotFound();
            return Ok(permiso);
        }

        // POST: api/permisos
        [HttpPost]
        public async Task<ActionResult> Create(Permiso permiso)
        {
            await _service.AddPermisoAsync(permiso);
            return CreatedAtAction(nameof(GetById), new { id = permiso.Id }, permiso);
        }

        // DELETE: api/permisos/{id}
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _service.DeletePermisoAsync(id);
            return NoContent();
        }
    }
}
