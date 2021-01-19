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
    [RoutePrefix("rest-api/cargo")]
    public class CargoController: ApiController
    {
        private readonly ICargoService _cargoService;
        private readonly IApiResponseMessage _utilitarios;
        public CargoController(ICargoService cargoService, IApiResponseMessage utilitarios)
        {
            _cargoService = cargoService;
            _utilitarios = utilitarios;
        }


        [HttpPost]
        [Route("crear-cargo")]
        public HttpResponseMessage crearCargoController (DtoCargo DtoCargo)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, _cargoService.CrearCargo(DtoCargo));
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, _utilitarios.crearDtoErrorExceptionMessage(ex));
            }
        }

        [HttpPut]
        [Route("actualizar-cargo")]
        public HttpResponseMessage modificarCargoController(DtoCargo DtoCargo)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, _cargoService.ActualizarCargo(DtoCargo));
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, _utilitarios.crearDtoErrorExceptionMessage(ex));
            }
        }

        [HttpPost]
        [Route("obtener-cargo-mediante-id")]
        public HttpResponseMessage obtenerCargoMedianteId(DtoCargo DtoCargo)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, _cargoService.ObtenerCargoMedianteId(DtoCargo));
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, _utilitarios.crearDtoErrorExceptionMessage(ex));

            }
        }

        [HttpDelete]
        [Route("eliminar-cargo")]
        public HttpResponseMessage eliminarCargo(DtoCargo DtoCargo)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, _cargoService.EliminarCargo(DtoCargo));
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, _utilitarios.crearDtoErrorExceptionMessage(ex));

            }
        }

        [HttpPost]
        [Route("obtener-cargos-por-nombre")]
        public HttpResponseMessage obtenerCargosPorNombre(DtoEspecificacion dtoSpec)
        { 
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, _cargoService.ObtenerCargosPorNombre(dtoSpec.parametroBusqueda1));
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, _utilitarios.crearDtoErrorExceptionMessage(ex));

            }
        }


    }
}