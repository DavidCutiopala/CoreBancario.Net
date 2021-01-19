using System.Collections.Generic;
using VotoElectronico.Entidades.Pk.PkSeguridad;
using VotoElectronico.Generico;

namespace VotoElectronico.LogicaNegocio.Servicios
{
    public interface IUsuarioService
    {

        DtoApiResponseMessage ObtenerUsuarioMedianteId(DtoUsuario dto);

        DtoApiResponseMessage CrearUsuario(DtoUsuario dto, string token);

        Sg01_Usuario InsertarUsuario(DtoUsuario dto, string token, bool envioEmail);

        Sg01_Usuario ValidarInsertarUsuario(DtoUsuario dto, string token);

        DtoApiResponseMessage ActualizarUsuario(DtoUsuario dto);

        DtoApiResponseMessage EliminarUsuario(DtoUsuario dto);

        DtoApiResponseMessage ObtenerUsuariosPorNombreOIdentificacion(string nombre, string identificacion);

        void EnvioEmailUsuario(Sg01_Usuario usuarios, Sg02_Persona persona);
    }
}
