using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BancaElectronicaWeb.Models
{
    public class Cuenta
    {

 

        public decimal saldo { get; set; }


        public DateTime fechaCreacion { get; set; }


        public string numeroCuenta { get; set; }


        public string tipoCuenta { get; set; }
    }
}