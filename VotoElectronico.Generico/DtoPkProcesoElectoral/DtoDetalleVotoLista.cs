using Newtonsoft.Json;
using System.Collections.Generic;
using VotoElectronico.Entidades.Shell;

namespace VotoElectronico.Generico
{
    public class DtoResultadosProcesoElectoral
    {
        public int NumeroEmpadronados { get; set; }
        public int NumeroVotosValidos { get; set; }
        public int NumeroVotosBlancos { get; set; }
        public int NumeroVotosNulos { get; set; }
        public decimal PorcentajeVotantes { get; set; }
        public IEnumerable<DtoVotoDetalleLista> DetallesListas { get; set; }

    }

    public class DtoVotoDetalleLista
    {
        public string NombreLista { get; set; }
        public string ImagenLista { get; set; }
        public IEnumerable<DtoCandidato> Candidatos { get; set; }
        public long ListaId { get; set; }
        public int CantidadVotos { get; set; }
        public int CantidadNulos { get; set; }
        public int CantidadBlancos { get; set; }
        public decimal NumeroEscaniosAsignadosDecimal { get; set; }
        public int NumeroEscaniosAsignados { get; set; }
        public long EscanioId { get; set; }
        public IEnumerable<DtoListaCoeficiente> Coeficintes { get; set; }
        public decimal PorcentajeValidos { get; set; }

    }
    public class DtoListaCoeficiente
    {
        public long ListaId { get; set; }
        public decimal coeficiente { get; set; }
    }
}
