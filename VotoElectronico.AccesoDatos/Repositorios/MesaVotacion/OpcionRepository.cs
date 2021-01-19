
using System.Collections.Generic;
using System.Linq;
using VotoElectronico.Entidades;
using VotoElectronico.Entidades.Pk.PkMesaIdentificacion;
using VotoElectronicoExtensiones.Configuraciones;
using VotoElectronicoExtensiones.EntityFrameworkRepository;

namespace EcVotoElectronico.Repositorios
{
    public class OpcionRepository : EntityFrameworkReadOnlyRepository<VotoDbContext>, IOpcionRepository
    {
        public OpcionRepository(VotoDbContext Context) : base(Context)
        {


        }

       public IEnumerable<Mv02_Opcion> FiltrarOpciones(ISpecification<Mv02_Opcion> specification)
          => Context.Mv02_Opciones.Where(specification.SatisfiedBy());




    }
}
