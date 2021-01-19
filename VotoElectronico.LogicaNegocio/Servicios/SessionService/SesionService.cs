using EcVotoElectronico.Repositorios;
using System;
using System.Collections.Generic;
using System.Configuration;
using VotoElectronico.Api.Controllers;
using VotoElectronico.Entidades.Pk.PkSeguridad;
using VotoElectronico.Generico;
using VotoElectronico.Generico.Propiedades;
using VotoElectronico.Mailer;
using VotoElectronico.Util;

namespace VotoElectronico.LogicaNegocio.Servicios
{
    public class SesionService : ISesionService
    {
        #region Propiedades
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IApiResponseMessage _apiResponseMessage;
        private readonly ITokenValidator _tokenValidator;
        private readonly IEnvioEmail _envioEmail;
        #endregion


        #region Constructor
        public SesionService(IUsuarioRepository usuarioRepository, IApiResponseMessage apiResponseMessage, ITokenValidator tokenValidator, IEnvioEmail envioEmail)
        {
            _usuarioRepository = usuarioRepository;
            _apiResponseMessage = apiResponseMessage;
            _envioEmail = envioEmail;
            _tokenValidator = tokenValidator;
        }
        #endregion

        #region Métodos Públicos
        public void AniadirTokenUsuario(long usuarioId, string token)
        {
            var usuarioEncontrado = _usuarioRepository.GetById<Sg01_Usuario>(usuarioId);
            usuarioEncontrado.Token = token;
            AgregarCamposAuditoriaActualizacion(usuarioEncontrado);
            ActualizarUsuario(usuarioEncontrado);
        }

        public DtoApiResponseMessage CerrarSesion(string nombreUsuario, string token)
        {
            var esNull = string.IsNullOrEmpty(token);
            var user = esNull ? _usuarioRepository.GetOneOrDefault<Sg01_Usuario>(x => x.NombreUsuario.Equals(nombreUsuario)) :
                _usuarioRepository.GetOneOrDefault<Sg01_Usuario>(x => x.Token.Equals(token));

            if (user != null)
            {
                user.InicioSesion = false;
                user.Token = null;
                AgregarCamposAuditoriaActualizacion(user);
                ActualizarUsuario(user);
                return _apiResponseMessage.CrearDtoApiResponseMessage(null, "VE_SSN_INS_001");
            }
            throw new Exception("Unauthorized");
        }


        public DtoApiResponseMessage CambioClave(string tokenCambioClave, string nuevaClave)
        {
            if (!string.IsNullOrEmpty(tokenCambioClave))
            {
                if (string.IsNullOrEmpty(nuevaClave))
                    throw new Exception("Clave no válida");

                var usuario = _usuarioRepository.GetOneOrDefault<Sg01_Usuario>(x => x.Estado.Equals(Auditoria.EstadoActivo) && x.TokenCambioClave.Equals(tokenCambioClave));
                if (usuario != null && _tokenValidator.ExisteTokenUsuarioCambioClave(tokenCambioClave))
                {
                    var claveEncriptada = UtilEncodeDecode.Sha256Hash(UtilEncodeDecode.Base64Decode(nuevaClave));
                    if (usuario.Clave == claveEncriptada)
                        return _apiResponseMessage.CrearDtoApiResponseMessage(new List<DtoGenerico>() { new DtoGenerico() { Bdt1 = false } }, "VE_SSN_INS_002");
                    usuario.Clave = claveEncriptada;
                    usuario.EnvioEmailActivacion = true;
                    usuario.FechaModificacion = DateTime.Now;
                    usuario.UsuarioModificacion = usuario.NombreUsuario;
                    usuario.InicioSesion = false;
                    usuario.Token = null;
                    usuario.TokenCambioClave = null;
                    ActualizarUsuario(usuario);
                    return _apiResponseMessage.CrearDtoApiResponseMessage(new List<DtoGenerico>() { new DtoGenerico() { Bdt1 = true } }, "VE_SSN_INS_003");
                }
            }

            throw new Exception("Permisos insuficientes");

        }


        public DtoApiResponseMessage SolicitudCambioClave(string email)
        {
            var usuario = _usuarioRepository.GetOneOrDefault<Sg01_Usuario>(x => x.Estado.Equals(Auditoria.EstadoActivo) && x.Persona.Email.Equals(email));
            if (usuario != null)
            {
                usuario.TokenCambioClave = TokenGenerator.GenerateTokenJwt(usuario.NombreUsuario, DateTime.Now.ToShortTimeString());
                usuario.FechaModificacion = DateTime.Now;
                ActualizarUsuario(usuario);
                // Task.Factory.StartNew(() => 
                EnvioEmailUsuarioCambioClave(usuario);
                //);
                return _apiResponseMessage.CrearDtoApiResponseMessage(null, "VE_SSN_INS_004");
            }
            throw new Exception("Usuario no existe");
        }

        void EnvioEmailUsuarioCambioClave(Sg01_Usuario usuario)
        {
            if (usuario != null)
            {
                var datos = new Dictionary<string, string>
                    {
                        { "0",$"{usuario.Persona?.NombreUno} {usuario.Persona?.ApellidoUno}"},
                        { "1", $"{ConfigurationManager.AppSettings["dominio"]}/sessions/cambioclave?tkn={usuario.TokenCambioClave}" },
                    };
                _envioEmail.EnviarEmail(usuario.Persona?.Email, "EVOTE EPN - cambio contraseña", _envioEmail.CambioClaveUsuario(datos));
            }
        }



        public Sg01_Usuario ObtenerUsuarioPorToken(string token)
            => _usuarioRepository.GetOneOrDefault<Sg01_Usuario>(x => x.Token.Equals(token));


        #endregion

        #region Métodos Privados
        void AgregarCamposAuditoriaActualizacion(Sg01_Usuario usuarioEncontrado)
        {
            usuarioEncontrado.UsuarioModificacion = usuarioEncontrado.NombreUsuario;
            usuarioEncontrado.FechaModificacion = DateTime.Now;

        }
        void ActualizarUsuario(Sg01_Usuario usuarioEncontrado)
        {
            _usuarioRepository.Update(usuarioEncontrado);
            _usuarioRepository.Save();
        }

        #endregion

    }
}
