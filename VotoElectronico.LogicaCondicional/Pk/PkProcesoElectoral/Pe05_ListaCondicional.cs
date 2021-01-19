using VotoElectronico.Entidades.Pk.PkProcesoElectoral;
using VotoElectronico.Generico.Propiedades;
using VotoElectronicoExtensiones.Configuraciones;

namespace VotoElectronico.LogicaCondicional
{
    public class Pe05_ListaCondicional
    {

        public ISpecification<Pe05_Lista> FiltrarListaPorNombre(string nombreLista="", string estado="")
        {
            return  new Specification<Pe05_Lista>(
                Lista =>(string.IsNullOrEmpty(nombreLista) || Lista.NombreLista.Contains(nombreLista))
                && (string.IsNullOrEmpty(estado) || Lista.Estado.Equals(estado))
                );
        }

        public ISpecification<Pe05_Lista> FiltrarListaPorProcesoElectoralId(long procesoElectoralId, string estado="")
        {
            return new Specification<Pe05_Lista>(
                Lista => (Lista.ProcesoElectoralId == procesoElectoralId )
                && (string.IsNullOrEmpty(estado) || Lista.Estado.Equals(estado))
                );
        }

        public ISpecification<Pe05_Lista> FiltrarListasCandidatosPorProcesoElectoralId(long procesoElectoralId)
        {
            return new Specification<Pe05_Lista>(
                Lista => (Lista.ProcesoElectoralId == procesoElectoralId)
                && (Lista.Estado.Equals(Auditoria.EstadoActivo))
                );
        }
    }
}
