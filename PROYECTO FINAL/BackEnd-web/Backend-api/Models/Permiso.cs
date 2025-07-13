using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Backend.Api.Models
{
    public class Permiso
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; } = string.Empty;

        public string? Descripcion { get; set; }

        public ICollection<UsuarioPermiso> UsuarioPermisos { get; set; } = new List<UsuarioPermiso>();
    }
}
