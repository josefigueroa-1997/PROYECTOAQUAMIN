namespace ApiAquamin.Models.Formularios
{
    public class Venta
    {
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
    }
}
