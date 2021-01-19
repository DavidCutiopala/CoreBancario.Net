using VotoElectronico.Entidades.Pk.PkSeguridad;
using VotoElectronico.Generico;

namespace VotoElectronico.LogicaNegocio.Servicios.PkUsuario
{
    public interface ILoginService
    {
        DtoGenerico IniciarSesion(string nombreUsuario, string clave);
    }
}
