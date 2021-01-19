using Newtonsoft.Json;


namespace VotoElectronico.Generico
{
    public class DtoPersona
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("nombreUno")]
        public string nombreUno { get; set; }
        [JsonProperty("nombreDos")]
        public string nombreDos { get; set; }
        [JsonProperty("apellidoUno")]
        public string apellidoUno { get; set; }
        [JsonProperty("apellidoDos")]
        public string apellidoDos { get; set; }
        [JsonProperty("identificacion")]
        public string identificacion { get; set; }
        [JsonProperty("telefono")]
        public string telefono { get; set; }
        [JsonProperty("teleemailfono")]
        public string email { get; set; }
        [JsonProperty("usuarioCreacion")]
        public string usuarioCreacion { get; set; }
        [JsonProperty("usuarioModificacion")]
        public string usuarioModificacion { get; set; }

        [JsonProperty("estado")]
        public string estado { get; set; }

        [JsonProperty("dt1")]
        public string dt1 { get; set; }


    }
}
