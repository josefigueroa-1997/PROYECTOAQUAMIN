﻿using ApiAquamin.DTO;
using ApiAquamin.Models;
using ApiAquamin.Models.Formularios;
using ApiAquamin.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace ApiAquamin.Services
{
    public class VentaService : IVenta
    {
        private readonly EjecutarSP ejecutarSP;
        public VentaService(EjecutarSP ejecutarSP)
        {
            this.ejecutarSP = ejecutarSP;
        }
        public async Task<bool> IngresarVenta([FromBody] Venta venta)
        {
            try
            {
                bool ingresar = await ejecutarSP.IngrsarVenta(venta);
                if (ingresar)
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }
        public async Task<List<VentaDTO>> ObtenerVenta(int? id, string? tipoventa, DateTime? fecha, int? idsector)
        {
            var venta = await ejecutarSP.ObtenerVentas(id, tipoventa,fecha,idsector);
            return venta;
        }
        public async Task<bool> ActualizarDatosVenta(int id, [FromBody] Venta venta)
        {
            try
            {
                bool actventa = await ejecutarSP.ActualizarDatosVenta(id, venta);
                if (actventa)
                    return true;
                else
                    return false;
            }
            catch 
            {
                return false;
            }
        }
        public async Task<List<HistorialClienteDTO>> HistorialProductoCliente(int idusuario, DateTime fecha)
        {
            try
            {
                var historial = await ejecutarSP.HistorialProductoCliente(idusuario, fecha);
                return historial;
            }
            catch
            {
                return new List<HistorialClienteDTO>();
            }
        }
        public async Task<bool> EliminarVenta(int id)
        {
            try
            {
                bool eliminar = await ejecutarSP.EliminarVenta(id);
                if (eliminar)
                    return true;
                else
                    return false;
            }catch
            {
                return false;
            }
        }
    }
}
