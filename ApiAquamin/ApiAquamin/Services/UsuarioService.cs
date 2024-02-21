using ApiAquamin.DTO;
using ApiAquamin.Models;
using ApiAquamin.Models.Formularios;
using ApiAquamin.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ApiAquamin.Services
{
    public class UsuarioService : IUsuario
    {
        private readonly EjecutarSP ejecutarSP;
        public UsuarioService(EjecutarSP ejecutarSP)
        {
            this.ejecutarSP = ejecutarSP;
        }
        public async Task<bool> RegistrarUsuario([FromBody] CrearUsuario usuario)
        {
            try
            {
                bool registrar = await ejecutarSP.RegistrarUsuario(usuario);
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error al registrar un usuario:{e.Message}");
                return false;
            }
        }

        public async Task<List<UsuarioDTO>> ObtenerUsuarios(int? id,string? nombre)
        {
            try
            {
                var usuarios = await ejecutarSP.ObtenerUsuarios(id,nombre);
                return usuarios;
            }
            catch(Exception e)
            {
                Debug.WriteLine($"Error al recuperar a los usuarios:{e.Message}");
                return new List<UsuarioDTO>();
            }
        }

        public async Task<bool> ActualizarUsuario(int id,[FromBody] ActualizarUsuario usuario)
        {
            try
            {
                bool actualizar = await ejecutarSP.ActualizarDatosUsuario(id,usuario);
                return true;
            }
            catch(Exception e)
            {
                Debug.WriteLine($"Erro al actualizar los datos:{e.Message}");
                return false;
            }
        }


    }
}
