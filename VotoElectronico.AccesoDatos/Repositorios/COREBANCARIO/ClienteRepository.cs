
using System.Collections.Generic;
using System.Linq;
using VotoElectronico.Entidades.Pk.PkMesaIdentificacion;
using VotoElectronico.Entidades.Pk.PkProcesoElectoral;
using VotoElectronicoExtensiones.Configuraciones;
using VotoElectronicoExtensiones.EntityFrameworkRepository;

namespace EcVotoElectronico.Repositorios
{
    public class ClienteRepository: EntityFrameworkReadOnlyRepository<VotoDbContext> , IClienteRepository
    {
        public ClienteRepository(VotoDbContext Context) : base(Context)
        {


        }

       public IEnumerable<CB01_Cliente> FiltrarClientesCB(ISpecification<CB01_Cliente> specification)
          => Context.CB01_Clientes.Where(specification.SatisfiedBy());




    }
}
