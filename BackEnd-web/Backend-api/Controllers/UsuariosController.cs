using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Api.Models;
using Backend.Api.Services;
using Backend.Api.Helpers;

namespace Backend.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly UsuarioService _service;
        private readonly string _jwtSecret;

        public UsuariosController(UsuarioService service, IConfiguration config)
        {
            _service = service;
            _jwtSecret = config.GetSection("JwtConfig:Secret").Value;
        }

        // GET: api/usuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetAll() =>
            Ok(await _service.GetAllUsuariosAsync());

        // GET: api/usuarios/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Usuario>> GetById(int id)
        {
            var usuario = await _service.GetUsuarioByIdAsync(id);
            if (usuario == null) return NotFound();
            return Ok(usuario);
        }

        // (Opcional) GET: api/usuarios/nombre/{nombreUsuario}
        [HttpGet("nombre/{nombreUsuario}")]
        public async Task<ActionResult<Usuario>> GetByUsername(string nombreUsuario)
        {
            var usuario = await _service.GetUsuarioByNombreUsuarioAsync(nombreUsuario);
            if (usuario == null) return NotFound();
            return Ok(usuario);
        }

        // POST: api/usuarios
        [HttpPost]
        public async Task<ActionResult> Create(Usuario usuarioDto)
        {
            var usuario = new Usuario
            {
                NombreUsuario = usuarioDto.NombreUsuario,
                Nombre = usuarioDto.Nombre,
                Email = usuarioDto.Email,
                // ENCRIPTAR/HASHEAR LA PASS ACÁ:
                PasswordHash = PasswordHasher.HashPassword(usuarioDto.PasswordHash),
                Activo = usuarioDto.Activo
            };

            await _service.AddUsuarioAsync(usuario);
            return CreatedAtAction(nameof(GetById), new { id = usuario.Id }, usuario);
        }

        // PUT: api/usuarios/{id}
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(int id, Usuario usuario)
        {
            if (id != usuario.Id) return BadRequest();
            await _service.UpdateUsuarioAsync(usuario);
            return NoContent();
        }

        // DELETE: api/usuarios/{id}
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _service.DeleteUsuarioAsync(id);
            return NoContent();
        }

        // GET: api/usuarios/{id}/passwordhash
        [HttpGet("{id:int}/passwordhash")]
        public async Task<ActionResult<string>> GetPasswordHash(int id)
        {
            var usuario = await _service.GetUsuarioByIdAsync(id);
            if (usuario == null) return NotFound();
            return Ok(usuario.PasswordHash);
        }

        // GET: api/usuarios/{id}/permisos/token
        [HttpGet("{id:int}/permisos/token")]
        public async Task<ActionResult<string>> GetPermisosToken(int id)
        {
            var usuario = await _service.GetUsuarioByIdAsync(id);
            if (usuario == null) return NotFound();
            var token = JwtHelper.GenerateToken(usuario, _jwtSecret);
            return Ok(token);
        }

        [HttpPost("{usuarioId:int}/permisos/{permisoId:int}")]
        public async Task<ActionResult> AsignarPermiso(int usuarioId, int permisoId)
        {
            await _service.AsignarPermisoAsync(usuarioId, permisoId);
            return NoContent();
        }
    }
}
