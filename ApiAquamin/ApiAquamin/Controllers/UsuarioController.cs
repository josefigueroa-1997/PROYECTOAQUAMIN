using Microsoft.AspNetCore.Mvc;
using ApiAquamin.Services;
using ApiAquamin.Models.Formularios;

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
    }
}
