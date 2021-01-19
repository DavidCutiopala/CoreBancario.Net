
using System.Collections.Generic;
using System.Linq;
using VotoElectronico.Entidades.Pk.PkMesaIdentificacion;
using VotoElectronico.Entidades.Pk.PkProcesoElectoral;
using VotoElectronicoExtensiones.Configuraciones;
using VotoElectronicoExtensiones.EntityFrameworkRepository;

namespace EcVotoElectronico.Repositorios
{
    public class MovimientoRepository: EntityFrameworkReadOnlyRepository<VotoDbContext>, IMovimientoRepository
    {
        public MovimientoRepository(VotoDbContext Context) : base(Context)
        {


        }

       public IEnumerable<CB03_Movimiento> FiltrarMovimientos(ISpecification<CB03_Movimiento> specification)
          => Context.CB03_Movimientos.Where(specification.SatisfiedBy());




    }
}
