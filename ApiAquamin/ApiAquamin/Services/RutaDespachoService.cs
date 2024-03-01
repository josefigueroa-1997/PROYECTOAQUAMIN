using ApiAquamin.DTO;
using ApiAquamin.Models;
using ApiAquamin.Services.Interface;

namespace ApiAquamin.Services
{
    public class RutaDespachoService : IRutaDespacho
    {
        private readonly EjecutarSP ejecutarSP;
        public RutaDespachoService(EjecutarSP ejecutarSP)
        {
            this.ejecutarSP = ejecutarSP;
        }
        public async Task<bool> IngresarRuta()
        {
            try
            {
                bool insertar = await ejecutarSP.IngresarRutaDespacho();
                if (insertar)
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
                
        }
        public async Task<List<RutaDTO>> DesplegarRuta(int id)
        {
            try
            {
                var rutas = await ejecutarSP.DesplegarRuta(id);
                return rutas;
            }
            catch
            {
                return new List<RutaDTO>();
            }
        }
    }
}
