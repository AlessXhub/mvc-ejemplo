using Microsoft.Data.SqlClient; // Para trabajar con SQL Server
using Microsoft.Extensions.Configuration; // Para leer appsettings.json
using System.IO; // Para acceder al directorio del proyecto

namespace mvc_ejemplo.DAL
{
    public class Conexion
    {
        private readonly string _connectionString; // Aqui guardamos la cadena de conexion

        public Conexion()
        {
            // Creamos un ConfigurationBuilder para leer appsettings.json
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()) // Ruta del proyecto
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true) // Cargar appsettings.json
                .Build(); // Construye la configuración

            // Obtenemos la cadena de conexión por nombre
            _connectionString = builder.GetConnectionString("DefaultConnection");
        }

        // Método que devuelve una conexión lista para abrir
        public SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}