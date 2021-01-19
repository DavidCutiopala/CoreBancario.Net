
using System.Collections.Generic;
using System.Linq;
using VotoElectronico.Entidades.Pk.PkSeguridad;
using VotoElectronicoExtensiones.Configuraciones;
using VotoElectronicoExtensiones.EntityFrameworkRepository;

namespace EcVotoElectronico.Repositorios
{
    public class CargoRepository : EntityFrameworkReadOnlyRepository<VotoDbContext>, ICargoRepository
    {
        public CargoRepository(VotoDbContext Context) : base(Context)
        {


        }

        public IEnumerable<Sg03_Cargo> FiltrarCargoPorNombre(ISpecification<Sg03_Cargo> specification)
          =>   Context.Sg03_Cargos.Where(specification.SatisfiedBy());
        


        
    }
}
