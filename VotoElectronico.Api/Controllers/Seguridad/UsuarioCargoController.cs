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
    [RoutePrefix("api/usuario-cargo")]
    public class UsuarioCargoController: ApiController
    {
        private readonly IUsuarioCargoService _usuarioCargoService;
        private readonly IApiResponseMessage _util;
        private readonly ITokenValidator _tokenValidator;
        public UsuarioCargoController(IUsuarioCargoService usuarioCargoService, IApiResponseMessage apiResponseMessage, ITokenValidator tokenValidator)
        {
            _usuarioCargoService = usuarioCargoService;
            _util = apiResponseMessage;
            _tokenValidator = tokenValidator;
        }



        [HttpPut]
        [Route("actualizar-usuario-cargo")]
        public HttpResponseMessage actualizarUsuarioCargoController(DtoUsuarioCargo DtoCargo)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, _usuarioCargoService.ActualizarUsuarioCargo(DtoCargo));
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, _util.crearDtoErrorExceptionMessage(ex));
            }
        }

        [HttpPost]
        [Route("obtener-usuario-cargo-mediante-id")]
        public HttpResponseMessage obtenerUsuarioCargoMedianteId(DtoUsuarioCargo DtoCargo)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, _usuarioCargoService.ObtenerUsuarioCargoMedianteId(DtoCargo));
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, _util.crearDtoErrorExceptionMessage(ex));

            }
        }



    }
}