
using System.Collections.Generic;
using VotoElectronico.Entidades.Pk.PkSeguridad;
using VotoElectronico.Generico;

namespace VotoElectronico.LogicaNegocio.Servicios
{
    public interface IPersonaService
    {

        DtoApiResponseMessage ObtenerPersonaMedianteId(DtoPersona dto);

        DtoApiResponseMessage CrearPersona(DtoPersona dto, string token);

        Sg02_Persona InsertarPersona(DtoPersona dto, string token);
        bool ExistePersonaActivoInactivo(string identificacion);
        bool ExistePersonaEstado(string identificacion, string estado);

        DtoApiResponseMessage ActualizarPersona(DtoPersona dto, string token);

        DtoApiResponseMessage EliminarPersona(DtoPersona dto);

        //DtoApiResponseMessage ObtenerPersonasPorNombres(string nombre, string identificacion);
        IEnumerable<DtoPersona> ObtenerPersonasPorParametros(string identificacion);
        void ReactivarEntidadPersona(Sg02_Persona persona);
        Sg02_Persona ObtenerPersonaIdentificacion(string identificacion, string estado = "");
    }
}
