using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BancaElectronicaWeb.Models
{
    public class Movimiento
    {
        public long Id { get; set; }

        public DateTime FechaCreacion { get; set; }


        public string TipoMovimiento { get; set; }


        public decimal Valor { get; set; }

        public decimal SaldoFinal { get; set; }


        public string CuentaOrigen { get; set; }


        public string CuentaDestino { get; set; }



        public long CuentaId { get; set; }

    }
}