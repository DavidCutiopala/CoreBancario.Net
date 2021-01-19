
using System.Collections.Generic;
using System.Linq;
using VotoElectronico.Entidades.Pk.PkSeguridad;
using VotoElectronicoExtensiones.Configuraciones;
using VotoElectronicoExtensiones.EntityFrameworkRepository;

namespace EcVotoElectronico.Repositorios
{
    public class RecursoRepository : EntityFrameworkReadOnlyRepository<VotoDbContext>, IRecursoRepository
    {
        public RecursoRepository(VotoDbContext Context) : base(Context)
        {


        }

        public IEnumerable<Sg06_Recurso> FiltrarRecursoPorNombre(ISpecification<Sg06_Recurso> specification)
          =>   Context.Sg06_Recursos.Where(specification.SatisfiedBy());
        


        
    }
}
