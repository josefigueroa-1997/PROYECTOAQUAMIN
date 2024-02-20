
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Scaffolding.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data.Common;
using System.Diagnostics;
using System.Data;
using ApiAquamin.DTO;

namespace ApiAquamin.Models
{
    public class EjecutarSP
    {

        private readonly Conexion conexion;
        public EjecutarSP(Conexion conexion)
        {
            this.conexion = conexion;
        }

        //OBTENER LOS SECTORES
        public async Task<List<Sector>> ObtenerSectores()
        {
            try
            {
                var sectores = new List<Sector>();
                DbConnection connection = await conexion.OpenDatabaseConnectionAsync();
                using(DbCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "OBTENERSECTORES";
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while(await reader.ReadAsync())
                        {
                            var sector = new Sector()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("ID")),
                                Nombre = reader.GetString(reader.GetOrdinal("NOMBRE")),
                            };
                            sectores.Add(sector);
                        }
                    }
                }
                await connection.CloseAsync();
                return sectores;
            }
            catch(Exception e)
            {
                Debug.WriteLine($"Error al obtener los datos de los sectores:{e.Message}");
                return new List<Sector>();
            }
        }

        //OBTENER LAS COMUNAS
        public async Task<List<ComunaDTO>> ObtenerComunas()
        {
            try
            {
                var comunas = new List<ComunaDTO>();
                DbConnection connection = await conexion.OpenDatabaseConnectionAsync();
                using( DbCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "OBTENERCOMUNAS";
                    cmd.CommandType = CommandType.StoredProcedure;
                    using(var reader = await cmd.ExecuteReaderAsync())
                    {
                        while(await reader.ReadAsync())
                        {
                            var comunasdto = new ComunaDTO()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("ID")),
                                Nombre = reader.GetString(reader.GetOrdinal("NOMBRE")),
                                Idsector = reader.GetInt32(reader.GetOrdinal("IDSECTOR")),
                                NombreSector = reader.GetString(reader.GetOrdinal("NOMBRESECTOR")),
                            };
                            comunas.Add(comunasdto);
                        }
                    }
                }
                await conexion.CloseDatabaseConnectionAsync();
                return comunas;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Erro al obtener las comunas:{e.Message}");
                return new List<ComunaDTO>();
            }
            
        }

        //OBTENER ROLES

        public async Task<List<RolDTO>> ObtenerRoles()
        {
            try
            {
                var roles = new List<RolDTO>();
                DbConnection connection = await conexion.OpenDatabaseConnectionAsync();
                using(DbCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "OBTENERROLES";
                    cmd.CommandType = CommandType.StoredProcedure;
                    using(var reader = await cmd.ExecuteReaderAsync())
                    {
                        while(await reader.ReadAsync())
                        {
                            var rol = new RolDTO()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("ID")),
                                Nombre = reader.GetString(reader.GetOrdinal("NOMBRE")),
                            };

                            roles.Add(rol);
                        }
                    }
                }
                await conexion.CloseDatabaseConnectionAsync();
                return roles;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Hubo un error al obtener los roles:{e.Message}");
                return new List<RolDTO>();
            }
            

        }

    }
}
