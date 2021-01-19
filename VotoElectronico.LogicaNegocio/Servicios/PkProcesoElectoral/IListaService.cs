
using VotoElectronico.Generico;

namespace VotoElectronico.LogicaNegocio.Servicios
{
    public interface IListaService
    {

        DtoApiResponseMessage ObtenerListaMedianteId(DtoLista dto);

        DtoApiResponseMessage CrearLista(DtoLista dto);

        DtoApiResponseMessage ActualizarLista(DtoLista dto);

        DtoApiResponseMessage EliminarLista(DtoLista dto);

        DtoApiResponseMessage ObtenerListasMedianteParams(string nombreLista, string estado);
        DtoApiResponseMessage obtenerListasPorProcesoElectoralId(long procesoElectoralId, string estado);
        void EliminarListasProceso(long procesoId);

        DtoApiResponseMessage obtenerListasYCandidatosByProcesoElectoralId(long procesoElectoralId);
    }
}

