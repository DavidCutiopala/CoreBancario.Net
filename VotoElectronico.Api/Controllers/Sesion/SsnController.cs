using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VotoElectronico.Generico;
using VotoElectronico.LogicaNegocio.Servicios;
using VotoElectronico.Util;

namespace VotoElectronico.Api.Controllers
{
    [AllowAnonymous]
    [RoutePrefix("api/Ssn")] 
    public class SsnController: ApiController
    {
        private readonly IApiResponseMessage _apiResponseMessage;
        private readonly ISesionService _sesionService;
        private readonly ITokenValidator _tokenValidator;
        public SsnController(IApiResponseMessage apiResponseMessage, ISesionService sesionService, ITokenValidator tokenValidator)
        {
            _apiResponseMessage = apiResponseMessage;
            _tokenValidator = tokenValidator;
            _sesionService = sesionService;
        }


        [HttpPost]
        [Route("VELlgOut")]
        public HttpResponseMessage SVELgOut(DtoGenerico dto)
        {
            try
            {

                dto.Dt20 = Request.Headers.Authorization?.Parameter;
                return Request.CreateResponse(HttpStatusCode.OK, _sesionService.CerrarSesion(dto.Dt1, dto.Dt20));
            }
            catch (Exception ex)
            {
                 return Request.CreateResponse(HttpStatusCode.BadRequest, _apiResponseMessage.crearDtoErrorExceptionMessage(ex));

            }
        }



        /// <summary>
        /// Verifica si existe el token de cambio de clave
        /// </summary>
        /// <param name="dto">Rececpcion de Datos</param>
        /// <returns></returns>
        [HttpPost]
        [Route("VExTkn")]
        public HttpResponseMessage VExTkn(DtoGenerico dto)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, _apiResponseMessage.CrearDtoApiResponseMessage(new List<DtoGenerico>() { new DtoGenerico() { Bdt1 = _tokenValidator.ExisteTokenUsuarioCambioClave(dto.Dt20) } }, "VE_LGN_INS_004"));
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, _apiResponseMessage.crearDtoErrorExceptionMessage(ex));
            }
        }


        [HttpPost]
        [Route("VECmbClv")]
        public HttpResponseMessage VECmbClv(DtoGenerico dto)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, _sesionService.CambioClave(dto.Dt20, dto.Dt1));
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, _apiResponseMessage.crearDtoErrorExceptionMessage(ex));
            }
        }


        [HttpPost]
        [Route("SlcCmCv")]
        public HttpResponseMessage SlcCmCv(DtoGenerico dto)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, _sesionService.SolicitudCambioClave(dto.Dt1));
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, _apiResponseMessage.crearDtoErrorExceptionMessage(ex));
            }
        }

        

    }
}