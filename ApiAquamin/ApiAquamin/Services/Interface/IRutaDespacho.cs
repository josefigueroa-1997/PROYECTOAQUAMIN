using ApiAquamin.DTO;
using ApiAquamin.Models.Formularios;
using Microsoft.AspNetCore.Mvc;

namespace ApiAquamin.Services.Interface
{
    public interface IRutaDespacho
    {

        public Task<List<RutaDTO>> DesplegarRuta(int id);
        public Task<bool> ActualizarRuta(int id, [FromBody] Ruta ruta);
        public Task<bool> EliminarRuta(int id);
        
    }
}
