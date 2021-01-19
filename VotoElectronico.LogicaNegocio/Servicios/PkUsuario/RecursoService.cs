using EcVotoElectronico.Repositorios;
using System.Collections.Generic;
using System.Linq;
using VotoElectronico.Entidades.Pk.PkSeguridad;
using VotoElectronico.Generico;
using VotoElectronico.Generico.Configs;
using VotoElectronico.Generico.Enumeraciones;
using VotoElectronico.Generico.Propiedades;
using VotoElectronico.Util;

namespace VotoElectronico.LogicaNegocio.Servicios
{
    public class RecursoService : IRecursoService
    {

        private readonly IRecursoRepository _RecursoRepository;
        private IApiResponseMessage _apiResponseMessage;
        private ISesionService _sesionService;

        public RecursoService(IRecursoRepository RecursoRepository, IApiResponseMessage apiResponseMessage, ISesionService sesionService)
        {
            _RecursoRepository = RecursoRepository;
            _apiResponseMessage = apiResponseMessage;
            _sesionService = sesionService;
        }

        public DtoApiResponseMessage ObtenerMenuUsuarioPorToken(string token)
        {
            var dtoMapeado = ObtenerMenuUsuario(token);
            if (dtoMapeado.Count() != 0)
                return _apiResponseMessage.CrearDtoApiResponseMessage(dtoMapeado, "VE_PEL_LIS_005");
            else
                return _apiResponseMessage.CrearDtoApiResponseMessage(dtoMapeado, "VE_PEL_LIS_006");
        }




        IEnumerable<Sg06_Recurso> ObtenerRecusosPadresUsuarioToken(string token)
        {
            var usuario = _sesionService.ObtenerUsuarioPorToken(token);
            var CodigoMenu = Enumeration.ObtenerDescripcion(TipoRecurso.Menu);
            var respuestaRecurso = new List<Sg06_Recurso>();
            if (usuario != null)
            {
                var rolesUsuario = usuario.UsuariosRol;
                if (rolesUsuario.Any())
                {
                    rolesUsuario.ToList().ForEach(
                        rolUsuario =>
                        {
                            var recursosRoles = rolUsuario.Rol?.Permisos?.Where(y => y.Recurso.CodigoRecurso.Equals(CodigoMenu) && y.Recurso.Estado.Equals(Auditoria.EstadoActivo) && y.Estado.Equals(Auditoria.EstadoActivo));
                            if (recursosRoles?.Any() ?? false)
                            {
                                recursosRoles.GroupBy(x => new { x.RecursoId, x.RolId }).ToList().ForEach(recursoRolGrupo =>
                                {
                                    var listaRecusoRol = recursoRolGrupo.ToList();
                                    listaRecusoRol.ForEach(x =>
                                    {
                                        if (x.Recurso?.RecursoId == null)
                                            respuestaRecurso.Add(x.Recurso);
                                    });
                                });
                            }
                        });
                }
            }
            return respuestaRecurso?.OrderBy(x => x.Orden);
        }

        IEnumerable<DtoRecurso> ObtenerMenuUsuario(string token)
        {
            var listaMenu = new List<DtoRecurso>();
            var recursosPadresUsuario = ObtenerRecusosPadresUsuarioToken(token);
            if (recursosPadresUsuario != null)
            {
                foreach (var recursoUsuario in recursosPadresUsuario)
                    listaMenu.Add(ConvertirEntidadGenerico(recursoUsuario));
            }
            return listaMenu;
        }

        DtoRecurso ConvertirEntidadGenerico(Sg06_Recurso recurso)
        {
            var CodigoMenu = Enumeration.ObtenerDescripcion(TipoRecurso.Menu);
            var recursosHijos = _RecursoRepository.Get<Sg06_Recurso>(x => x.CodigoRecurso.Equals(CodigoMenu) && x.Estado.Equals(Auditoria.EstadoActivo) && x.RecursoId == recurso.Id)?.OrderBy(x => x.Orden);
            var recursoRespuesta = new DtoRecurso()
            {
                nombreRecurso = recurso.NombreRecurso,
                RutaMenu = recurso.RutaMenu,
                RecursosHijo = recursosHijos?.ToList()?.Select(x => ConvertirEntidadGenerico(x))
            };
            if (recurso.RecursoId == null)
            {
                recursoRespuesta.ToolTip = recurso.NombreRecurso;
                recursoRespuesta.Tipo = "dropDown";
                recursoRespuesta.Icono = recurso.Icono;
            }
            return recursoRespuesta;
        }
    }
}
