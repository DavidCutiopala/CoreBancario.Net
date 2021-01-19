using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace VotoElectronico.Entidades.Shell
{
    /**
     * Clase que lleva los atributos para auditoria de tablas
    */
    public class Auditoria
    {

        [StringLength(10)]
        [DefaultValue("ACTIVO")]
        public string Estado { get; set; }

        [DefaultValue("SISTEMA ELECTRONICO")]
        [StringLength(maximumLength: 80)]
        public string UsuarioCreacion { get; set; }

        public DateTime? FechaCreacion { get; set; }

        [DefaultValue("SISTEMA ELECTRONICO")]
        [StringLength(maximumLength: 80)]
        public string UsuarioModificacion { get; set; }


        public DateTime? FechaModificacion { get; set; }

    }
}
