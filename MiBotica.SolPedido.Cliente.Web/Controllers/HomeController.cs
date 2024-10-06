using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Configuration;


namespace MiBotica.SolPedido.Cliente.Web.Controllers
{
    public class HomeController : Controller
    {
        // Acción para el Dashboard
        public ActionResult Index()
        {
            // Obtener los datos de los clientes desde la base de datos
            var clientes = ObtenerClientes(); // Método para obtener los clientes

            // Pasar los clientes a la vista
            return View(clientes);
        }

        // Acción para la página About
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        // Acción para la página Contact
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }

        // Método para manejar el error 500 (Error interno del servidor)
        public ActionResult Error500()
        {
            return View();
        }

        // Método para manejar el error 404 (Página no encontrada)
        public ActionResult PaginaNoEncontrada()
        {
            return View();
        }

        // Método privado para obtener los datos de los clientes desde la base de datos
        private List<MiBotica.SolPedido.Entidades.Core.Cliente> ObtenerClientes()
        {
            List<MiBotica.SolPedido.Entidades.Core.Cliente> clientes = new List<MiBotica.SolPedido.Entidades.Core.Cliente>();

            // Cadena de conexión
            string connectionString = WebConfigurationManager.ConnectionStrings["cnnSql"].ConnectionString;

            // Consulta SQL
            string query = "SELECT Codigo, NombreCompleto, Zona FROM Clientes";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        MiBotica.SolPedido.Entidades.Core.Cliente cliente = new MiBotica.SolPedido.Entidades.Core.Cliente
                        {
                            Codigo = Convert.ToInt32(reader["Codigo"]),
                            NombreCompleto = reader["NombreCompleto"].ToString(),
                            Zona = reader["Zona"].ToString()
                        };

                        clientes.Add(cliente);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
                Console.WriteLine("Error al obtener los clientes: " + ex.Message);
            }

            return clientes;
        }

    }
}
