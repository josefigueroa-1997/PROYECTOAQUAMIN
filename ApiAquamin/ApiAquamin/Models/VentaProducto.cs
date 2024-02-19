using System;
using System.Collections.Generic;

namespace ApiAquamin.Models
{
    public partial class VentaProducto
    {
        public VentaProducto()
        {
            Boleta = new HashSet<Boletum>();
            RutaDespachos = new HashSet<RutaDespacho>();
        }

        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public int IdProducto { get; set; }
        public string EstadoPago { get; set; } = null!;
        public string MetodoPago { get; set; } = null!;
        public int Cantidad { get; set; }
        public string Detalle { get; set; } = null!;
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }
        public string TipoVenta { get; set; } = null!;

        public virtual Producto IdProductoNavigation { get; set; } = null!;
        public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
        public virtual ICollection<Boletum> Boleta { get; set; }
        public virtual ICollection<RutaDespacho> RutaDespachos { get; set; }
    }
}
