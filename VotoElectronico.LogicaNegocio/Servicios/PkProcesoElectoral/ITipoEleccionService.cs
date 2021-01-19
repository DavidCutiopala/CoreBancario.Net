
using VotoElectronico.Generico;

namespace VotoElectronico.LogicaNegocio.Servicios
{
    public interface ITipoEleccionService
    {

        DtoApiResponseMessage ObtenerTipoEleccionMedianteId(DtoTipoEleccion dto);

        DtoApiResponseMessage CrearTipoEleccion(DtoTipoEleccion dto);

        DtoApiResponseMessage ActualizarTipoEleccion(DtoTipoEleccion dto);

        DtoApiResponseMessage EliminarTipoEleccion(DtoTipoEleccion dto);

        DtoApiResponseMessage ObtenerTipoEleccionPorParametros(string nombreTipoEleccion, string estado);
    }
}

