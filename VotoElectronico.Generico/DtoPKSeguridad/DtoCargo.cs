using Newtonsoft.Json;


namespace VotoElectronico.Generico
{
    public class DtoCargo
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("nombreCargo")]
        public string nombreCargo { get; set; }
       
        [JsonProperty("usuarioCreacion")]
        public string usuarioCreacion { get; set; }
        [JsonProperty("usuarioModificacion")]
        public string usuarioModificacion { get; set; }

        [JsonProperty("estado")]
        public string estado { get; set; }


    }
}
