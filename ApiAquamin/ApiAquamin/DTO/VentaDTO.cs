namespace ApiAquamin.DTO
{
    public class VentaDTO
    {
        public int NumeroVenta { get; set; }
        public int IdVenta { get; set; }
        public string Fecha { get; set; } = null!;
        public int IdUsuario { get; set; }
        public string NombreUsuario { get; set; } = null!;
        public int veinteLTS { get; set; }
        public int diezLTS { get; set; }
        public string? Detalle { get; set; }
        public string Metodo_Pago { get; set; } = null!;
        public string Estado_Pago { get; set; } = null!;
        public string Comuna { get; set; } = null!;
        public decimal Total { get; set; }
    }
}
