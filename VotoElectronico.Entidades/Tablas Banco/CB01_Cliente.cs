

using Castle.MicroKernel.SubSystems.Conversion;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VotoElectronico.Entidades.Shell;

namespace VotoElectronico.Entidades.Pk.PkProcesoElectoral
{
    public class CB01_Cliente
    {
        public CB01_Cliente()
        {
            cuentas = new HashSet<CB02_Cuenta>();
            //candidatos = new HashSet<Pe06_Candidato>();
            //Opciones = new HashSet<Mv02_Opcion>();
            usuarios = new HashSet<CB04_Usuario>();
        }
        public long Id { get; set; }


        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        //[Required(ErrorMessage = "Nombre requerido")]
        public string Nombre { get; set; }


        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        //[Required(ErrorMessage = "Apellido requerido")]
        public string Apellido { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(30)]
        //[Required(ErrorMessage = "Cedula requerido")]
        public string Cedula { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(100)]
        //[Required(ErrorMessage = "Cedula requerido")]
        public string Direccion { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(20)]
        //[Required(ErrorMessage = "Telefono requerido")]
        public string Telefono { get; set; }

        [StringLength(100)]
        public string Email { get; set; }
        #region
        //public long ProcesoElectoralId { get; set; }
        //public virtual Pe01_ProcesoElectoral ProcesoElectoral { get; set; } 

        //public virtual ICollection<Pe06_Candidato> candidatos { get; set; }
        //public virtual ICollection<Mv02_Opcion> Opciones { get; set; }
        public virtual ICollection<CB02_Cuenta> cuentas { get; set; }
        public virtual ICollection<CB04_Usuario> usuarios { get; set; }
        //public long UsuarioId { get; set; }
       // public virtual CB04_Usuario Usuario { get; set; }

       

        #endregion
    }
}
