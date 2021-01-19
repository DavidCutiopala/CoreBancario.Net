
using System.Collections.Generic;
using System.Linq;
using VotoElectronico.Entidades.Pk.PkProcesoElectoral;
using VotoElectronico.Entidades.Pk.PkSeguridad;
using VotoElectronicoExtensiones.Configuraciones;
using VotoElectronicoExtensiones.EntityFrameworkRepository;

namespace EcVotoElectronico.Repositorios
{
    public class ListaRepository : EntityFrameworkReadOnlyRepository<VotoDbContext>, IListaRepository
    {
        public ListaRepository(VotoDbContext Context) : base(Context)
        {


        }

        public IEnumerable<Pe05_Lista> FiltrarListaPorNombre(ISpecification<Pe05_Lista> specification)
          => Context.Pe05_Listas.Where(specification.SatisfiedBy());

        public IEnumerable<Pe05_Lista> FiltrarListaPorProcesoElectoral(ISpecification<Pe05_Lista> specification)
        => Context.Pe05_Listas.Where(specification.SatisfiedBy());
    }
}
