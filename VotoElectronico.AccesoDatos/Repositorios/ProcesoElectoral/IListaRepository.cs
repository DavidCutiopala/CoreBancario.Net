
using System.Collections.Generic;
using VotoElectronico.Entidades.Pk.PkProcesoElectoral;
using VotoElectronicoExtensiones.Configuraciones;
using VotoElectronicoExtensiones.EntityFrameworkRepository;

namespace EcVotoElectronico.Repositorios
{
    public interface IListaRepository : IEntityFrameworkRepositoryVotoElectronico
    {
        IEnumerable<Pe05_Lista> FiltrarListaPorNombre(ISpecification<Pe05_Lista> specification);
        IEnumerable<Pe05_Lista> FiltrarListaPorProcesoElectoral(ISpecification<Pe05_Lista> specification);
    }
}
