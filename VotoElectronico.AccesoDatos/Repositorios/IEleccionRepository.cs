
using System.Collections.Generic;
using VotoElectronico.Entidades.Pk.PkProcesoElectoral;
using VotoElectronicoExtensiones.Configuraciones;
using VotoElectronicoExtensiones.EntityFrameworkRepository;

namespace EcVotoElectronico.Repositorios
{
    public interface IEleccionRepository : IEntityFrameworkRepositoryVotoElectronico
    {
        IEnumerable<Pe02_Eleccion> FiltrarEleccionPorNombre(ISpecification<Pe02_Eleccion> specification);
    }
}
