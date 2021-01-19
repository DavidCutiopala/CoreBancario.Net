
using VotoElectronico.Generico;

namespace VotoElectronico.LogicaNegocio.Servicios
{
    public interface IEleccionService
    {

        DtoApiResponseMessage ObtenerEleccionMedianteId(DtoEleccion dto);

        DtoApiResponseMessage CrearEleccion(DtoEleccion dto);

        DtoApiResponseMessage ActualizarEleccion(DtoEleccion dto);

        DtoApiResponseMessage EliminarEleccion(DtoEleccion dto);

        DtoApiResponseMessage ObtenerEleccionesPorNombre(string nombre, string estado);
    }
}

