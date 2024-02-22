﻿
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
        //AQUI COMIENZAN LOS METODOS DE USUARIO
        //REGISTRAR USUARIO
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
                   var resultado = await cmd.ExecuteNonQueryAsync();
                    if (resultado == 1)
                    {
                        Debug.WriteLine("Ya Existe un Usuario con ese correo");
                        return false;
                    }
                    else if(resultado == 2)
                        return true;
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
        //OBTENER USUARIOS
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
        //ACTUALIZAR USUARIOS
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
                    command.Parameters.Add(new SqlParameter("@CONTRASENA", encryptedpass));
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
        //ELIMINAR USUARIOS
        public async Task<bool> EliminarUsuario(int id)
        {
            try
            {
                DbConnection connection = await conexion.OpenDatabaseConnectionAsync();
                using(DbCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "ELIMINARUSUARIO";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@IDUSUARIO",id));
                    await cmd.ExecuteNonQueryAsync();
                }
                await conexion.CloseDatabaseConnectionAsync();
                return true;
            }
            catch(Exception e)
            {
                Debug.WriteLine($"Hubo un error al eliminar el usuario:{e.Message}");
                return false;
            }
        }
        //LOGIN
        public async Task<LoginDTO> IniciarSesion([FromBody] Login credenciales)
        {
            try
            {
                if(!string.IsNullOrEmpty(credenciales.Correo) && !string.IsNullOrEmpty(credenciales.contrasena))
                {
                    DbConnection connection = await conexion.OpenDatabaseConnectionAsync();
                    using(DbCommand cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = "INICIARSESION";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@CORREO",credenciales.Correo));
                        LoginDTO usuario = new LoginDTO();
                        using(var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                usuario = new LoginDTO()
                                {
                                    IdUsuario = reader.GetInt32(reader.GetOrdinal("IDUSUARIO")),
                                    NombreUsuario = reader.GetString(reader.GetOrdinal("NOMBREUSUARIO")),
                                    IdRol = reader.GetInt32(reader.GetOrdinal("IDROL")),
                                    Contrasena = reader.GetString(reader.GetOrdinal("CONTRASENA")),
                                };
                            }
                        }
                        bool verificar = VerificarContrasena(credenciales.contrasena, usuario.Contrasena);
                        if (verificar)
                            return usuario;
                        else
                            Debug.WriteLine("La contraseña es incorrecta");
                        return new LoginDTO();
                    }
                }
                Debug.WriteLine("Debe ingresar un correo y una contraseña");
                return new LoginDTO();
            }
            catch(Exception e)
            {
                Debug.WriteLine($"Error al Iniciar Sesión:{e.Message}");
                return new LoginDTO();
            }
        }

        //ENCRIPTAR CONTRASEÑA
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
                Debug.WriteLine($"Error al encriptar la contraseña:{e.Message}");
                return "";
            }
        }
        //VERIFICAR CONTRASEÑA
        private Boolean VerificarContrasena(string passuser,string passbd)
        {
            try
            {
                bool verificar = BCrypt.Net.BCrypt.Verify(passuser, passbd);
                return verificar;
            }
            catch(Exception e)
            {
                Debug.WriteLine($"Error al verificar las contraseñas:{e.Message}");
                return false;
            }
        }

       //METODOS PRODUCTOS
       //INGRESAR PRODUCTOS
       public async Task<bool> IngresarProducto(int idrol, [FromBody] IngresarProducto producto)
        {
            try
            {
                if (idrol == 1)
                {
                    DbConnection connection = await conexion.OpenDatabaseConnectionAsync();
                    using(DbCommand command = connection.CreateCommand())
                    {
                        command.CommandText = "INGRESARPRODUCTOS";
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@TIPO_PRODUCTO", producto.Tipo_Producto));
                        command.Parameters.Add(new SqlParameter("@PRECIO",producto.Precio));
                        command.Parameters.Add(new SqlParameter("@STOCK", producto.Stock));
                        await command.ExecuteNonQueryAsync();
                    }
                    await conexion.CloseDatabaseConnectionAsync();
                    return true;
                }
                else
                {
                    Debug.WriteLine($"Usted no está autorizado para ingresar un producto");
                    return false;
                }
                    
            }
            catch(Exception e)
            {
                Debug.WriteLine($"Hubo un error al ingresar un producto:{e.Message}");
                return false;
            }
        }

    }
}
