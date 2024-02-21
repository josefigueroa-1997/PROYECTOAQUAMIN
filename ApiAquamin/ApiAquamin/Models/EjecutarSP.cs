
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Scaffolding.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data.Common;
using System.Diagnostics;
using System.Data;
using ApiAquamin.DTO;
using ApiAquamin.Models.Formularios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using BCrypt.Net;
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

        public async Task<bool> RegistrarUsuario([FromBody] CrearUsuario usuario)
        {
            try
            {
                string encryptedpass = "";
                if(usuario.IdRol != 4)
                {
                    if (usuario.Correo != null  && usuario.Contrasena != null)
                    {
                        encryptedpass = EncriptarContrasena(usuario.Contrasena);
                    }
                    else
                        return false;
                }
                
                DbConnection connection = await conexion.OpenDatabaseConnectionAsync();
                using( DbCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "CREAR_USUARIO";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@NOMBRE", SqlDbType.VarChar, 200) { Value = usuario.Nombre});
                    cmd.Parameters.Add(new SqlParameter("@TELEFONO", usuario.Telefono));
                    cmd.Parameters.Add(new SqlParameter("@CALLE", SqlDbType.VarChar, 200) { Value = usuario.Calle });
                    cmd.Parameters.Add(new SqlParameter("@NUMERO", SqlDbType.Int) { Value = usuario.Numero });
                    cmd.Parameters.Add(new SqlParameter("@IDCOMUNA", SqlDbType.Int) { Value = usuario.IdComuna });
                    cmd.Parameters.Add(new SqlParameter("@CORREO", SqlDbType.VarChar, 100) { Value = usuario.Correo });
                    cmd.Parameters.Add(new SqlParameter("@CONTRASENA", SqlDbType.NVarChar, -1) { Value = encryptedpass });
                    cmd.Parameters.Add(new SqlParameter("@IDROL", SqlDbType.Int) { Value = usuario.IdRol});
                    SqlParameter outputparameter = new SqlParameter("@IDIRECCION", SqlDbType.Int);
                    outputparameter.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outputparameter);
                    await cmd.ExecuteNonQueryAsync();
                }
                await conexion.CloseDatabaseConnectionAsync();
                return true;
            }
            catch (Exception e)
            {

                Debug.WriteLine($"Error al registrar el usuario:{e.Message}");
                return false;

            }
        }

        public async Task<List<UsuarioDTO>> ObtenerUsuarios(int? id,string? nombre)
        {
            try
            {
                #pragma warning disable CS8600
                object idparameter = (object)id ?? DBNull.Value;
                object nombreparameter = (object)nombre ?? DBNull.Value;
                #pragma warning restore CS8600
                var usuarios = new List<UsuarioDTO>();
                DbConnection connection = await conexion.OpenDatabaseConnectionAsync();
                using(DbCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "OBTENERUSUARIOS";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@IDUSUARIO",idparameter));
                    cmd.Parameters.Add(new SqlParameter("@NOMBRE", nombreparameter));
                    using(var reader = await cmd.ExecuteReaderAsync())
                    {
                        while(await reader.ReadAsync())
                        {
                            UsuarioDTO usuario = new UsuarioDTO() {

                                Id = reader.GetInt32(reader.GetOrdinal("ID")),
                                Nombre = reader.GetString(reader.GetOrdinal("NOMBRE")),
                                Telefono = reader.GetString(reader.GetOrdinal("TELEFONO")),
                                Calle = reader.GetString(reader.GetOrdinal("CALLE")),
                                Numero = reader.GetInt32(reader.GetOrdinal("NUMERO")),
                                NombreComuna = reader.GetString(reader.GetOrdinal("NOMBRECOMUNA")),
                            };
                            usuarios.Add(usuario);
                        }
                    }
                }
                await conexion.CloseDatabaseConnectionAsync();
                return usuarios;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error al obtener los datos del usuario:{e.Message}");
                return new List<UsuarioDTO>();
            }

        }

        public async Task<bool> ActualizarDatosUsuario(int id,[FromBody] ActualizarUsuario usuario)
        {
            try
            {
                string encryptedpass = "";
                if (usuario.IdRol != 4)
                {
                    if (usuario.Correo != null && usuario.Contrasena != null)
                    {
                        encryptedpass = EncriptarContrasena(usuario.Contrasena);
                    }
                    else
                        return false;
                }
                DbConnection connection = await conexion.OpenDatabaseConnectionAsync();
                using(DbCommand command = connection.CreateCommand())
                {
                    command.CommandText = "ACTUALIZARDATOSUSUARIO";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@IDUSUARIO", id));
                    command.Parameters.Add(new SqlParameter("@NOMBRE", usuario.Nombre));
                    command.Parameters.Add(new SqlParameter("@TELEFONO ", usuario.Telefono));
                    command.Parameters.Add(new SqlParameter("@CALLE", usuario.Calle));
                    command.Parameters.Add(new SqlParameter("@NUMERO", usuario.Numero));
                    command.Parameters.Add(new SqlParameter("@IDCOMUNA", usuario.IdComuna));
                    command.Parameters.Add(new SqlParameter("@CORREO", usuario.Correo));
                    command.Parameters.Add(new SqlParameter("@CONTRASENA", usuario.Contrasena));
                    command.Parameters.Add(new SqlParameter("@IDIRECCION", usuario.IDireccion));
                    command.Parameters.Add(new SqlParameter("@IDROL", usuario.IdRol));
                    await command.ExecuteNonQueryAsync();
                }
                await conexion.CloseDatabaseConnectionAsync();
                return true;
            }
            catch(Exception e)
            {
                Debug.WriteLine($"Error al actualizar los datos del usuario:{e.Message}");
                return false;
            }
        } 
        private string EncriptarContrasena(string contrasena)
        {
            try
            {
                string salt = BCrypt.Net.BCrypt.GenerateSalt();
                string constrasenaencriptada = BCrypt.Net.BCrypt.HashPassword(contrasena, salt);
                return constrasenaencriptada;
            }
            catch(Exception e)
            {
                Debug.WriteLine($"Error al encriptar la contraseña_{e.Message}");
                return "";
            }
        }

    }
}
