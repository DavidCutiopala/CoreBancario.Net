using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VotoElectronico.LogicaNegocio.Servicios;
using VotoElectronico.Util;

namespace VotoElectronico.Api.Controllers
{
    [Authorize]
    [RoutePrefix("api/Rcrs")]
    public class RecursoController: ApiController
    {
        private readonly IRecursoService _recursoService;
        private readonly IApiResponseMessage _apiResponseMessage;
        private readonly ITokenValidator _tokenValidator;
        public RecursoController(IRecursoService recursoService, IApiResponseMessage util, ITokenValidator tokenValidator)
        {
            _recursoService = recursoService;
            _apiResponseMessage = util;
            _tokenValidator = tokenValidator;
        }




        [HttpPost]
        [Route("VEslMnUsr")]
        public HttpResponseMessage VEslMnUsr()
        {
            try
            {
                var token = Request.Headers.Authorization.Parameter;
                _tokenValidator.ValidarToken(token);
                return Request.CreateResponse(HttpStatusCode.OK, _recursoService.ObtenerMenuUsuarioPorToken(token));
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, _apiResponseMessage.crearDtoErrorExceptionMessage(ex));
            }
        }




    }
}