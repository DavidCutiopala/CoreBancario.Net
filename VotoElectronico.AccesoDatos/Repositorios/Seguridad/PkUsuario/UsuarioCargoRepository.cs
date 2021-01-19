
using System.Collections.Generic;
using System.Linq;
using VotoElectronico.Entidades.Pk.PkSeguridad;
using VotoElectronicoExtensiones.Configuraciones;
using VotoElectronicoExtensiones.EntityFrameworkRepository;

namespace EcVotoElectronico.Repositorios
{
    public class UsuarioCargoRepository : EntityFrameworkReadOnlyRepository<VotoDbContext>, IUsuarioCargoRepository
    {
        public UsuarioCargoRepository(VotoDbContext Context) : base(Context)
        {


        }



        
    }
}
