
using System.Collections.Generic;
using VotoElectronico.Entidades.Pk.PkSeguridad;
using VotoElectronicoExtensiones.Configuraciones;
using VotoElectronicoExtensiones.EntityFrameworkRepository;

namespace EcVotoElectronico.Repositorios
{
    public interface ICargoRepository : IEntityFrameworkRepositoryVotoElectronico
    {
        IEnumerable<Sg03_Cargo> FiltrarCargoPorNombre(ISpecification<Sg03_Cargo> specification);
    }
}
