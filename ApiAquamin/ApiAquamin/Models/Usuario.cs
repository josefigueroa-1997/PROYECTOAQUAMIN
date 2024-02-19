using System;
using System.Collections.Generic;

namespace ApiAquamin.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            VentaProductos = new HashSet<VentaProducto>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Telefono { get; set; } = null!;
        public string? Correo { get; set; }
        public string? Constrasena { get; set; }
        public int IdDireccion { get; set; }
        public int IdRol { get; set; }

        public virtual Direccion IdDireccionNavigation { get; set; } = null!;
        public virtual Rol IdRolNavigation { get; set; } = null!;
        public virtual ICollection<VentaProducto> VentaProductos { get; set; }
    }
}
