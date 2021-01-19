
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using VotoElectronico.Entidades.Shell;

namespace VotoElectronico.Entidades.Pk.PkProcesoElectoral
{
    public class Pe02_Eleccion : Auditoria
    {
        public Pe02_Eleccion()
        {
            ProcesosElectorales = new HashSet<Pe01_ProcesoElectoral>();
            Escanios = new HashSet<Pe04_Escanio>();
        }
        public long Id { get; set; }
        [Required (ErrorMessage = "Nombre Elección requerido")]
        public string NombreEleccion { get; set; }
        public int CantidadEscanios { get; set; }
        public string EtiquetaEscanios { get; set; }

        #region relaciones
        public virtual ICollection<Pe01_ProcesoElectoral> ProcesosElectorales { get; set; }
        public virtual ICollection<Pe04_Escanio> Escanios { get; set; }
        public long TipoEleccionId { get; set; }
        public virtual Pe03_TipoEleccion TipoEleccion { get; set; }

        #endregion

    }
}
