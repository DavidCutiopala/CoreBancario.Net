

using Castle.MicroKernel.SubSystems.Conversion;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VotoElectronico.Entidades.Shell;

namespace VotoElectronico.Entidades.Pk.PkProcesoElectoral
{
    public class CB02_Cuenta
    {
        public CB02_Cuenta()
        {
            //candidatos = new HashSet<Pe06_Candidato>();
            //Opciones = new HashSet<Mv02_Opcion>();
            movimientos = new HashSet<CB03_Movimiento>();
        }
        public long Id { get; set; }
        //Cuenta Codigo.


       // [Column(TypeName = "decimal(18, 2)")]
        //[Required(ErrorMessage = "Nombre requerido")]
        public decimal Saldo { get; set; }


        //[Column(TypeName = "VARCHAR")]
        //[StringLength(50)]
        //[Required(ErrorMessage = "Apellido requerido")]
        public DateTime FechaCreacion { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(10)]
        public string NumeroCuenta { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(100)]
        public string TipoCuenta { get; set; }

       
        
        #region
        //public long ProcesoElectoralId { get; set; }
        //public virtual Pe01_ProcesoElectoral ProcesoElectoral { get; set; } 

        //public virtual ICollection<Pe06_Candidato> candidatos { get; set; }
        //public virtual ICollection<Mv02_Opcion> Opciones { get; set; }
        public virtual ICollection<CB03_Movimiento> movimientos { get; set; }

        public long ClienteId { get; set; }
        public virtual CB01_Cliente Cliente { get; set; }
        #endregion
    }
}
