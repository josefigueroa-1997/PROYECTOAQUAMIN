using ApiAquamin.DTO;
namespace ApiAquamin.Services.Interface
{
    public interface IRol
    {
        public Task<List<RolDTO>> ObtenerRoles();


    }
}
