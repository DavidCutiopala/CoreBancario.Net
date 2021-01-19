using Newtonsoft.Json;


namespace VotoElectronico.Generico
{
    public class DtoClienteCB
    {
        [JsonProperty("idCliente")]
        public long IdCliente { get; set; }

        [JsonProperty("nombre")]
        public string Nombre { get; set; }
        
        [JsonProperty("apellido")]
        public string Apellido { get; set; }

        [JsonProperty("cedula")]
        public string Cedula { get; set; }

        [JsonProperty("telefono")]
        public string Telefono { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("direccion")]
        public string Direccion { get; set; }

        // Campos de usuario

        [JsonProperty("usuario")]
        public string Usuario { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }


    }
}
