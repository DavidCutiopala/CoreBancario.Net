using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VotoElectronico.Generico;
using VotoElectronico.LogicaNegocio.Servicios;
using VotoElectronico.Util;

namespace VotoElectronico.Api.Controllers
{

    //[Authorize]
    [RoutePrefix("api/Movimiento")]
    public class MovimientoController : ApiController
    {
        private readonly IMovimientosService _movimientoService;
        private readonly IApiResponseMessage _apiResponseMessage;
        public MovimientoController(IMovimientosService movimientoService, IApiResponseMessage apiResponseMessage)
        {
            _movimientoService = movimientoService;
            _apiResponseMessage = apiResponseMessage;
        }


        [HttpPost]
        [Route("TransferirMonto")]
        public HttpResponseMessage transferirMonto(DtoTranferencia dto)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, _movimientoService.RealizarTransferencia(dto));
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, _apiResponseMessage.crearDtoErrorExceptionMessage(ex));
            }
        }

        [HttpPost]
        [Route("ObtenerMovimientosByCliente")]
        public HttpResponseMessage ObtenerMovimientosByCliente(DtoClienteCB dto)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, _movimientoService.ObtenerMovimientosByClientes(dto.IdCliente));
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, _apiResponseMessage.crearDtoErrorExceptionMessage(ex));
            }
        }

    }
}