using ApiAquamin.DTO;

namespace ApiAquamin.Services.Interface
{
    public interface IRutaDespacho
    {
        public Task<bool> IngresarRuta();
        public Task<List<RutaDTO>> DesplegarRuta(int id);
        
    }
}
