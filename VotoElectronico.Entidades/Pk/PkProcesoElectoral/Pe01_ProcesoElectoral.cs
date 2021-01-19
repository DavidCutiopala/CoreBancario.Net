using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VotoElectronico.Entidades.Pk.PkMesaIdentificacion;
using VotoElectronico.Entidades.Shell;

namespace VotoElectronico.Entidades.Pk.PkProcesoElectoral
{
    public class Pe01_ProcesoElectoral : Auditoria
    {
        public Pe01_ProcesoElectoral()
        {
            PadronesVotacion = new HashSet<Mi01_PadronVotacion>();
            Listas = new HashSet<Pe05_Lista>();
            Votos = new HashSet<Mv01_Voto>();
        }

        public long Id { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(200)]
        [Required(ErrorMessage = "Nombre del proceso electoral requerido")]
        public string NombreProcesoElectoral { get; set; }

        [Required(ErrorMessage = "Fecha de inicio requerida")]
        public DateTime FechaInicio { get; set; }

        [Required(ErrorMessage = "Fecha de fin requerida")]
        public DateTime FechaFin { get; set; }


        #region relaciones
        public long EleccionId { get; set; }
        public virtual Pe02_Eleccion Eleccion { get; set; }
        public virtual ICollection<Mi01_PadronVotacion> PadronesVotacion { get; set; }
        public virtual ICollection<Pe05_Lista> Listas { get; set; }
        public virtual ICollection<Mv01_Voto> Votos { get; set; }
        #endregion

    }
}
