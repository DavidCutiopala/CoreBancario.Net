using Newtonsoft.Json;
using System.Collections.Generic;

namespace VotoElectronico.Generico
{
    public class DtoRecurso
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string nombreRecurso { get; set; }
        [JsonProperty("tooltip")]
        public string ToolTip { get; set; }

        [JsonProperty("estado")]
        public string Estado { get; set; }
        [JsonProperty("type")]
        public string Tipo { get; set; }
        [JsonProperty("state")]
        public string RutaMenu { get; set; }
        [JsonProperty("icon")]
        public string Icono { get; set; }
        [JsonProperty("sub")]
        public IEnumerable<DtoRecurso> RecursosHijo { get; set; }

    }
}
