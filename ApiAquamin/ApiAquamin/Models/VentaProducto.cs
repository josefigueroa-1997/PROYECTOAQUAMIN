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
        public string Estado_Pago { get; set; } = null!;
        public string Metodo_Pago { get; set; } = null!;
        public int Cantidad { get; set; }
        public string? Detalle { get; set; } 
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }
        public string Tipo_Venta { get; set; } = null!;
        public decimal Valor_Despacho { get; set; }
        public int? Prioridad { get; set; }
        public virtual Producto IdProductoNavigation { get; set; } = null!;
        public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
        public virtual ICollection<Boletum> Boleta { get; set; }
        public virtual ICollection<RutaDespacho> RutaDespachos { get; set; }
    }
}
