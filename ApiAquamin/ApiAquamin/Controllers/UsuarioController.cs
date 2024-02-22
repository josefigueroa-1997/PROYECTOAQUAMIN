using Microsoft.AspNetCore.Mvc;
using ApiAquamin.Services;
using ApiAquamin.Models.Formularios;
using ApiAquamin.DTO;

namespace ApiAquamin.Controllers
{
    [ApiController]
    [Route("Usuario")]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioService usuarioservice;
        public UsuarioController(UsuarioService usuarioService)
        {
            this.usuarioservice = usuarioService;
        }
        [HttpPost]
        [Route("AddUsuario")]
        public async Task<IActionResult> AddUsuario([FromBody] CrearUsuario usuario)
        {
            bool resultado = await usuarioservice.RegistrarUsuario(usuario);
            if (resultado)
                return Ok("Usuario Registrado con éxito");
            else
                return BadRequest("Error al registrar el usuario");
        }

        [HttpGet]
        [Route("GetUsuarios")]
        public async Task<IActionResult> GetUsuarios([FromQuery] int? id, [FromQuery] string? nombre)
        {
            try
            {
                var usuarios = await usuarioservice.ObtenerUsuarios(id,nombre);
                return Ok(usuarios);
            }
            catch(Exception e)
            {
                return BadRequest(e.StackTrace);
            }
        }
        [HttpPut]
        [Route("UpdateUsuario/{id}")]
        public async Task<IActionResult> UpdateUsuario(int id, [FromBody] ActualizarUsuario usuario)
        {
            bool resultado = await usuarioservice.ActualizarUsuario(id, usuario);
            if (resultado)
                return Ok("Exito en la actualización de los datos del usuarios");
            else
                return BadRequest("Hubo un error al actualizar los datos del usuario");
        }
        [HttpDelete]
        [Route("DeleteUsuario/{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            bool resultado = await usuarioservice.EliminarUsuario(id);
            if (resultado)
                return Ok("Usuario eliminado con éxito");
            else
                return BadRequest("No se pudo eliminar el usuario");
        }
        [HttpPost]
        [Route("IniciarSesion")]
        public async Task<IActionResult> IniciarSesion([FromBody] Login usuario) 
        {
            LoginDTO resultado = await usuarioservice.IniciarSesion(usuario);
            return Ok(resultado);
        
        }
    }
}
