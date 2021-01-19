

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using VotoElectronico.Entidades.Shell;

namespace VotoElectronico.Entidades.Pk.PkProcesoElectoral
{
    public class Pe05_Lista: Auditoria
    {
        public Pe05_Lista()
        {
            candidatos = new HashSet<Pe06_Candidato>();
            Opciones = new HashSet<Mv02_Opcion>();
        }
        public long Id { get; set; }
        [Required (ErrorMessage ="Nombre Lista Requerido")]
        public string NombreLista { get; set; }

        
        [StringLength(60)]
        public string Logo { get; set; }

        [StringLength(200)]
        public string Eslogan { get; set; }

        #region
        public long ProcesoElectoralId { get; set; }
        public virtual Pe01_ProcesoElectoral ProcesoElectoral { get; set; } 

        public virtual ICollection<Pe06_Candidato> candidatos { get; set; }
        public virtual ICollection<Mv02_Opcion> Opciones { get; set; }
        #endregion
    }
}
