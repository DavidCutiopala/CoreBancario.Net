
using System.Collections.Generic;
using System.Linq;
using VotoElectronico.Entidades.Pk.PkSeguridad;
using VotoElectronicoExtensiones.Configuraciones;
using VotoElectronicoExtensiones.EntityFrameworkRepository;

namespace EcVotoElectronico.Repositorios
{
    public class PermisoRepository : EntityFrameworkReadOnlyRepository<VotoDbContext>, IPermisoRepository
    {
        public PermisoRepository(VotoDbContext Context) : base(Context)
        {


        }

        public IEnumerable<Sg05_Permiso> FiltrarCargosMedianteEspecificacion(ISpecification<Sg05_Permiso> specification)
        => Context.Sg05_Permisos.Where(specification.SatisfiedBy());

        


        
    }
}
