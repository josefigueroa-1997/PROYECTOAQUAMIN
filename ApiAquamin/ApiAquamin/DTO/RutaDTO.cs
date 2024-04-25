namespace ApiAquamin.DTO
{
    public class RutaDTO
    {
        public int NumeroDespacho{ get; set; }
        public int NumeroVenta { get; set; }
        public DateTime Fecha { get; set; }
        public string NombreUsuario { get; set; } = null!;
        public int VEINTELTS { get; set; }
        public int DIEZLTS { get; set; }
        public string Detalle { get; set; } = null!;
        public string Telefono { get; set; } = null!;
        public string Calle { get; set; } = null!;
        public int Numero { get; set; }
        public string Comuna { get; set; } = null!;
        public string Metodo_Pago { get; set; } = null!;
        public decimal Total { get; set; }
        public string EstadoEntrega { get; set; } = null!;
        public string Comentario { get; set; } = null!;
        public byte[]? Foto { get; set; }


    }
}
