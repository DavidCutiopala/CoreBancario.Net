using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VotoElectronico.Entidades.Shell;

namespace VotoElectronico.Entidades.Pk.PkSeguridad
{
    public class Sg06_Recurso : Auditoria
    {

        public Sg06_Recurso()
        {
            Permisos = new HashSet<Sg05_Permiso>();
        }


        public long Id { get; set; }

        [Required(ErrorMessage = "Codigo del Recurso requerido")]
        [StringLength(20)]
        public string CodigoRecurso { get; set; }

        [StringLength(50)]
        public string NombreRecurso { get; set; }

        [StringLength(100)]
        public string DescripcionRecurso { get; set; }

        [StringLength(200)]
        public string ValorRecursoString { get; set; }

        [StringLength(100)]
        public string ValorRecursoBoolean { get; set; }
        [StringLength(100)]
        public string RutaMenu { get; set; }
        [StringLength(40)]
        public string Icono { get; set; }
        public int Orden { get; set; }

        #region relaciones


        public virtual ICollection<Sg05_Permiso> Permisos { get; set; }
        public long? RecursoId { get; set; }
        public virtual Sg06_Recurso Recurso { get; set; }
        #endregion

    }
}
