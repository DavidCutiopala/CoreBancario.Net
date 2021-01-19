using Newtonsoft.Json;
using System;

namespace VotoElectronico.Generico
{
    public class DtoUsuario
    {
        [JsonProperty("idUsuario")]
        public long Id { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("envioEmailHabilitado")]
        public bool EnvioEmailActivacion { get; set; }
        [JsonProperty("clave")]
        public string Clave { get; set; }
        [JsonProperty("nombreUsuario")]
        public string nombreUsuario { get; set; }
        [JsonProperty("estado")]
        public string estado { get; set; }

        [JsonProperty("usuarioCreacion")]
        public string usuarioCreacion { get; set; }
        [JsonProperty("fechaCreacion")]
        public DateTime fechaCreacion { get; set; }
        [JsonProperty("usuarioModificacion")]
        public string usuarioModificacion { get; set; }
        [JsonProperty("fechaModificacion")]
        public DateTime fechaModificacion { get; set; }
        [JsonProperty("token")]
        public string Token { get; set; }
        [JsonProperty("personaId")]
        public long PersonaId { get; set; }

    }
}
