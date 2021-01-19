
using System.Collections.Generic;
using System.Linq;
using VotoElectronico.Entidades.Pk.PkMesaIdentificacion;
using VotoElectronico.Entidades.Pk.PkProcesoElectoral;
using VotoElectronicoExtensiones.Configuraciones;
using VotoElectronicoExtensiones.EntityFrameworkRepository;

namespace EcVotoElectronico.Repositorios
{
    public class CuentaRepository: EntityFrameworkReadOnlyRepository<VotoDbContext>, ICuentasRepository
    {
        public CuentaRepository(VotoDbContext Context) : base(Context)
        {


        }

       public IEnumerable<CB02_Cuenta> FiltrarCuentas(ISpecification<CB02_Cuenta> specification)
          => Context.CB02_Cuentas.Where(specification.SatisfiedBy());




    }
}
