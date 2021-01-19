using BancaElectronicaWeb.Dto;
using BancaElectronicaWeb.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace BancaElectronicaWeb.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        
        public ActionResult Index()
        {
            return View();
        }

        public async System.Threading.Tasks.Task<ActionResult> autorizacion(Cliente clienteVista)
        {
          
            Cliente cliente = new Cliente();
            cliente.usuario = clienteVista.usuario;
            cliente.password = clienteVista.password;
            Console.WriteLine(cliente);
            var json = JsonConvert.SerializeObject(cliente);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var url = "http://localhost:59316/api/usuario/login";
            var client = new HttpClient();

            var response = await client.PostAsync(url, data);

            string result = response.Content.ReadAsStringAsync().Result;
            Console.WriteLine(result);

            var respuesta = JsonConvert.DeserializeObject<RespuestaLogin>(result);
            // return View(respuesta);
            if (respuesta.responseList !=null) {
                Session["nombre"] = respuesta.responseList[0].nombre;
                Session["apellido"] = respuesta.responseList[0].apellido;
                Session["idCliente"] = respuesta.responseList[0].idCliente;
                Session["cedula"] = respuesta.responseList[0].cedula;
                return RedirectToAction("Index", "Inicio");
            }
            else {
                return RedirectToAction("Index", "Home");
            }
        
        }
    }
}