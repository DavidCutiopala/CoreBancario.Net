using EcVotoElectronico.Repositorios;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography.X509Certificates;
using VotoElectronico.Entidades.Pk.PkSeguridad;
using VotoElectronico.Generico;
using VotoElectronico.Generico.Propiedades;
using VotoElectronico.LogicaCondicional;
using VotoElectronico.Util;

namespace VotoElectronico.LogicaNegocio.Servicios
{
    public class PermisoService : IPermisoService
    {

        private readonly IPermisoRepository _permisoRepository;
        private IApiResponseMessage _utilitarios;
        private readonly IUsuarioRolService _usuarioRolService;

        public PermisoService(
            IPermisoRepository permisoRepository, 
            IApiResponseMessage utilitarios,
            IUsuarioRolService usuarioRolService)
        {
            _permisoRepository = permisoRepository;
            _utilitarios = utilitarios;
            _usuarioRolService = usuarioRolService;
        }

        public bool ValidarRutaMedianteRol(DtoPermiso dto, string token)
        {
            var rolId = _usuarioRolService.obtenerRolIdbyToken(token);
            return rolId != 0 ?  obtenerPermisosMedianteCargo(rolId, dto.ruta) : false;
        }

       

        #region Métodos Publicos


        bool obtenerPermisosMedianteCargo(long rolId, string ruta)
        {
           return _permisoRepository.GetExists<Sg05_Permiso>(permiso => permiso.RolId.Equals(rolId)
           && permiso.Recurso.ValorRecursoString.Equals(ruta)
           && permiso.Recurso.CodigoRecurso.Equals("RUTA")
           );

            
        }


        #endregion


        //#region Métodos Privados
        //Sg03_Cargo ObtenerrolId(long id)
        //=> _cargoRepository.GetById<Sg03_Cargo>(id);

        //Sg03_Cargo mapearDtoAEntidad(DtoCargo dto)
        //    => new Sg03_Cargo()
        //    {
        //        NombreCargo = dto.nombreCargo,
        //        Estado = dto.estado,
        //        UsuarioCreacion = dto.usuarioCreacion,
        //        FechaCreacion = DateTime.Now,
        //    };

        //IEnumerable<DtoCargo> mapearEntidadADto(Sg03_Cargo Cargo)
        //{
        //    DtoCargo dto = new DtoCargo();
        //    dto.Id = Cargo.Id; ;
        //    dto.nombreCargo = Cargo.NombreCargo;
        //    dto.usuarioCreacion = Cargo.UsuarioCreacion;
        //    dto.usuarioModificacion = Cargo.UsuarioModificacion;
        //    dto.estado = Cargo.Estado;

        //    List<DtoCargo> lista = new List<DtoCargo>();
        //    lista.Add(dto);
        //    return lista;
        //}

        //IEnumerable<DtoCargo> MapearListaEntidadADtoCargo(IEnumerable<Sg03_Cargo> cargos)
        //{
        //    return cargos == null ?null: cargos.Select(x => new DtoCargo()
        //    {
        //        Id = x.Id,
        //        nombreCargo = x.NombreCargo,
        //        usuarioCreacion = x.UsuarioCreacion,
        //        usuarioModificacion = x.UsuarioModificacion,
        //        estado = x.Estado

        //    });
        //}


        //void Crear(Sg03_Cargo Cargo)
        //{
        //    _cargoRepository.Create<Sg03_Cargo>(Cargo);
        //    _cargoRepository.Save();
        //}

        //void Actualizar(Sg03_Cargo Cargo)
        //{
        //    _cargoRepository.Update<Sg03_Cargo>(Cargo);
        //    _cargoRepository.Save();
        //}

        //void Eliminar(long idCargo)
        //{
        //    _cargoRepository.Delete<Sg03_Cargo>(idCargo);
        //    _cargoRepository.Save();
        //}

        //public DtoApiResponseMessage ValidarRutaMedianteCargo(DtoUsuario dto)
        //{
        //    throw new NotImplementedException();
        //}


    }
}
