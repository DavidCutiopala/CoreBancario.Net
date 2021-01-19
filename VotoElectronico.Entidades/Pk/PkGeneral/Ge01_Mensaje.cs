
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VotoElectronico.Entidades.Shell;

namespace VotoElectronico.Entidades
{
    public class Ge01_Mensaje : Auditoria
    {


        [Column(TypeName = "VARCHAR")]
        [StringLength(200)]
        [Required(ErrorMessage = "Codigo aux de mensaje requerido")]
        [Key]
        public string CodigoMensaje { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(200)]
        public string Titulo { get; set; }

        [Column(TypeName = "VARCHAR")]
        public string Descripcion { get; set; }



        #region relaciones

        public string TipoMensajeId { get; set; }
        public virtual Ge02_TipoMensaje TipoMensaje { get; set; }

        #endregion

    }
}
