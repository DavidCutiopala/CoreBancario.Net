using Newtonsoft.Json;
using System.Collections.Generic;
using VotoElectronico.Entidades.Shell;

namespace VotoElectronico.Generico
{
    public class DtoLista
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        
        [JsonProperty("nombreLista")]
        public string nombreLista { get; set; }

        [JsonProperty("procesoElectoralId")]
        public long procesoElectoralId { get; set; }

        [JsonProperty("nombreProcesoElectoral")]
        public string nombreProcesoElectoral { get; set; }

        [JsonProperty("eslogan")]
        public string eslogan { get; set; }

        [JsonProperty("logoObjeto")]
        public DtoImagen logoObjeto { get; set; }

        [JsonProperty("logoUrl")]
        public string logoUrl { get; set; }

        [JsonProperty("usuarioCreacion")]
        public string usuarioCreacion { get; set; }
        [JsonProperty("usuarioModificacion")]
        public string usuarioModificacion { get; set; }

        [JsonProperty("estado")]
        public string estado { get; set; }

        //Informacion sobre candidatos 
        [JsonProperty("candidatos")]
        public IEnumerable<DtoCandidato> candidatos { get; set; }

    }
}
