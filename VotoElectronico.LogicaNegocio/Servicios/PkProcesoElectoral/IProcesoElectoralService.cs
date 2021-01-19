
using VotoElectronico.Generico;

namespace VotoElectronico.LogicaNegocio.Servicios
{
    public interface IProcesoElectoralService
    {

        DtoApiResponseMessage ObtenerProcesoElectoralMedianteId(DtoProcesoElectoral dto);

        DtoApiResponseMessage CrearProcesoElectoral(DtoProcesoElectoral dto);

        DtoApiResponseMessage ActualizarProcesoElectoral(DtoProcesoElectoral dto);

        DtoApiResponseMessage EliminarProcesoElectoral(DtoProcesoElectoral dto);

        DtoApiResponseMessage ObtenerProcesoElectoralesPorNombre(string nombreProcesoElectoral, string estado, int anio);

        DtoApiResponseMessage ObtenerProcesoElectoralesVigentesByToken(string token);
        DtoApiResponseMessage ObtenerTodosProcesosElectoralesActivos();
    }
}

