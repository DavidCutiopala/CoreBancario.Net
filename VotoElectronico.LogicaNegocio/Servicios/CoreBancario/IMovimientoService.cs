
using VotoElectronico.Generico;

namespace VotoElectronico.LogicaNegocio.Servicios
{
    public interface IMovimientosService
    {


        DtoApiResponseMessage RealizarTransferencia(DtoTranferencia dto);
        DtoApiResponseMessage ObtenerMovimientosByClientes(long ClienteId);


    }
}

