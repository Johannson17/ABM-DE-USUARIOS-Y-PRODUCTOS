using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Api.Models
{
    public class UsuarioPermiso
    {
        [Key]
        public int Id { get; set; }

        // FK a Usuario
        public int UsuarioId { get; set; }
        [ForeignKey("UsuarioId")]
        public Usuario Usuario { get; set; }

        // FK a Permiso
        public int PermisoId { get; set; }
        [ForeignKey("PermisoId")]
        public Permiso Permiso { get; set; }
    }
}
