using Newtonsoft.Json;


namespace VotoElectronico.Generico
{
    public class DtoTipoEleccion
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("nombreTipoEleccion")]
        public string nombreTipoEleccion { get; set; }
       
        [JsonProperty("usuarioCreacion")]
        public string usuarioCreacion { get; set; }
        [JsonProperty("usuarioModificacion")]
        public string usuarioModificacion { get; set; }

        [JsonProperty("estado")]
        public string estado { get; set; }


    }
}
