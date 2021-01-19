
using System.Collections.Generic;
using VotoElectronico.Entidades.Pk.PkMesaIdentificacion;
using VotoElectronico.Entidades.Pk.PkProcesoElectoral;
using VotoElectronicoExtensiones.Configuraciones;
using VotoElectronicoExtensiones.EntityFrameworkRepository;

namespace EcVotoElectronico.Repositorios
{
    public interface IUsuarioCoreBRepository : IEntityFrameworkRepositoryVotoElectronico
    {
        IEnumerable<CB04_Usuario> FiltarUsuarioCB(ISpecification<CB04_Usuario> specification);
    }
}
