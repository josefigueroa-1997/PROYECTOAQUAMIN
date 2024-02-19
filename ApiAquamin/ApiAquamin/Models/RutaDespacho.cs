using System;
using System.Collections.Generic;

namespace ApiAquamin.Models
{
    public partial class RutaDespacho
    {
        public int Id { get; set; }
        public int IdVenta { get; set; }
        public int IdRuta { get; set; }
        public DateTime Fecha { get; set; }
        public string? Comentario { get; set; }
        public byte[]? Foto { get; set; }

        public virtual Rutum IdRutaNavigation { get; set; } = null!;
        public virtual VentaProducto IdVentaNavigation { get; set; } = null!;
    }
}
