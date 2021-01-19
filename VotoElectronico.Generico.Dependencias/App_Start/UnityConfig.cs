using EcVotoElectronico.Repositorios;
using System.Net.Http.Headers;
using System.Web.Http;
using Unity;
using Unity.WebApi;
using VotoElectronico.AccesoDatos.Repositorio;
using VotoElectronico.AccesoDatos.Repositorios;
using VotoElectronico.AccesoDatos.Repositorios.General;
using VotoElectronico.Importaciones.Importaciones.Padron;
using VotoElectronico.LogicaNegocio.Servicios;
using VotoElectronico.LogicaNegocio.Servicios.GoogleDrive;
using VotoElectronico.Mailer;
using VotoElectronico.Util;

namespace VotoElectronico.Generico.Dependencias
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {

            var contenedor = new UnityContainer();
            #region Utilitarios
            contenedor.RegisterType<IApiResponseMessage, ApiResponseMessage >();
            #endregion

            #region repositorios
            contenedor.RegisterType<IUsuarioRepository, UsuarioRepository>();
            contenedor.RegisterType<IMensajeRepository, MensajeRepository>();
            contenedor.RegisterType<IPersonaRepository, PersonaRepository>();
            contenedor.RegisterType<ICargoRepository, CargoRepository>();
            contenedor.RegisterType<IUsuarioCargoRepository, UsuarioCargoRepository>();
            contenedor.RegisterType<ITipoEleccionRepository, TipoEleccionRepository>();
            contenedor.RegisterType<IEleccionRepository, EleccionRepository>();
            contenedor.RegisterType<IEscanioRepository, EscanioRepository>();
            contenedor.RegisterType<IProcesoElectoralRepository, ProcesoElectoralRepository>(); 
            contenedor.RegisterType<IListaRepository, ListaRepository>();
            contenedor.RegisterType<ICandidatoRepository, CandidatoRepository>();
            contenedor.RegisterType<IPadronVotacionRepository, PadronVotacionRepository>();
            contenedor.RegisterType<IGoogleDriveFilesRepository, GoogleDriveFilesRepository>();
            contenedor.RegisterType<IPermisoRepository, PermisoRepository>();
            contenedor.RegisterType<IUsuarioRolRepository, UsuarioRolRepository>();
            contenedor.RegisterType<IRolRepository, RolRepository>();
            contenedor.RegisterType<IRecursoRepository, RecursoRepository>();
            contenedor.RegisterType<IUsuarioCoreBRepository, UsuarioCoreBancarioRepository>();



            #endregion


            #region repositorios
            contenedor.RegisterType<IUsuarioRepository, UsuarioRepository>();
            contenedor.RegisterType<IMensajeRepository, MensajeRepository>();
            contenedor.RegisterType<IVotoRepository, VotoRepository>();
            contenedor.RegisterType<IOpcionRepository, OpcionRepository>();
            contenedor.RegisterType<IClienteRepository, ClienteRepository>();
            contenedor.RegisterType<IMovimientoRepository, MovimientoRepository>();
            contenedor.RegisterType<IUsuarioCoreBRepository, UsuarioCoreBancarioRepository>();
            contenedor.RegisterType<ICuentasRepository, CuentaRepository>();


            #endregion

            #region services
            contenedor.RegisterType<IUsuarioService, UsuarioService>();
            contenedor.RegisterType<IPersonaService, PersonaService>();
            contenedor.RegisterType<ICargoService, CargoService>();
            contenedor.RegisterType<IUsuarioCargoService, UsuarioCargoService >();
            contenedor.RegisterType<ITipoEleccionService, TipoEleccionService >();
            contenedor.RegisterType<IEleccionService, EleccionService >();
            contenedor.RegisterType<IEscanioService, EscanioService>();
            contenedor.RegisterType<IProcesoElectoralService, ProcesoElectoralService>();
            contenedor.RegisterType<IListaService, ListaService>();
            contenedor.RegisterType<ICandidatoService, CandidatoService>();
            contenedor.RegisterType<ISesionService, SesionService>();
            contenedor.RegisterType<ILoginService, LoginService>();
            contenedor.RegisterType<IPadronVotacionService, PadronVotacionService>();
            contenedor.RegisterType<IGoogleDriveService, GoogleDriveService>();
            contenedor.RegisterType<IImportacionPadron, ImportacionPadron>();
            contenedor.RegisterType<IPermisoService, PermisoService>();
            contenedor.RegisterType<IRolService, RolService>();
            contenedor.RegisterType<IUsuarioRolService, UsuarioRolService>();
            contenedor.RegisterType<IRecursoService, RecursoService>();
            contenedor.RegisterType<IAesService, AesService>();
            contenedor.RegisterType<IVotoService, VotoService>();
            contenedor.RegisterType<IOpcionService, OpcionService>();
            contenedor.RegisterType<IRsaHelper, RsaHelper>();
            contenedor.RegisterType<IMesaVotoService, MesaVotoService>();
            contenedor.RegisterType<IClienteCBService, ClienteCBService>();
            contenedor.RegisterType<IMovimientosService, MovimientoService>();
            contenedor.RegisterType<ICuentaService, CuentaService>();
            contenedor.RegisterType<IUsuarioCBService, UsuarioCBService>();
            #endregion

            #region token
            contenedor.RegisterType<ITokenValidator, TokenValidator>();
            #endregion


            #region Mailing
            contenedor.RegisterType<IEnvioEmail, EnvioEmail>();
             
            #endregion
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(contenedor);

        }
    }
}