using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VotoElectronico.Entidades.Pk.PkMesaIdentificacion;
using VotoElectronico.Entidades.Shell;

namespace VotoElectronico.Entidades.Pk.PkSeguridad
{
    public class Sg01_Usuario : Auditoria
    {

         public Sg01_Usuario()
        {
            UsuariosCargo = new HashSet<Sg04_UsuarioCargo>();
            UsuariosRol = new HashSet<Sg08_UsuarioRol>();
            PadronVotacion = new HashSet<Mi01_PadronVotacion>();
        }
        public long Id { get; set; }

        public bool EnvioEmailActivacion { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(100)]
        public string Clave { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(100)]
        [Required(ErrorMessage = "Nombre usuario requerido")]
        public string NombreUsuario { get; set; }

        public string Token { get; set; }
        public string TokenCambioClave { get; set; }
        public bool InicioSesion { get; set; }


        #region relaciones
        public long PersonaId { get; set; }
        public virtual Sg02_Persona Persona { get; set; }
        public virtual ICollection<Sg04_UsuarioCargo> UsuariosCargo { get; set; }
        public virtual ICollection<Sg08_UsuarioRol> UsuariosRol { get; set; }
        public virtual ICollection<Mi01_PadronVotacion> PadronVotacion { get; set; }
        #endregion

    }
}
