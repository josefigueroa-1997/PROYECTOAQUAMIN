using ApiAquamin.Models;
using ApiAquamin.DTO;
using System.Diagnostics;

namespace ApiAquamin.Services
{
    public class RolService
    {
        private readonly EjecutarSP ejecutarSP;
        public RolService(EjecutarSP ejecutarSP)
        {
            this.ejecutarSP = ejecutarSP;
        }

        public async Task<List<RolDTO>> ObtenerRoles()
        {
            try
            {
                var roles = await ejecutarSP.ObtenerRoles();
                return roles;
            }
            catch(Exception e)
            {
                Debug.WriteLine($"Error al recuperar los roles:{e.Message}");
                return new List<RolDTO>();
            }
        }


    }
}
