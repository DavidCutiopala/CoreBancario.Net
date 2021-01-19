using Newtonsoft.Json;


namespace VotoElectronico.Generico
{
    public class DtoPermiso
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("rolId")]
        public long rolId { get; set; }
       
        [JsonProperty("recursoId")]
        public long recursoId { get; set; }
       
        [JsonProperty("estado")]
        public string estado { get; set; }

        // Variable auxiliar para consulta de validacion de rutas 

        [JsonProperty("ruta")]
        public string ruta { get; set; }


    }
}
