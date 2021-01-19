
using VotoElectronico.Entidades.Pk.PkMesaIdentificacion;
using VotoElectronico.Generico;

namespace VotoElectronico.LogicaNegocio.Servicios
{
    public interface IPadronVotacionService
    {

        DtoApiResponseMessage ObtenerPadronVotacionMedianteId(DtoPadronVotacion dto);
        Mi01_PadronVotacion ObtenerPadronVotacionId(long id);
        Mi01_PadronVotacion ObtenerPadronVotacionMedianteIdentificacion(string identificacion, long procesoId, string estado);

        DtoApiResponseMessage CrearPadronVotacion(DtoPadronVotacion dto, string token);

        DtoApiResponseMessage ActualizarPadronVotacion(DtoPadronVotacion dto);

        DtoApiResponseMessage EliminarPadronVotacion(DtoPadronVotacion dto);

        bool ExisteEmpadronado(string Identificacion, string Estado);

        void HabilitarEntodadPadronVotacion(Mi01_PadronVotacion padron);
        void EliminarPadronesPorProceso(long procesoId);

        DtoApiResponseMessage ObtenerPadronPorProceso(long id);
        DtoApiResponseMessage ObtenerPadronVotacionesPorNombre(long procesoElectoralId , string nombre);
        void InsertarPadroVotacion(DtoPadronVotacion dto, string token);

        DtoApiResponseMessage AutorizarVoto(DtoVotoRSA dtoVotoRsa, string token);
    }
}

