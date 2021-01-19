using System;

namespace BancaElectronica.Controllers
{
    public class DtoCuenta<T>
    {


        public decimal Saldo { get; set; }


        public DateTime FechaCreacion { get; set; }

        public string NumeroCuenta { get; set; }

        public string TipoCuenta { get; set; }



    }
}