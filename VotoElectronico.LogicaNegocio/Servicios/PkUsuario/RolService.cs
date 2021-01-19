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
    public class RolService : IRolService
    {

        private readonly IRolRepository _RolRepository;
        private IApiResponseMessage _utilitarios;
        private readonly ISesionService _sesionService;

        public RolService(IRolRepository RolRepository, IApiResponseMessage utilitarios, ISesionService sesionService)
        {
            _RolRepository = RolRepository;
            _utilitarios = utilitarios;
            _sesionService = sesionService;
        }

        #region Métodos Publicos

        public DtoApiResponseMessage ObtenerRolsPorNombre(string nombreRol, string estado = "")
        {
            var dtoMapeado = MapearListaEntidadADtoRol(ObtenerEntidadesRolesPorNombre(nombreRol, estado));
            if (dtoMapeado!= null)
            {
                return _utilitarios.CrearDtoApiResponseMessage(dtoMapeado, "VE_SEG_CAR_005");
            }
            else
            {
                return _utilitarios.CrearDtoApiResponseMessage(dtoMapeado, "VE_SEG_CAR_006");
            }

        }
        public IEnumerable<Sg07_Rol> ObtenerEntidadesRolesPorNombre(string nombre, string estado = "")
            => _RolRepository.FiltrarRoles(new Sg07_RolCondicional().FiltraarRoles(nombre, estado));
        public Sg07_Rol ObtenerRolPorNombre(string nombre)
           => _RolRepository.GetOneOrDefault<Sg07_Rol>(x => x.NombreRol.Equals(nombre) && x.Estado.Equals(Auditoria.EstadoActivo));



        public DtoApiResponseMessage ObtenerRolMedianteId(DtoRol dto)
        {
            var Rol = ObtenerRolId(dto.Id);

            if (Rol != null)
            {
                var dtoMapeado = MapearEntidadADto(Rol);
                return _utilitarios.CrearDtoApiResponseMessage(dtoMapeado, "VE_SEG_CAR_004");
            }
            else
            {
                return _utilitarios.CrearDtoApiResponseMessage(null, "VE_SEG_CAR_007");
            }
        }



        public DtoApiResponseMessage CrearRol(DtoRol dto, string token)
        {
            var Rol = MapearDtoAEntidad(dto, token);
            Crear(Rol);
            var dtoMapeado = MapearEntidadADto(Rol);
            return _utilitarios.CrearDtoApiResponseMessage(dtoMapeado, "VE_SEG_CAR_001");
        }

        public DtoApiResponseMessage ActualizarRol(DtoRol dto, string token)
        {
            var Rol = ObtenerRolId(dto.Id);

            if (Rol != null)
            {
                Rol.NombreRol = dto.NombreRol;
                Rol.FechaModificacion = DateTime.Now;
                Rol.UsuarioModificacion = _sesionService.ObtenerUsuarioPorToken(token)?.NombreUsuario;
                Actualizar(Rol);
                var dtoMapeado = MapearEntidadADto(Rol);
                return _utilitarios.CrearDtoApiResponseMessage(dtoMapeado, "VE_SEG_CAR_002");
            }
            else
            {
                return _utilitarios.CrearDtoApiResponseMessage(null, "VE_SEG_CAR_007");
            }


        }


        public DtoApiResponseMessage EliminarRol(DtoRol dto)
        {
            var Rol = ObtenerRolId(dto.Id);
            if (Rol != null)
            {
                Eliminar(Rol.Id);
                var dtoMapeado = MapearEntidadADto(Rol);
                return _utilitarios.CrearDtoApiResponseMessage(dtoMapeado, "VE_SEG_CAR_007");
            }
            else
            {
                return _utilitarios.CrearDtoApiResponseMessage(null, "VE_SEG_CAR_007");
            }

        }

        public void HabilitarRol(DtoRol dto)
        {
            var Rol = ObtenerRolId(dto.Id);
            Rol.Estado = Auditoria.EstadoActivo;
            Actualizar(Rol);
        }

        public void InhabilitarRol(DtoRol dto)
        {
            var Rol = ObtenerRolId(dto.Id);
            Rol.Estado = Auditoria.EstadoInactivo;
            Actualizar(Rol);
        }

        public bool ExisteRol(string nombreRol)
            => _RolRepository.GetExists<Sg07_Rol>(x => x.Estado.Equals(Auditoria.EstadoActivo) && x.NombreRol.Equals(nombreRol));






        #endregion

        #region Métodos Privados
        Sg07_Rol ObtenerRolId(long id)
        => _RolRepository.GetById<Sg07_Rol>(id);

        Sg07_Rol MapearDtoAEntidad(DtoRol dto, string token)
            => new Sg07_Rol()
            {
                NombreRol = dto.NombreRol,
                Estado = dto.Estado,
                UsuarioCreacion = _sesionService.ObtenerUsuarioPorToken(token)?.NombreUsuario,
                FechaCreacion = DateTime.Now,
            };

        IEnumerable<DtoRol> MapearEntidadADto(Sg07_Rol Rol)
        {
            DtoRol dto = new DtoRol();
            dto.Id = Rol.Id; ;
            dto.NombreRol = Rol.NombreRol;
            dto.UsuarioCreacion = Rol.UsuarioCreacion;
            dto.UsuarioModificacion = Rol.UsuarioModificacion;
            dto.Estado = Rol.Estado;

            List<DtoRol> lista = new List<DtoRol>();
            lista.Add(dto);
            return lista;
        }

        IEnumerable<DtoRol> MapearListaEntidadADtoRol(IEnumerable<Sg07_Rol> Rols)
        {
            return Rols == null ?null: Rols.Select(x => new DtoRol()
            {
                Id = x.Id,
                NombreRol = x.NombreRol,
                UsuarioCreacion = x.UsuarioCreacion,
                UsuarioModificacion = x.UsuarioModificacion,
                Estado = x.Estado

            });
        }


        void Crear(Sg07_Rol Rol)
        {
            _RolRepository.Create<Sg07_Rol>(Rol);
            _RolRepository.Save();
        }

        void Actualizar(Sg07_Rol Rol)
        {
            _RolRepository.Update<Sg07_Rol>(Rol);
            _RolRepository.Save();
        }

        void Eliminar(long idRol)
        {
            _RolRepository.Delete<Sg07_Rol>(idRol);
            _RolRepository.Save();
        }
        #endregion


    }
}
