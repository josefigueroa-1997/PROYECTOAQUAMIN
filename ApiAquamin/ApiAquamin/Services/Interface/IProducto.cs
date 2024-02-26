using ApiAquamin.DTO;
using ApiAquamin.Models.Formularios;
using Microsoft.AspNetCore.Mvc;

namespace ApiAquamin.Services.Interface
{
    public interface IProducto
    {
        public Task<bool> IngresarProducto([FromBody] IngresarProducto producto);
        public Task<List<ProductoDTO>> BuscarProducto(int? id,string? nombre);
        public Task<bool> ActualizarProducto(int id, [FromBody] IngresarProducto producto);
        public Task<bool> EliminarProducto(int idproducto, [FromBody] EliminarProducto producto);
    }
}
