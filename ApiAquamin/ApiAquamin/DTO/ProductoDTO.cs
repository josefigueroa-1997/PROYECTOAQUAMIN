namespace ApiAquamin.DTO
{
    public class ProductoDTO
    {
        public int Id { get; set; }
        public string Tipo_Producto { get; set; } = null!;
        public decimal Precio { get; set; }
        public int Stock { get; set; }
    }
}
