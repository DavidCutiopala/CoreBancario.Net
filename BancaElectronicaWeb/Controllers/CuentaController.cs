using BancaElectronicaWeb.Dto;
using BancaElectronicaWeb.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BancaElectronicaWeb.Controllers
{
   
    public class CuentaController : Controller
    {
        private string cedula = "1723882036";
        // GET: Cuenta
        public async Task<ActionResult> Index()
        {

            Cliente cliente = new Cliente();
            cliente.cedula = cedula;
            Console.WriteLine(cliente);
            var json = JsonConvert.SerializeObject(cliente);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var url = "http://localhost:59316/api/Cuenta/ObtenerCuentaByCI";
            var client = new HttpClient();

            var response = await client.PostAsync(url, data);

            string result = response.Content.ReadAsStringAsync().Result;
            Console.WriteLine(result);

            var respuesta = JsonConvert.DeserializeObject<RespuestaCuenta>(result);
            // return View(respuesta);
            return View(respuesta);
        }
    }
}