using ApiAquamin.Models.Formularios;
using ApiAquamin.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiAquamin.Controllers
{
    [ApiController]
    [Route("Despacho")]
    public class RutaDespachoController : ControllerBase
    {
        private readonly RutaDespachoService rutaDespacho;
        public RutaDespachoController(RutaDespachoService rutaDespacho)
        {
            this.rutaDespacho = rutaDespacho;
        }
        [HttpPost]
        [Route("AddDespacho")]
        public async Task<IActionResult> AddDespacho()
        {
            bool resultado = await rutaDespacho.IngresarRuta();
            if (resultado)
                return Ok("Se gregó una venta a ruta");
            else
                return BadRequest("Error al ingresar la venta a la ruta");
        }
        [HttpGet]
        [Route("GetRuta/{id}")]
        public async Task<IActionResult> GetRuta(int id)
        {
            var rutas = await rutaDespacho.DesplegarRuta(id);
            return Ok(rutas);
        }
        [HttpPut]
        [Route("UpdateRuta/{id}")]
        public async Task<IActionResult> UpdateRuta(int id, [FromBody] Ruta ruta)
        {
            bool resultado = await rutaDespacho.ActualizarRuta(id, ruta);
            if (resultado)
                return Ok("Exito en la actualización de la ruta");
            else
                return BadRequest("Hubo un error en la actualización de la ruta");
        }
        [HttpDelete]
        [Route("DeleteRuta/{id}")]
        public async Task<IActionResult> DeleteRuta(int id)
        {
            bool resultado = await rutaDespacho.EliminarRuta(id);
            if (resultado)
                return Ok("Ruta eleiminada con exito");
            else
                return BadRequest("Error al eliminar una venta de la ruta");
        }
    }
}
