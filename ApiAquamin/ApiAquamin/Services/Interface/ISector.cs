using ApiAquamin.Models;

namespace ApiAquamin.Services.Interface
{
    public interface ISector
    {
        public Task<List<Sector>> ObtenerSectores();


    }
}
