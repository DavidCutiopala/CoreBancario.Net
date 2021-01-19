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
    [RoutePrefix("api/usuario")]
    public class UsrController: ApiController
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IApiResponseMessage _util;
        private readonly ITokenValidator _tokenValidator;
        public UsrController(IUsuarioService usuarioService, IApiResponseMessage util, ITokenValidator tokenValidator)
        {
            _usuarioService = usuarioService;
            _util = util;
            _tokenValidator = tokenValidator;
        }


        [HttpPost]
        [Route("crear-usuario")]
        public HttpResponseMessage crearUsuarioController (DtoUsuario dtoUsuario)
        {
            try
            {
                var token = Request.Headers.Authorization.Parameter;
                _tokenValidator.ValidarToken(token);
                return Request.CreateResponse(HttpStatusCode.OK, _usuarioService.CrearUsuario(dtoUsuario,token));
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, _util.crearDtoErrorExceptionMessage(ex));
            }
        }

        [HttpPut]
        [Route("actualizar-usuario")]
        public HttpResponseMessage modificarUsuarioController(DtoUsuario dtoUsuario)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, _usuarioService.ActualizarUsuario(dtoUsuario));
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, _util.crearDtoErrorExceptionMessage(ex));
            }
        }

        [HttpPost]
        [Route("obtener-usuario-mediante-id")]
        public HttpResponseMessage obtenerUsuarioMedianteId(DtoUsuario dtoUsuario)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, _usuarioService.ObtenerUsuarioMedianteId(dtoUsuario));
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, _util.crearDtoErrorExceptionMessage(ex));

            }
        }

        [HttpDelete]
        [Route("eliminar-usuario")]
        public HttpResponseMessage eliminarUsuario(DtoUsuario dtoUsuario)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, _usuarioService.EliminarUsuario(dtoUsuario));
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, _util.crearDtoErrorExceptionMessage(ex));

            }
        }

        [HttpPost]
        [Route("obtener-usuarios-por-nombre-o-id")]
        public HttpResponseMessage obtenerUsuariosPorNombreOId(DtoEspecificacion dtoSpec)
        { 
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, _usuarioService.ObtenerUsuariosPorNombreOIdentificacion(dtoSpec.parametroBusqueda1 , dtoSpec.parametroBusqueda2));
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, _util.crearDtoErrorExceptionMessage(ex));

            }
        }


    }
}