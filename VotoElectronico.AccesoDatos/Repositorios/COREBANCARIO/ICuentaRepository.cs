
using System.Collections.Generic;
using VotoElectronico.Entidades.Pk.PkMesaIdentificacion;
using VotoElectronico.Entidades.Pk.PkProcesoElectoral;
using VotoElectronicoExtensiones.Configuraciones;
using VotoElectronicoExtensiones.EntityFrameworkRepository;

namespace EcVotoElectronico.Repositorios
{
    public interface ICuentasRepository : IEntityFrameworkRepositoryVotoElectronico
    {
        IEnumerable<CB02_Cuenta> FiltrarCuentas(ISpecification<CB02_Cuenta> specification);
    }
}
