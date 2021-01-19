using Newtonsoft.Json;


namespace VotoElectronico.Generico
{
    public class DtoCandidato
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        
        [JsonProperty("personaId")]
        public long personaId { get; set; }

        [JsonProperty("nombreCandidato")]
        public string nombreCandidato { get; set; }

        [JsonProperty("identificacion")]
        public string identificacion { get; set; }

        [JsonProperty("procesoElectoralId")]
        public long procesoElectoralId { get; set; }
        [JsonProperty("nombreProcesoElectoral")]
        public string nombreProcesoElectoral { get; set; }

        [JsonProperty("escanioId")]
        public long escanioId { get; set; }

        [JsonProperty("nombreEscanio")]
        public string nombreEscanio { get; set; }

        [JsonProperty("listaId")]
        public long listaId { get; set; }

        [JsonProperty("nombreLista")]
        public string nombreLista { get; set; }

        [JsonProperty("usuarioCreacion")]
        public string usuarioCreacion { get; set; }
        [JsonProperty("usuarioModificacion")]
        public string usuarioModificacion { get; set; }

        [JsonProperty("estado")]
        public string estado { get; set; }

        [JsonProperty("fotoObjeto")]
        public DtoImagen fotoObjeto { get; set; }

        [JsonProperty("fotoUrl")]
        public string fotoUrl { get; set; }


    }
}
