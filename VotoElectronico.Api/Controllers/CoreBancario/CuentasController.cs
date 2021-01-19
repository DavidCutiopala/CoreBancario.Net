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
    [RoutePrefix("api/Cuenta")]
    public class CuentaController : ApiController
    {
        private readonly  ICuentaService _cuentaService;
        private readonly IApiResponseMessage _apiResponseMessage;
        public CuentaController(
            ICuentaService cuentaService, 
            IApiResponseMessage apiResponseMessage)
        {
            _cuentaService = cuentaService;
            _apiResponseMessage = apiResponseMessage;
        }


        [HttpPost]
        [Route("ObtenerCuentaByCI")]
        public HttpResponseMessage obtenerCuentasByClienteId(DtoClienteCB dto)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, _cuentaService.ObtenerCuentasByCedula(dto.Cedula));
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, _apiResponseMessage.crearDtoErrorExceptionMessage(ex));
            }
        }

        [HttpPost]
        [Route("CrearCuenta")]
        public HttpResponseMessage crearCuentaController(DtoCuenta dto)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, _cuentaService.CrearCuenta(dto));
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, _apiResponseMessage.crearDtoErrorExceptionMessage(ex));
            }
        }


    }
}