

using System.Collections.Generic;
using VotoElectronico.Entidades.Pk.PkProcesoElectoral;
using VotoElectronico.Entidades.Shell;

namespace VotoElectronico.Entidades
{
    public class Mv01_Voto: Auditoria
    {
        public Mv01_Voto()
        {
            Opciones = new HashSet<Mv02_Opcion>();
        }

        public long Id { get; set; }
        public string Mascara { get; set; }
        public long ProcesoElectoralId { get; set; }
        public virtual Pe01_ProcesoElectoral ProcesoElectoral { get; set; }

        #region relaciones
        public virtual ICollection<Mv02_Opcion> Opciones { get; set; }
        #endregion
    }
}
