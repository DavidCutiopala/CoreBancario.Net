
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using VotoElectronico.Entidades.Shell;

namespace VotoElectronico.Entidades.Pk.PkProcesoElectoral
{
    public class Pe03_TipoEleccion : Auditoria
    {
        public Pe03_TipoEleccion()
        {
            Elecciones = new HashSet<Pe02_Eleccion>();
        }
        public long Id { get; set; }
        [Required (ErrorMessage = "Nombre Elección requerido")]
        public string NombreTipoEleccion { get; set; }
        #region relaciones
        public virtual ICollection<Pe02_Eleccion> Elecciones { get; set; }
         
        #endregion

    }
}
