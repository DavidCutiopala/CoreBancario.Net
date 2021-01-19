
using VotoElectronico.Generico;

namespace VotoElectronico.LogicaNegocio.Servicios
{
    public interface IMesaVotoService
    {
        DtoApiResponseMessage EmitirVoto(DtoAES dtoAes);

    }
}

