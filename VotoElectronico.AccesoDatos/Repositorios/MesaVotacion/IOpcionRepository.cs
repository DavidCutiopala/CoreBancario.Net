
using System.Collections.Generic;
using VotoElectronico.Entidades;
using VotoElectronicoExtensiones.Configuraciones;
using VotoElectronicoExtensiones.EntityFrameworkRepository;

namespace EcVotoElectronico.Repositorios
{
    public interface IOpcionRepository : IEntityFrameworkRepositoryVotoElectronico
    {
        IEnumerable<Mv02_Opcion> FiltrarOpciones(ISpecification<Mv02_Opcion> specification);
    }
}
