using Newtonsoft.Json;

namespace VotoElectronico.Generico
{
    public class DtoPadronVotacion
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("nombreEmpadronado")]
        public string NombreEmpadronado { get; set; }

        [JsonProperty("emailEmoadronado")]
        public string emailEmpadronado { get; set; }

        [JsonProperty("identificacionEmpadronado")]
        public string identificacionEmpadronado { get; set; }

        [JsonProperty("procesoElectoralId")]
        public long procesoElectoralId { get; set; }

        [JsonProperty("usuarioId")]
        public long usuarioId { get; set; }

        [JsonProperty("votoRealizado")]
        public bool votoRealizado { get; set; }


        [JsonProperty("usuarioCreacion")]
        public string usuarioCreacion { get; set; }

        [JsonProperty("usuarioModificacion")]
        public string usuarioModificacion { get; set; }

        [JsonProperty("estado")]
        public string estado { get; set; }


    }
}
