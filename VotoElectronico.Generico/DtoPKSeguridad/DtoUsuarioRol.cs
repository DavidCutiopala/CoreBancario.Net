using Newtonsoft.Json;


namespace VotoElectronico.Generico
{
    public class DtoUsuarioRol
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("rolId")]
        public long RolId { get; set; }

        [JsonProperty("usuarioId")]
        public long UsuarioId { get; set; }

        [JsonProperty("estado")]
        public string Estado { get; set; }


    }
}
