using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using VotoElectronico.Generico.Configs;

namespace VotoElectronico.Generico
{
    public class DtoImportacionPadron
    {
        [Column(1)]
        [Required]
        [JsonProperty("NombreUno")]
        public string NombreUno { get; set; }

        [Column(2)]
        [JsonProperty("NombreDos")]
        public string NombreDos { get; set; }

        [Column(3)]
        [Required]
        [JsonProperty("ApellidoUno")]
        public string ApellidoUno { get; set; }
        [Column(4)]
        [JsonProperty("ApellidoDos")]
        public string ApellidoDos { get; set; }

        [Column(5)]
        [Required]
        [JsonProperty("Identificación")]
        public string Identificacion { get; set; }
        [Column(6)]
        [Required]
        [JsonProperty("Email")]
        public string Email { get; set; }

        [Column(7)]
        [Required]
        [JsonProperty("Cargo")]
        public string Cargo { get; set; }

        [Column(8)]
        [Required]
        [JsonProperty("Telefono")]
        public string Telefono { get; set; }
        public long ProcesoId { get; set; }
        public long PadronId { get; set; }
        public long CargoId { get; set; }
        public string MensajeError { get; set; }
        public string MensajeDosError { get; set; }
        public bool TieneError { get; set; } = false;
    }
}
