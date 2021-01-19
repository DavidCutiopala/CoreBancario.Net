using System;
using System.Collections.Generic;
using VotoElectronico.Generico;

namespace VotoElectronico.Util
{
    public interface IApiResponseMessage
    {

        DtoApiResponseMessage CrearDtoApiResponseMessage(IEnumerable<object> responseList = null, string codigoMesaje = "");
        DtoApiResponseMessage crearDtoErrorExceptionMessage(Exception ex);

    }
}
