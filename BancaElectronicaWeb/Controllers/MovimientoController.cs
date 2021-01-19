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
using System.Threading.Tasks;

namespace BancaElectronicaWeb.Controllers
{
    public class MovimientoController : Controller
    {
        // GET: Movimiento
        public async Task<ActionResult> Index()
        {


            Cliente cliente = new Cliente();
            cliente.idCliente = Session["idCliente"].ToString();

            Console.WriteLine(cliente);
            var json = JsonConvert.SerializeObject(cliente);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var url = "http://localhost:59316/api/Movimiento/ObtenerMovimientosByCliente";
            var client = new HttpClient();

            var response = await client.PostAsync(url, data);

            string result = response.Content.ReadAsStringAsync().Result;
            Console.WriteLine(result);

            var respuesta = JsonConvert.DeserializeObject<RespuestaMovimiento>(result);
            return View(respuesta.responseList);
            
        }
    }
}