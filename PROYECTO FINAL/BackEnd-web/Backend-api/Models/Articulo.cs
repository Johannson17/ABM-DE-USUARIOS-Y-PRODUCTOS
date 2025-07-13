using System.ComponentModel.DataAnnotations;

namespace Backend.Api.Models
{
    public class Articulo
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string SKU { get; set; } = string.Empty;

        [Required]
        public string Nombre { get; set; } = string.Empty;

        public string Descripcion { get; set; } = string.Empty;
    }
}
