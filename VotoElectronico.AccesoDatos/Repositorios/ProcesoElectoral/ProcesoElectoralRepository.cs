
using System.Collections.Generic;
using System.Linq;
using VotoElectronico.Entidades.Pk.PkProcesoElectoral;
using VotoElectronico.Entidades.Pk.PkSeguridad;
using VotoElectronicoExtensiones.Configuraciones;
using VotoElectronicoExtensiones.EntityFrameworkRepository;

namespace EcVotoElectronico.Repositorios
{
    public class ProcesoElectoralRepository : EntityFrameworkReadOnlyRepository<VotoDbContext>, IProcesoElectoralRepository
    {
        public ProcesoElectoralRepository(VotoDbContext Context) : base(Context)
        {


        }

        public IEnumerable<Pe01_ProcesoElectoral> FiltrarProcesoElectoralEspecificacion(ISpecification<Pe01_ProcesoElectoral> specification)
          => Context.Pe01_ProcesoElectorales.Where(specification.SatisfiedBy());




    }
}
