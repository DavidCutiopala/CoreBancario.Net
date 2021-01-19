
using System.Collections.Generic;
using VotoElectronico.Entidades.Pk.PkProcesoElectoral;
using VotoElectronicoExtensiones.Configuraciones;
using VotoElectronicoExtensiones.EntityFrameworkRepository;

namespace EcVotoElectronico.Repositorios
{
    public interface IEscanioRepository : IEntityFrameworkRepositoryVotoElectronico
    {
        IEnumerable<Pe04_Escanio> FiltrarEscanios(ISpecification<Pe04_Escanio> specification);

       // IEnumerable<Pe04_Escanio> FiltrarEscaniosMedianteListaId(ISpecification<Pe04_Escanio> specification);
    }
}
