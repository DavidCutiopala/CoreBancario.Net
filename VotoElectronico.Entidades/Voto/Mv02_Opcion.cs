using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VotoElectronico.Entidades.Pk.PkProcesoElectoral;
using VotoElectronico.Entidades.Shell;

namespace VotoElectronico.Entidades
{
    public class Mv02_Opcion : Auditoria
    {

        public long Id { get; set; }

        public long EscanioId { get; set; }
        #region relaciones  

        public long ListaId { get; set; }
        public virtual Pe05_Lista Lista { get; set; }
        public long? CandidatoId { get; set; }
        public virtual Pe06_Candidato Candidato {get; set;}
        public long VotoId { get; set; }
        public virtual Mv01_Voto   Voto {get; set;}

        #endregion

    }
}
