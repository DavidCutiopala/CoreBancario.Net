﻿using EcVotoElectronico.Repositorios;
using System;
using VotoElectronico.Entidades.Pk.PkSeguridad;
using VotoElectronico.Generico;
using VotoElectronico.Generico.Propiedades;
using VotoElectronico.Util;

namespace VotoElectronico.LogicaNegocio.Servicios.PkUsuario
{
    public class LoginService : ILoginService
    {

        private readonly IUsuarioRepository _usuarioRepository;
        private IUtilitarios _util;

        public LoginService(IUsuarioRepository usuarioRepository, IUtilitarios util)
        {
            _usuarioRepository = usuarioRepository;
            _util = util;
        }

        #region Métodos Publicos

        public DtoGenerico IniciarSesion(string nombreUsuario, string clave)
        {
            var claveEncoded = UtilEncodeDecode.Sha256Hash(UtilEncodeDecode.Base64Decode(clave));
            var usuario = UsuarioValidoCredenciales(nombreUsuario, claveEncoded);
            if (usuario != null)
            {
                var estadoAutenticacion = ValidarUsuarioActivado(usuario);
                if (estadoAutenticacion.Bdt1) estadoAutenticacion = ValidarUsuarioSesionesAbiertas(usuario);
                if (estadoAutenticacion.Bdt1) AutenticarUsuario(usuario);
                return MapearUsuarioAutenticado(usuario);
            }
            return new DtoGenerico() { Dt1 = "Credenciales Inválidas", Bdt1 = false };
        }




        #endregion

        #region Métodos Privados
        void AutenticarUsuario(Sg01_Usuario usuario) {
          //  usuario.InicioSesion = true;
            usuario.FechaModificacion = DateTime.Now;
            _usuarioRepository.Update(usuario);
            _usuarioRepository.Save();
        }
        DtoGenerico MapearUsuarioAutenticado(Sg01_Usuario usuario)
        => new DtoGenerico()
        {
            Id = usuario?.Id ?? 0,
            Dt1= usuario?.NombreUsuario,

        };
        

        DtoGenerico ValidarUsuarioActivado(Sg01_Usuario usuario)
            => new DtoGenerico() { Dt1 = (bool)usuario?.EnvioEmailActivacion? "":"Usuario no habilitado", Bdt1 = (bool)usuario?.EnvioEmailActivacion };

        DtoGenerico ValidarUsuarioSesionesAbiertas(Sg01_Usuario usuario)
        => new DtoGenerico() { Dt1 = /*(bool)usuario?.InicioSesion?  "" :*/ "Inició Sesión en otro lugar", Bdt1 = false/*!(bool)usuario?.InicioSesion*/};



        Sg01_Usuario UsuarioValidoCredenciales(string usuario, string claveEncriptada)
        {
            var user = _usuarioRepository.GetOneOrDefault<Sg01_Usuario>(x => x.Estado.Equals(Auditoria.EstadoActivo) && x.NombreUsuario.Equals(usuario));
                return user != null? user.Clave.Equals(claveEncriptada)? user:null:null;
        }
        #endregion


    }
}
