
using VotoElectronico.Entidades.Pk.PkSeguridad;
using VotoElectronico.Generico;

namespace VotoElectronico.LogicaNegocio.Servicios
{
    public interface IUsuarioCargoService
    {

        DtoApiResponseMessage ObtenerUsuarioCargoMedianteId(DtoUsuarioCargo dto);

        void InsertarUsuarioCargo(DtoUsuarioCargo dto, string token);
        void ValidarInsertarUsuarioCargo(DtoUsuarioCargo dto, string token);
        DtoApiResponseMessage ActualizarUsuarioCargo(DtoUsuarioCargo dto);
        long obtenerCargoIdbyToken(string token);
    }
}
