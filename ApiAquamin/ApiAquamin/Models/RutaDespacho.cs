using System;
using System.Collections.Generic;

namespace ApiAquamin.Models
{
    public partial class RutaDespacho
    {
        public int Id { get; set; }
        public int IdVenta { get; set; }
        
        public string? Comentario { get; set; }
        public byte[]? Foto { get; set; }
        public string? Estado { get; set; }
        public virtual VentaProducto IdVentaNavigation { get; set; } = null!;
    }
}
