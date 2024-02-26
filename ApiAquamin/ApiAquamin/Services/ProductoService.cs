using ApiAquamin.DTO;
using ApiAquamin.Models;
using ApiAquamin.Models.Formularios;
using ApiAquamin.Services.Interface;
using Humanizer.DateTimeHumanizeStrategy;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ApiAquamin.Services
{
    public class ProductoService : IProducto
    {
        private readonly EjecutarSP EjecutarSP;
        public ProductoService(EjecutarSP ejecutarSP)
        {
            EjecutarSP = ejecutarSP;
        }

        public async Task<bool> IngresarProducto([FromBody] IngresarProducto producto)
        {
            try
            {
                bool ingresar = await EjecutarSP.IngresarProducto(producto);
                if (ingresar)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Ocurrió un error al ingresar un producto:{e.Message}");
                return false;
            }
        }
        public async Task<List<ProductoDTO>> BuscarProducto(int? id, string? nombre)
        {
            try
            {
                var productos = await EjecutarSP.BuscarProductos(id, nombre);
                return productos;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error al buscar un producto:{e.Message}");
                return new List<ProductoDTO>();
            }
        }
        public async Task<bool> ActualizarProducto(int id, [FromBody] IngresarProducto producto)
        {
            try
            {
                bool actualizar = await EjecutarSP.ActualizarProducto(id, producto);
                if (actualizar)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Erro al actualizar los datos del producto:{e.Message}");
                return false;
            }
        }
        public async Task<bool> EliminarProducto(int idproducto, [FromBody] EliminarProducto producto)
        {
            try
            {
                bool eliminar = await EjecutarSP.EliminarProducto(idproducto, producto);
                if (eliminar)
                    return true;
                else
                    return false;
            }
            catch 
            {
                return false;
            }
        }
    }
}
