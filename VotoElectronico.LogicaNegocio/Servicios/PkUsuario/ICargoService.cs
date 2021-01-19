
using System.Collections.Generic;
using VotoElectronico.Entidades.Pk.PkSeguridad;
using VotoElectronico.Generico;

namespace VotoElectronico.LogicaNegocio.Servicios
{
    public interface ICargoService
    {

        DtoApiResponseMessage ObtenerCargoMedianteId(DtoCargo dto);

        DtoApiResponseMessage CrearCargo(DtoCargo dto);

        DtoApiResponseMessage ActualizarCargo(DtoCargo dto);

        DtoApiResponseMessage EliminarCargo(DtoCargo dto);

        DtoApiResponseMessage ObtenerCargosPorNombre(string nombre, string estado = "");
        IEnumerable<Sg03_Cargo> ObtenerEntidadesCargosPorNombre(string nombre, string estado = "");

        bool ExisteCargo(string nombreCargo);
    }
}
