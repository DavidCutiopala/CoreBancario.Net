using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VotoElectronico.Entidades.Pk.PkProcesoElectoral;
using VotoElectronico.Entidades.Shell;

namespace VotoElectronico.Entidades.Pk.PkSeguridad
{
    public class Sg02_Persona : Auditoria
    {
        public Sg02_Persona()
        {
            Usuarios = new HashSet<Sg01_Usuario>();
            Candidatos = new HashSet<Pe06_Candidato>();
        }

        public long Id { get; set; }
        [Required(ErrorMessage = "Nombre Uno requerido")]
        [StringLength(100)]
        public string NombreUno { get; set; }
        [StringLength(100)]
        public string NombreDos { get; set; }
        [Required(ErrorMessage = "Apellido Uno requerido")]
        [StringLength(100)]
        public string ApellidoUno { get; set; }
        [StringLength(100)]
        public string ApellidoDos { get; set; }

        [Required(ErrorMessage = "Identificación requerida")]
        [StringLength(20)]
        public string Identificacion { get; set; }

        [Required(ErrorMessage = "Teléfono requerido")]
        [StringLength(20)]
        public string Telefono { get; set; }

        [StringLength(20)]
        public string Telefono2 { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(60)]
        [Required(ErrorMessage = "Email requerido")]
        public string Email { get; set; }

        #region relaciones
        public virtual ICollection<Sg01_Usuario> Usuarios { get; set; }

        public virtual ICollection<Pe06_Candidato> Candidatos { get; set; }
        #endregion

    }
}
