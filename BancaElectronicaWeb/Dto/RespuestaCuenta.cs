using BancaElectronicaWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BancaElectronicaWeb.Dto
{
    public class RespuestaCuenta
    {
        public string errorExceptionMessage { get; set; }

        public InformacionMensaje informationMessage { get; set; }

        public List<Cuenta> responseList { get; set; }

    }
}