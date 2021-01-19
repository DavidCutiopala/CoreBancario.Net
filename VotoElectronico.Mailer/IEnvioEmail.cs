using System.Collections.Generic;
using VotoElectronico.Generico;

namespace VotoElectronico.Mailer
{
    public interface IEnvioEmail
    {
        DtoGenerico EnviarEmail(string destino, string asunto, string html);
        string ActivarUsuarioGenerico(Dictionary<string, string> datos);
        string CambioClaveUsuario(Dictionary<string, string> datos);
    }
}
