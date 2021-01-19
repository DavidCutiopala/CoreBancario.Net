
using System.Collections.Generic;
using VotoElectronico.Entidades.Pk.PkMesaIdentificacion;
using VotoElectronico.Entidades.Pk.PkProcesoElectoral;
using VotoElectronicoExtensiones.Configuraciones;
using VotoElectronicoExtensiones.EntityFrameworkRepository;

namespace EcVotoElectronico.Repositorios
{
    public interface IClienteRepository : IEntityFrameworkRepositoryVotoElectronico
    {
        IEnumerable<CB01_Cliente> FiltrarClientesCB(ISpecification<CB01_Cliente> specification);
    }
}
