using System.ComponentModel.DataAnnotations;

namespace ApiAquamin.Models.Formularios
{
    public class ActualizarUsuario
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; } = null!;
        [Required]
        public string Telefono { get; set; } = null!;
        [Required]
        public string Calle { get; set; } = null!;
        [Required]
        public int Numero { get; set; }
        [Required]
        public int IdComuna { get; set; }
        public string? Correo { get; set; }
        public string? Contrasena { get; set; }
        public int IdRol { get; set; }
        [Required]
        public int IDireccion { get; set; }
    }
}
