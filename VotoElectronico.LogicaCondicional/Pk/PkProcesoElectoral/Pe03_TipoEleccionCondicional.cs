using VotoElectronico.Entidades.Pk.PkProcesoElectoral;
using VotoElectronicoExtensiones.Configuraciones;

namespace VotoElectronico.LogicaCondicional
{
    public class Pe03_TipoEleccionCondicional
    {

        public ISpecification<Pe03_TipoEleccion> FiltrarEleccionPorParametros(string nombreTipoEleccion, string estado)
        {
            return  new Specification<Pe03_TipoEleccion>(
                tipoEleccion =>string.IsNullOrEmpty(nombreTipoEleccion) || tipoEleccion.NombreTipoEleccion.Contains(nombreTipoEleccion)
                 && (string.IsNullOrEmpty(estado) || tipoEleccion.Estado.Equals(estado))

                );
        }
    }
}
