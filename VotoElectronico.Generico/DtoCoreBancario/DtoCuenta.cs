using Newtonsoft.Json;
using System;

namespace VotoElectronico.Generico
{
    public class DtoCuenta
    {
        [JsonProperty("saldo")]
        public decimal Saldo { get; set; }

        [JsonProperty("fechaCreacion")]
        public DateTime FechaCreacion { get; set; }
        
        [JsonProperty("numeroCuenta")]
        public string NumeroCuenta { get; set; }

        [JsonProperty("tipoCuenta")]
        public string TipoCuenta { get; set; }

        [JsonProperty("clienteId")]
        public long ClienteId { get; set; }




    }
}
