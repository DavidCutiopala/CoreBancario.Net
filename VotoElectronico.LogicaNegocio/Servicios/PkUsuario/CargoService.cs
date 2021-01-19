using EcVotoElectronico.Repositorios;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using VotoElectronico.Entidades.Pk.PkSeguridad;
using VotoElectronico.Generico;
using VotoElectronico.Generico.Propiedades;
using VotoElectronico.LogicaCondicional;
using VotoElectronico.Util;

namespace VotoElectronico.LogicaNegocio.Servicios
{
    public class CargoService : ICargoService
    {

        private readonly ICargoRepository _cargoRepository;
        private IApiResponseMessage _utilitarios;

        public CargoService(ICargoRepository cargoRepository, IApiResponseMessage utilitarios)
        {
            _cargoRepository = cargoRepository;
            _utilitarios = utilitarios;
        }

        #region Métodos Publicos

        public DtoApiResponseMessage ObtenerCargosPorNombre(string nombreCargo, string estado = "")
        {
            var dtoMapeado = MapearListaEntidadADtoCargo(ObtenerEntidadesCargosPorNombre(nombreCargo, estado));
            if (dtoMapeado!= null)
            {
                return _utilitarios.CrearDtoApiResponseMessage(dtoMapeado, "VE_SEG_CAR_005");
            }
            else
            {
                return _utilitarios.CrearDtoApiResponseMessage(dtoMapeado, "VE_SEG_CAR_006");
            }

        }
        public IEnumerable<Sg03_Cargo> ObtenerEntidadesCargosPorNombre(string nombre, string estado = "")
            => _cargoRepository.FiltrarCargoPorNombre(new Sg03_CargoCondicional().FiltrarCargoPorNombre(nombre, estado));
        




        public DtoApiResponseMessage ObtenerCargoMedianteId(DtoCargo dto)
        {
            var cargo = ObtenerCargoId(dto.Id);

            if (cargo != null)
            {
                var dtoMapeado = mapearEntidadADto(cargo);
                return _utilitarios.CrearDtoApiResponseMessage(dtoMapeado, "VE_SEG_CAR_004");
            }
            else
            {
                return _utilitarios.CrearDtoApiResponseMessage(null, "VE_SEG_CAR_007");
            }



        }

        public DtoApiResponseMessage CrearCargo(DtoCargo dto)
        {
            var Cargo = mapearDtoAEntidad(dto);
            Crear(Cargo);
            var dtoMapeado = mapearEntidadADto(Cargo);
            return _utilitarios.CrearDtoApiResponseMessage(dtoMapeado, "VE_SEG_CAR_001");
        }

        public DtoApiResponseMessage ActualizarCargo(DtoCargo dto)
        {
            var cargo = ObtenerCargoId(dto.Id);

            if (cargo != null)
            {
                cargo.NombreCargo = dto.nombreCargo;
                cargo.UsuarioModificacion = dto.usuarioModificacion;
                cargo.FechaModificacion = DateTime.Now;
                Actualizar(cargo);
                var dtoMapeado = mapearEntidadADto(cargo);
                return _utilitarios.CrearDtoApiResponseMessage(dtoMapeado, "VE_SEG_CAR_002");
            }
            else
            {
                return _utilitarios.CrearDtoApiResponseMessage(null, "VE_SEG_CAR_007");
            }


        }


        public DtoApiResponseMessage EliminarCargo(DtoCargo dto)
        {
            var cargo = ObtenerCargoId(dto.Id);
            if (cargo != null)
            {
                Eliminar(cargo.Id);
                var dtoMapeado = mapearEntidadADto(cargo);
                return _utilitarios.CrearDtoApiResponseMessage(dtoMapeado, "VE_SEG_CAR_003");
            }
            else
            {
                return _utilitarios.CrearDtoApiResponseMessage(null, "VE_SEG_CAR_007");
            }

        }

        public void HabilitarCargo(DtoCargo dto)
        {
            var cargo = ObtenerCargoId(dto.Id);
            cargo.Estado = Auditoria.EstadoActivo;
            Actualizar(cargo);
        }

        public void InhabilitarCargo(DtoCargo dto)
        {
            var cargo = ObtenerCargoId(dto.Id);
            cargo.Estado = Auditoria.EstadoInactivo;
            Actualizar(cargo);
        }

        public bool ExisteCargo(string nombreCargo)
            => _cargoRepository.GetExists<Sg03_Cargo>(x => x.Estado.Equals(Auditoria.EstadoActivo) && x.NombreCargo.Equals(nombreCargo));






        #endregion

        #region Métodos Privados
        Sg03_Cargo ObtenerCargoId(long id)
        => _cargoRepository.GetById<Sg03_Cargo>(id);

        Sg03_Cargo mapearDtoAEntidad(DtoCargo dto)
            => new Sg03_Cargo()
            {
                NombreCargo = dto.nombreCargo,
                Estado = dto.estado,
                UsuarioCreacion = dto.usuarioCreacion,
                FechaCreacion = DateTime.Now,
            };

        IEnumerable<DtoCargo> mapearEntidadADto(Sg03_Cargo Cargo)
        {
            DtoCargo dto = new DtoCargo();
            dto.Id = Cargo.Id; ;
            dto.nombreCargo = Cargo.NombreCargo;
            dto.usuarioCreacion = Cargo.UsuarioCreacion;
            dto.usuarioModificacion = Cargo.UsuarioModificacion;
            dto.estado = Cargo.Estado;

            List<DtoCargo> lista = new List<DtoCargo>();
            lista.Add(dto);
            return lista;
        }

        IEnumerable<DtoCargo> MapearListaEntidadADtoCargo(IEnumerable<Sg03_Cargo> cargos)
        {
            return cargos == null ?null: cargos.Select(x => new DtoCargo()
            {
                Id = x.Id,
                nombreCargo = x.NombreCargo,
                usuarioCreacion = x.UsuarioCreacion,
                usuarioModificacion = x.UsuarioModificacion,
                estado = x.Estado

            });
        }


        void Crear(Sg03_Cargo Cargo)
        {
            _cargoRepository.Create<Sg03_Cargo>(Cargo);
            _cargoRepository.Save();
        }

        void Actualizar(Sg03_Cargo Cargo)
        {
            _cargoRepository.Update<Sg03_Cargo>(Cargo);
            _cargoRepository.Save();
        }

        void Eliminar(long idCargo)
        {
            _cargoRepository.Delete<Sg03_Cargo>(idCargo);
            _cargoRepository.Save();
        }
        #endregion


    }
}
