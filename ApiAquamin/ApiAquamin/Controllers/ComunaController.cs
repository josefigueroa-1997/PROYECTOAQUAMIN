using Microsoft.AspNetCore.Mvc;
using ApiAquamin.Services;
using System.Diagnostics;

namespace ApiAquamin.Controllers
{
    [ApiController]
    [Route("Comuna")]
    public class ComunaController : ControllerBase
    {
        private readonly ComunaService comunaService;

        public ComunaController(ComunaService comunaService)
        {
            this.comunaService = comunaService;
        }
        [HttpGet]
        [Route("GetComunas")]
        public async Task <IActionResult> GetComunas()
        {
            try
            {
                var comunas = await comunaService.ObtenerComunas();
                return Ok(comunas);
            }
            catch(Exception e)
            {
                Debug.WriteLine(e.Message);
                return BadRequest("Error al obtener los datos de las comunas del servidor");
            }
        }
    }
}
