namespace ApiAquamin.DTO
{
    public class HistorialClienteDTO
    {
        public int IDVENTA { get; set; }
        public DateTime FECHA { get; set; }
        public string NombreUsuario { get; set; } = null!;
        public int ID_PRODUCTO { get; set; }
        public string TIPO_PRODUCTO { get; set; } = null!;
        public decimal PRECIOUNITARIO { get; set; }
        public int CANTIDAD { get; set; }
        public string? DETALLE { get; set; }
        public string ESTADO_PAGO { get; set; } = null!;
        public string METODO_PAGO { get; set; } = null!;
        public decimal TOTAL { get; set; }

    }
}
