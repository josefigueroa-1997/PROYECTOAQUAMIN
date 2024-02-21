namespace ApiAquamin.DTO
{
    public class UsuarioDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Telefono { get; set; } = null!;
        public string Calle { get; set; } = null!;
        public int Numero { get; set; }
        public string NombreComuna { get; set; } = null!;
    }
}
