
using VotoElectronico.Generico;

namespace VotoElectronico.LogicaNegocio.Servicios
{
    public interface IEscanioService
    {

        DtoApiResponseMessage ObtenerEscanioMedianteId(DtoEscanio dto);

        DtoApiResponseMessage CrearEscanio(DtoEscanio dto);

        DtoApiResponseMessage ActualizarEscanio(DtoEscanio dto);

        DtoApiResponseMessage EliminarEscanio(DtoEscanio dto);

        DtoApiResponseMessage ObtenerEscanios(string nombre, string estado);

        DtoApiResponseMessage ObtenerEscaniosPorListaId(long listaId);

        DtoApiResponseMessage ObtenerEscaniosPorProcesoElectoralId(long procesoElectoralId);

    }
}

