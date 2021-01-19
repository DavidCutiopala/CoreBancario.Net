
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VotoElectronico.Entidades.Shell;

namespace VotoElectronico.Entidades
{
    public class Ge02_TipoMensaje: Auditoria
    {
        public Ge02_TipoMensaje()
        {
            Mensajes = new HashSet<Ge01_Mensaje>();
        }
       

        [Column(TypeName = "VARCHAR")]
        [StringLength(100)]
        [Required(ErrorMessage = "TipoMensajeId requerido")]
        [Key]
        public string TipoMensajeId { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(100)]
        public string NombreMensaje { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(200)]
        [Required(ErrorMessage = "Descripcion de mensaje requerido")]
        public string Descripcion { get; set; }


        #region relaciones
        public virtual ICollection<Ge01_Mensaje> Mensajes { get; set; }

        #endregion

    }
}
