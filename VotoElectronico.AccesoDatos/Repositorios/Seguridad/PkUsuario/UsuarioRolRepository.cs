using VotoElectronicoExtensiones.EntityFrameworkRepository;

namespace EcVotoElectronico.Repositorios
{
    public class UsuarioRolRepository : EntityFrameworkReadOnlyRepository<VotoDbContext>, IUsuarioRolRepository
    {
        public UsuarioRolRepository(VotoDbContext Context) : base(Context)
        {

        }



        
    }
}
