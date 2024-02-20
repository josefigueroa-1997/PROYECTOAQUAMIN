using ApiAquamin.DTO; 
using ApiAquamin.Services.Interface;
using ApiAquamin.Models;
using System.Diagnostics;

namespace ApiAquamin.Services
{
    public class ComunaService : IComuna
    {
        private readonly EjecutarSP ejecutarsp;
        public ComunaService(EjecutarSP ejecutarSP) 
        {
            this.ejecutarsp = ejecutarSP;
        }
        public async Task<List<ComunaDTO>> ObtenerComunas()
        {
            try
            {
                var comunas = await ejecutarsp.ObtenerComunas();
                return comunas;
            }
            catch(Exception e)
            {
                Debug.WriteLine($"Error al obtener las comunas:{e.Message}");
                return new List<ComunaDTO>();
            }
        }
    }
}
