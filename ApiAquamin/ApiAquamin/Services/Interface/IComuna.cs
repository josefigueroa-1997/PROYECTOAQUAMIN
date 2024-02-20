using ApiAquamin.DTO;
namespace ApiAquamin.Services.Interface
{
    public interface IComuna
    {
        public Task<List<ComunaDTO>> ObtenerComunas();


    }
}
