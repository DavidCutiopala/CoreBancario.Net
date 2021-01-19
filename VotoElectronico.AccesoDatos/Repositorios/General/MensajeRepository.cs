using System.Collections.Generic;
using System.Linq;
using VotoElectronico.Entidades;
using VotoElectronicoExtensiones.Configuraciones;
using VotoElectronicoExtensiones.EntityFrameworkRepository;

namespace VotoElectronico.AccesoDatos.Repositorios.General
{
    public class MensajeRepository: EntityFrameworkReadOnlyRepository<EcVotoElectronico.VotoDbContext>, IMensajeRepository
    {
        public MensajeRepository(EcVotoElectronico.VotoDbContext Context) : base(Context)
        {


        }

        public IEnumerable<Ge01_Mensaje> FiltrarUsuario(ISpecification<Ge01_Mensaje> specification)
        => Context.Ge01_Mensajes.Where(specification.SatisfiedBy());
    }
}
