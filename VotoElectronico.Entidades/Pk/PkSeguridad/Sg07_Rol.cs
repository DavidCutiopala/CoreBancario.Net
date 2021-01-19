using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using VotoElectronico.Entidades.Shell;

namespace VotoElectronico.Entidades.Pk.PkSeguridad
{
    public class Sg07_Rol : Auditoria
    {

        public Sg07_Rol()
        {
            UsuariosRol = new HashSet<Sg08_UsuarioRol>();
            Permisos = new HashSet<Sg05_Permiso>();
        }
        public long Id { get; set; }
        [Required(ErrorMessage = "Nombre Rol Requerido")]
        public string NombreRol { get; set; }

        #region relaciones
        public virtual ICollection<Sg08_UsuarioRol> UsuariosRol { get; set; }
        public virtual ICollection<Sg05_Permiso> Permisos { get; set; }
        #endregion

    }
}
