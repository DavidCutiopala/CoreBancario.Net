using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace VotoElectronico.Generico
{
    public class DtoProcesoElectoral
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        
        [JsonProperty("nombreProcesoElectoral")]
        public string nombreProcesoElectoral { get; set; }

        [JsonProperty("fechaInicio")]
        public DateTime fechaInicio { get; set; }

        [JsonProperty("fechaFin")]
        public DateTime fechaFin { get; set; }


        [JsonProperty("eleccionId")]
        public long eleccionId { get; set; }

        [JsonProperty("nombreEleccion")]
        public string nombreEleccion { get; set; }
        [JsonProperty("usuarioCreacion")]
        public string usuarioCreacion { get; set; }

        [JsonProperty("usuarioModificacion")]
        public string usuarioModificacion { get; set; }

        [JsonProperty("estado")]
        public string estado { get; set; }

        [JsonProperty("votoRealizado")]
        public bool votoRealizado { get; set; }

        // Campos adicionales al DTO para obtener mayor informacion.

        [JsonProperty("tipoEleccion")]
        public string tipoEleccion { get; set; }


    }
}
