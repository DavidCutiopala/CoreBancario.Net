using EcVotoElectronico.Repositorios;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using VotoElectronico.Entidades.Pk.PkProcesoElectoral;
using VotoElectronico.Generico;
using VotoElectronico.LogicaCondicional;
using VotoElectronico.LogicaNegocio.Servicios.GoogleDrive;
using VotoElectronico.Util;

namespace VotoElectronico.LogicaNegocio.Servicios
{
    public class CandidatoService : ICandidatoService
    {

        private readonly ICandidatoRepository _candidatoRepository;
        private IApiResponseMessage _utilitarios;
        private readonly IGoogleDriveService _googleDriveService;
        private readonly string pathCandidatos = ConfigurationManager.AppSettings["path-candidatos"];


        public CandidatoService(
            ICandidatoRepository candidatoRepository, 
            IApiResponseMessage apiResponseMessage,
            IGoogleDriveService googleDriveService)
        {
            _candidatoRepository = candidatoRepository;
            _utilitarios = apiResponseMessage;
            _googleDriveService = googleDriveService;
        }

        #region Métodos Publicos
        public void EliminarCandidatosLista(long listaId)
        =>EliminarListaCandidatos(_candidatoRepository.Get<Pe06_Candidato>(x => x.ListaId == listaId));
        
        
        public DtoApiResponseMessage ObtenerCandidatoMedianteParametros(long parametro1, string parametro2, string parametro3)
        {
            var spec = new Pe06_CandidatoCondicional().FiltrarCandidatosPorParametros(parametro1, parametro2, parametro3);
            var dtoMapeado = MapearCandidatoEntidadADtoCandidato(_candidatoRepository.FiltrarCandidatoMedianteParametros(spec));

            if (dtoMapeado.Count() != 0)
            {
                return _utilitarios.CrearDtoApiResponseMessage(dtoMapeado, "VE_PEL_CAN_005");
            }
            else
            {
                return _utilitarios.CrearDtoApiResponseMessage(null, "VE_PEL_CAN_006");
            }

        }



        public DtoApiResponseMessage ObtenerCandidatoMedianteListaId(long listaId, string estado)
        {
            var spec = new Pe06_CandidatoCondicional().FiltrarCandidatosPorListaId(listaId, estado);
            var dtoMapeado = MapearCandidatoEntidadADtoCandidato(_candidatoRepository.FiltrarCandidatoMedianteListaID(spec));

            if (dtoMapeado.Count() != 0)
            {
                return _utilitarios.CrearDtoApiResponseMessage(dtoMapeado, "VE_PEL_CAN_005");
            }
            else
            {
                return _utilitarios.CrearDtoApiResponseMessage(null, "VE_PEL_CAN_006");
            }

        }

        public DtoApiResponseMessage ObtenerCandidatoMedianteId(DtoCandidato dto)
        {
            var candidato = ObtenerCandidatoId(dto.Id);

            if (candidato != null)
            {
                var dtoMapeado = mapearEntidadADto(candidato);
                return _utilitarios.CrearDtoApiResponseMessage(dtoMapeado, "VE_PEL_CAN_004");
            }
            else
            {
                return _utilitarios.CrearDtoApiResponseMessage(null, "VE_PEL_CAN_007");
            }
        }

        public DtoApiResponseMessage CrearCandidato(DtoCandidato dto)
        {

            if (dto.fotoObjeto != null)
                dto.fotoUrl = _googleDriveService.UploadBase64(dto.fotoObjeto.base64, dto.fotoObjeto.tipo, dto.fotoObjeto.extension, pathCandidatos);
            var candidato = mapearDtoAEntidad(dto);
            Crear(candidato);
            var dtoMapeado = mapearEntidadADto(candidato);
            return _utilitarios.CrearDtoApiResponseMessage(dtoMapeado, "VE_PEL_CAN_001");
        }

        public DtoApiResponseMessage ActualizarCandidato(DtoCandidato dto)
        {
            var candidato = ObtenerCandidatoId(dto.Id);

            if (candidato != null)
            {
                candidato.PersonaId = dto.personaId;
                candidato.EscanioId = dto.escanioId;
                candidato.ListaId = dto.listaId;
                candidato.UsuarioModificacion = dto.usuarioModificacion;
                candidato.FechaModificacion = DateTime.Now;
                candidato.Estado = dto.estado;
                Actualizar(candidato);
                var dtoMapeado = mapearEntidadADto(candidato);
                return _utilitarios.CrearDtoApiResponseMessage(dtoMapeado, "VE_PEL_CAN_002");
            }
            else
            {
                return _utilitarios.CrearDtoApiResponseMessage(null, "VE_PEL_CAN_007");
            }
        }


        public DtoApiResponseMessage EliminarCandidato(DtoCandidato dto)
        {
            var Candidato = ObtenerCandidatoId(dto.Id);
            if (Candidato != null)
            {
                Eliminar(Candidato.Id);
                var dtoMapeado = mapearEntidadADto(Candidato);
                return _utilitarios.CrearDtoApiResponseMessage(dtoMapeado, "VE_PEL_CAN_003");
            }
            else
            {
                return _utilitarios.CrearDtoApiResponseMessage(null, "VE_PEL_CAN_007");
            }

        }

        public void HabilitarCandidato(DtoCandidato dto)
        {
            var Candidato = ObtenerCandidatoId(dto.Id);
            Candidato.Estado = "ACTIVO";
            Actualizar(Candidato);
        }

        public void InhabilitarCandidato(DtoCandidato dto)
        {
            var Candidato = ObtenerCandidatoId(dto.Id);
            Candidato.Estado = "INACTIVO";
            Actualizar(Candidato);
        }





        #endregion

        #region Métodos Privados
        Pe06_Candidato ObtenerCandidatoId(long id)
        => _candidatoRepository.GetById<Pe06_Candidato>(id);

        Pe06_Candidato mapearDtoAEntidad(DtoCandidato dto)
            => new Pe06_Candidato()
            {
                PersonaId = dto.personaId,
                EscanioId = dto.escanioId,
                ListaId = dto.listaId,
                Estado = dto.estado,
                UsuarioCreacion = dto.usuarioCreacion,
                FechaCreacion = DateTime.Now,
                Foto = dto.fotoUrl,
            };

        IEnumerable<DtoCandidato> mapearEntidadADto(Pe06_Candidato candidato)
        {
            DtoCandidato dto = new DtoCandidato();
            dto.Id = candidato.Id; ;
            dto.personaId = candidato.PersonaId;
            dto.escanioId = candidato.EscanioId;
            dto.listaId = candidato.ListaId;
            dto.usuarioCreacion = candidato.UsuarioCreacion;
            dto.usuarioModificacion = candidato.UsuarioModificacion;
            dto.estado = candidato.Estado;
            dto.fotoUrl = candidato.Foto;

            List<DtoCandidato> Candidato = new List<DtoCandidato>();
            Candidato.Add(dto);
            return Candidato;
        }

        IEnumerable<DtoCandidato> MapearCandidatoEntidadADtoCandidato(IEnumerable<Pe06_Candidato> Candidato)
        {
            return Candidato.Select(x => new DtoCandidato()
            {
                Id = x.Id,
                personaId = x.PersonaId,
                nombreCandidato = x.Persona.NombreUno + " " + x.Persona.ApellidoUno,
                identificacion = x.Persona.Identificacion,
                procesoElectoralId = x.Lista.ProcesoElectoralId,
                nombreProcesoElectoral = x.Lista.ProcesoElectoral.NombreProcesoElectoral,
                escanioId = x.EscanioId,
                nombreEscanio = x.Escanio.NombreEscanio,
                listaId = x.ListaId,
                nombreLista = x.Lista.NombreLista,
                usuarioCreacion = x.UsuarioCreacion,
                usuarioModificacion = x.UsuarioModificacion,
                estado = x.Estado,
                fotoUrl = string.IsNullOrEmpty(x.Foto) ? null : $"{CtEstaticas.StrGoogleDrive}{x.Foto}",


            });
        }


        void Crear(Pe06_Candidato Candidato)
        {
            _candidatoRepository.Create<Pe06_Candidato>(Candidato);
            _candidatoRepository.Save();
        }

        void Actualizar(Pe06_Candidato Candidato)
        {
            _candidatoRepository.Update<Pe06_Candidato>(Candidato);
            _candidatoRepository.Save();
        }

        void Eliminar(long idCandidato)
        {
            _candidatoRepository.Delete<Pe06_Candidato>(idCandidato);
            _candidatoRepository.Save();
        }
        void EliminarListaCandidatos(IEnumerable<Pe06_Candidato> candidatos)
        {
            if (candidatos != null)
            {
                foreach (var candidato in candidatos)
                    _candidatoRepository.Delete<Pe06_Candidato>(candidato.Id);
                _candidatoRepository.Save();
            }
        }
        #endregion


    }
}
