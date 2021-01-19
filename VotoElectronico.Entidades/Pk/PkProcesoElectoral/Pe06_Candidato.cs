

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using VotoElectronico.Entidades.Pk.PkSeguridad;
using VotoElectronico.Entidades.Shell;

namespace VotoElectronico.Entidades.Pk.PkProcesoElectoral
{
    public class Pe06_Candidato: Auditoria
    {
        public Pe06_Candidato()
        {
            Opciones = new HashSet<Mv02_Opcion>();
        }
        public long Id { get; set; }
        
        [StringLength(60)]
        public string Foto { get; set; }

        #region Relaciones
        public long ListaId { get; set; }
        public virtual Pe05_Lista Lista { get; set; }
        public long PersonaId { get; set; }
        public virtual Sg02_Persona Persona { get; set; }
        public long EscanioId { get; set; }
        public virtual Pe04_Escanio Escanio { get; set; }
        public virtual ICollection<Mv02_Opcion> Opciones { get; set; }
        #endregion

    }
}
