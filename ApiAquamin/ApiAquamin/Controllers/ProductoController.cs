using ApiAquamin.Models.Formularios;
using ApiAquamin.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiAquamin.Controllers
{
    [ApiController]
    [Route("Producto")]
    public class ProductoController : ControllerBase
    {
        private readonly ProductoService productoService;
        public ProductoController(ProductoService productoService)
        {
            this.productoService = productoService;
        }

        [HttpPost]
        [Route("AddProducto")]
        public async Task<IActionResult> AddProducto([FromBody] IngresarProducto producto)
        {
            bool resultado = await productoService.IngresarProducto(producto);
            if (resultado)
                return Ok("Producto añadido con éxito");
            else
                return BadRequest("Hubo un error al añadir un producto");
        }
        [HttpGet]
        [Route("GetProductos")]
        public async Task<IActionResult> GetProductos([FromQuery] int? id, [FromQuery] string? nombre)
        {
            var productos = await productoService.BuscarProducto(id,nombre);
            return Ok(productos);
        }
        [HttpPut]
        [Route("UpdateProducto/{id}")]
        public async Task<IActionResult> ActualizarProducto(int id, [FromBody] IngresarProducto producto)
        {
            bool resultado = await productoService.ActualizarProducto(id, producto);
            if (resultado)
                return Ok("¡Éxito en la actualización!");
            else
                return BadRequest("Hubo un error al actualizar los datos del producto");
        }
        [HttpDelete]
        [Route("DeleteProducto/{id}")]
        public async Task<IActionResult> DeleteProducto(int id, [FromBody] EliminarProducto producto)
        {
            bool resultado = await productoService.EliminarProducto(id, producto);
            if (resultado)
                return Ok("Se eliminó el producto");
            else
                return BadRequest("No se pudo eliminar el producto");

        }
    }
}
