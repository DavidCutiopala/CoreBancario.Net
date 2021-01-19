
using System.Collections.Generic;
using System.Linq;
using VotoElectronico.Entidades.Pk.PkSeguridad;
using VotoElectronicoExtensiones.Configuraciones;
using VotoElectronicoExtensiones.EntityFrameworkRepository;

namespace EcVotoElectronico.Repositorios
{
    public class PersonaRepository : EntityFrameworkReadOnlyRepository<VotoDbContext>, IPersonaRepository
    {
        public PersonaRepository(VotoDbContext Context) : base(Context)
        {


        }

        public IEnumerable<Sg02_Persona> FiltrarPersonasEspecificacion(ISpecification<Sg02_Persona> specification)
          =>   Context.Sg02_Personas.Where(specification.SatisfiedBy());
        


        
    }
}
