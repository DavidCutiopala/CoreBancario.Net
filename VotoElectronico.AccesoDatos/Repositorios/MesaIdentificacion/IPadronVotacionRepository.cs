
using System.Collections.Generic;
using VotoElectronico.Entidades.Pk.PkMesaIdentificacion;
using VotoElectronicoExtensiones.Configuraciones;
using VotoElectronicoExtensiones.EntityFrameworkRepository;

namespace EcVotoElectronico.Repositorios
{
    public interface IPadronVotacionRepository : IEntityFrameworkRepositoryVotoElectronico
    {
        IEnumerable<Mi01_PadronVotacion> FiltrarPadronVotacion(ISpecification<Mi01_PadronVotacion> specification);
    }
}
