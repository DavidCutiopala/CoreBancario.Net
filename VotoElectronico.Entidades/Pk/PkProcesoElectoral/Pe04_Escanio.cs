
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VotoElectronico.Entidades.Shell;

namespace VotoElectronico.Entidades.Pk.PkProcesoElectoral
{
    public class Pe04_Escanio : Auditoria
    {
        public Pe04_Escanio()
        {
            Candidatos = new HashSet<Pe06_Candidato>();
        }
        public long Id { get; set; }
        [Required (ErrorMessage = "Nombre Escaño requerido")]
        [StringLength(maximumLength: 100)]
        [Index("IX_NombreEleccion", IsUnique = true, Order = 1)]
        public string NombreEscanio { get; set; }
        public int Orden { get; set; }

        #region relaciones
        [Index("IX_NombreEleccion", IsUnique = true, Order = 2)]
        public long EleccionId { get; set; }
        public virtual Pe02_Eleccion Eleccion { get; set; }

        public virtual ICollection<Pe06_Candidato> Candidatos { get; set; }
        #endregion

    }
}
