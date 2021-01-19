using System;
using System.Linq;
using VotoElectronico.Entidades.Pk.PkProcesoElectoral;
using VotoElectronico.Generico.Propiedades;
using VotoElectronicoExtensiones.Configuraciones;

namespace VotoElectronico.LogicaCondicional
{
    public class Pe01_ProcesoElectoralCondicional
    {

        public ISpecification<Pe01_ProcesoElectoral> FiltrarProcesoElectoralPorNombre(string nombreProcesoElectoral="", string estado="", int anio=0)
        {
           

            return  new Specification<Pe01_ProcesoElectoral>(
                ProcesoElectoral =>(string.IsNullOrEmpty(nombreProcesoElectoral) || ProcesoElectoral.NombreProcesoElectoral.Contains(nombreProcesoElectoral))
                && (string.IsNullOrEmpty(estado) || ProcesoElectoral.Estado.Equals(estado))
                && (anio == 0 || ProcesoElectoral.FechaInicio.Year == anio )
                );
        }

        public ISpecification<Pe01_ProcesoElectoral> FiltrarEleccionesVigentesByToken(string token)
        {
            return new Specification<Pe01_ProcesoElectoral>(
                ProcesoElectoral => (ProcesoElectoral.Estado.Equals(Auditoria.EstadoActivo))
                && (ProcesoElectoral.PadronesVotacion.Any(proceso => proceso.Usuario.Token == token))
                && (DateTime.Now >= ProcesoElectoral.FechaInicio)
                && ( DateTime.Now <= ProcesoElectoral.FechaFin)
                );
        }
    }
}
