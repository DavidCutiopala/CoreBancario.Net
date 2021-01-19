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
    [RoutePrefix("api/Cliente")]
    public class ClienteController : ApiController
    {
        private readonly IClienteCBService _clienteCbService;
        private readonly IApiResponseMessage _apiResponseMessage;
        public ClienteController(IClienteCBService clienteCbService, IApiResponseMessage apiResponseMessage)
        {
            _clienteCbService = clienteCbService;
            _apiResponseMessage = apiResponseMessage;
        }


        [HttpPost]
        [Route("VELCrLis")]
        public HttpResponseMessage crearClienteController(DtoClienteCB dto)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, _clienteCbService.CrearClienteCB(dto));
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, _apiResponseMessage.crearDtoErrorExceptionMessage(ex));
            }
        }
   

    }
}