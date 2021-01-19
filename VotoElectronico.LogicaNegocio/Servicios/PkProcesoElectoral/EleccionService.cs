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
    public class EleccionService : IEleccionService
    {

        private readonly IEleccionRepository _eleccionRepository;
        private IApiResponseMessage _utilitarios;

        public EleccionService(IEleccionRepository eleccionRepository, IApiResponseMessage utilitarios)
        {
            _eleccionRepository = eleccionRepository;
            _utilitarios = utilitarios;
        }

        #region Métodos Publicos

        public DtoApiResponseMessage ObtenerEleccionesPorNombre(string nombreEleccion, string estado)
        {
            var spec = new Pe02_EleccionCondicional().FiltrarEleccionPorNombre(nombreEleccion, estado);
            var dtoMapeado = MapearListaEntidadADtoEleccion(_eleccionRepository.FiltrarEleccionPorNombre(spec));

            if (dtoMapeado.Count() != 0)
            {
                return _utilitarios.CrearDtoApiResponseMessage(dtoMapeado, "VE_PEL_ELC_005");
            }
            else
            {
                return _utilitarios.CrearDtoApiResponseMessage(dtoMapeado, "VE_PEL_ELC_006");
            }

        }


        public DtoApiResponseMessage ObtenerEleccionMedianteId(DtoEleccion dto)
        {
            var eleccion = ObtenerEleccionId(dto.Id);

            if (eleccion != null)
            {
                var dtoMapeado = mapearEntidadADto(eleccion);
                return _utilitarios.CrearDtoApiResponseMessage(dtoMapeado, "VE_PEL_ELC_004");
            }
            else
            {
                return _utilitarios.CrearDtoApiResponseMessage(null, "VE_PEL_ELC_007");
            }
        }

        public DtoApiResponseMessage CrearEleccion(DtoEleccion dto)
        {
            var eleccion = mapearDtoAEntidad(dto);
            Crear(eleccion);
            var dtoMapeado = mapearEntidadADto(eleccion);
            return _utilitarios.CrearDtoApiResponseMessage(dtoMapeado, "VE_PEL_ELC_001");
        }

        public DtoApiResponseMessage ActualizarEleccion(DtoEleccion dto)
        {
            var eleccion = ObtenerEleccionId(dto.Id);

            if(eleccion != null)
            {
                eleccion.NombreEleccion = dto.nombreEleccion;
                eleccion.TipoEleccionId = dto.tipoEleccionId;
                eleccion.UsuarioModificacion = dto.usuarioModificacion;
                eleccion.FechaModificacion = DateTime.Now;
                eleccion.Estado = dto.estado;
                Actualizar(eleccion);
                var dtoMapeado = mapearEntidadADto(eleccion);
                return _utilitarios.CrearDtoApiResponseMessage(dtoMapeado, "VE_PEL_ELC_002");
            }
            else
            {
                return _utilitarios.CrearDtoApiResponseMessage(null, "VE_PEL_ELC_007");
            }
        }


        public DtoApiResponseMessage EliminarEleccion(DtoEleccion dto)
        {
            var eleccion = ObtenerEleccionId(dto.Id);
            if (eleccion != null)
            {
                Eliminar(eleccion.Id);
                var dtoMapeado = mapearEntidadADto(eleccion);
                return _utilitarios.CrearDtoApiResponseMessage(dtoMapeado, "VE_PEL_ELC_003");
            }
            else
            {
                return _utilitarios.CrearDtoApiResponseMessage(null, "VE_PEL_ELC_007");
            }
            
        }

        public void HabilitarEleccion(DtoEleccion dto)
        {
            var Eleccion = ObtenerEleccionId(dto.Id);
            Eleccion.Estado = "ACTIVO";
            Actualizar(Eleccion);
        }

        public void InhabilitarEleccion(DtoEleccion dto)
        {
            var Eleccion = ObtenerEleccionId(dto.Id);
            Eleccion.Estado = "INACTIVO";
            Actualizar(Eleccion);
        }





        #endregion

        #region Métodos Privados
        Pe02_Eleccion ObtenerEleccionId(long id)
        => _eleccionRepository.GetById<Pe02_Eleccion>(id);

        Pe02_Eleccion mapearDtoAEntidad(DtoEleccion dto)
            => new Pe02_Eleccion()
            {
                NombreEleccion = dto.nombreEleccion,
                TipoEleccionId = dto.tipoEleccionId,
                Estado = dto.estado,
                UsuarioCreacion = dto.usuarioCreacion,
                FechaCreacion = DateTime.Now,
            };

        IEnumerable<DtoEleccion> mapearEntidadADto(Pe02_Eleccion Eleccion)
        {
            DtoEleccion dto = new DtoEleccion();
            dto.Id = Eleccion.Id; ;
            dto.nombreEleccion = Eleccion.NombreEleccion;
            dto.tipoEleccionId = Eleccion.TipoEleccionId;
            dto.usuarioCreacion = Eleccion.UsuarioCreacion;
            dto.usuarioModificacion = Eleccion.UsuarioModificacion;
            dto.estado = Eleccion.Estado;

            List<DtoEleccion> lista = new List<DtoEleccion>();
            lista.Add(dto);
            return lista;
        }

        IEnumerable<DtoEleccion> MapearListaEntidadADtoEleccion(IEnumerable<Pe02_Eleccion> Eleccion)
        {
            return Eleccion.Select(x => new DtoEleccion()
            {
                Id = x.Id,
                nombreEleccion = x.NombreEleccion,
                tipoEleccionId = x.TipoEleccionId,
                nombreTipoEleccion = x.TipoEleccion.NombreTipoEleccion,
                usuarioCreacion = x.UsuarioCreacion,
                usuarioModificacion = x.UsuarioModificacion,
                estado = x.Estado

            });
        }


        void Crear(Pe02_Eleccion Eleccion)
        {
            _eleccionRepository.Create<Pe02_Eleccion>(Eleccion);
            _eleccionRepository.Save();
        }

        void Actualizar(Pe02_Eleccion Eleccion)
        {
            _eleccionRepository.Update<Pe02_Eleccion>(Eleccion);
            _eleccionRepository.Save();
        }

        void Eliminar(long idEleccion)
        {
            _eleccionRepository.Delete<Pe02_Eleccion>(idEleccion);
            _eleccionRepository.Save();
        }
        #endregion


    }
}
