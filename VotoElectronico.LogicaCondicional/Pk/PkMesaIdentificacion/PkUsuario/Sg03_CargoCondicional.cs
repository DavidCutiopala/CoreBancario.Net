using VotoElectronico.Entidades.Pk.PkSeguridad;
using VotoElectronicoExtensiones.Configuraciones;

namespace VotoElectronico.LogicaCondicional
{
    public class Sg03_CargoCondicional
    {

        public ISpecification<Sg03_Cargo> FiltrarCargoPorNombre(string nombreCargo = "", string estado = "")
        {
            return  new Specification<Sg03_Cargo>(
                persona =>(string.IsNullOrEmpty(nombreCargo) || persona.NombreCargo.Contains(nombreCargo))
                && (string.IsNullOrEmpty(estado) || persona.NombreCargo.Contains(estado)));
        }
    }
}
