using ApiAquamin.Models;
using ApiAquamin.Models.Formularios;
using ApiAquamin.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiAquamin.Controllers
{
    [ApiController]
    [Route("Venta")]
    public class VentaController : ControllerBase
    {
        private readonly VentaService ventaService;
        public VentaController(VentaService ventaService)
        {
            this.ventaService = ventaService;
        }
        [HttpPost]
        [Route("AddVenta")]
        public async Task<IActionResult> AddVenta([FromBody] Venta venta)
        {
            bool resultado = await ventaService.IngresarVenta(venta);
            if (resultado)
                return Ok("Venta registrada con éxito");
            else
                return BadRequest("Error al registrar la venta");
        }
        [HttpGet]
        [Route("GetVentas")]
        public async Task<IActionResult> GetVentas([FromQuery] int? id, [FromQuery] string? tipoventa, [FromQuery] DateTime? fecha, [FromQuery] int? idsector)
        {
            var resultado = await ventaService.ObtenerVenta(id, tipoventa,fecha,idsector);
            return Ok(resultado);
        }

        [HttpGet]
        [Route("GetHistorialCliente")]
        public async Task<IActionResult> GetHistorialCliente([FromQuery] int idusuario, [FromQuery] DateTime fecha)
        {
            try
            {
                var historial = await ventaService.HistorialProductoCliente(idusuario, fecha);
                return Ok(historial);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            
        }

        [HttpPut]
        [Route("UpdateVenta/{id}")]
        public async Task<IActionResult> UpdateVenta(int id, [FromBody] Venta venta)
        {
            bool resultado = await ventaService.ActualizarDatosVenta(id, venta);
            if (resultado)
                return Ok("Venta actualizada con éxito");
            else
                return BadRequest("La venta no se ha podido actualizar");

        }
        [HttpDelete]
        [Route("DeleteVenta/{id}")]
        public async Task<IActionResult> DeleteVenta(int id)
        {
            bool resultado = await ventaService.EliminarVenta(id);
            if (resultado)
                return Ok("Venta eliminada con éxito");
            else
                return BadRequest("La venta no se ha podido eliminar");
        }
    }
}
