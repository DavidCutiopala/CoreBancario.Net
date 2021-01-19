using EcVotoElectronico.Repositorios;
using System;
using VotoElectronico.Entidades.Pk.PkSeguridad;
using VotoElectronico.Generico;
using VotoElectronico.Generico.Propiedades;

namespace VotoElectronico.LogicaNegocio.Servicios
{
    public class UsuarioRolService : IUsuarioRolService
    {

        private readonly IUsuarioRolRepository _usuarioRolRepository;
        private readonly ISesionService _sesionService;
        private readonly IRolRepository _rolRepository;

        public UsuarioRolService(IUsuarioRolRepository usuarioRolRepository, ISesionService sesionService)
        {
            _usuarioRolRepository = usuarioRolRepository;
            _sesionService = sesionService;
         
        }

        #region Métodos Publicos


        public void InsertarUsuarioRol(DtoUsuarioRol dto, string token)
        {
            var usuarioRol = MapearDtoAEntidad(dto, token);
            Crear(usuarioRol);
        }


        public void ValidarInsertarUsuarioRol(DtoUsuarioRol dto, string token)
        {
  
            var userRol = _usuarioRolRepository.GetOneOrDefault<Sg08_UsuarioRol>(x => x.RolId == dto.RolId && x.UsuarioId == dto.UsuarioId);
            if (userRol != null)
            {
                if (userRol.Estado.Equals(Auditoria.EstadoInactivo))
                {
                    userRol.UsuarioModificacion = _sesionService.ObtenerUsuarioPorToken(token)?.NombreUsuario;
                    ReactivarUsuarioRol(userRol);
                }
            }
            else
                InsertarUsuarioRol(dto, token);
        }




        public void ReactivarUsuarioRol(Sg08_UsuarioRol usuarioRol)
        {
            usuarioRol.Estado = Auditoria.EstadoActivo;
            Actualizar(usuarioRol); 
        }


        //Obtener el registro de usuarioRol mediante token de sesion

        public long obtenerRolIdbyToken(string token)
        {
            var usuarioRol = _usuarioRolRepository.GetOneOrDefault<Sg08_UsuarioRol>(usuarioRol => usuarioRol.Usuario.Token.Equals(token));

            return usuarioRol != null ? usuarioRol.RolId : 0;
        }

        #endregion

        #region Métodos Privados

        Sg08_UsuarioRol MapearDtoAEntidad(DtoUsuarioRol dto, string token)
            => new Sg08_UsuarioRol()
            {
                RolId = dto.RolId,
                UsuarioId = dto.UsuarioId,
                Estado = Auditoria.EstadoActivo,
                UsuarioCreacion = _sesionService.ObtenerUsuarioPorToken(token)?.NombreUsuario,
                FechaCreacion = DateTime.Now,
            };

        


        void Crear(Sg08_UsuarioRol usuarioRol)
        {
            _usuarioRolRepository.Create<Sg08_UsuarioRol>(usuarioRol);
            _usuarioRolRepository.Save();
        }

        void Actualizar(Sg08_UsuarioRol usuarioRol)
        {
            _usuarioRolRepository.Update<Sg08_UsuarioRol>(usuarioRol);
            _usuarioRolRepository.Save();
        }

        void Eliminar(long idusuarioRol)
        {
            _usuarioRolRepository.Delete<Sg08_UsuarioRol>(idusuarioRol);
            _usuarioRolRepository.Save();
        }
        #endregion


    }
}
