
using System.Collections.Generic;
using VotoElectronico.Entidades;
using VotoElectronico.Entidades.Pk.PkMesaIdentificacion;
using VotoElectronicoExtensiones.Configuraciones;
using VotoElectronicoExtensiones.EntityFrameworkRepository;

namespace EcVotoElectronico.Repositorios
{
    public interface IVotoRepository : IEntityFrameworkRepositoryVotoElectronico
    {
        IEnumerable<Mv01_Voto> FiltrarVoto(ISpecification<Mv01_Voto> specification);
    }
}
