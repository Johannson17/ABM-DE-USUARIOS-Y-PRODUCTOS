using Microsoft.AspNetCore.Mvc;
using Backend.Api.Models;
using Backend.Api.Services;
using Backend.Api.Helpers;

namespace Backend.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UsuarioService _service;
        private readonly string _jwtSecret;

        public AuthController(UsuarioService service, IConfiguration config)
        {
            _service = service;
            _jwtSecret = config.GetSection("JwtConfig:Secret").Value;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var usuario = await _service.GetUsuarioByNombreUsuarioAsync(request.NombreUsuario);
            if (usuario == null || !PasswordHasher.VerifyPassword(request.Password, usuario.PasswordHash))
                return Unauthorized(new { mensaje = "Usuario o contraseña incorrectos." });

            // Opcional: Solo usuarios activos pueden loguear
            if (!usuario.Activo)
                return Unauthorized(new { mensaje = "Usuario inactivo." });

            var token = JwtHelper.GenerateToken(usuario, _jwtSecret);
            return Ok(new
            {
                token,
                usuario = new
                {
                    usuario.Id,
                    usuario.NombreUsuario,
                    usuario.Nombre,
                    usuario.Email,
                    usuario.Activo,
                    Permisos = usuario.UsuarioPermisos.Select(up => up.Permiso.Nombre)
                }
            });
        }
    }
}
