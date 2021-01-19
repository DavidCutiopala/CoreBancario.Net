
using System.Collections.Generic;
using System.Linq;
using VotoElectronico.Entidades.Pk.PkSeguridad;
using VotoElectronicoExtensiones.Configuraciones;
using VotoElectronicoExtensiones.EntityFrameworkRepository;

namespace EcVotoElectronico.Repositorios
{
    public class RolRepository : EntityFrameworkReadOnlyRepository<VotoDbContext>, IRolRepository
    {
        public RolRepository(VotoDbContext Context) : base(Context)
        {


        }

        public IEnumerable<Sg07_Rol> FiltrarRoles(ISpecification<Sg07_Rol> specification)
          =>   Context.Sg07_Roles.Where(specification.SatisfiedBy());
        


        
    }
}
