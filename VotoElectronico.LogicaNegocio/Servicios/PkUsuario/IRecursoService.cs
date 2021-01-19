
using VotoElectronico.Generico;

namespace VotoElectronico.LogicaNegocio.Servicios
{
    public interface IRecursoService
    {
        DtoApiResponseMessage ObtenerMenuUsuarioPorToken(string token);

    }
}
