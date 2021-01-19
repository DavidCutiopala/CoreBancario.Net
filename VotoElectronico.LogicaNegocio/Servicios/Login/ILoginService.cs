using VotoElectronico.Generico;

namespace VotoElectronico.LogicaNegocio.Servicios
{
    public interface ILoginService
    {
        DtoGenerico IniciarSesion(string nombreUsuario, string clave);
    }
}
