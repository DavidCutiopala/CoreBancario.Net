
using System.Collections.Generic;
using VotoElectronico.Entidades.Pk.PkProcesoElectoral;
using VotoElectronicoExtensiones.Configuraciones;
using VotoElectronicoExtensiones.EntityFrameworkRepository;

namespace EcVotoElectronico.Repositorios
{
    public interface ITipoEleccionRepository : IEntityFrameworkRepositoryVotoElectronico
    {
        //IEnumerable<Pe01_ProcesoElectoral> FiltrarCargoPorNombre(ISpecification<Pe01_ProcesoElectoral> specification);
    }
}
