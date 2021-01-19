using VotoElectronico.Entidades.Pk.PkSeguridad;
using VotoElectronico.Generico;

namespace VotoElectronico.LogicaNegocio.Servicios
{
    public interface ISesionService
    {
        void AniadirTokenUsuario(long usuarioId, string token);
        DtoApiResponseMessage CerrarSesion(string nombreUsuario, string token);
        Sg01_Usuario ObtenerUsuarioPorToken(string token);
        DtoApiResponseMessage CambioClave(string token, string nuevaClave);
        DtoApiResponseMessage SolicitudCambioClave(string email);
    }
}
