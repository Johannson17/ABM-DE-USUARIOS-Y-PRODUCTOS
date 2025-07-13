using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Backend.Api.Models;
using Microsoft.IdentityModel.Tokens;
using System.Linq;

namespace Backend.Api.Helpers
{
    public static class JwtHelper
    {
        /// <summary>
        /// Genera un JWT para el usuario con los permisos como claims.
        /// </summary>
        public static string GenerateToken(Usuario usuario, string secret, int expireMinutes = 60)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret);

            // Claims base
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Name, usuario.Nombre ?? "")
                // Podés agregar otros claims aquí
            };

            // Agregar permisos como claims (usando la propiedad de navegación)
            if (usuario.UsuarioPermisos != null)
            {
                var permisos = usuario.UsuarioPermisos
                    .Where(up => up.Permiso != null)
                    .Select(up => up.Permiso.Nombre)
                    .ToList();

                // Todos los permisos en un solo claim
                claims.Add(new Claim("permisos", string.Join(",", permisos)));

                // Si preferís un claim por permiso, descomentá esto:
                // foreach (var p in permisos)
                //     claims.Add(new Claim("permiso", p));
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(expireMinutes),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature
                )
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
