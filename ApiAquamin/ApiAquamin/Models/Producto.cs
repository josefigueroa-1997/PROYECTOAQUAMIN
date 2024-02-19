using System;
using System.Collections.Generic;

namespace ApiAquamin.Models
{
    public partial class Producto
    {
        public Producto()
        {
            VentaProductos = new HashSet<VentaProducto>();
        }

        public int Id { get; set; }
        public string TipoProducto { get; set; } = null!;
        public decimal Precio { get; set; }
        public int Stock { get; set; }

        public virtual ICollection<VentaProducto> VentaProductos { get; set; }
    }
}
