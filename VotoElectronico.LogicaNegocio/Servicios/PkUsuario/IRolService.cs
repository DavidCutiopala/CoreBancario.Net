
using System.Collections.Generic;
using VotoElectronico.Entidades.Pk.PkSeguridad;
using VotoElectronico.Generico;

namespace VotoElectronico.LogicaNegocio.Servicios
{
    public interface IRolService
    {

        DtoApiResponseMessage ObtenerRolMedianteId(DtoRol dto);

        DtoApiResponseMessage CrearRol(DtoRol dto, string token);

        DtoApiResponseMessage ActualizarRol(DtoRol dto, string token);

        DtoApiResponseMessage EliminarRol(DtoRol dto);

        DtoApiResponseMessage ObtenerRolsPorNombre(string nombre, string estado = "");
        IEnumerable<Sg07_Rol> ObtenerEntidadesRolesPorNombre(string nombre, string estado = "");
        Sg07_Rol ObtenerRolPorNombre(string nombre);

        bool ExisteRol(string nombreRol);
    }
}
