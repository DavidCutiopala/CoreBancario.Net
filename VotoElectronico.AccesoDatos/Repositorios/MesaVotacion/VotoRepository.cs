
using System.Collections.Generic;
using System.Linq;
using VotoElectronico.Entidades;
using VotoElectronico.Entidades.Pk.PkMesaIdentificacion;
using VotoElectronicoExtensiones.Configuraciones;
using VotoElectronicoExtensiones.EntityFrameworkRepository;

namespace EcVotoElectronico.Repositorios
{
    public class VotoRepository : EntityFrameworkReadOnlyRepository<VotoDbContext>, IVotoRepository
    {
        public VotoRepository(VotoDbContext Context) : base(Context)
        {


        }

       public IEnumerable<Mv01_Voto> FiltrarVoto (ISpecification<Mv01_Voto> specification)
          => Context.Mv01_Votos.Where(specification.SatisfiedBy());




    }
}
