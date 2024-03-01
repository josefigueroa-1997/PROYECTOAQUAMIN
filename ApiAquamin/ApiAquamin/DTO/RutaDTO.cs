namespace ApiAquamin.DTO
{
    public class RutaDTO
    {
        public int IdVenta{ get; set; }
        public string NombreUsuario { get; set; } = null!;
        public int veinteLTs { get; set; }
        public int diezLTs { get; set; }
        public int extra { get; set; }
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
