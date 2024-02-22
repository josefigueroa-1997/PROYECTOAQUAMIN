using ApiAquamin.DTO;
using ApiAquamin.Models.Formularios;
using Microsoft.AspNetCore.Mvc;

namespace ApiAquamin.Services.Interface
{
    public interface IUsuario
    {
        public Task<bool> RegistrarUsuario([FromBody] CrearUsuario usuario);
        public Task<List<UsuarioDTO>> ObtenerUsuarios(int? id,string? nombre);
        public Task<bool> ActualizarUsuario(int id,[FromBody] ActualizarUsuario usuario);
        public Task<bool> EliminarUsuario(int id);
        public Task<LoginDTO> IniciarSesion([FromBody] Login login);

    }
}
