using ApiAquamin.DTO;
using ApiAquamin.Models;
using ApiAquamin.Models.Formularios;
using Microsoft.AspNetCore.Mvc;

namespace ApiAquamin.Services.Interface
{
    public interface IVenta
    {
        public Task<bool> IngresarVenta([FromBody] Venta venta);
        public Task<List<VentaDTO>> ObtenerVenta(int? id, string? tipoventa,DateTime? fecha,int? idsector);
        public Task<List<HistorialClienteDTO>> HistorialProductoCliente(int idusuario, DateTime fecha);
        public Task<bool> ActualizarDatosVenta(int id, [FromBody] Venta venta);
        public Task<bool> EliminarVenta(int id);
    }
}
