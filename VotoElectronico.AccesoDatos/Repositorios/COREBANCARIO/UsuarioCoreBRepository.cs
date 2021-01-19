
using System.Collections.Generic;
using System.Linq;
using VotoElectronico.Entidades.Pk.PkMesaIdentificacion;
using VotoElectronico.Entidades.Pk.PkProcesoElectoral;
using VotoElectronicoExtensiones.Configuraciones;
using VotoElectronicoExtensiones.EntityFrameworkRepository;

namespace EcVotoElectronico.Repositorios
{
    public class UsuarioCoreBancarioRepository: EntityFrameworkReadOnlyRepository<VotoDbContext>, IUsuarioCoreBRepository
    {
        public UsuarioCoreBancarioRepository(VotoDbContext Context) : base(Context)
        {


        }

       public IEnumerable<CB04_Usuario> FiltarUsuarioCB(ISpecification<CB04_Usuario> specification)
          => Context.CB04_Usuarios.Where(specification.SatisfiedBy());




    }
}
