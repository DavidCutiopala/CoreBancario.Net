
using VotoElectronico.Generico;

namespace VotoElectronico.LogicaNegocio.Servicios
{
    public interface ICandidatoService
    {

        DtoApiResponseMessage ObtenerCandidatoMedianteId(DtoCandidato dto);

        DtoApiResponseMessage CrearCandidato(DtoCandidato dto);

        DtoApiResponseMessage ActualizarCandidato(DtoCandidato dto);

        DtoApiResponseMessage EliminarCandidato(DtoCandidato dto);

        DtoApiResponseMessage ObtenerCandidatoMedianteParametros(long parametroBusqueda1, string parametroBusqueda2, string parametroBusqueda3);
        DtoApiResponseMessage ObtenerCandidatoMedianteListaId(long listaId, string estado);
        void EliminarCandidatosLista(long listaId);


    }
}

