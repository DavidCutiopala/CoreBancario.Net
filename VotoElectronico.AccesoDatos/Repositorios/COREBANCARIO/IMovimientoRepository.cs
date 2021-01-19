
using System.Collections.Generic;
using VotoElectronico.Entidades.Pk.PkMesaIdentificacion;
using VotoElectronico.Entidades.Pk.PkProcesoElectoral;
using VotoElectronicoExtensiones.Configuraciones;
using VotoElectronicoExtensiones.EntityFrameworkRepository;

namespace EcVotoElectronico.Repositorios
{
    public interface IMovimientoRepository : IEntityFrameworkRepositoryVotoElectronico
    {
        IEnumerable<CB03_Movimiento> FiltrarMovimientos(ISpecification<CB03_Movimiento> specification);
    }
}
