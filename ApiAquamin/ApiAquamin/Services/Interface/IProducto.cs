using ApiAquamin.Models.Formularios;
using Microsoft.AspNetCore.Mvc;

namespace ApiAquamin.Services.Interface
{
    public interface IProducto
    {
        public Task<bool> IngresarProducto(int idrol, [FromBody] IngresarProducto producto);
    }
}
