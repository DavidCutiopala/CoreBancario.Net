
using System.Collections.Generic;
using VotoElectronico.Entidades.Pk.PkProcesoElectoral;
using VotoElectronicoExtensiones.Configuraciones;
using VotoElectronicoExtensiones.EntityFrameworkRepository;

namespace EcVotoElectronico.Repositorios
{
    public interface IEscanioRepository : IEntityFrameworkRepositoryVotoElectronico
    {
        //IEnumerable<Pe04_Escanio> FiltrarEscanioPorNombre(ISpecification<Pe04_Escanio> specification);
    }
}
