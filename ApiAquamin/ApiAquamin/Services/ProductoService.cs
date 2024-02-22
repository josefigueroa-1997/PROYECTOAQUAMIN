using ApiAquamin.Models;
using ApiAquamin.Models.Formularios;
using ApiAquamin.Services.Interface;
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

        public async Task<bool> IngresarProducto(int idrol, [FromBody] IngresarProducto producto)
        {
            try
            {
                bool ingresar = await EjecutarSP.IngresarProducto(idrol, producto);
                if (ingresar)
                    return true;
                else
                    return false;
            }
            catch(Exception e)
            {
                Debug.WriteLine($"Ocurrió un error al ingresar un producto:{e.Message}");
                return false;
            }
        }
    }
}
