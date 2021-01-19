using EcVotoElectronico.Repositorios;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using VotoElectronico.Entidades.Pk.PkSeguridad;
using VotoElectronico.Generico;
using VotoElectronico.Generico.Propiedades;
using VotoElectronico.LogicaCondicional;
using VotoElectronico.Util;

namespace VotoElectronico.LogicaNegocio.Servicios
{
    public class UsuarioCargoService : IUsuarioCargoService
    {

        private readonly IUsuarioCargoRepository _usuarioCargoRepository;
        private IApiResponseMessage _utilitarios;
        private ISesionService _sesionService;

        public UsuarioCargoService(IUsuarioCargoRepository usuarioCargoRepository, IApiResponseMessage utilitarios, ISesionService sesionService)
        {
            _usuarioCargoRepository = usuarioCargoRepository;
            _utilitarios = utilitarios;
            _sesionService = sesionService;
        }

        #region Métodos Publicos

        //public DtoApiResponseMessage ObtenerusuarioCargosPorNombre(string nombreusuarioCargo)
        //{
        //    var spec = new Sg04_UsuarioCargoCondicional().FiltrarusuarioCargoPorNombre(nombreusuarioCargo);
        //    var dtoMapeado = MapearListaEntidadADtoUsuarioCargo(_usuarioCargoRepository.FiltrarusuarioCargoPorNombre(spec));

        //    if (dtoMapeado.Count() != 0)
        //    {
        //        return _apiResponseMessage.crearDtoApiResponseMessage(dtoMapeado, "VE_SEG_USC_005");
        //    }
        //    else
        //    {
        //        return _apiResponseMessage.crearDtoApiResponseMessage(dtoMapeado, "VE_SEG_USC_006");
        //    }
            
        //}


        public DtoApiResponseMessage ObtenerUsuarioCargoMedianteId(DtoUsuarioCargo dto)
        {
            var usuarioCargo = ObtenerUsuarioCargoId(dto.Id);

            if (usuarioCargo != null)
            {
                var dtoMapeado = mapearEntidadADto(usuarioCargo);
                return _utilitarios.CrearDtoApiResponseMessage(dtoMapeado, "VE_SEG_USC_004");
            }
            else
            {
                return _utilitarios.CrearDtoApiResponseMessage(null, "VE_SEG_USC_007");
            }


            
        }


        public void InsertarUsuarioCargo(DtoUsuarioCargo dto, string token)
        {
            var usuarioCargo = MapearDtoAEntidad(dto, token);
            Crear(usuarioCargo);
        }


        public void ValidarInsertarUsuarioCargo(DtoUsuarioCargo dto, string token)
        {
            var userCargo = _usuarioCargoRepository.GetOneOrDefault<Sg04_UsuarioCargo>(x => x.CargoId == dto.cargoId && x.UsuarioId == dto.usuarioId);
            if (userCargo != null)
            {
                if (userCargo.Estado.Equals(Auditoria.EstadoInactivo))
                {
                    userCargo.UsuarioModificacion = _sesionService.ObtenerUsuarioPorToken(token)?.NombreUsuario;
                    ReactivarUsuarioCargo(userCargo);
                }
                InactivarOtrosCargos(dto.cargoId, dto.usuarioId, token);
            }
            else
            {
                InsertarUsuarioCargo(dto, token);
                InactivarOtrosCargos(dto.cargoId, dto.usuarioId, token);
            }
                
        }

        void InactivarOtrosCargos(long cargoActualId, long usuarioId,string token)
        {
            var otrosCargos = _usuarioCargoRepository.Get<Sg04_UsuarioCargo>(x => x.Estado.Equals(Auditoria.EstadoActivo) && x.CargoId != cargoActualId && x.UsuarioId == usuarioId);
            foreach (var cargo in otrosCargos)
            {
                InactivarActivarCargo(cargo, Auditoria.EstadoInactivo, token);
            }
        }

        public DtoApiResponseMessage ActualizarUsuarioCargo(DtoUsuarioCargo dto)
        {
            var usuarioCargo = ObtenerUsuarioCargoId(dto.Id);

            if(usuarioCargo != null)
            {
                usuarioCargo.CargoId = dto.cargoId;
                usuarioCargo.UsuarioId = dto.usuarioId;
                usuarioCargo.Estado = dto.estado;
                usuarioCargo.UsuarioModificacion = dto.usuarioModificacion;
                usuarioCargo.FechaModificacion = DateTime.Now;
                Actualizar(usuarioCargo);
                var dtoMapeado = mapearEntidadADto(usuarioCargo);
                return _utilitarios.CrearDtoApiResponseMessage(dtoMapeado, "VE_SEG_USC_002");
            }
            else
            {
                return _utilitarios.CrearDtoApiResponseMessage(null, "VE_SEG_USC_007");
            }

            
        }

        public void InactivarActivarCargo(Sg04_UsuarioCargo usuarioCargo, string estado, string token)
        {
            usuarioCargo.Estado = estado;
            usuarioCargo.UsuarioModificacion = _sesionService.ObtenerUsuarioPorToken(token)?.NombreUsuario;
            usuarioCargo.FechaModificacion = DateTime.Now;
            Actualizar(usuarioCargo);
        }

        public void HabilitarusuarioCargo(DtoUsuarioCargo dto)
        {
            var usuarioCargo = ObtenerUsuarioCargoId(dto.Id);
            ReactivarUsuarioCargo(usuarioCargo);
        }

        public void ReactivarUsuarioCargo(Sg04_UsuarioCargo usuarioCargo)
        {
            usuarioCargo.Estado = Auditoria.EstadoActivo;
            Actualizar(usuarioCargo);
        }
        public void InhabilitarusuarioCargo(DtoUsuarioCargo dto)
        {
            var usuarioCargo = ObtenerUsuarioCargoId(dto.Id);
            usuarioCargo.Estado = "INACTIVO";
            Actualizar(usuarioCargo);
        }

        //Obtener el registro de usuarioCargo mediante token de sesion

        public long obtenerCargoIdbyToken(string token)
        {
            var usuarioCargo = _usuarioCargoRepository.GetOneOrDefault<Sg04_UsuarioCargo>(usuarioCargo => usuarioCargo.Usuario.Token.Equals(token));

            return usuarioCargo != null ? usuarioCargo.CargoId : 0;
        }





        #endregion

        #region Métodos Privados
        Sg04_UsuarioCargo ObtenerUsuarioCargoId(long id)
        => _usuarioCargoRepository.GetById<Sg04_UsuarioCargo>(id);

        Sg04_UsuarioCargo MapearDtoAEntidad(DtoUsuarioCargo dto, string token)
            => new Sg04_UsuarioCargo()
            {
                CargoId = dto.cargoId,
                UsuarioId = dto.usuarioId,
                Estado = Auditoria.EstadoActivo,
                UsuarioCreacion = _sesionService.ObtenerUsuarioPorToken(token)?.NombreUsuario,
                FechaCreacion = DateTime.Now,
            };

        IEnumerable<DtoUsuarioCargo> mapearEntidadADto(Sg04_UsuarioCargo usuarioCargo)
        {
            var dto = new DtoUsuarioCargo();
            dto.Id = usuarioCargo.Id; ;
            dto.cargoId = usuarioCargo.CargoId;
            dto.usuarioId = usuarioCargo.UsuarioId;
            dto.usuarioCreacion = usuarioCargo.UsuarioCreacion;
            dto.usuarioModificacion = usuarioCargo.UsuarioModificacion;
            dto.estado = usuarioCargo.Estado;

            List<DtoUsuarioCargo> lista = new List<DtoUsuarioCargo>();
            lista.Add(dto);
            return lista;
        }

        
        

        //IEnumerable<DtoUsuarioCargo> MapearListaEntidadADtoUsuarioCargo(IEnumerable<Sg04_UsuarioCargo> usuarioCargo)
        //{
        //    return usuarioCargo.Select(x => new DtoUsuarioCargo()
        //    {
        //        Id = x.Id,
        //        nombreusuarioCargo = x.NombreusuarioCargo,
        //        usuarioCreacion = x.UsuarioCreacion,
        //        usuarioModificacion = x.UsuarioModificacion,
        //        estado = x.Estado

        //    });
        //}


        void Crear(Sg04_UsuarioCargo usuarioCargo)
        {
            _usuarioCargoRepository.Create<Sg04_UsuarioCargo>(usuarioCargo);
            _usuarioCargoRepository.Save();
        }

        void Actualizar(Sg04_UsuarioCargo usuarioCargo)
        {
            _usuarioCargoRepository.Update<Sg04_UsuarioCargo>(usuarioCargo);
            _usuarioCargoRepository.Save();
        }

        void Eliminar(long idusuarioCargo)
        {
            _usuarioCargoRepository.Delete<Sg04_UsuarioCargo>(idusuarioCargo);
            _usuarioCargoRepository.Save();
        }
        #endregion


    }
}
