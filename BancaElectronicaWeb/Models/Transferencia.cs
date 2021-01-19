using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BancaElectronicaWeb.Models
{
    public class Transferencia
    {

        public string CuentaOrigen { get; set; }

 
        public string CuentaDestino { get; set; }

  
        public decimal Monto { get; set; }
    }
}