
using System.Collections.Generic;
using System.Linq;
using VotoElectronico.Entidades.Pk.PkProcesoElectoral;
using VotoElectronico.Entidades.Pk.PkSeguridad;
using VotoElectronicoExtensiones.Configuraciones;
using VotoElectronicoExtensiones.EntityFrameworkRepository;

namespace EcVotoElectronico.Repositorios
{
    public class EscanioRepository : EntityFrameworkReadOnlyRepository<VotoDbContext>, IEscanioRepository
    {
        public EscanioRepository(VotoDbContext Context) : base(Context)
        {


        }

        public IEnumerable<Pe04_Escanio> FiltrarEscanios(ISpecification<Pe04_Escanio> specification)
          => Context.Pe04_Escanios.Where(specification.SatisfiedBy());





    }
}
