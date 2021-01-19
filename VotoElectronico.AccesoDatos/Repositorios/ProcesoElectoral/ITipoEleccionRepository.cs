
using System.Collections.Generic;
using VotoElectronico.Entidades.Pk.PkProcesoElectoral;
using VotoElectronicoExtensiones.Configuraciones;
using VotoElectronicoExtensiones.EntityFrameworkRepository;

namespace EcVotoElectronico.Repositorios
{
    public interface ITipoEleccionRepository : IEntityFrameworkRepositoryVotoElectronico
    {
        IEnumerable<Pe03_TipoEleccion> FiltrarTipoEleccion(ISpecification<Pe03_TipoEleccion> specification);
    }
}
