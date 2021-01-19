

using Castle.MicroKernel.SubSystems.Conversion;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VotoElectronico.Entidades.Shell;

namespace VotoElectronico.Entidades.Pk.PkProcesoElectoral
{
    public class CB03_Movimiento
    {
        public CB03_Movimiento()
        {
            //candidatos = new HashSet<Pe06_Candidato>();
            //Opciones = new HashSet<Mv02_Opcion>();
        }
        public long Id { get; set; }

        public DateTime FechaCreacion { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(64)]
        public string TipoMovimiento { get; set; }


        //[Column(TypeName = "decimal(12, 2)")]
        public decimal Valor { get; set; }

        //[Column(TypeName = "decimal(12, 2)")]
        public decimal SaldoFinal { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(10)]
        public string CuentaOrigen { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(10)]
        public string CuentaDestino { get; set; }

        #region
        //public long ProcesoElectoralId { get; set; }
        //public virtual Pe01_ProcesoElectoral ProcesoElectoral { get; set; } 

        //public virtual ICollection<Pe06_Candidato> candidatos { get; set; }
        //public virtual ICollection<Mv02_Opcion> Opciones { get; set; }

        public long CuentaId { get; set; }
        public virtual CB02_Cuenta Cuenta { get; set; }
        #endregion
    }
}
