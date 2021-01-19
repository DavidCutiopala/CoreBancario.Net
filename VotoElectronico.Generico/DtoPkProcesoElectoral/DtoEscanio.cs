using Newtonsoft.Json;


namespace VotoElectronico.Generico
{
    public class DtoEscanio
    {
        [JsonProperty("escanioId")]
        public long escanioId { get; set; }
        
        [JsonProperty("nombreEscanio")]
        public string nombreEscanio { get; set; }

        [JsonProperty("eleccionId")]
        public long eleccionId { get; set; }

        [JsonProperty("nombreEleccion")]
        public string nombreEleccion { get; set; }

        [JsonProperty("usuarioCreacion")]
        public string usuarioCreacion { get; set; }

        [JsonProperty("usuarioModificacion")]
        public string usuarioModificacion { get; set; }

        [JsonProperty("estado")]
        public string estado { get; set; }


    }
}
