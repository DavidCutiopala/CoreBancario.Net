
using System.Collections.Generic;
using VotoElectronico.Entidades.Pk.PkSeguridad;
using VotoElectronicoExtensiones.Configuraciones;
using VotoElectronicoExtensiones.EntityFrameworkRepository;

namespace EcVotoElectronico.Repositorios
{
    public interface IUsuarioCargoRepository : IEntityFrameworkRepositoryVotoElectronico
    {
        //IEnumerable<Sg04_UsuarioCargo> FiltrarCargoPorNombre(ISpecification<Sg04_UsuarioCargo> specification);
    }
}
