namespace ApiAquamin.DTO
{
    public class LoginDTO
    {
        public int IdUsuario { get; set; }
        public string NombreUsuario { get; set; } = null!;
        public string Contrasena { get; set; } = null!;
        public int IdRol { get; set; }
    }
}
