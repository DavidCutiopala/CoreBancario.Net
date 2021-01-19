using Newtonsoft.Json;


namespace VotoElectronico.Generico
{
    public class DtoTranferencia
    {
        [JsonProperty("cuentaOrigen")]
        public string CuentaOrigen { get; set; }
        
        [JsonProperty("cuentaDestino")]
        public string CuentaDestino { get; set; }

        [JsonProperty("monto")]
        public decimal Monto { get; set; }

    }
}
