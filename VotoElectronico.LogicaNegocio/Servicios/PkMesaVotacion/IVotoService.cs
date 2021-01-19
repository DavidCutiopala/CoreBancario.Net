
using VotoElectronico.Generico;

namespace VotoElectronico.LogicaNegocio.Servicios
{
    public interface IVotoService
    {
        DtoApiResponseMessage ObtenerResumenProcesoElectoral(long procesoElectoralId);
    }
}

