

namespace VotoElectronico.LogicaNegocio.Servicios
{
    public interface ITokenValidator
    {
         void ValidarToken(string token);
        bool ExisteTokenUsuarioCambioClave(string token);
    }
}
