
using System.Collections.Generic;
using VotoElectronico.Entidades.Pk.PkSeguridad;
using VotoElectronicoExtensiones.Configuraciones;
using VotoElectronicoExtensiones.EntityFrameworkRepository;

namespace EcVotoElectronico.Repositorios
{
    public interface IRolRepository : IEntityFrameworkRepositoryVotoElectronico
    {
        IEnumerable<Sg07_Rol> FiltrarRoles(ISpecification<Sg07_Rol> specification);
    }
}
