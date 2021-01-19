
using System.Collections.Generic;
using VotoElectronico.Entidades.Pk.PkSeguridad;
using VotoElectronicoExtensiones.Configuraciones;
using VotoElectronicoExtensiones.EntityFrameworkRepository;

namespace EcVotoElectronico.Repositorios
{
    public interface IPermisoRepository : IEntityFrameworkRepositoryVotoElectronico
    {
        IEnumerable<Sg05_Permiso> FiltrarCargosMedianteEspecificacion(ISpecification<Sg05_Permiso> specification);
    }
}
