
using System.Collections.Generic;
using System.Linq;
using VotoElectronico.Entidades.Pk.PkSeguridad;
using VotoElectronicoExtensiones.Configuraciones;
using VotoElectronicoExtensiones.EntityFrameworkRepository;

namespace EcVotoElectronico.Repositorios
{
    public class UsuarioRepository : EntityFrameworkReadOnlyRepository<VotoDbContext>, IUsuarioRepository
    {
        public UsuarioRepository(VotoDbContext Context) : base(Context)
        {


        }

        public IEnumerable<Sg01_Usuario> FiltrarUsuario(ISpecification<Sg01_Usuario> specification)
          =>   Context.Sg01_Usuarios.Where(specification.SatisfiedBy());
        


        
    }
}
