using Newtonsoft.Json;


namespace VotoElectronico.Generico
{
    public class DtoRol
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("nombreRol")]
        public string NombreRol { get; set; }
       
        [JsonProperty("usuarioCreacion")]
        public string UsuarioCreacion { get; set; }
        [JsonProperty("usuarioModificacion")]
        public string UsuarioModificacion { get; set; }

        [JsonProperty("estado")]
        public string Estado { get; set; }


    }
}
