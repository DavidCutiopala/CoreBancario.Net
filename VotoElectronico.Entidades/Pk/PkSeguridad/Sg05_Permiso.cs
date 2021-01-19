using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VotoElectronico.Entidades.Shell;

namespace VotoElectronico.Entidades.Pk.PkSeguridad
{
    public class Sg05_Permiso : Auditoria
    {

        public long Id { get; set; }

        #region relaciones


        //public long CargoId { get; set; }
        //public virtual Sg03_Cargo Cargo { get; set; }
        public long RolId { get; set; }
        public virtual Sg07_Rol Rol { get; set; }
        public long RecursoId { get; set; }
        public virtual Sg06_Recurso Recurso { get; set; }
        #endregion

    }
}
