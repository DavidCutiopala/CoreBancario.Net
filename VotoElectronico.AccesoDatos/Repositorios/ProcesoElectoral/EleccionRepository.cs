
using System.Collections.Generic;
using System.Linq;
using VotoElectronico.Entidades.Pk.PkProcesoElectoral;
using VotoElectronico.Entidades.Pk.PkSeguridad;
using VotoElectronicoExtensiones.Configuraciones;
using VotoElectronicoExtensiones.EntityFrameworkRepository;

namespace EcVotoElectronico.Repositorios
{
    public class EleccionRepository : EntityFrameworkReadOnlyRepository<VotoDbContext>, IEleccionRepository
    {
        public EleccionRepository(VotoDbContext Context) : base(Context)
        {


        }

        public IEnumerable<Pe02_Eleccion> FiltrarEleccionPorNombre(ISpecification<Pe02_Eleccion> specification)
          => Context.Pe02_Elecciones.Where(specification.SatisfiedBy());




    }
}
