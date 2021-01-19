
using System.Collections.Generic;
using VotoElectronico.Entidades.Pk.PkSeguridad;
using VotoElectronicoExtensiones.Configuraciones;
using VotoElectronicoExtensiones.EntityFrameworkRepository;

namespace EcVotoElectronico.Repositorios
{
    public interface IRecursoRepository : IEntityFrameworkRepositoryVotoElectronico
    {
        IEnumerable<Sg06_Recurso> FiltrarRecursoPorNombre(ISpecification<Sg06_Recurso> specification);
    }
}
