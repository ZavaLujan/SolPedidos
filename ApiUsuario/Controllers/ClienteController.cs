using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MiBotica.SolPedido.Entidades.Core;
using MiBotica.SolPedido.LogicaNegocio.Core;

namespace ApiUsuario.Controllers
{
    public class ClienteController : ApiController
    {
        // GET: api/Cliente
        public IEnumerable<Cliente> Get()
        {
            List <Cliente> cliente = new List<Cliente>();
            cliente = new ClienteLN().ListaClientes();
            //return Variables.ListaClientes;
            return cliente;
        }
        // GET: api/Cliente/5
        public Cliente Get(int id)
        {
            // Obtener un cliente específico usando el método BuscarCliente de la capa de negocio
            Cliente cliente = new ClienteLN().BuscarCliente(id);
            return cliente;
        }

        // POST: api/Cliente
        public void Post([FromBody] Cliente cliente)
        {
            // Insertar un nuevo cliente usando el método InsertarCliente de la capa de negocio
            new ClienteLN().InsertarCliente(cliente);
        }

        // PUT: api/Cliente/5
        public void Put(int id, [FromBody] Cliente cliente)
        {
            // Actualizar un cliente existente usando el método ActualizarCliente de la capa de negocio
            new ClienteLN().ActualizarCliente(id, cliente);
        }

        // DELETE: api/Cliente/5
        public void Delete(int id)
        {
            // Eliminar un cliente usando el método EliminarCliente de la capa de negocio
            new ClienteLN().EliminarCliente(id);
        }
    }
}
