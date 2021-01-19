using VotoElectronico.Entidades.Pk.PkSeguridad;
using VotoElectronicoExtensiones.Configuraciones;

namespace VotoElectronico.LogicaCondicional
{
    public class Sg02_PersonaCondicional
    {

        public ISpecification<Sg02_Persona> FiltrarPersonaPorNombres(string nombreUno = "", string nombreDos = "")
        {
            return  new Specification<Sg02_Persona>(
                persona =>(string.IsNullOrEmpty(nombreUno) || persona.NombreUno.Contains(nombreUno)) 
               && (string.IsNullOrEmpty(nombreDos) || persona.NombreDos.Contains(nombreDos))
                );
        }

        public ISpecification<Sg02_Persona> FiltrarPersonaPorIdentificacion(string identificacion = "")
        {
            return new Specification<Sg02_Persona>(
                persona => (string.IsNullOrEmpty(identificacion) || persona.Identificacion.Contains(identificacion))
                );
        }
    }
}
