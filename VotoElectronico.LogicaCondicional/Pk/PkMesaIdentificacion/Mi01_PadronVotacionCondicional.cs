using VotoElectronico.Entidades.Pk.PkMesaIdentificacion;
using VotoElectronico.Generico.Propiedades;
using VotoElectronicoExtensiones.Configuraciones;

namespace VotoElectronico.LogicaCondicional
{
    public class Mi01_PadronVotacionCondicional
    {

        public ISpecification<Mi01_PadronVotacion> FiltrarEmpadronados(long procesoElectoralId = 0, string parametroBusqueda= "")
        {
            return  new Specification<Mi01_PadronVotacion>(
                x=>(string.IsNullOrEmpty(parametroBusqueda) 
                || x.Usuario.Persona.NombreUno.Contains(parametroBusqueda) 
                || x.Usuario.Persona.NombreDos.Contains(parametroBusqueda) 
                || x.Usuario.Persona.ApellidoUno.Contains(parametroBusqueda) 
                || x.Usuario.Persona.ApellidoDos.Contains(parametroBusqueda) 
                || x.Usuario.Persona.Identificacion.Contains(parametroBusqueda)) 
                && (x.Estado.Equals(Auditoria.EstadoActivo))
                && (procesoElectoralId == 0 || x.ProcesoElectoralId == procesoElectoralId)
                );
        }
    }
}
