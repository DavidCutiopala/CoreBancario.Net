using BancaElectronicaWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BancaElectronicaWeb.Dto
{
    public class RespuestaLogin
    {
        public string errorExceptionMessage { get; set; }

        public InformacionMensaje informationMessage { get; set; }

        public List<Cliente> responseList { get; set; }
    }
}