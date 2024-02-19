using System;
using System.Collections.Generic;

namespace ApiAquamin.Models
{
    public partial class Boletum
    {
        public int Id { get; set; }
        public string Rut { get; set; } = null!;
        public DateTime Fecha { get; set; }
        public string Detalle { get; set; } = null!;
        public int IdVenta { get; set; }

        public virtual VentaProducto IdVentaNavigation { get; set; } = null!;
    }
}
