using EcVotoElectronico.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using VotoElectronico.Entidades.Pk.PkProcesoElectoral;
using VotoElectronico.Generico;
using VotoElectronico.Generico.Configs;
using VotoElectronico.Generico.Enumeraciones;
using VotoElectronico.Generico.Propiedades;
using VotoElectronico.LogicaCondicional;
using VotoElectronico.Util;

namespace VotoElectronico.LogicaNegocio.Servicios
{
    public class ProcesoElectoralService : IProcesoElectoralService
    {

        private readonly IProcesoElectoralRepository _procesoElectoralRepository;
        private readonly IApiResponseMessage _apiResponseMessage;
        private readonly IListaService _listaService;
        private readonly IPadronVotacionService _padronVotacionService;
        private readonly ISesionService _sesionService;
        private readonly IRolService _rolService;

        public ProcesoElectoralService(IProcesoElectoralRepository ProcesoElectoralRepository, IApiResponseMessage utilitarios, IListaService listaService, IPadronVotacionService padronVotacionService, ISesionService sesionService, IRolService rolService)
        {
            _procesoElectoralRepository = ProcesoElectoralRepository;
            _padronVotacionService = padronVotacionService;
            _apiResponseMessage = utilitarios;
            _listaService = listaService;
            _sesionService = sesionService;
            _rolService = rolService;
        }

        #region Métodos Publicos

        public DtoApiResponseMessage ObtenerProcesoElectoralesPorNombre(string nombreProcesoElectoral, string estado, int anio)
        {
            var spec = new Pe01_ProcesoElectoralCondicional().FiltrarProcesoElectoralPorNombre(nombreProcesoElectoral, estado, anio);
            var dtoMapeado = MapearListaEntidadADtoProcesoElectoral(_procesoElectoralRepository.FiltrarProcesoElectoralEspecificacion(spec));

            if ((dtoMapeado?.Count() ?? 0) != 0)
            {
                return _apiResponseMessage.CrearDtoApiResponseMessage(dtoMapeado, "VE_PEL_PEL_005");
            }
            else
            {
                return _apiResponseMessage.CrearDtoApiResponseMessage(null, "VE_PEL_PEL_006");
            }

        }

        public DtoApiResponseMessage ObtenerProcesoElectoralesVigentesByToken(string token)
        {
            var spec = new Pe01_ProcesoElectoralCondicional().FiltrarEleccionesVigentesByToken(token);
            var dtoMapeado = MapearListaEntidadADtoProcesoElectoral(_procesoElectoralRepository.FiltrarProcesoElectoralEspecificacion(spec));

            if ((dtoMapeado?.Count() ?? 0) != 0)
            {
                return _apiResponseMessage.CrearDtoApiResponseMessage(dtoMapeado, "VE_PEL_PEL_005");
            }
            else
            {
                return _apiResponseMessage.CrearDtoApiResponseMessage(null, "VE_PEL_PEL_006");
            }

        }
        public DtoApiResponseMessage ObtenerTodosProcesosElectoralesActivos()
        {
            var dtoMapeado = MapearListaEntidadADtoProcesoElectoral(_procesoElectoralRepository.Get<Pe01_ProcesoElectoral>(x => x.Estado.Equals(Auditoria.EstadoActivo))?.OrderByDescending(y => y.FechaCreacion));
            if ((dtoMapeado?.Count() ?? 0) != 0)
                return _apiResponseMessage.CrearDtoApiResponseMessage(dtoMapeado, "VE_PEL_PEL_005");
            return _apiResponseMessage.CrearDtoApiResponseMessage(null, "VE_PEL_PEL_006");

        }




        public DtoApiResponseMessage ObtenerProcesoElectoralMedianteId(DtoProcesoElectoral dto)
        {
            var ProcesoElectoral = ObtenerProcesoElectoralId(dto.Id);

            if (ProcesoElectoral != null)
            {
                var dtoMapeado = mapearEntidadADto(ProcesoElectoral);
                return _apiResponseMessage.CrearDtoApiResponseMessage(dtoMapeado, "VE_PEL_PEL_004");
            }
            else
            {
                return _apiResponseMessage.CrearDtoApiResponseMessage(null, "VE_PEL_PEL_007");
            }
        }

        public DtoApiResponseMessage CrearProcesoElectoral(DtoProcesoElectoral dto)
        {
            var ProcesoElectoral = mapearDtoAEntidad(dto);
            Crear(ProcesoElectoral);
            var dtoMapeado = mapearEntidadADto(ProcesoElectoral);
            return _apiResponseMessage.CrearDtoApiResponseMessage(dtoMapeado, "VE_PEL_PEL_001");
        }

        public DtoApiResponseMessage ActualizarProcesoElectoral(DtoProcesoElectoral dto)
        {
            var ProcesoElectoral = ObtenerProcesoElectoralId(dto.Id);

            if (ProcesoElectoral != null)
            {
                ProcesoElectoral.NombreProcesoElectoral = dto.nombreProcesoElectoral;
                ProcesoElectoral.FechaInicio = dto.fechaInicio;
                ProcesoElectoral.FechaFin = dto.fechaFin;
                ProcesoElectoral.EleccionId = dto.eleccionId;
                ProcesoElectoral.Estado = dto.estado;
                ProcesoElectoral.UsuarioModificacion = dto.usuarioModificacion;
                ProcesoElectoral.FechaModificacion = DateTime.Now;
                Actualizar(ProcesoElectoral);
                var dtoMapeado = mapearEntidadADto(ProcesoElectoral);
                return _apiResponseMessage.CrearDtoApiResponseMessage(dtoMapeado, "VE_PEL_PEL_002");
            }
            else
            {
                return _apiResponseMessage.CrearDtoApiResponseMessage(null, "VE_PEL_PEL_007");
            }
        }


        public DtoApiResponseMessage EliminarProcesoElectoral(DtoProcesoElectoral dto)
        {
            var ProcesoElectoral = ObtenerProcesoElectoralId(dto.Id);
            if (ProcesoElectoral != null)
            {
                using var transaccion = new TransactionScope();
                try
                {
                    EliminarPadroProcesoElectoral(ProcesoElectoral.Id);
                    EliminarListasProcesoElectoral(ProcesoElectoral.Id);
                    Eliminar(ProcesoElectoral.Id);
                    transaccion.Complete();

                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    transaccion.Dispose();
                }
                var dtoMapeado = mapearEntidadADto(ProcesoElectoral);
                return _apiResponseMessage.CrearDtoApiResponseMessage(dtoMapeado, "VE_PEL_PEL_003");
            }
            else
            {
                return _apiResponseMessage.CrearDtoApiResponseMessage(null, "VE_PEL_PEL_007");
            }

        }

        void EliminarListasProcesoElectoral(long procesoId)
        => _listaService.EliminarListasProceso(procesoId);

        void EliminarPadroProcesoElectoral(long procesoElectoralId)
            => _padronVotacionService.EliminarPadronesPorProceso(procesoElectoralId);


        public void HabilitarProcesoElectoral(DtoProcesoElectoral dto)
        {
            var ProcesoElectoral = ObtenerProcesoElectoralId(dto.Id);
            ProcesoElectoral.Estado = "ACTIVO";
            Actualizar(ProcesoElectoral);
        }

        public void InhabilitarProcesoElectoral(DtoProcesoElectoral dto)
        {
            var ProcesoElectoral = ObtenerProcesoElectoralId(dto.Id);
            ProcesoElectoral.Estado = "INACTIVO";
            Actualizar(ProcesoElectoral);
        }





        #endregion

        #region Métodos Privados
        Pe01_ProcesoElectoral ObtenerProcesoElectoralId(long id)
        => _procesoElectoralRepository.GetById<Pe01_ProcesoElectoral>(id);

        Pe01_ProcesoElectoral mapearDtoAEntidad(DtoProcesoElectoral dto)
            => new Pe01_ProcesoElectoral()
            {
                NombreProcesoElectoral = dto.nombreProcesoElectoral,
                FechaInicio = dto.fechaInicio,
                FechaFin = dto.fechaFin,
                EleccionId = dto.eleccionId,
                Estado = dto.estado,
                UsuarioCreacion = dto.usuarioCreacion,
                FechaCreacion = DateTime.Now,
            };

        IEnumerable<DtoProcesoElectoral> mapearEntidadADto(Pe01_ProcesoElectoral ProcesoElectoral)
        {
            DtoProcesoElectoral dto = new DtoProcesoElectoral();
            dto.Id = ProcesoElectoral.Id; ;
            dto.nombreProcesoElectoral = ProcesoElectoral.NombreProcesoElectoral;
            dto.fechaInicio = ProcesoElectoral.FechaInicio;
            dto.fechaFin = ProcesoElectoral.FechaFin;
            dto.eleccionId = ProcesoElectoral.EleccionId;
            dto.usuarioCreacion = ProcesoElectoral.UsuarioCreacion;
            dto.usuarioModificacion = ProcesoElectoral.UsuarioModificacion;
            dto.estado = ProcesoElectoral.Estado;
            dto.nombreEleccion = ProcesoElectoral.Eleccion.NombreEleccion;
            //dto.listas = ProcesoElectoral.Listas;
            dto.tipoEleccion = ProcesoElectoral.Eleccion.TipoEleccion.NombreTipoEleccion;

            List<DtoProcesoElectoral> lista = new List<DtoProcesoElectoral>();
            lista.Add(dto);
            return lista;
        }

        IEnumerable<DtoProcesoElectoral> MapearListaEntidadADtoProcesoElectoral(IEnumerable<Pe01_ProcesoElectoral> ProcesoElectoral)
        {

            return ProcesoElectoral?.Select(x => new DtoProcesoElectoral()
            {
                Id = x.Id,
                nombreProcesoElectoral = x.NombreProcesoElectoral,
                fechaInicio = x.FechaInicio,
                fechaFin = x.FechaFin,
                eleccionId = x.EleccionId,
                nombreEleccion = x.Eleccion.NombreEleccion,
                usuarioCreacion = x.UsuarioCreacion,
                usuarioModificacion = x.UsuarioModificacion,
                estado = x.Estado,
                tipoEleccion = x.Eleccion.TipoEleccion.NombreTipoEleccion,
                votoRealizado = x.PadronesVotacion.Any(padron => padron.VotoRealizado)

            });
        }


        void Crear(Pe01_ProcesoElectoral ProcesoElectoral)
        {
            _procesoElectoralRepository.Create<Pe01_ProcesoElectoral>(ProcesoElectoral);
            _procesoElectoralRepository.Save();
        }

        void Actualizar(Pe01_ProcesoElectoral ProcesoElectoral)
        {
            _procesoElectoralRepository.Update<Pe01_ProcesoElectoral>(ProcesoElectoral);
            _procesoElectoralRepository.Save();
        }

        void Eliminar(long idProcesoElectoral)
        {
            _procesoElectoralRepository.Delete<Pe01_ProcesoElectoral>(idProcesoElectoral);
            _procesoElectoralRepository.Save();
        }
        #endregion


    }
}
