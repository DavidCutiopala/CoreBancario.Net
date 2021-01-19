using Newtonsoft.Json;

namespace VotoElectronico.Generico
{
    public class DtoEspecificacion
    {
        [JsonProperty("parametroBusqueda1")]
        public string parametroBusqueda1 { get; set; }

        [JsonProperty("parametroBusqueda2")]
        public string parametroBusqueda2 { get; set; }

        [JsonProperty("parametroBusqueda3")]
        public string parametroBusqueda3 { get; set; }

        [JsonProperty("parametroBusqueda4")]
        public int parametroBusqueda4 { get; set; }
        [JsonProperty("parametroBusqueda5")]
        public long parametroBusqueda5 { get; set; }
    }
}
