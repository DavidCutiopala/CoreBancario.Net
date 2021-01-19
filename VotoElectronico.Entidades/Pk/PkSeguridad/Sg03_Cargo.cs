using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VotoElectronico.Entidades.Shell;

namespace VotoElectronico.Entidades.Pk.PkSeguridad
{
    public class Sg03_Cargo : Auditoria
    {

        public Sg03_Cargo()
        {
            UsuariosCargos = new HashSet<Sg04_UsuarioCargo>();
            //Permisos = new HashSet<Sg05_Permiso>();
        }
        public long Id { get; set; }
        [Required(ErrorMessage = "Nombre Cargo Requerido")]
        public string NombreCargo { get; set; }

        #region relaciones
        public virtual ICollection<Sg04_UsuarioCargo> UsuariosCargos { get; set; }
        //public virtual ICollection<Sg05_Permiso> Permisos { get; set; }
        #endregion

    }
}
