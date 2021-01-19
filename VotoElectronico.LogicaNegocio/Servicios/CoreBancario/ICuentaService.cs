
using VotoElectronico.Generico;

namespace VotoElectronico.LogicaNegocio.Servicios
{
    public interface ICuentaService
    {


        DtoApiResponseMessage ObtenerCuentasByCedula(string cedula);

        DtoApiResponseMessage CrearCuenta(DtoCuenta dto);


    }
}

