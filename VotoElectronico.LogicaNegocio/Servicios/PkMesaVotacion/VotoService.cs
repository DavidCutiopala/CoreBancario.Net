using EcVotoElectronico.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using VotoElectronico.Entidades;
using VotoElectronico.Entidades.Pk.PkProcesoElectoral;
using VotoElectronico.Generico;
using VotoElectronico.Generico.Propiedades;
using VotoElectronico.Util;

namespace VotoElectronico.LogicaNegocio.Servicios
{
    public class VotoService : IVotoService
    {


        private readonly IApiResponseMessage _apiResponseMessage;
        private readonly IPadronVotacionRepository _padronVotacionRepository;
        private readonly IVotoRepository _votoRepository;
        private readonly ICargoRepository _cargoRepository;


        public VotoService(IApiResponseMessage apiResponseMessage, IPadronVotacionRepository padronVotacionRepository, IVotoRepository votoRepository, ICargoRepository cargoRepository)
        {
            _apiResponseMessage = apiResponseMessage;
            _padronVotacionRepository = padronVotacionRepository;
            _votoRepository = votoRepository;
            _cargoRepository = cargoRepository;
        }

        #region Métodos Públicos
        public DtoApiResponseMessage ObtenerResumenProcesoElectoral(long procesoElectoralId)
            => _apiResponseMessage.CrearDtoApiResponseMessage(new List<DtoResultadosProcesoElectoral>() { ContarVotosProcesoElectoral(procesoElectoralId) }, "VE_RSM_INS_001");

        #endregion
        #region  Métodos Privados


        DtoResultadosProcesoElectoral ContarVotosProcesoElectoral(long procesoElectoralId)
        {
            var procesoelectoral = _padronVotacionRepository.GetById<Pe01_ProcesoElectoral>(procesoElectoralId);
            if (verificaresunipersonal(procesoelectoral))
                return ContarVotosProcesoElectoralUnipersonal(procesoelectoral);
            else if (verificarespluriperdonal(procesoelectoral))
                return ContarVotosProcesoElectoralPluripersonal(procesoelectoral);
            throw new Exception("No se puede definir el tipo de Eleccción");
        }

        bool verificaresunipersonal(Pe01_ProcesoElectoral proceso)
            => proceso?.Eleccion?.TipoEleccion?.NombreTipoEleccion?.ToLower()?.Equals("unipersonal") ?? false;
        bool verificarespluriperdonal(Pe01_ProcesoElectoral proceso)
        => proceso?.Eleccion?.TipoEleccion?.NombreTipoEleccion?.ToLower()?.Equals("pluripersonal") ?? false;



        DtoResultadosProcesoElectoral ContarVotosProcesoElectoralPluripersonal(Pe01_ProcesoElectoral procesoElectoral)
        {
            if (procesoElectoral != null)
            {
                var listasProceso = procesoElectoral.Listas;
                if (listasProceso?.Count() > 0)
                {
                    var numeroEscaniosProceso = procesoElectoral.Eleccion?.Escanios?.Count() ?? 0;
                    if (numeroEscaniosProceso == 0)
                        throw new Exception("El número de escaños de un proceso electorál no puede ser cero");
                    var votosProceso = _votoRepository.Get<Mv01_Voto>(x => x.Estado.Equals(Auditoria.EstadoActivo) && x.ProcesoElectoralId == procesoElectoral.Id);
                    var resultadoListas = new List<DtoVotoDetalleLista>();
                    listasProceso.ToList()?.ForEach(x =>
                    {
                        resultadoListas.Add(ObtenerInformacionLista(x, votosProceso, numeroEscaniosProceso));
                    });
                    AsignarVotosListas(resultadoListas, numeroEscaniosProceso);

                    return new DtoResultadosProcesoElectoral()
                    {
                        NumeroEmpadronados = procesoElectoral?.PadronesVotacion?.Count() ?? 0,
                        NumeroVotosValidos = resultadoListas?.Sum(x => x.CantidadVotos) ?? 0,
                        NumeroVotosBlancos = resultadoListas?.Sum(x => x.CantidadBlancos) ?? 0,
                        NumeroVotosNulos = resultadoListas?.Sum(x => x.CantidadNulos) ?? 0,
                        PorcentajeVotantes = (procesoElectoral?.PadronesVotacion?.Count() ?? 0) == 0 ? 0:  ((votosProceso?.Count() ?? 0) / (procesoElectoral.PadronesVotacion.Count())*100),
                        DetallesListas = resultadoListas
                    };
                }
                throw new Exception("Proceso sin candidatos ");

            }
            throw new Exception("Proceso electoral votación no existe");

        }

        DtoResultadosProcesoElectoral ContarVotosProcesoElectoralUnipersonal(Pe01_ProcesoElectoral procesoElectoral)
        {
            if (procesoElectoral != null)
            {
                var listasProceso = procesoElectoral.Listas;
                if (listasProceso?.Count() > 0)
                {
                    var votosProceso = _votoRepository.Get<Mv01_Voto>(x => x.Estado.Equals(Auditoria.EstadoActivo) && x.ProcesoElectoralId == procesoElectoral.Id);
                    var resultadoListas = new List<DtoVotoDetalleLista>();
                    listasProceso.ToList()?.ForEach(x =>
                    {
                        resultadoListas.Add(ObtenerInformacionListaUniPersonal(x, votosProceso));
                    });

                    return new DtoResultadosProcesoElectoral()
                    {
                        NumeroEmpadronados = procesoElectoral?.PadronesVotacion?.Count() ?? 0,
                        NumeroVotosValidos = resultadoListas?.Sum(x => x.CantidadVotos) ?? 0,
                        NumeroVotosBlancos = resultadoListas?.Sum(x => x.CantidadBlancos) ?? 0,
                        NumeroVotosNulos = resultadoListas?.Sum(x => x.CantidadNulos) ?? 0,
                        PorcentajeVotantes = (procesoElectoral?.PadronesVotacion?.Count() ?? 0) == 0 ? 0 : ((votosProceso?.Count() ?? 0) / (procesoElectoral.PadronesVotacion.Count()) * 100),
                        DetallesListas = resultadoListas
                    };
                }
                throw new Exception("Proceso sin listas ");
            }
            throw new Exception("Proceso electoral votación no existe");
        }


        DtoVotoDetalleLista ObtenerInformacionLista(Pe05_Lista lista, IEnumerable<Mv01_Voto> votos, int numeroEscanios)
        {

            var detalleLista = new DtoVotoDetalleLista()
            {
                CantidadVotos = (votos?.Sum(x => (x.Opciones.Where(y => y.Candidato.ListaId == lista.Id)?.Count() ?? 0) * (1 /*ObtenerCargoPonderacion(x.Cargo.Ponderacion)*/))) ?? 0,
                ListaId = lista?.Id ?? 0,
            };
            detalleLista.Coeficintes = CalcularCoeficientes(detalleLista.CantidadVotos, numeroEscanios, lista.Id);
            return detalleLista;
        }

        DtoVotoDetalleLista ObtenerInformacionListaUniPersonal(Pe05_Lista lista, IEnumerable<Mv01_Voto> votos)
        {

            var detalleLista = new DtoVotoDetalleLista()
            {
                CantidadVotos = votos?.Where(x => (x.Opciones.FirstOrDefault(y => y.ListaId == lista.Id) != null))?.Count() ?? 0,
                ListaId = lista?.Id ?? 0,
                NombreLista = lista?.NombreLista,
                ImagenLista = string.IsNullOrEmpty(lista?.Logo)? CtEstaticas.StrImagenNoLista : $"{ CtEstaticas.StrGoogleDrive}{lista?.Logo}",

                Candidatos = lista?.candidatos?.Select(x => new DtoCandidato() { 
                    nombreCandidato = $"{x.Persona?.NombreUno} {x.Persona?.ApellidoUno} - {x.Escanio.NombreEscanio}",
                    fotoUrl = string.IsNullOrEmpty(x?.Foto) ? CtEstaticas.StrImagenNoUser : $"{ CtEstaticas.StrGoogleDrive}{x.Foto}"
                })
            };
            return detalleLista;
        }

        IEnumerable<DtoListaCoeficiente> CalcularCoeficientes(int cantidadVotos, int numeroEscanios, long listaId)
        {
            var listaCoeficientesLista = new List<DtoListaCoeficiente>();
            for (int i = 1; i <= numeroEscanios; i++)
            {
                listaCoeficientesLista.Add(new DtoListaCoeficiente()
                {
                    ListaId = listaId,
                    coeficiente = cantidadVotos / i
                });
            }
            return listaCoeficientesLista;
        }
        void AsignarVotosListas(List<DtoVotoDetalleLista> detallesLista, int numeroEscanios)
        {

            var listaCoeficientes = new List<DtoListaCoeficiente>();

            detallesLista?.ForEach(x =>
            {
                x.Coeficintes?.ToList()?.ForEach(y =>
                {
                    listaCoeficientes.Add(y);
                });
            });

            listaCoeficientes = listaCoeficientes.OrderByDescending(x => x.coeficiente)?.ToList();
            var cifraRepetidora = listaCoeficientes[numeroEscanios]?.coeficiente;

            if (cifraRepetidora == null)
                throw new Exception("CIfra repetidora no existe");

            if (cifraRepetidora == 0)
                throw new Exception("CIfra repartidora no puede ser cero");

            for (int i = 1; i <= numeroEscanios; i++)
            {
                var x = detallesLista[i];
                var votosAsignados = x.CantidadVotos / (decimal)cifraRepetidora;
                var parteentera = (int)Math.Truncate(votosAsignados);
                x.NumeroEscaniosAsignados = parteentera;
                x.NumeroEscaniosAsignadosDecimal = votosAsignados - parteentera;
            }

            if (!ValidarEscaniosCompletos(detallesLista, numeroEscanios, out int numeroEscaniosAsinados))
            {
                AgragarEscaniosFaltantes(detallesLista, numeroEscanios, numeroEscaniosAsinados);
            }


        }
        bool ValidarEscaniosCompletos(List<DtoVotoDetalleLista> detalleLista, int numeroEscanios, out int numeroEscaniosAsignados)
        {
            numeroEscaniosAsignados = detalleLista?.Select(x => x.NumeroEscaniosAsignados)?.Sum() ?? 0;
            return numeroEscaniosAsignados == numeroEscanios;
        }

        void AgragarEscaniosFaltantes(List<DtoVotoDetalleLista> detalleLista, int numeroEscanios, int numeroEscaniosAsinados)
        {
            var escaniosFaltantes = numeroEscanios - numeroEscaniosAsinados;
            var listaACompletar = detalleLista.Where(x => x.NumeroEscaniosAsignados < 1)?.OrderByDescending(x => x.NumeroEscaniosAsignadosDecimal)?.FirstOrDefault();
            listaACompletar.NumeroEscaniosAsignados = escaniosFaltantes;
        }
        #endregion
    }
}
