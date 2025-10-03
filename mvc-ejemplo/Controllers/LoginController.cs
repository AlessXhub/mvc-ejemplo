using Microsoft.AspNetCore.Mvc;
using mvc_ejemplo.DAL;
using mvc_ejemplo.Models;
using Microsoft.Data.SqlClient;




namespace mvc_ejemplo.Controllers
{
    public class LoginController : Controller
    {
        Conexion cn = new Conexion();

        [HttpGet]

        public IActionResult Index()
        {
            return View();
        }
        // Acción para procesar el formulario de Login (cuando el usuario envía usuario y contraseña)
 // Utilizamos Post, porque vamos a enviar.

// La parte que está en paréntesis son los datos que extraemos del formulario, es el "name" de cada input.

public IActionResult Index(string usuario, string contrasena)
{
    // Aquí guardaremos si existe en la base de datos, que es el objeto a crear en el modelo denominado UsuarioModelo.cs.
    UsuarioModelo user = null;

    // Abrimos conexión a la base de datos usando SqlConnection
    using (SqlConnection conn = cn.GetConnection())
    {
        // Consulta SQL para verificar el usuario y contraseña, y traer también el rol asociado
        // Porque por medio del rol, le vas a dejar que dependa del rol, así es como nos iremos a las capacidades correspondientes.
        string query = @"SELECT U.IdUsuario, U.NombreUsuario, U.UsuarioLogin, U.ContraLogin, U.IdRol, R.NombreRol
                         FROM Usuarios U
                         INNER JOIN Roles R ON U.IdRol = R.IdRol
                         WHERE U.UsuarioLogin = @usuario AND U.ContraLogin = @contrasena";

        // Preparamos el comando con la consulta y los parámetros
        SqlCommand cmd = new SqlCommand(query, conn);
        cmd.Parameters.AddWithValue("@usuario", usuario);
        cmd.Parameters.AddWithValue("@contrasena", contrasena);

        // Abrimos la conexión y ejecutamos la consulta SQL
        conn.Open();
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.Read())
        {
            user = new UsuarioModelo
            {
                IdUsuario = (int)dr["IdUsuario"],
                NombreUsuario = dr["NombreUsuario"].ToString(),
                UsuarioLogin = dr["UsuarioLogin"].ToString(),
                IdRol = (int)dr["IdRol"],
                RolNombre = dr["NombreRol"].ToString()
            };
        }
        dr.Close();
    }

    if (user != null)
    {
       if(user.RolNombre == "Admin") // Si es administrador
        
            // Redirigir a la vista de administrador
            return RedirectToAction("Index", "Admin");
        
        else if (user.RolNombre == "Profesor") // Si es profesor
      
            // Redirigir a la vista de profesor
            return RedirectToAction("Index", "Profesor");
        
        else  
        
            // Redirigir a la vista de estudiante
            return RedirectToAction("Index", "Alumno");
        }
            // Rol no reconocido, mostrar error en la vista de login
            ViewBag.Error = "Rol de usuario no reconocido";
            return View();
                }
            }
  
    }

