using Newtonsoft.Json;


namespace VotoElectronico.Generico
{
    public class DtoMensaje
    {
        [JsonProperty("codigoMensaje")]
        public string CodigoMensaje { get; set; }
        [JsonProperty("tipoMensajeId")]
        public string TipoMensajeId { get; set; }
        [JsonProperty("titulo")]
        public string Titulo { get; set; }
        [JsonProperty("descripcion")]
        public string Descripcion { get; set; }

    }
}
