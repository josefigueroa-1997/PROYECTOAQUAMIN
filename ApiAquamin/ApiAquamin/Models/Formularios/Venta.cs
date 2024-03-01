namespace ApiAquamin.Models.Formularios
{
    public class Venta
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public int IdProducto { get; set; }
        public string Estado_Pago { get; set; } = null!;
        public string Metodo_Pago { get; set; } = null!;
        public string? Detalle { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }
        public string Tipo_Venta { get; set; } = null!;
        public decimal Valor_Despacho { get; set; }
        public int? Prioridad { get; set; }
        public int Cantidad_20LTS { get; set; }
        public int Cantidad_10LTS { get; set; }
        public int Extra { get; set; }
    }
}
