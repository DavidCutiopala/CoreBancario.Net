using EcVotoElectronico.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using VotoElectronico.Entidades.Pk.PkMesaIdentificacion;
using VotoElectronico.Generico;
using VotoElectronico.Generico.Propiedades;
using VotoElectronico.LogicaCondicional;
using VotoElectronico.Util;

namespace VotoElectronico.LogicaNegocio.Servicios
{
    public class OpcionService : IOpcionService
    {


        private readonly IApiResponseMessage _apiResponseMessage;
        private readonly ISesionService _sesionService;

        public OpcionService(IApiResponseMessage apiResponseMessage, ISesionService sesionService)
        {
            _apiResponseMessage = apiResponseMessage;
            _sesionService = sesionService;
        }



    }
}
