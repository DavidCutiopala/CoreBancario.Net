using EcVotoElectronico.Repositorios;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using VotoElectronico.Api.Controllers;
using VotoElectronico.Entidades.Pk.PkSeguridad;
using VotoElectronico.Generico;
using VotoElectronico.Generico.Propiedades;
using VotoElectronico.LogicaCondicional;
using VotoElectronico.Mailer;
using VotoElectronico.Util;

namespace VotoElectronico.LogicaNegocio.Servicios
{
    public class UsuarioService : IUsuarioService
    {

        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IPersonaRepository _personaRepository;
        private readonly ISesionService _sesionService;
        private IApiResponseMessage _apiResponseMessage;
        private readonly IEnvioEmail _envioEmail;

        public UsuarioService(IUsuarioRepository usuarioRepository, IApiResponseMessage apiResponseMessage, ISesionService sesionService, IEnvioEmail envioEmail, IPersonaRepository personaRepository)
        {
            _usuarioRepository = usuarioRepository;
            _personaRepository = personaRepository;
            _apiResponseMessage = apiResponseMessage;
            _envioEmail = envioEmail;
            _sesionService = sesionService;
        }

        #region Métodos Publicos

        public DtoApiResponseMessage ObtenerUsuariosPorNombreOIdentificacion(string nombre, string identificacion)
        {
            var spec = new Sg01_UsuarioCondicional().FiltrarUsuario(nombre, identificacion);
            var dtoMapeado = MapearListaEntidadGenerico(_usuarioRepository.FiltrarUsuario(spec));

            if (dtoMapeado.Count() != 0)
            {
                return _apiResponseMessage.CrearDtoApiResponseMessage(dtoMapeado, "VE_SEG_USR_005");
            }
            else
            {
                return _apiResponseMessage.CrearDtoApiResponseMessage(dtoMapeado, "VE_SEG_USR_006");
            }
            
        }


        public DtoApiResponseMessage ObtenerUsuarioMedianteId(DtoUsuario dto)
        {
            var usuario = ObtenerUsuarioId(dto.Id);
            var dtoMapeado = mapearEntidadADto(usuario);
            return _apiResponseMessage.CrearDtoApiResponseMessage(dtoMapeado, "VE_SEG_USR_004");
        }

        public DtoApiResponseMessage CrearUsuario(DtoUsuario dto, string token)
            => _apiResponseMessage.CrearDtoApiResponseMessage(mapearEntidadADto(InsertarUsuario(dto, token, false)), "VE_SEG_USR_001");

        public Sg01_Usuario InsertarUsuario(DtoUsuario dto, string token, bool envioEmail)
        {
           
            var usuario = mapearDtoAEntidad(dto, token);
            if(envioEmail)
                usuario.TokenCambioClave = TokenGenerator.GenerateTokenJwt(usuario.NombreUsuario, DateTime.Now.ToShortTimeString());

            Crear(usuario);
            if (envioEmail)
            {
                // Task.Factory.StartNew(() => 
                EnvioEmailUsuario(usuario,_personaRepository.GetOneOrDefault<Sg02_Persona>(x=> x.Id == usuario.PersonaId));
                //);
            }
            return usuario;
        }

        public Sg01_Usuario ValidarInsertarUsuario(DtoUsuario dto, string token)
        {
            var usuario = _usuarioRepository.GetOneOrDefault<Sg01_Usuario>(x => x.NombreUsuario.Equals(dto.nombreUsuario));
            if (usuario != null)
            {
                if (usuario.Estado.Equals(Auditoria.EstadoInactivo))
                {
                    usuario.UsuarioModificacion = _sesionService.ObtenerUsuarioPorToken(token)?.NombreUsuario;
                    ReactivarUsuario(usuario.Id);
                }
                return usuario;
            }
            else
                return InsertarUsuario(dto, token, envioEmail: true);
        }

  

        public DtoApiResponseMessage ActualizarUsuario(DtoUsuario dto)
        {
            var usuario = ObtenerUsuarioId(dto.Id);
            //usuario.Email = dto.Email;
            usuario.EnvioEmailActivacion = dto.EnvioEmailActivacion;
            usuario.Clave = dto.Clave;
            usuario.NombreUsuario = dto.nombreUsuario;
            usuario.Estado = dto.estado;
            usuario.UsuarioModificacion = dto.usuarioModificacion;
            usuario.FechaModificacion = DateTime.Now;
            usuario.Token = dto.Token;
            usuario.PersonaId = dto.PersonaId;
            Actualizar(usuario);

            var dtoMapeado = mapearEntidadADto(usuario);
            return _apiResponseMessage.CrearDtoApiResponseMessage(dtoMapeado, "VE_SEG_USR_002");
        }




        //public void EliminarUsuario(DtoUsuario dto)
        //{
        //    //var usuario = ObtenerUsuarioId(id);
        //    //usuario.Estado = "INACTIVO";
        //    //Actualizar(usuario);
        //    //_usuarioRepository.Delete<Sg01_Usuario>(dto.Id);


        public DtoApiResponseMessage EliminarUsuario(DtoUsuario dto)
        {
            var usuario = ObtenerUsuarioId(dto.Id);
            _usuarioRepository.Delete<Sg01_Usuario>(usuario.Id);
            _usuarioRepository.Save();

            var dtoMapeado = mapearEntidadADto(usuario);
            return _apiResponseMessage.CrearDtoApiResponseMessage(dtoMapeado, "VE_SEG_USR_003");
        }

        public void ReactivarUsuario(long id)
        {
            var usuario = ObtenerUsuarioId(id);
            usuario.Estado = "ACTIVO";
            Actualizar(usuario);
        }


        public void EnvioEmailUsuario(Sg01_Usuario usuario, Sg02_Persona persona)
        {
            if (usuario != null)
            {
                var datos = new Dictionary<string, string>
                    {
                        { "0",$"{persona?.NombreUno} {persona?.ApellidoUno}"},
                        { "1", $"{ConfigurationManager.AppSettings["dominio"]}/sessions/cambioclave?tkn={usuario.TokenCambioClave}" },
                    };
                _envioEmail.EnviarEmail(persona.Email,"EVOTE EPN - nuevo usuario",_envioEmail.ActivarUsuarioGenerico(datos));
            }
        }

        #endregion

        #region Métodos Privados
        Sg01_Usuario ObtenerUsuarioId(long id)
        => _usuarioRepository.GetById<Sg01_Usuario>(id);

        Sg01_Usuario mapearDtoAEntidad(DtoUsuario dto, string token)
            => new Sg01_Usuario()
            {
                NombreUsuario = dto.nombreUsuario,
                //Email = dto.Email,
                Clave = dto.Clave,
                PersonaId = (long)dto.PersonaId,
                Estado = Auditoria.EstadoActivo,
                UsuarioCreacion = _sesionService.ObtenerUsuarioPorToken(token)?.NombreUsuario,
                FechaCreacion = DateTime.Now,
            };

        IEnumerable<DtoUsuario> mapearEntidadADto(Sg01_Usuario usuario)
        {
            DtoUsuario dto = new DtoUsuario();
            dto.Id = usuario.Id;
            //dto.Email = usuario.Email;
            dto.EnvioEmailActivacion = usuario.EnvioEmailActivacion;
            dto.Clave = usuario.Clave;
            dto.nombreUsuario = usuario.NombreUsuario;
            dto.usuarioCreacion = usuario.UsuarioCreacion;
            if (usuario.FechaCreacion != null)
            {
                dto.fechaCreacion = (DateTime)usuario.FechaCreacion;
            }
            dto.usuarioModificacion = usuario.UsuarioModificacion;
            if (usuario.FechaModificacion != null)
            {
                dto.fechaModificacion = (DateTime)usuario.FechaModificacion;
            }
            dto.Token = usuario.Token;
            dto.PersonaId = usuario.PersonaId;

            List<DtoUsuario> lista = new List<DtoUsuario>();
            lista.Add(dto);
            return lista;
        }

        IEnumerable<DtoUsuario> MapearListaEntidadGenerico(IEnumerable<Sg01_Usuario> usuario)
        {
            return usuario.Select(x => new DtoUsuario()
            {
                nombreUsuario = x.NombreUsuario,
                //Email = x.Email
            });
        }


        void Crear(Sg01_Usuario usuario)
        {
            _usuarioRepository.Create<Sg01_Usuario>(usuario);
            _usuarioRepository.Save();
        }

        void Actualizar(Sg01_Usuario usuario)
        {
            _usuarioRepository.Update<Sg01_Usuario>(usuario);
            _usuarioRepository.Save();
        }

        void Eliminar(Sg01_Usuario usuario)
        {
            _usuarioRepository.Delete<Sg01_Usuario>(usuario.Id);
            _usuarioRepository.Save();
        }
        #endregion


    }
}
