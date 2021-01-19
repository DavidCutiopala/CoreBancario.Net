using VotoElectronico.Entidades.Pk.PkProcesoElectoral;
using VotoElectronico.Entidades.Pk.PkSeguridad;
using VotoElectronico.Entidades.Shell;

namespace VotoElectronico.Entidades.Pk.PkMesaIdentificacion
{
    public class Mi01_PadronVotacion : Auditoria
    {

        public long Id { get; set; }
        #region relaciones
        public long ProcesoElectoralId { get; set; }
        
        public bool VotoRealizado { get; set; }
        public virtual Pe01_ProcesoElectoral ProcesoElectoral { get; set; }
        public long UsuarioId { get; set; }
        public virtual Sg01_Usuario Usuario { get; set; }
        #endregion
        
    }
}
