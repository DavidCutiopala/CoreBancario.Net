
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

        //public IEnumerable<Pe02_Eleccion> FiltrarEleccionPorNombre(ISpecification<Pe02_Eleccion> specification)
        //  => Context.Pe02_Eleccion.Where(specification.SatisfiedBy());




    }
}
