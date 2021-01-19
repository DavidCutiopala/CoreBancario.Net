using Newtonsoft.Json;


namespace VotoElectronico.Generico
{
    public class DtoUsuarioCargo
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("cargoId")]
        public long cargoId { get; set; }

        [JsonProperty("usuarioId")]
        public long usuarioId { get; set; }
       
        [JsonProperty("usuarioCreacion")]
        public string usuarioCreacion { get; set; }
        [JsonProperty("usuarioModificacion")]
        public string usuarioModificacion { get; set; }

        [JsonProperty("estado")]
        public string estado { get; set; }


    }
}
