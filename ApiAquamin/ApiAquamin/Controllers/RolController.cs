using ApiAquamin.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiAquamin.Controllers
{
    [ApiController]
    [Route("Rol")]
    public class RolController : ControllerBase
    {
        private readonly RolService rolService;
        public RolController(RolService rolService)
        {
            this.rolService = rolService;
        }

        [HttpGet]
        [Route("GetRoles")]
        public async Task<IActionResult> GetRoles()
        {
            try
            {
                var roles = await rolService.ObtenerRoles();
                return Ok(roles);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
