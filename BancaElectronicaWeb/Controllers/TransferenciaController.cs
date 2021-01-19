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
    public class TransferenciaController : Controller
    {
        // GET: Transferencia
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> Index(Transferencia transferencia)
        {

            Transferencia nuevaTransferencia = new Transferencia();
            nuevaTransferencia.CuentaOrigen = transferencia.CuentaOrigen;
            nuevaTransferencia.CuentaDestino = transferencia.CuentaDestino;
            nuevaTransferencia.Monto = transferencia.Monto;

            Console.WriteLine(transferencia);
            var json = JsonConvert.SerializeObject(transferencia);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var url = "http://localhost:59316/api/Movimiento/TransferirMonto";
            var client = new HttpClient();

            var response = await client.PostAsync(url,data);

            string result = response.Content.ReadAsStringAsync().Result;
            Console.WriteLine(result);

            var respuesta = JsonConvert.DeserializeObject<RespuestaTransferencia>(result);

            ViewBag.Message = respuesta.informationMessage.descripcion;

            return View(transferencia);

        }
    }
}