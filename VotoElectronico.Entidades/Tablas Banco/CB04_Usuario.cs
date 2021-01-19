

using Castle.MicroKernel.SubSystems.Conversion;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VotoElectronico.Entidades.Shell;

namespace VotoElectronico.Entidades.Pk.PkProcesoElectoral
{
    public class CB04_Usuario
    {
        public CB04_Usuario()
        {
            
        }
        public long Id { get; set; }


        [Column(TypeName = "VARCHAR")]
        [StringLength(100)]
        //[Required(ErrorMessage = "Nombre requerido")]
        public string Usuario { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(100)]
        //[Required(ErrorMessage = "Nombre requerido")]
        public string Password { get; set; }


        #region
       // [Key, ForeignKey("Cliente")]
        public long ClienteId { get; set; }

        public virtual CB01_Cliente Cliente { get; set; }

        //public long ProcesoElectoralId { get; set; }
        //public virtual Pe01_ProcesoElectoral ProcesoElectoral { get; set; } 

        //public virtual ICollection<Pe06_Candidato> candidatos { get; set; }
        //public virtual ICollection<Mv02_Opcion> Opciones { get; set; }
        //public virtual ICollection<CB02_Cuenta> cuentas { get; set; }
        #endregion
    }
}
