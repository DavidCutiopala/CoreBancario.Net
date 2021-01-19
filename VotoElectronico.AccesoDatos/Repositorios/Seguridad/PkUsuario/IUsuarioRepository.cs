
using System.Collections.Generic;
using VotoElectronico.Entidades.Pk.PkSeguridad;
using VotoElectronicoExtensiones.Configuraciones;
using VotoElectronicoExtensiones.EntityFrameworkRepository;

namespace EcVotoElectronico.Repositorios
{
    public interface IUsuarioRepository : IEntityFrameworkRepositoryVotoElectronico
    {
        IEnumerable<Sg01_Usuario> FiltrarUsuario(ISpecification<Sg01_Usuario> specification);
    }
}
