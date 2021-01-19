using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VotoElectronico.Entidades.Shell;

namespace VotoElectronico.Entidades.Pk.PkSeguridad
{
    public class Sg04_UsuarioCargo : Auditoria
    {

        public long Id { get; set; }

        #region relaciones
        public long CargoId { get; set; }
        public virtual Sg03_Cargo Cargo { get; set; }
        public long UsuarioId { get; set; }
        public virtual Sg01_Usuario Usuario { get; set; }
        #endregion

    }
}
