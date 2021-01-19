using EcVotoElectronico.Repositorios;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using VotoElectronico.Entidades.Pk.PkProcesoElectoral;
using VotoElectronico.Generico;
using VotoElectronico.LogicaCondicional;
using VotoElectronico.Util;

namespace VotoElectronico.LogicaNegocio.Servicios
{
    public class TipoEleccionService : ITipoEleccionService
    {

        private readonly ITipoEleccionRepository _tipoEleccionRepository;
        private IApiResponseMessage _utilitarios;

        public TipoEleccionService(ITipoEleccionRepository tipoEleccionRepository, IApiResponseMessage utilitarios)
        {
            _tipoEleccionRepository = tipoEleccionRepository;
            _utilitarios = utilitarios;
        }

        #region Métodos Publicos

        public DtoApiResponseMessage ObtenerTipoEleccionPorParametros(string nombreTipoEleccion, string estado)
        {
            var spec = new Pe03_TipoEleccionCondicional().FiltrarEleccionPorParametros(nombreTipoEleccion, estado);
            var dtoMapeado = MapearListaEntidadADtoTipoEleccion(_tipoEleccionRepository.FiltrarTipoEleccion(spec));

            if (dtoMapeado.Count() != 0)
            {
                return _utilitarios.CrearDtoApiResponseMessage(dtoMapeado, "VE_PEL_TEL_005");
            }
            else
            {
                return _utilitarios.CrearDtoApiResponseMessage(dtoMapeado, "VE_PEL_TEL_006");
            }

        }


        public DtoApiResponseMessage ObtenerTipoEleccionMedianteId(DtoTipoEleccion dto)
        {
            var TipoEleccion = ObtenerTipoEleccionId(dto.Id);

            if (TipoEleccion != null)
            {
                var dtoMapeado = mapearEntidadADto(TipoEleccion);
                return _utilitarios.CrearDtoApiResponseMessage(dtoMapeado, "VE_PEL_TEL_004");
            }
            else
            {
                return _utilitarios.CrearDtoApiResponseMessage(null, "VE_PEL_TEL_007");
            }
        }

        public DtoApiResponseMessage CrearTipoEleccion(DtoTipoEleccion dto)
        {
            var TipoEleccion = mapearDtoAEntidad(dto);
            Crear(TipoEleccion);
            var dtoMapeado = mapearEntidadADto(TipoEleccion);
            return _utilitarios.CrearDtoApiResponseMessage(dtoMapeado, "VE_PEL_TEL_001");
        }

        public DtoApiResponseMessage ActualizarTipoEleccion(DtoTipoEleccion dto)
        {
            var TipoEleccion = ObtenerTipoEleccionId(dto.Id);

            if(TipoEleccion != null)
            {
                TipoEleccion.NombreTipoEleccion = dto.nombreTipoEleccion;
                TipoEleccion.UsuarioModificacion = dto.usuarioModificacion;
                TipoEleccion.FechaModificacion = DateTime.Now;
                Actualizar(TipoEleccion);
                var dtoMapeado = mapearEntidadADto(TipoEleccion);
                return _utilitarios.CrearDtoApiResponseMessage(dtoMapeado, "VE_PEL_TEL_002");
            }
            else
            {
                return _utilitarios.CrearDtoApiResponseMessage(null, "VE_PEL_TEL_007");
            }
        }


        public DtoApiResponseMessage EliminarTipoEleccion(DtoTipoEleccion dto)
        {
            var TipoEleccion = ObtenerTipoEleccionId(dto.Id);
            if (TipoEleccion != null)
            {
                Eliminar(TipoEleccion.Id);
                var dtoMapeado = mapearEntidadADto(TipoEleccion);
                return _utilitarios.CrearDtoApiResponseMessage(dtoMapeado, "VE_PEL_TEL_003");
            }
            else
            {
                return _utilitarios.CrearDtoApiResponseMessage(null, "VE_PEL_TEL_007");
            }
            
        }

        public void HabilitarTipoEleccion(DtoTipoEleccion dto)
        {
            var TipoEleccion = ObtenerTipoEleccionId(dto.Id);
            TipoEleccion.Estado = "ACTIVO";
            Actualizar(TipoEleccion);
        }

        public void InhabilitarTipoEleccion(DtoTipoEleccion dto)
        {
            var TipoEleccion = ObtenerTipoEleccionId(dto.Id);
            TipoEleccion.Estado = "INACTIVO";
            Actualizar(TipoEleccion);
        }





        #endregion

        #region Métodos Privados
        Pe03_TipoEleccion ObtenerTipoEleccionId(long id)
        => _tipoEleccionRepository.GetById<Pe03_TipoEleccion>(id);

        Pe03_TipoEleccion mapearDtoAEntidad(DtoTipoEleccion dto)
            => new Pe03_TipoEleccion()
            {
                NombreTipoEleccion = dto.nombreTipoEleccion,
                Estado = dto.estado,
                UsuarioCreacion = dto.usuarioCreacion,
                FechaCreacion = DateTime.Now,
            };

        IEnumerable<DtoTipoEleccion> mapearEntidadADto(Pe03_TipoEleccion TipoEleccion)
        {
            DtoTipoEleccion dto = new DtoTipoEleccion();
            dto.Id = TipoEleccion.Id; ;
            dto.nombreTipoEleccion = TipoEleccion.NombreTipoEleccion;
            dto.usuarioCreacion = TipoEleccion.UsuarioCreacion;
            dto.usuarioModificacion = TipoEleccion.UsuarioModificacion;
            dto.estado = TipoEleccion.Estado;

            List<DtoTipoEleccion> lista = new List<DtoTipoEleccion>();
            lista.Add(dto);
            return lista;
        }

        IEnumerable<DtoTipoEleccion> MapearListaEntidadADtoTipoEleccion(IEnumerable<Pe03_TipoEleccion> TipoEleccion)
        {
            return TipoEleccion.Select(x => new DtoTipoEleccion()
            {
                Id = x.Id,
                nombreTipoEleccion = x.NombreTipoEleccion,
                usuarioCreacion = x.UsuarioCreacion,
                usuarioModificacion = x.UsuarioModificacion,
                estado = x.Estado

            });
        }


        void Crear(Pe03_TipoEleccion TipoEleccion)
        {
            _tipoEleccionRepository.Create<Pe03_TipoEleccion>(TipoEleccion);
            _tipoEleccionRepository.Save();
        }

        void Actualizar(Pe03_TipoEleccion TipoEleccion)
        {
            _tipoEleccionRepository.Update<Pe03_TipoEleccion>(TipoEleccion);
            _tipoEleccionRepository.Save();
        }

        void Eliminar(long idTipoEleccion)
        {
            _tipoEleccionRepository.Delete<Pe03_TipoEleccion>(idTipoEleccion);
            _tipoEleccionRepository.Save();
        }

        #endregion


    }
}
