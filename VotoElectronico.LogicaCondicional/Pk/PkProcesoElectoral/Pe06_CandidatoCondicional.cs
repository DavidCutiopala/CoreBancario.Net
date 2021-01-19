using VotoElectronico.Entidades.Pk.PkProcesoElectoral;
using VotoElectronicoExtensiones.Configuraciones;

namespace VotoElectronico.LogicaCondicional
{
    public class Pe06_CandidatoCondicional
    {

        public ISpecification<Pe06_Candidato> FiltrarCandidatosPorParametros(long parametro4 , string parametro2= "",string parametro3="")
        {
            return  new Specification<Pe06_Candidato>(
                Candidato =>(parametro4 == 0 || Candidato.Lista.ProcesoElectoralId.Equals(parametro4))
                && (string.IsNullOrEmpty(parametro2) || Candidato.Estado.Equals(parametro2))
                );
        }

        public ISpecification<Pe06_Candidato> FiltrarCandidatosPorListaId(long listaId, string estado)
        {
            //return new Specification<Pe06_Candidato>(
            //    Candidato => (listaId == 0 || Candidato.Lista.Id.Equals(listaId))
            //    && (string.IsNullOrEmpty(estado) || Candidato.Estado.Equals(estado))
            //    );

            return new Specification<Pe06_Candidato>(
                Candidato => (Candidato.Lista.Id.Equals(listaId))
                && (string.IsNullOrEmpty(estado) || Candidato.Estado.Equals(estado))
                );
        }
    }
}
