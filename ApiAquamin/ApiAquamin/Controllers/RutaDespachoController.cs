using ApiAquamin.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiAquamin.Controllers
{
    [ApiController]
    [Route("Despacho")]
    public class RutaDespachoController : ControllerBase
    {
        private readonly RutaDespachoService ruta;
        public RutaDespachoController(RutaDespachoService ruta)
        {
            this.ruta = ruta;
        }
        [HttpPost]
        [Route("AddDespacho")]
        public async Task<IActionResult> AddDespacho()
        {
            bool resultado = await ruta.IngresarRuta();
            if (resultado)
                return Ok("Se gregó una venta a ruta");
            else
                return BadRequest("Error al ingresar la venta a la ruta");
        }
        [HttpGet]
        [Route("GetRuta/{id}")]
        public async Task<IActionResult> GetRuta(int id)
        {
            var rutas = await ruta.DesplegarRuta(id);
            return Ok(rutas);
        }
    }
}
