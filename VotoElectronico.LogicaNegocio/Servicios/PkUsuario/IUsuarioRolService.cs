
using VotoElectronico.Entidades.Pk.PkSeguridad;
using VotoElectronico.Generico;

namespace VotoElectronico.LogicaNegocio.Servicios
{
    public interface IUsuarioRolService
    {
        void ValidarInsertarUsuarioRol(DtoUsuarioRol dto, string token);

        long obtenerRolIdbyToken(string token);
    }
}
