using MiBotica.SolPedido.Entidades.Core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace MiBotica.SolPedido.AccesoDatos.Core
{
    public class ClienteDA
    {
        public Cliente LlenarEntidad(IDataReader reader)
        {
            Cliente cliente = new Cliente();

            reader.GetSchemaTable().DefaultView.RowFilter = "ColumnName='Codigo'";
            if (reader.GetSchemaTable().DefaultView.Count.Equals(1))
            {
                if (!Convert.IsDBNull(reader["Codigo"]))
                    cliente.Codigo = Convert.ToInt32(reader["Codigo"]);
            }
            reader.GetSchemaTable().DefaultView.RowFilter = "ColumnName='NombreCompleto'";
            if (reader.GetSchemaTable().DefaultView.Count.Equals(1))
            {
                if (!Convert.IsDBNull(reader["NombreCompleto"]))
                    cliente.NombreCompleto = Convert.ToString(reader["NombreCompleto"]);
            }
            reader.GetSchemaTable().DefaultView.RowFilter = "ColumnName='Zona'";
            if (reader.GetSchemaTable().DefaultView.Count.Equals(1))
            {
                if (!Convert.IsDBNull(reader["Zona"]))
                    cliente.Zona = Convert.ToString(reader["Zona"]);
            }
            return cliente;
        }

        public List<Cliente> ListaClientes()
        {
            List<Cliente> listaEntidad = new List<Cliente>();
            Cliente entidad = null;

            using (SqlConnection conexion = new
            SqlConnection(ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["cnnSql"]].ConnectionString))

            {

                using (SqlCommand comando = new SqlCommand("paListarClientes", conexion))
                {
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    conexion.Open();
                    SqlDataReader reader = comando.ExecuteReader();
                    while (reader.Read())
                    {
                        entidad = LlenarEntidad(reader);
                        listaEntidad.Add(entidad);
                    }
                }
                conexion.Close();
            }
            return listaEntidad;
        }

        // Método para agregar un nuevo cliente usando el procedimiento almacenado paInsertarClientes
        public void InsertarCliente(Cliente cliente)
        {
            using (SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["cnnSql"]].ConnectionString))
            {
                using (SqlCommand comando = new SqlCommand("paInsertarClientes", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@NombreCompleto", cliente.NombreCompleto);
                    comando.Parameters.AddWithValue("@Zona", cliente.Zona);

                    try
                    {
                        conexion.Open();
                        comando.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        // Verificar si el error es por duplicado de nombre y zona
                        if (ex.Message.Contains("Ya existe un cliente con el mismo nombre y zona"))
                        {
                            throw new Exception("Ya existe un cliente con el mismo nombre y zona.");
                        }
                        else
                        {
                            throw;
                        }
                    }
                    finally
                    {
                        conexion.Close();
                    }
                }
            }
        }


        // Método para actualizar un cliente usando el procedimiento almacenado paModificarCliente
        public void ActualizarCliente(Cliente cliente)
        {
            using (SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["cnnSql"]].ConnectionString))
            {
                using (SqlCommand comando = new SqlCommand("paModificarCliente", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@Codigo", cliente.Codigo);
                    comando.Parameters.AddWithValue("@NombreCompleto", cliente.NombreCompleto);
                    comando.Parameters.AddWithValue("@Zona", cliente.Zona);

                    conexion.Open();
                    comando.ExecuteNonQuery();
                    conexion.Close();
                }
            }
        }

        // Método para buscar un cliente por ID usando el procedimiento almacenado paBuscarCliente
        public Cliente BuscarCliente(int id)
        {
            Cliente cliente = null;
            using (SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["cnnSql"]].ConnectionString))
            {
                using (SqlCommand comando = new SqlCommand("paBuscarCliente", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@Codigo", id);

                    conexion.Open();
                    SqlDataReader reader = comando.ExecuteReader();
                    if (reader.Read())
                    {
                        cliente = LlenarEntidad(reader);
                    }
                    conexion.Close();
                }
            }
            return cliente;
        }

        // Método para eliminar un cliente usando el procedimiento almacenado paEliminarCliente
        public void EliminarCliente(int id)
        {
            using (SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["cnnSql"]].ConnectionString))
            {
                using (SqlCommand comando = new SqlCommand("paEliminarCliente", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@Codigo", id);

                    conexion.Open();
                    comando.ExecuteNonQuery();
                    conexion.Close();
                }
            }
        }
    }
}
