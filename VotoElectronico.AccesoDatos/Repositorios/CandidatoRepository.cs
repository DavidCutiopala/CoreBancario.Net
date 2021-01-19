
using System.Collections.Generic;
using System.Linq;
using VotoElectronico.Entidades.Pk.PkProcesoElectoral;
using VotoElectronicoExtensiones.Configuraciones;
using VotoElectronicoExtensiones.EntityFrameworkRepository;

namespace EcVotoElectronico.Repositorios
{
    public class CandidatoRepository : EntityFrameworkReadOnlyRepository<VotoDbContext>, ICandidatoRepository
    {
        public CandidatoRepository(VotoDbContext Context) : base(Context)
        {


        }

        //public IEnumerable<Pe06_Candidato> FiltrarCandidatoPorNombre(ISpecification<Pe06_Candidato> specification)
        //  => Context.Pe06_Candidato.Where(specification.SatisfiedBy());




    }
}
