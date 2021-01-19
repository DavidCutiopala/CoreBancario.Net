using Newtonsoft.Json;


namespace VotoElectronico.Generico
{
    public class DtoImagen
    {
        [JsonProperty("base64")]
        public string base64 { get; set; }
        [JsonProperty("tipo")]
        public string tipo { get; set; }
        [JsonProperty("extension")]
        public string extension { get; set; }
      

    }
}
