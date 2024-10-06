using MiBotica.SolPedido.Entidades.Core;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace MiBotica.SolPedido.Cliente.Web.Controllers
{
    public class ClienteController : Controller
    {
        string rutaApi = "https://localhost:44393/api/";
        string jsonMediaType = "application/json";

        // GET: Cliente
        public ActionResult Index()
        {
            string metodo = "Cliente"; //refiere al controlador del la Api
            string accion = "Get"; // refiere a la Accion de que se ejecuta
            List<MiBotica.SolPedido.Entidades.Core.Cliente> lista = new List<MiBotica.SolPedido.Entidades.Core.Cliente>();
            using (WebClient cliente = new WebClient())
            {
                cliente.Headers.Clear(); // borra datos anterioes de la cabecera
                cliente.Headers[HttpRequestHeader.ContentType] = jsonMediaType; // tipo de dato
                cliente.Encoding = UTF8Encoding.UTF8;// tipo de decodificación textos en chino ñ y otros
                string rutacompleta = rutaApi + metodo;
                var data = cliente.DownloadString(new Uri(rutacompleta));
                lista = JsonConvert.DeserializeObject<List<MiBotica.SolPedido.Entidades.Core.Cliente>>(data);
            }
            return View(lista);
        }

        // GET: Cliente/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Cliente/Create
        public ActionResult Create()
        {
            MiBotica.SolPedido.Entidades.Core.Cliente cliente = new MiBotica.SolPedido.Entidades.Core.Cliente();
            return View(cliente);
        }

        // POST: Cliente/Create
        [HttpPost]
        public ActionResult Create(MiBotica.SolPedido.Entidades.Core.Cliente cliente)
        {
            try
            {
                string metodo = "Cliente";
                using (WebClient client = new WebClient())
                {
                    client.Headers.Clear();
                    client.Headers[HttpRequestHeader.ContentType] = jsonMediaType;
                    client.Encoding = UTF8Encoding.UTF8;

                    // Serializamos el objeto cliente a JSON
                    string clienteJson = JsonConvert.SerializeObject(cliente);

                    // Ruta completa hacia la API
                    string rutacompleta = rutaApi + metodo;

                    // Enviamos el JSON con una solicitud POST
                    client.UploadString(new Uri(rutacompleta), "POST", clienteJson);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View(cliente);
            }
        }

        // GET: Cliente/Edit/5
        public ActionResult Edit(int id)
        {
            string metodo = "Cliente"; //refiere al controlador del la Api
            string accion = "Get"; // refiere a la Accion de que se ejecuta
            MiBotica.SolPedido.Entidades.Core.Cliente Entcli = new MiBotica.SolPedido.Entidades.Core.Cliente();
            using (WebClient cliente = new WebClient())
            {
                cliente.Headers.Clear(); // borra datos anterioes d e la cabecera
                cliente.Headers[HttpRequestHeader.ContentType] = jsonMediaType; // tipo de dato
                cliente.Encoding = UTF8Encoding.UTF8;// tipo de decodificacion reconocimiento de carateres especiales.
                string rutacompleta = rutaApi + metodo + "/" + id;
                var data = cliente.DownloadString(new Uri(rutacompleta));// ejecuta la busqueda en el Api.
                Entcli = JsonConvert.DeserializeObject<MiBotica.SolPedido.Entidades.Core.Cliente>(data);// transforma los datos a una lista tipada
            }
            return View(Entcli);
        }

        // POST: Cliente/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, MiBotica.SolPedido.Entidades.Core.Cliente collection)
        {
            string metodo = "Cliente"; //refiere al controlador del la Api
            try
            {
                // TODO: Add update logic here
                using (WebClient cliente = new WebClient())
                {
                    cliente.Headers.Clear(); // borra datos anterioes de la cabecera
                    cliente.Headers[HttpRequestHeader.ContentType] = jsonMediaType;
                    // tipo de dato
                    cliente.Encoding = UTF8Encoding.UTF8;// tipo de decodificacion reconocimiento de carateres especiales.
                    var clienteJson = JsonConvert.SerializeObject(collection);// convierte a clase Cliente en una trama Json
                    // forma 1
                    string rutacompleta = rutaApi + metodo + "/" + id;
                    var resultado = cliente.UploadString(new Uri(rutacompleta), "Put", clienteJson);

                    // otra forma
                    // Uri rutacompleta = new Uri(RutaApi + metodo);
                    // var resultado = Cliente.UploadString(rutacompleta, Put, clienteJson);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                var zz = ex.Message;
                return View();
            }
        }

        // GET: Cliente/Delete/5
        public ActionResult Delete(int id)
        {
            MiBotica.SolPedido.Entidades.Core.Cliente cliente = null;
            string metodo = "Cliente/" + id; // ruta de la API con el ID

            using (WebClient client = new WebClient())
            {
                client.Headers.Clear();
                client.Headers[HttpRequestHeader.ContentType] = jsonMediaType;
                client.Encoding = UTF8Encoding.UTF8;

                // Obtenemos el cliente desde la API
                string rutacompleta = rutaApi + metodo;
                var data = client.DownloadString(new Uri(rutacompleta));
                cliente = JsonConvert.DeserializeObject<MiBotica.SolPedido.Entidades.Core.Cliente>(data);
            }

            return View(cliente); // Devolvemos los detalles del cliente para confirmación
        }

        // POST: Cliente/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                string metodo = "Cliente/" + id; // ruta de la API con el ID
                using (WebClient client = new WebClient())
                {
                    client.Headers.Clear();
                    client.Headers[HttpRequestHeader.ContentType] = jsonMediaType;
                    client.Encoding = UTF8Encoding.UTF8;

                    // Ejecutamos la solicitud DELETE
                    string rutacompleta = rutaApi + metodo;
                    client.UploadString(new Uri(rutacompleta), "DELETE", "");
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
