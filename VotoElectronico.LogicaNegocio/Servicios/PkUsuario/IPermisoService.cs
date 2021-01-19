using System.Security;
using VotoElectronico.Entidades.Pk.PkSeguridad;
using VotoElectronico.Generico;

namespace VotoElectronico.LogicaNegocio.Servicios
{
    public interface IPermisoService
    {

        bool ValidarRutaMedianteRol(DtoPermiso dto, string token);
    }
}
