using System.Linq;
using VotoElectronico.Entidades.Pk.PkProcesoElectoral;
using VotoElectronico.Generico.Propiedades;
using VotoElectronicoExtensiones.Configuraciones;

namespace VotoElectronico.LogicaCondicional
{
    public class Pe04_EscanioCondicional
    {

        public ISpecification<Pe04_Escanio> FiltrarEscanios(string nombreEscanio = "", string estado = "")
        {
            return  new Specification<Pe04_Escanio>(
                Escanio =>(string.IsNullOrEmpty(nombreEscanio) || Escanio.NombreEscanio.Contains(nombreEscanio))
                && (string.IsNullOrEmpty(estado) || Escanio.Estado.Equals(estado))
                );
        }

        public ISpecification<Pe04_Escanio> FiltrarEscaniosMedianteListaId(long listaId)
        {
            return new Specification<Pe04_Escanio>(
                Escanio => ( Escanio.Estado.Equals(Auditoria.EstadoActivo))
                && (Escanio.Eleccion.ProcesosElectorales.Any(procesoElectoral => procesoElectoral.Listas.
                Any(lista => lista.Id == listaId && !lista.candidatos.Any(candidato => candidato.EscanioId == Escanio.Id))))
                );
        }

        public ISpecification<Pe04_Escanio> FiltrarEscaniosMedianteProcesoElectoralId(long procesoElectoralId)
        {
            return new Specification<Pe04_Escanio>(
                Escanio => (Escanio.Estado.Equals(Auditoria.EstadoActivo))
                && (Escanio.Eleccion.ProcesosElectorales.Any(procesoElectoral => procesoElectoral.Id == procesoElectoralId))
                );
        }
    }
}
