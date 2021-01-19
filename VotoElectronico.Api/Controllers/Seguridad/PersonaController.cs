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
    [RoutePrefix("api/persona")]
    public class personaController: ApiController
    {
        private readonly IPersonaService _personaService;
        private readonly IApiResponseMessage _apiResponseMessage;
        private readonly ITokenValidator _tokenValidator;
        public personaController(IPersonaService personaService, IApiResponseMessage apiResponseMessage, ITokenValidator tokenValidator)
        {
            _personaService = personaService;
            _apiResponseMessage = apiResponseMessage;
            _tokenValidator = tokenValidator;
        }


        [HttpPost]
        [Route("crear-persona")]
        public HttpResponseMessage crearpersonaController (DtoPersona dtopersona)
        {
            try
            {
                var token = Request.Headers.Authorization.Parameter;
                _tokenValidator.ValidarToken(token);
                
                return Request.CreateResponse(HttpStatusCode.OK, _personaService.CrearPersona(dtopersona, token));
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, _apiResponseMessage.crearDtoErrorExceptionMessage(ex));
            }
        }

        //[HttpPut]
        //[Route("actualizar-persona")]
        //public HttpResponseMessage modificarpersonaController(DtoPersona dtopersona)
        //{
        //    try
        //    {
        //        return Request.CreateResponse(HttpStatusCode.OK, _personaService.ActualizarPersona(dtopersona));
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateResponse(HttpStatusCode.BadRequest, _utilitarios.crearDtoErrorExceptionMessage(ex));
        //    }
        //}

        [HttpPost]
        [Route("obtener-persona-mediante-id")]
        public HttpResponseMessage obtenerpersonaMedianteId(DtoPersona dtopersona)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, _personaService.ObtenerPersonaMedianteId(dtopersona));
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, _apiResponseMessage.crearDtoErrorExceptionMessage(ex));

            }
        }

        [HttpDelete]
        [Route("eliminar-persona")]
        public HttpResponseMessage eliminarpersona(DtoPersona dtopersona)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, _personaService.EliminarPersona(dtopersona));
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, _apiResponseMessage.crearDtoErrorExceptionMessage(ex));

            }
        }

        [HttpPost]
        [Route("obtener-personas-por-nombres")]
        public HttpResponseMessage obtenerpersonasPorNombreOId(DtoEspecificacion dtoSpec)
        { 
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, _personaService.ObtenerPersonasPorParametros(dtoSpec.parametroBusqueda1));
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, _apiResponseMessage.crearDtoErrorExceptionMessage(ex));

            }
        }


    }
}