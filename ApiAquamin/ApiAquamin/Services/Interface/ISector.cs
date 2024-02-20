using ApiAquamin.Models;
using ApiAquamin.DTO;
namespace ApiAquamin.Services.Interface
{
    public interface ISector
    {
        public Task<List<Sector>> ObtenerSectores();
        public List<SectorDTO> AgregarSectoresDTO(List<Sector> sectores);


    }
}
