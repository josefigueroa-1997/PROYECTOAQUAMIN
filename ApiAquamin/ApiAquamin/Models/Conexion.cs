using ApiAquamin.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data.Common;
using System.Threading.Tasks;

namespace ApiAquamin.Models
{
    public class Conexion
    {
        private readonly AQUAMINContext context;

        public Conexion(AQUAMINContext context)
        {
            this.context = context;
        }

        public async Task<DbConnection> OpenDatabaseConnectionAsync()
        {
            try
            {
                
                await context.Database.OpenConnectionAsync();

              
                return context.Database.GetDbConnection();
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"Error al abrir la conexión a la base de datos: {ex.Message}");
                throw; 
            }
        }
        public async Task CloseDatabaseConnectionAsync()
        {
            try
            {
               
                await context.Database.CloseConnectionAsync();
            }
            catch (Exception ex)
            {
               
                Console.WriteLine($"Error al cerrar la conexión a la base de datos: {ex.Message}");
                throw; 
            }
        }
    }
}
