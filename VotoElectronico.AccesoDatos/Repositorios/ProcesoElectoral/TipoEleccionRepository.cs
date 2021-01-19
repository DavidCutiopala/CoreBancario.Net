
using System.Collections.Generic;
using System.Linq;
using VotoElectronico.Entidades.Pk.PkProcesoElectoral;
using VotoElectronico.Entidades.Pk.PkSeguridad;
using VotoElectronicoExtensiones.Configuraciones;
using VotoElectronicoExtensiones.EntityFrameworkRepository;

namespace EcVotoElectronico.Repositorios
{
    public class TipoEleccionRepository : EntityFrameworkReadOnlyRepository<VotoDbContext>, ITipoEleccionRepository
    {
        public TipoEleccionRepository(VotoDbContext Context) : base(Context)
        {


        }

        public IEnumerable<Pe03_TipoEleccion> FiltrarTipoEleccion(ISpecification<Pe03_TipoEleccion> specification)
        => Context.Pe03_TipoElecciones.Where(specification.SatisfiedBy());
    }
}
