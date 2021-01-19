
using System.Collections.Generic;
using System.Linq;
using VotoElectronico.Entidades.Pk.PkMesaIdentificacion;
using VotoElectronicoExtensiones.Configuraciones;
using VotoElectronicoExtensiones.EntityFrameworkRepository;

namespace EcVotoElectronico.Repositorios
{
    public class PadronVotacionRepository : EntityFrameworkReadOnlyRepository<VotoDbContext>, IPadronVotacionRepository
    {
        public PadronVotacionRepository(VotoDbContext Context) : base(Context)
        {


        }

       public IEnumerable<Mi01_PadronVotacion> FiltrarPadronVotacion(ISpecification<Mi01_PadronVotacion> specification)
          => Context.Mi01_PadronVotaciones.Where(specification.SatisfiedBy());




    }
}
