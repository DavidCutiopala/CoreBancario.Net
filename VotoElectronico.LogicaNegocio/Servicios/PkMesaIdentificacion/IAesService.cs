
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using VotoElectronico.Entidades.Pk.PkMesaIdentificacion;
using VotoElectronico.Generico;

namespace VotoElectronico.LogicaNegocio.Servicios
{
    public interface IAesService
    {

        string Encrypt(string text, string Key, string IV);
        string Decrypt(string text, string Key, string IV);

        //JArray DecryptStringAES(string textoCifrado, string key, string IV);

        List<DtoOpcion> DecryptStringAES(string textoCifrado, string key, string IV);
    }

}

