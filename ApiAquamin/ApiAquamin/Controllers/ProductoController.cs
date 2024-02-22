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
        public async Task<IActionResult> AddProducto(int idrol, [FromBody] IngresarProducto producto)
        {
            bool resultado = await productoService.IngresarProducto(idrol, producto);
            if (resultado)
                return Ok("Producto añadido con éxito");
            else
                return BadRequest("Hubo un error al añadir un producto");
        }
        
    }
}
