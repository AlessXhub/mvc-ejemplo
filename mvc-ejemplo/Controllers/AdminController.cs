using Microsoft.AspNetCore.Mvc;
using mvc_ejemplo.DAL;
using mvc_ejemplo.Models;
using Microsoft.Data.SqlClient;

namespace mvc_ejemplo.Controllers
{
    public class AdminController : Controller
    {



        Conexion cn = new Conexion();

        public IActionResult Index()
        {

            ViewBag.Mensaje = "Bienvenido al panel de administración.";
            return View();
        }
        [HttpGet]

        public IActionResult CrearUsuario()
        {
            // Aquí podrías implementar la lógica para gestionar usuarios

            return View();
        }


        [HttpPost]

        public IActionResult CrearUsuario(string nombre, string login, string contra, int rolId)
        {
            using (SqlConnection conn = cn.GetConnection())
            {
                string query = "INSERT INTO Usuarios (NombreUsuario, UsuarioLogin, ContraLogin, IdRol) VALUES (@nombre, @login, @contra, @rolId)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@nombre", nombre);
                cmd.Parameters.AddWithValue("@login", login);
                cmd.Parameters.AddWithValue("@contra", contra);
                cmd.Parameters.AddWithValue("@rolId", rolId);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            ViewBag.Mensaje = "Usuario creado exitosamente.";
            return View();
        }
    }

}