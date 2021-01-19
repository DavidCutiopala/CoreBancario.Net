
using System.Collections.Generic;
using VotoElectronico.Entidades.Pk.PkSeguridad;
using VotoElectronicoExtensiones.Configuraciones;
using VotoElectronicoExtensiones.EntityFrameworkRepository;

namespace EcVotoElectronico.Repositorios
{
    public interface IPersonaRepository : IEntityFrameworkRepositoryVotoElectronico
    {
        IEnumerable<Sg02_Persona> FiltrarPersonasEspecificacion(ISpecification<Sg02_Persona> specification);
    }
}
