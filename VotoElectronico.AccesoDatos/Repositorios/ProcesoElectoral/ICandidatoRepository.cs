﻿
using System.Collections.Generic;
using VotoElectronico.Entidades.Pk.PkProcesoElectoral;
using VotoElectronicoExtensiones.Configuraciones;
using VotoElectronicoExtensiones.EntityFrameworkRepository;

namespace EcVotoElectronico.Repositorios
{
    public interface ICandidatoRepository : IEntityFrameworkRepositoryVotoElectronico
    {
        IEnumerable<Pe06_Candidato> FiltrarCandidatoMedianteParametros(ISpecification<Pe06_Candidato> specification);
        IEnumerable<Pe06_Candidato> FiltrarCandidatoMedianteListaID(ISpecification<Pe06_Candidato> specification);
    }
}
