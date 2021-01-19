using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VotoElectronico.Generico;
using VotoElectronico.LogicaNegocio.Servicios;
using VotoElectronico.Util;

namespace VotoElectronico.Api.Controllers
{
    [Authorize]
    [RoutePrefix("api/Prm")]
    public class PermisoController: ApiController
    {
        private readonly IPermisoService _permisoService;
        private readonly IApiResponseMessage _util;
        private readonly ITokenValidator _tokenValidator;
        public PermisoController(IPermisoService permisoService, IApiResponseMessage util, ITokenValidator tokenValidator)
        {
            _permisoService = permisoService;
            _util = util;
            _tokenValidator = tokenValidator;
        }


        //[HttpPost]
        //[Route("ValRtCrg")]
        //public HttpResponseMessage validarRutaMedianteCargo (DtoPermiso dtoPermiso)
        //{
        //    try
        //    {
        //        var token = Request.Headers.Authorization.Parameter;
        //        //_tokenValidator.ValidarToken(token);
        //        return Request.CreateResponse(HttpStatusCode.OK, _permisoService.ValidarRutaMedianteCargo(dtoPermiso, token));
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateResponse(HttpStatusCode.BadRequest, _util.crearDtoErrorExceptionMessage(ex));
        //    }
        //}

        [HttpPost]
        [Route("ValRtaRl")]
        public HttpResponseMessage validarRutaMedianteRol(DtoPermiso dtoPermiso)
        {
            try
            {
                var token = Request.Headers.Authorization.Parameter;
                //_tokenValidator.ValidarToken(token);
                return Request.CreateResponse(HttpStatusCode.OK, _permisoService.ValidarRutaMedianteRol(dtoPermiso, token));
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, _util.crearDtoErrorExceptionMessage(ex));
            }
        }




    }
}