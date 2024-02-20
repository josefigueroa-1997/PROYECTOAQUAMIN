using Microsoft.AspNetCore.Mvc;
using ApiAquamin.Services;
using System.Diagnostics;

namespace ApiAquamin.Controllers
{
    [ApiController]
    [Route("Sector")]
    public class SectorController : ControllerBase
    {
        private readonly SectorService sectorService;
        public SectorController(SectorService sectorService)
        {
            this.sectorService = sectorService;
        }


        [HttpGet]
        [Route("GetSectores")]
        public async Task<IActionResult> GetSectores()
        {
            try
            {
                var sectores = await sectorService.ObtenerSectores();
                var sectoresdto = sectorService.AgregarSectoresDTO(sectores);
                return Ok(sectoresdto);
            }
            catch(Exception e)
            {
                Debug.WriteLine(e.Message);
                return BadRequest("Error en la llamada al servidor para obtener sectores");
            }
        }
    }
}
