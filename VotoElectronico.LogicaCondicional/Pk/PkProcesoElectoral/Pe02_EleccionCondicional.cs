using VotoElectronico.Entidades.Pk.PkProcesoElectoral;
using VotoElectronicoExtensiones.Configuraciones;

namespace VotoElectronico.LogicaCondicional
{
    public class Pe02_EleccionCondicional
    {

        public ISpecification<Pe02_Eleccion> FiltrarEleccionPorNombre(string nombreEleccion = "", string estado = "")
        {
            return  new Specification<Pe02_Eleccion>(
                eleccion =>(string.IsNullOrEmpty(nombreEleccion) || eleccion.NombreEleccion.Contains(nombreEleccion))
                 && (string.IsNullOrEmpty(estado) || eleccion.Estado.Equals(estado))

                );
        }
    }
}
