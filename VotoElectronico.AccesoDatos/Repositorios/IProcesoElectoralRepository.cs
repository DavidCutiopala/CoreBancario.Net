
using System.Collections.Generic;
using VotoElectronico.Entidades.Pk.PkProcesoElectoral;
using VotoElectronicoExtensiones.Configuraciones;
using VotoElectronicoExtensiones.EntityFrameworkRepository;

namespace EcVotoElectronico.Repositorios
{
    public interface IProcesoElectoralRepository : IEntityFrameworkRepositoryVotoElectronico
    {
        IEnumerable<Pe01_ProcesoElectoral> FiltrarProcesoElectoralPorNombre(ISpecification<Pe01_ProcesoElectoral> specification);
    }
}
