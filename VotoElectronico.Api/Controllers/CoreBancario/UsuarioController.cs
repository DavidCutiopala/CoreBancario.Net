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
    [RoutePrefix("api/usuario")]
    public class UsuarioController : ApiController
    {
        private readonly IUsuarioCBService _usuarioCbService;
        private readonly IApiResponseMessage _apiResponseMessage;
        public UsuarioController(
             IUsuarioCBService usuarioCbService, 
            IApiResponseMessage apiResponseMessage
            )
        {
            _usuarioCbService = usuarioCbService;
            _apiResponseMessage = apiResponseMessage;
        }


        [HttpPost]
        [Route("login")]
        public HttpResponseMessage crearClienteController(DtoClienteCB dto)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, _usuarioCbService.Login(dto));
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, _apiResponseMessage.crearDtoErrorExceptionMessage(ex));
            }
        }
   

    }
}