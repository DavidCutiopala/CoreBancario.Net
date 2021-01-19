using VotoElectronico.Entidades.Shell;

namespace VotoElectronico.Entidades.Pk.PkSeguridad
{
    public class Sg08_UsuarioRol : Auditoria
    {

        public long Id { get; set; }

        #region relaciones
        public long RolId { get; set; }
        public virtual Sg07_Rol Rol { get; set; }
        public long UsuarioId { get; set; }
        public virtual Sg01_Usuario Usuario { get; set; }
        #endregion

    }
}
