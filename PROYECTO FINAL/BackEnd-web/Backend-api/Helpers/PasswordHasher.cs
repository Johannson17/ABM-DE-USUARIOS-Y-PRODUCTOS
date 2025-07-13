using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Backend.Api.Helpers
{
    /// <summary>
    /// Helper para hashear y verificar contraseñas usando PBKDF2 (HMACSHA256).
    /// 
    /// Algoritmo: PBKDF2-HMACSHA256
    /// Iteraciones: 10.000
    /// Longitud de hash: 256 bits (32 bytes)
    /// Salt: generado aleatoriamente por usuario, guardado junto al hash (ambos en base64)
    /// Formato del hash: "base64(salt).base64(hash)"
    /// 
    /// Interoperable con frontend usando:
    /// - PBKDF2, HMACSHA256, 10.000 iteraciones, 256 bits
    /// - Salt tomado del string antes del punto (base64)
    /// - Hash calculado y comparado con la parte después del punto (base64)
    /// </summary>
    public static class PasswordHasher
    {
        /// <summary>
        /// Genera un hash seguro a partir de una contraseña en texto plano.
        /// </summary>
        public static string HashPassword(string password)
        {
            // Generar salt aleatorio
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            // Derivar hash usando PBKDF2
            var hash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            // Devolver salt y hash en base64, separados por punto
            return $"{Convert.ToBase64String(salt)}.{hash}";
        }

        /// <summary>
        /// Verifica si una contraseña coincide con un hash almacenado.
        /// </summary>
        public static bool VerifyPassword(string password, string hashedPassword)
        {
            var parts = hashedPassword.Split('.', 2);
            if (parts.Length != 2) return false;

            // Extraer el salt en base64
            var salt = Convert.FromBase64String(parts[0]);

            // Volver a calcular el hash con el mismo salt
            var hashToCheck = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            // Comparar con el hash almacenado
            return hashToCheck == parts[1];
        }
    }
}
