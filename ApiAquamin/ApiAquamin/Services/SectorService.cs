
using ApiAquamin.Services.Interface;
using ApiAquamin.Models;
using System.Diagnostics;
using ApiAquamin.DTO;

namespace ApiAquamin.Services
{
    public class SectorService : ISector
    {
        private readonly EjecutarSP ejecutarsp;
        public SectorService(EjecutarSP ejecutarsp)
        {
            this.ejecutarsp = ejecutarsp;
        }

        public async Task<List<Sector>> ObtenerSectores()
        {
            try
            {
                var sectores = await ejecutarsp.ObtenerSectores();
                return sectores;
            }
            catch(Exception e)
            {
                Debug.WriteLine($"Error al obtener los datos de los sectores:{e.Message}");
                return new List<Sector>();
            }
        }

        public List<SectorDTO> AgregarSectoresDTO(List<Sector> sectores)
        {
            try
            {
                var sectoresdto = sectores.Select(s => new SectorDTO
                {
                    Id = s.Id,
                    Nombre = s.Nombre,
                }).ToList();

                return sectoresdto;
            }
            catch(Exception e)
            {
                Debug.WriteLine($"Error al obtener los datos de sectores:{e.Message}");
                return new List<SectorDTO>();
            }
        }
    }
}
