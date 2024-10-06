using MiBotica.SolPedido.AccesoDatos.Core;
using MiBotica.SolPedido.Entidades.Base;
using MiBotica.SolPedido.Entidades.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace MiBotica.SolPedido.LogicaNegocio.Core
{
    public class ClienteLN : BaseLN
    {
        public List<Cliente> ListaClientes()
        {
            try
            {
                return new ClienteDA().ListaClientes();
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw;
            }
        }

        public bool InsertarCliente(Cliente nuevoCliente)
        {
            try
            {
                // Verificar si ya existe un cliente con el mismo nombre y zona
                var listaClientes = new ClienteDA().ListaClientes();
                if (listaClientes.Any(c => c.NombreCompleto == nuevoCliente.NombreCompleto && c.Zona == nuevoCliente.Zona))
                {
                    // Retornar falso si ya existe el cliente con el mismo nombre y zona
                    return false;
                }

                // Insertar el nuevo cliente
                new ClienteDA().InsertarCliente(nuevoCliente);
                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw;
            }
        }



        // Método para actualizar un cliente
        public void ActualizarCliente(int id, Cliente nuevoCliente)
        {
            try
            {
                nuevoCliente.Codigo = id; // Asegurar que el ID está configurado correctamente
                new ClienteDA().ActualizarCliente(nuevoCliente);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw;
            }
        }
        public Cliente BuscarCliente(int id)
        {
            try
            {
                return new ClienteDA().BuscarCliente(id);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw;
            }
        }

        // Método para eliminar un cliente
        public void EliminarCliente(int id)
        {
            try
            {
                new ClienteDA().EliminarCliente(id);
            }
            catch (Exception ex)
            {
                Log.Error(ex);

                throw;
            }
        }
    }
}