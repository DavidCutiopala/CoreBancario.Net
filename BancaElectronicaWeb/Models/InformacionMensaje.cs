using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BancaElectronicaWeb.Models
{
    public class InformacionMensaje
    {

        public string codigoMensaje { get; set; }

        public string tipoMensajeId { get; set; }
        public string titulo { get; set; }
        public string descripcion { get; set; }

    }
}