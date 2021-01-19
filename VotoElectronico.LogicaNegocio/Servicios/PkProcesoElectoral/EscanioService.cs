using EcVotoElectronico.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using VotoElectronico.Entidades.Pk.PkProcesoElectoral;
using VotoElectronico.Generico;
using VotoElectronico.LogicaCondicional;
using VotoElectronico.Util;

namespace VotoElectronico.LogicaNegocio.Servicios
{
    public class EscanioService : IEscanioService
    {

        private readonly IEscanioRepository _escanioRepository;
        private IApiResponseMessage _apiResponseMessage;

        public EscanioService(IEscanioRepository escanioRepository, IApiResponseMessage utilitarios)
        {
            _escanioRepository = escanioRepository;
            _apiResponseMessage = utilitarios;
        }

        #region Métodos Publicos

        //public DtoApiResponseMessage ObtenerEscaniosPorNombre(string nombreEscanio)
        //{
        //    var spec = new Pe04_EscanioCondicional().FiltrarEscanioPorNombre(nombreEscanio);
        //    var dtoMapeado = MapearListaEntidadADtoEscanio(_escanioRepository.FiltrarEscanioPorNombre(spec));

        //    if (dtoMapeado.Count() != 0)
        //    {
        //        return _apiResponseMessage.crearDtoApiResponseMessage(dtoMapeado, "VE_PEL_ESC_005");
        //    }
        //    else
        //    {
        //        return _apiResponseMessage.crearDtoApiResponseMessage(dtoMapeado, "VE_PEL_ESC_006");
        //    }

        //}

        public DtoApiResponseMessage ObtenerEscaniosPorListaId(long listaId)
        {
            var spec = new Pe04_EscanioCondicional().FiltrarEscaniosMedianteListaId(listaId);
            var dtoMapeado = MapearListaEntidadADtoEscanio(_escanioRepository.FiltrarEscanios(spec));

            return dtoMapeado != null && dtoMapeado.Count() > 0 ? _apiResponseMessage.CrearDtoApiResponseMessage(dtoMapeado, "VE_PEL_ESC_005"):
                _apiResponseMessage.CrearDtoApiResponseMessage(null, "VE_PEL_ESC_006");
                
        }


        public DtoApiResponseMessage ObtenerEscanioMedianteId(DtoEscanio dto)
        {
            var Escanio = ObtenerEscanioId(dto.escanioId);

            if (Escanio != null)
            {
                var dtoMapeado = mapearEntidadADto(Escanio);
                return _apiResponseMessage.CrearDtoApiResponseMessage(dtoMapeado, "VE_PEL_ESC_004");
            }
            else
            {
                return _apiResponseMessage.CrearDtoApiResponseMessage(null, "VE_PEL_ESC_007");
            }
        }

        public DtoApiResponseMessage CrearEscanio(DtoEscanio dto)
        {
            var escanio = mapearDtoAEntidad(dto);
            Crear(escanio);
            var dtoMapeado = mapearEntidadADto(escanio);
            return _apiResponseMessage.CrearDtoApiResponseMessage(dtoMapeado, "VE_PEL_ESC_001");
        }

        public DtoApiResponseMessage ActualizarEscanio(DtoEscanio dto)
        {
            var Escanio = ObtenerEscanioId(dto.escanioId);

            if(Escanio != null)
            {
                Escanio.NombreEscanio = dto.nombreEscanio;
                Escanio.EleccionId = dto.eleccionId;
                Escanio.UsuarioModificacion = dto.usuarioModificacion;
                Escanio.FechaModificacion = DateTime.Now;
                Escanio.Estado = dto.estado;
                Actualizar(Escanio);
                var dtoMapeado = mapearEntidadADto(Escanio);
                return _apiResponseMessage.CrearDtoApiResponseMessage(dtoMapeado, "VE_PEL_ESC_002");
            }
            else
            {
                return _apiResponseMessage.CrearDtoApiResponseMessage(null, "VE_PEL_ESC_007");
            }
        }


        public DtoApiResponseMessage EliminarEscanio(DtoEscanio dto)
        {
            var Escanio = ObtenerEscanioId(dto.escanioId);
            if (Escanio != null)
            {
                Eliminar(Escanio.Id);
                var dtoMapeado = mapearEntidadADto(Escanio);
                return _apiResponseMessage.CrearDtoApiResponseMessage(dtoMapeado, "VE_PEL_ESC_003");
            }
            else
            {
                return _apiResponseMessage.CrearDtoApiResponseMessage(null, "VE_PEL_ESC_007");
            }
            
        }
        public DtoApiResponseMessage ObtenerEscanios(string nombre, string estado)
        {
            var escanios = MapearListaEntidadADtoEscanio(_escanioRepository.FiltrarEscanios(new Pe04_EscanioCondicional().FiltrarEscanios(nombre, estado)));
            if(escanios != null)
            return _apiResponseMessage.CrearDtoApiResponseMessage(escanios, "VE_PEL_ESC_005");
            return _apiResponseMessage.CrearDtoApiResponseMessage(null, "VE_PEL_ESC_006");
        }
           


        public void HabilitarEscanio(DtoEscanio dto)
        {
            var Escanio = ObtenerEscanioId(dto.escanioId);
            Escanio.Estado = "ACTIVO";
            Actualizar(Escanio);
        }

        public void InhabilitarEscanio(DtoEscanio dto)
        {
            var Escanio = ObtenerEscanioId(dto.escanioId);
            Escanio.Estado = "INACTIVO";
            Actualizar(Escanio);
        }

        public DtoApiResponseMessage ObtenerEscaniosPorProcesoElectoralId(long procesoElectoralId)
        {
            var spec = new Pe04_EscanioCondicional().FiltrarEscaniosMedianteProcesoElectoralId(procesoElectoralId);
            var dtoMapeado = MapearListaEntidadADtoEscanio(_escanioRepository.FiltrarEscanios(spec));

            return dtoMapeado != null && dtoMapeado.Count() > 0 ? _apiResponseMessage.CrearDtoApiResponseMessage(dtoMapeado, "VE_PEL_ESC_005") :
                _apiResponseMessage.CrearDtoApiResponseMessage(null, "VE_PEL_ESC_006");
        }




        #endregion

        #region Métodos Privados
        Pe04_Escanio ObtenerEscanioId(long id)
        => _escanioRepository.GetById<Pe04_Escanio>(id);

        Pe04_Escanio mapearDtoAEntidad(DtoEscanio dto)
            => new Pe04_Escanio()
            {
                NombreEscanio = dto.nombreEscanio,
                EleccionId = dto.eleccionId,
                Estado = dto.estado,
                UsuarioCreacion = dto.usuarioCreacion,
                FechaCreacion = DateTime.Now,
            };

        IEnumerable<DtoEscanio> mapearEntidadADto(Pe04_Escanio Escanio)
        {
            DtoEscanio dto = new DtoEscanio();
            dto.escanioId = Escanio.Id; ;
            dto.nombreEscanio = Escanio.NombreEscanio;
            dto.eleccionId = Escanio.EleccionId;
            dto.usuarioCreacion = Escanio.UsuarioCreacion;
            dto.usuarioModificacion = Escanio.UsuarioModificacion;
            dto.estado = Escanio.Estado;

            List<DtoEscanio> lista = new List<DtoEscanio>();
            lista.Add(dto);
            return lista;
        }

        IEnumerable<DtoEscanio> MapearListaEntidadADtoEscanio(IEnumerable<Pe04_Escanio> escanio)
        {
             
            return escanio == null ? null : escanio.Select(x => new DtoEscanio()
            {
                escanioId = x.Id,
                nombreEscanio = x.NombreEscanio,
                eleccionId = x.EleccionId,
                nombreEleccion = x.Eleccion.NombreEleccion,
                usuarioCreacion = x.UsuarioCreacion,
                usuarioModificacion = x.UsuarioModificacion,
                estado = x.Estado
            });
        }


        void Crear(Pe04_Escanio Escanio)
        {
            _escanioRepository.Create<Pe04_Escanio>(Escanio);
            _escanioRepository.Save();
        }

        void Actualizar(Pe04_Escanio Escanio)
        {
            _escanioRepository.Update<Pe04_Escanio>(Escanio);
            _escanioRepository.Save();
        }

        void Eliminar(long idEscanio)
        {
            _escanioRepository.Delete<Pe04_Escanio>(idEscanio);
            _escanioRepository.Save();
        }
        #endregion


    }
}
