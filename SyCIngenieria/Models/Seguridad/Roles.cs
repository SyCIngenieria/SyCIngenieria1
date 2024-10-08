using System.ComponentModel.DataAnnotations;

namespace SyCIngenieria.Models.Seguridad
{
    public class Roles
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres")]
        public string NombreRol { get; set; } = null!;
        public bool Estado { get; set; } = true;
    }
}
