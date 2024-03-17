namespace ApiAquamin.DTO
{
    public class UsuarioDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Telefono { get; set; } = null!;
        public string Calle { get; set; } = null!;
        public int Numero { get; set; }
        public int IdComuna { get; set; }
        public string NombreComuna { get; set; } = null!;
        public int IdRol { get; set; }
        public int IDireccion { get; set; }
    }
}
