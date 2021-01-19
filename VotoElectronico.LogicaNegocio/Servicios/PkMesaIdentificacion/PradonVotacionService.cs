using EcVotoElectronico.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using VotoElectronico.Entidades.Pk.PkMesaIdentificacion;
using VotoElectronico.Generico;
using VotoElectronico.Generico.Enumeraciones;
using VotoElectronico.Generico.Propiedades;
using VotoElectronico.LogicaCondicional;
using VotoElectronico.Util;

namespace VotoElectronico.LogicaNegocio.Servicios
{
    public class PadronVotacionService : IPadronVotacionService
    {

        private readonly IPadronVotacionRepository _padronVotacionRepository;
        private readonly IApiResponseMessage _apiResponseMessage;
        private readonly ISesionService _sesionService;
        private readonly IRsaHelper _rsaService;

        public PadronVotacionService(
            IPadronVotacionRepository padronVotacionRepository, 
            IApiResponseMessage apiResponseMessage, 
            ISesionService sesionService,
            IRsaHelper rsaService
            )
        {
            _padronVotacionRepository = padronVotacionRepository;
            _apiResponseMessage = apiResponseMessage;
            _sesionService = sesionService;
            _rsaService = rsaService;
        }

        #region Métodos Publicos
        public void EliminarPadronesPorProceso(long procesoId)
        {
            var padrones = _padronVotacionRepository.Get<Mi01_PadronVotacion>(x => x.ProcesoElectoralId == procesoId);
            foreach (var padron in padrones)
                _padronVotacionRepository.Delete<Mi01_PadronVotacion>(padron.Id);
            _padronVotacionRepository.Save();
        }

        public DtoApiResponseMessage ObtenerPadronPorProceso(long id)
        {
            var empadronados = _padronVotacionRepository.Get<Mi01_PadronVotacion>(x => x.Estado.Equals(Auditoria.EstadoActivo) && x.ProcesoElectoralId == id);
            var listaEmpadronados = MapearListaEntidadADtoPadronVotacion(empadronados);
            return _apiResponseMessage.CrearDtoApiResponseMessage(listaEmpadronados, "VE_PEL_PVT_004");
        }

        public DtoApiResponseMessage ObtenerPadronVotacionesPorNombre(long id, string nombre)
        {
            var empadronados = _padronVotacionRepository.FiltrarPadronVotacion(new Mi01_PadronVotacionCondicional().FiltrarEmpadronados(id, nombre));
            var listaEmpadronados = MapearListaEntidadADtoPadronVotacion(empadronados);
            return _apiResponseMessage.CrearDtoApiResponseMessage(listaEmpadronados, "VE_PEL_PVT_004");
        }

        public DtoApiResponseMessage ObtenerPadronVotacionMedianteId(DtoPadronVotacion dto)
        {
            var PadronVotacion = ObtenerPadronVotacionId(dto.Id);

            if (PadronVotacion != null)
            {
                var dtoMapeado = mapearEntidadADto(PadronVotacion);
                return _apiResponseMessage.CrearDtoApiResponseMessage(dtoMapeado, "VE_PEL_PVT_004");
            }
            else
            {
                return _apiResponseMessage.CrearDtoApiResponseMessage(null, "VE_PEL_PVT_007");
            }
        }

        public Mi01_PadronVotacion ObtenerPadronVotacionMedianteIdentificacion(string identificacion, long procesoId, string estado)
            => _padronVotacionRepository.GetOneOrDefault<Mi01_PadronVotacion>(x => x.Estado.Equals(estado) && x.Usuario.Persona.Identificacion.Equals(identificacion) && x.ProcesoElectoralId == procesoId);

        public DtoApiResponseMessage CrearPadronVotacion(DtoPadronVotacion dto, string token)
        {
            var PadronVotacion = mapearDtoAEntidad(dto, token);
            Crear(PadronVotacion);
            var dtoMapeado = mapearEntidadADto(PadronVotacion);
            return _apiResponseMessage.CrearDtoApiResponseMessage(dtoMapeado, "VE_PEL_PVT_001");
        }
        public void InsertarPadroVotacion(DtoPadronVotacion dto, string token)
        {
            var PadronVotacion = mapearDtoAEntidad(dto, token);
            Crear(PadronVotacion);
        }


        public DtoApiResponseMessage ActualizarPadronVotacion(DtoPadronVotacion dto)
        {
            var PadronVotacion = ObtenerPadronVotacionId(dto.Id);

            if (PadronVotacion != null)
            {
                PadronVotacion.ProcesoElectoralId = dto.procesoElectoralId;
                PadronVotacion.UsuarioId = dto.usuarioId;
                PadronVotacion.VotoRealizado = dto.votoRealizado;

                PadronVotacion.Estado = dto.estado;
                PadronVotacion.UsuarioModificacion = dto.usuarioModificacion;
                PadronVotacion.FechaModificacion = DateTime.Now;
                Actualizar(PadronVotacion);
                var dtoMapeado = mapearEntidadADto(PadronVotacion);
                return _apiResponseMessage.CrearDtoApiResponseMessage(dtoMapeado, "VE_PEL_PVT_002");
            }
            else
            {
                return _apiResponseMessage.CrearDtoApiResponseMessage(null, "VE_PEL_PVT_007");
            }
        }


        public DtoApiResponseMessage EliminarPadronVotacion(DtoPadronVotacion dto)
        {
            var PadronVotacion = ObtenerPadronVotacionId(dto.Id);
            if (PadronVotacion != null)
            {
                Eliminar(PadronVotacion.Id);
                var dtoMapeado = mapearEntidadADto(PadronVotacion);
                return _apiResponseMessage.CrearDtoApiResponseMessage(dtoMapeado, "VE_PEL_PVT_003");
            }
            else
            {
                return _apiResponseMessage.CrearDtoApiResponseMessage(null, "VE_PEL_PVT_007");
            }

        }


        public bool ExisteEmpadronado(string Identificacion, string estado)
            => _padronVotacionRepository.GetExists<Mi01_PadronVotacion>(x => x.Usuario.Persona.Identificacion.Equals(Identificacion) && x.Estado.Equals(estado));


        public void HabilitarPadronVotacion(DtoPadronVotacion dto)
        {
            var PadronVotacion = ObtenerPadronVotacionId(dto.Id);
            HabilitarEntodadPadronVotacion(PadronVotacion);
        }
        public void HabilitarEntodadPadronVotacion(Mi01_PadronVotacion padron)
        {
            padron.Estado = Auditoria.EstadoActivo;
            Actualizar(padron);
        }

        public void InhabilitarPadronVotacion(DtoPadronVotacion dto)
        {
            var PadronVotacion = ObtenerPadronVotacionId(dto.Id);
            PadronVotacion.Estado = "INACTIVO";
            Actualizar(PadronVotacion);
        }


      

        public Mi01_PadronVotacion ObtenerPadronVotacionId(long id)
            => _padronVotacionRepository.GetById<Mi01_PadronVotacion>(id);


        //Mesa de identificacion
        //Funcion para Autorizar y firmar el voto cifrado
        public DtoApiResponseMessage AutorizarVoto(DtoVotoRSA dtoVotoRsa, string token)
        {
            var usuario = _sesionService.ObtenerUsuarioPorToken(token);

            if (ExisteUsuarioPadron(usuario.Id))
            {

                if (VotoRealizado(usuario.Id, dtoVotoRsa.ProcesoElectoralId)) {
                    return _apiResponseMessage.CrearDtoApiResponseMessage(null, "VE_PEL_MI_002");
                }
                else
                {
                    try
                    {
                        var padronVotacion = ObtenerPadronVotacionMedianteUsuarioId(usuario.Id, dtoVotoRsa.ProcesoElectoralId);
                        padronVotacion.VotoRealizado = true;
                        padronVotacion.FechaModificacion = DateTime.Now;
                        padronVotacion.UsuarioModificacion = usuario.NombreUsuario;
                        Actualizar(padronVotacion);
                        //Firmar Voto
                        var votoFirmado = _rsaService.FirmaDigital(dtoVotoRsa.VotoCifrado + dtoVotoRsa.Mascara);
                        DtoVotoRSA dtoVotofirmado = new DtoVotoRSA { VotoFirmado = votoFirmado };

                        //Cambiar voto a realizado.

                        List<DtoVotoRSA> lista = new List<DtoVotoRSA>();
                        lista.Add(dtoVotofirmado);
                        return _apiResponseMessage.CrearDtoApiResponseMessage(lista, "VE_PEL_MI_003");
                    }
                    catch (Exception)
                    {
                        return _apiResponseMessage.CrearDtoApiResponseMessage(null, "VE_PEL_MI_004");
                    }
                   
                }
            }
            else
            {
                return _apiResponseMessage.CrearDtoApiResponseMessage(null, "VE_PEL_MI_001");
            }
        }

        public bool ExisteUsuarioPadron(long? usuarioId)
            => _padronVotacionRepository.GetExists<Mi01_PadronVotacion>(
                padron => padron.UsuarioId == usuarioId
                && padron.Estado.Equals(Auditoria.EstadoActivo)
                );

        public bool VotoRealizado(long? usuarioId, long procesoId)
            => _padronVotacionRepository.GetExists<Mi01_PadronVotacion>(
                padron => padron.UsuarioId == usuarioId
                && padron.Estado.Equals(Auditoria.EstadoActivo)
                && padron.VotoRealizado
                && padron.ProcesoElectoralId == procesoId
                );


        public Mi01_PadronVotacion ObtenerPadronVotacionMedianteUsuarioId(long? usuarioId, long procesoId)
            => _padronVotacionRepository.GetOne<Mi01_PadronVotacion>(
                padron => padron.UsuarioId == usuarioId
                && padron.Estado.Equals(Auditoria.EstadoActivo)
                && !padron.VotoRealizado 
                && padron.ProcesoElectoralId == procesoId
                );

        #endregion

        #region Métodos Privados


        Mi01_PadronVotacion mapearDtoAEntidad(DtoPadronVotacion dto, string token)
            => new Mi01_PadronVotacion()
            {
                ProcesoElectoralId = dto.procesoElectoralId,
                UsuarioId = dto.usuarioId,
                VotoRealizado = dto.votoRealizado,
                Estado = Auditoria.EstadoActivo,
                UsuarioCreacion = _sesionService.ObtenerUsuarioPorToken(token)?.NombreUsuario,
                FechaCreacion = DateTime.Now,
            };

        IEnumerable<DtoPadronVotacion> mapearEntidadADto(Mi01_PadronVotacion PadronVotacion)
        {
            DtoPadronVotacion dto = new DtoPadronVotacion();
            dto.Id = PadronVotacion.Id; ;
            dto.procesoElectoralId = PadronVotacion.ProcesoElectoralId;
            dto.usuarioId = PadronVotacion.UsuarioId;
            dto.votoRealizado = PadronVotacion.VotoRealizado;

            dto.usuarioCreacion = PadronVotacion.UsuarioCreacion;
            dto.usuarioModificacion = PadronVotacion.UsuarioModificacion;
            dto.estado = PadronVotacion.Estado;

            List<DtoPadronVotacion> lista = new List<DtoPadronVotacion>();
            lista.Add(dto);
            return lista;
        }

        IEnumerable<DtoPadronVotacion> MapearListaEntidadADtoPadronVotacion(IEnumerable<Mi01_PadronVotacion> PadronVotacion)
        {
            return PadronVotacion.Select(x => new DtoPadronVotacion()
            {
                Id = x.Id,
                procesoElectoralId = x.ProcesoElectoralId,
                NombreEmpadronado = $"{x.Usuario?.Persona?.NombreUno} {x.Usuario?.Persona?.NombreDos}  {x.Usuario?.Persona?.ApellidoUno}  {x.Usuario?.Persona?.ApellidoDos}",
                identificacionEmpadronado = x.Usuario?.Persona?.Identificacion,
                emailEmpadronado = x.Usuario?.Persona?.Email,
                usuarioId = x.UsuarioId,
                usuarioCreacion = x.UsuarioCreacion,
                votoRealizado = x.VotoRealizado,
                usuarioModificacion = x.UsuarioModificacion,
                estado = x.Estado

            }); ;
        }


        void Crear(Mi01_PadronVotacion PadronVotacion)
        {
            _padronVotacionRepository.Create<Mi01_PadronVotacion>(PadronVotacion);
            _padronVotacionRepository.Save();
        }

        void Actualizar(Mi01_PadronVotacion PadronVotacion)
        {
            _padronVotacionRepository.Update<Mi01_PadronVotacion>(PadronVotacion);
            _padronVotacionRepository.Save();
        }

        void Eliminar(long idPadronVotacion)
        {
            _padronVotacionRepository.Delete<Mi01_PadronVotacion>(idPadronVotacion);
            _padronVotacionRepository.Save();
        }
        #endregion


    }
}
