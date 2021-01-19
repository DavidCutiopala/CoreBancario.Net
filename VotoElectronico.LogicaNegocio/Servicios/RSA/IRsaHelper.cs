using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VotoElectronico.LogicaNegocio.Servicios
{
    public interface IRsaHelper
    {
        string FirmaDigital(string datosOriginales);
        bool VerificarFirmaDigital(string datosOriginales, string datosFirmados);

    }
}
