using EcVotoElectronico.Repositorios;
using VotoElectronico.LogicaNegocio.Servicios.GoogleDrive;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using VotoElectronico.Entidades.Pk.PkProcesoElectoral;
using VotoElectronico.Generico;
using VotoElectronico.Generico.Propiedades;
using VotoElectronico.LogicaCondicional;
using VotoElectronico.Util;
using System.Net.Http.Headers;
using System.Transactions;

namespace VotoElectronico.LogicaNegocio.Servicios
{
    public class CuentaService : ICuentaService
    {

        private readonly IListaRepository _listaRepository;
        private IApiResponseMessage _apiResponseMessage;
        private readonly IGoogleDriveService _googleDriveService;
        private readonly ICandidatoService _candidatoService;
        private readonly ICandidatoRepository _candidatoRepository;
        private readonly string pathListas = ConfigurationManager.AppSettings["path-listas"];
        private readonly IClienteRepository  _clienteRepository;
        private readonly ICuentasRepository _cuentasRespository;

        public CuentaService(
            IListaRepository listaRepository,
            IApiResponseMessage apiResponseMessage,
            IGoogleDriveService googleDriveService,
            ICandidatoService candidatoService,
            ICandidatoRepository candidatoRepository,
            IClienteRepository clientesRepository,
            ICuentasRepository cuentasRespository
            )
        {
            _listaRepository = listaRepository;
            _googleDriveService = googleDriveService;
            _apiResponseMessage = apiResponseMessage;
            _candidatoService = candidatoService;
            _candidatoRepository = candidatoRepository;
            _clienteRepository = clientesRepository;
            _cuentasRespository = cuentasRespository;
    }

        #region Métodos Publicos

        public DtoApiResponseMessage ObtenerListasMedianteParams(string nombreLista, string estado)
        {
            var spec = new Pe05_ListaCondicional().FiltrarListaPorNombre(nombreLista, estado);
            var dtoMapeado = MapearListaEntidadADtoLista(_listaRepository.FiltrarListaPorNombre(spec));

            if (dtoMapeado.Count() != 0)
            {
                return _apiResponseMessage.CrearDtoApiResponseMessage(dtoMapeado, "VE_PEL_LIS_005");
            }
            else
            {
                return _apiResponseMessage.CrearDtoApiResponseMessage(null, "VE_PEL_LIS_006");
            }

        }

        public DtoApiResponseMessage obtenerListasPorProcesoElectoralId(long procesoElectoralId, string estado)
        {
            var spec = new Pe05_ListaCondicional().FiltrarListaPorProcesoElectoralId(procesoElectoralId, estado);
            var dtoMapeado = MapearListaEntidadADtoLista(_listaRepository.FiltrarListaPorProcesoElectoral(spec));
            //var Lista = ObtenerMedianteProcesoElectoralId(procesoElectoralId);

            if (dtoMapeado.Count() != 0)
            {
                //var dtoMapeado = mapearEntidadADto(Lista);
                return _apiResponseMessage.CrearDtoApiResponseMessage(dtoMapeado, "VE_PEL_LIS_005");
            }
            else
            {
                return _apiResponseMessage.CrearDtoApiResponseMessage(null, "VE_PEL_LIS_006");
            }
        }


        public DtoApiResponseMessage ObtenerListaMedianteId(DtoLista dto)
        {
            var Lista = ObtenerListaId(dto.Id);

            if (Lista != null)
            {
                var dtoMapeado = mapearEntidadADto(Lista);
                return _apiResponseMessage.CrearDtoApiResponseMessage(dtoMapeado, "VE_PEL_LIS_004");
            }
            else
            {
                return _apiResponseMessage.CrearDtoApiResponseMessage(null, "VE_PEL_LIS_007");
            }
        }

        public DtoApiResponseMessage CrearLista(DtoLista dto)
        {
            if (dto.logoObjeto != null)
                dto.logoUrl = _googleDriveService.UploadBase64(dto.logoObjeto.base64, dto.logoObjeto.tipo, dto.logoObjeto.extension, pathListas);
            var Lista = mapearDtoAEntidad(dto);
            Crear(Lista);
            var dtoMapeado = mapearEntidadADto(Lista);
            return _apiResponseMessage.CrearDtoApiResponseMessage(dtoMapeado, "VE_PEL_LIS_001");
        }



   
        public DtoApiResponseMessage ActualizarLista(DtoLista dto)
        {
            var Lista = ObtenerListaId(dto.Id);

            if (Lista != null)
            {
                string imagenLogo;
                if (dto.logoObjeto != null)
                    imagenLogo = _googleDriveService.UploadBase64(dto.logoObjeto.base64, dto.logoObjeto.tipo, dto.logoObjeto.extension, pathListas);
                else imagenLogo = Lista.Logo;

                Lista.NombreLista = dto.nombreLista;
                Lista.ProcesoElectoralId = dto.procesoElectoralId;
                Lista.UsuarioModificacion = dto.usuarioModificacion;
                Lista.FechaModificacion = DateTime.Now;
                Lista.Estado = dto.estado;
                Lista.Eslogan = dto.eslogan;
                Lista.Logo = imagenLogo;
                Actualizar(Lista);
                var dtoMapeado = mapearEntidadADto(Lista);
                return _apiResponseMessage.CrearDtoApiResponseMessage(dtoMapeado, "VE_PEL_LIS_002");
            }
            else
            {
                return _apiResponseMessage.CrearDtoApiResponseMessage(null, "VE_PEL_LIS_007");
            }
        }

        public DtoApiResponseMessage EliminarLista(DtoLista dto)
        {
            var lista = ObtenerListaId(dto.Id);
            if (lista != null)
            {
                EliminarListaEntidad(lista);
                var dtoMapeado = mapearEntidadADto(lista);
                return _apiResponseMessage.CrearDtoApiResponseMessage(dtoMapeado, "VE_PEL_LIS_003");
            }
            return _apiResponseMessage.CrearDtoApiResponseMessage(null, "VE_PEL_LIS_007");
        }


        void EliminarListaEntidad(Pe05_Lista lista)
        {
            if (lista != null)
            {
                using var transaccion = new TransactionScope();
                try
                {
                    EliminarCandidatos(lista.Id);
                    Eliminar(lista.Id);
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
            }
        }

        public void EliminarListasProceso(long procesoId)
        {
            var listas = _listaRepository.Get<Pe05_Lista>(x => x.ProcesoElectoralId == procesoId);
            foreach (var lista in listas)
                EliminarListaEntidad(lista);
        }




        public DtoApiResponseMessage obtenerListasYCandidatosByProcesoElectoralId(long procesoElectoralId)
        {
            //var spec = new Pe05_ListaCondicional().FiltrarListaPorProcesoElectoralId(procesoElectoralId);
            //var listas = _listaRepository.FiltrarListaPorProcesoElectoral(spec);
            //var Lista = ObtenerMedianteProcesoElectoralId(procesoElectoralId);
            var dtoMapeado = obtenerListasConInformacionCandidatos(procesoElectoralId);
            if (dtoMapeado.Count() != 0)
            {
                //var dtoMapeado = mapearEntidadADto(Lista);
                return _apiResponseMessage.CrearDtoApiResponseMessage(dtoMapeado, "VE_PEL_LIS_005");
            }
            else
            {
                return _apiResponseMessage.CrearDtoApiResponseMessage(null, "VE_PEL_LIS_006");
            }
        }


        IEnumerable<DtoLista> obtenerListasConInformacionCandidatos(long procesoElectoralId)
        {
            var listasCandidatos = new List<DtoLista>();
            var listas =  _listaRepository.Get<Pe05_Lista>(lista => lista.ProcesoElectoralId.Equals(procesoElectoralId) && lista.Estado.Equals(Auditoria.EstadoActivo));
            if (listas.Count() != 0)
            {
                foreach (var registro in listas)
                    listasCandidatos.Add(MapearListaCandidatoEntidadADtoLista(registro));

            }

            return listasCandidatos;
        }


        DtoLista  MapearListaCandidatoEntidadADtoLista(Pe05_Lista Lista)
        {
            var candidatos = _candidatoRepository.Get<Pe06_Candidato>(candidato => candidato.ListaId.Equals(Lista.Id) && candidato.Estado.Equals(Auditoria.EstadoActivo))?
                .OrderBy(candidato => candidato.Escanio.Orden);
            var dto = new DtoLista()
            {
                Id = Lista.Id,
                nombreLista = Lista.NombreLista,
                eslogan = Lista.Eslogan,
                logoUrl = string.IsNullOrEmpty(Lista.Logo) ? null : $"{CtEstaticas.StrGoogleDrive}{Lista.Logo}",
                procesoElectoralId = Lista.ProcesoElectoralId,
                usuarioCreacion = Lista.UsuarioCreacion,
                usuarioModificacion = Lista.UsuarioModificacion,
                estado = Lista.Estado,
                candidatos = candidatos.ToList()?.Select(candidato => ConvertirEntidadCantidatoADto(candidato))
            };
            return dto;
        }

        DtoCandidato ConvertirEntidadCantidatoADto(Pe06_Candidato candidato)
        {
            return new DtoCandidato()
            {
                Id = candidato.Id,
                personaId = candidato.PersonaId,
                nombreCandidato = candidato.Persona.NombreUno + " " + candidato.Persona.ApellidoUno,
                identificacion = candidato.Persona.Identificacion,
                procesoElectoralId = candidato.Lista.ProcesoElectoralId,
                nombreProcesoElectoral = candidato.Lista.ProcesoElectoral.NombreProcesoElectoral,
                escanioId = candidato.EscanioId,
                nombreEscanio = candidato.Escanio.NombreEscanio,
                listaId = candidato.ListaId,
                nombreLista = candidato.Lista.NombreLista,
                usuarioCreacion = candidato.UsuarioCreacion,
                usuarioModificacion = candidato.UsuarioModificacion,
                estado = candidato.Estado,
                fotoUrl = string.IsNullOrEmpty(candidato.Foto) ? null : $"{CtEstaticas.StrGoogleDrive}{candidato.Foto}",
            };

        }
        #endregion



        #region
        //public DtoApiResponseMessage CrearClienteCB(DtoClienteCB dto)
        //{
        //    var ClienteCB = mapearDtoCBAEntidad(dto);
        //    CrearCB(ClienteCB);
        //    var dtoMapeado = mapearEntidadADto(ClienteCB);
        //    return _apiResponseMessage.CrearDtoApiResponseMessage(dtoMapeado, "VE_PEL_LIS_001");
        //}


     

           CB02_Cuenta mapearDtoCBAEntidad(DtoCuenta cuenta)
            => new CB02_Cuenta()
            {
                Saldo = cuenta.Saldo,
                FechaCreacion = new DateTime(),
                NumeroCuenta = cuenta.NumeroCuenta,
                TipoCuenta = cuenta.TipoCuenta,
                ClienteId = cuenta.ClienteId,
            };


        void CrearCB(CB01_Cliente clienteCB)
        {
            _clienteRepository.Create(clienteCB);
            _clienteRepository.Save();
        }

        IEnumerable<DtoCuenta> mapearEntidadCuentaADto(CB02_Cuenta cuenta)
       => new List<DtoCuenta>() { new DtoCuenta()
            {
           Saldo=cuenta.Saldo,
           FechaCreacion= cuenta.FechaCreacion,
           NumeroCuenta = cuenta.NumeroCuenta,
           TipoCuenta = cuenta.TipoCuenta,
            }
       };


        #endregion






        #region Métodos Privados
        Pe05_Lista ObtenerListaId(long id)
        => _listaRepository.GetById<Pe05_Lista>(id);

        //pe05_lista obtenermedianteprocesoelectoralid(long procesoid)
        //=> _listarepository.getbyid<pe05_lista>(x => x.procesoelectoralid == procesoid);

        Pe05_Lista mapearDtoAEntidad(DtoLista dto)
            => new Pe05_Lista()
            {
                NombreLista = dto.nombreLista,
                ProcesoElectoralId = dto.procesoElectoralId,
                Eslogan = dto.eslogan,
                Logo = dto.logoUrl,
                Estado = dto.estado,
                UsuarioCreacion = dto.usuarioCreacion,
                FechaCreacion = DateTime.Now,
            };

        IEnumerable<DtoLista> mapearEntidadADto(Pe05_Lista Lista)
        => new List<DtoLista>() { new DtoLista()
            {
                Id = Lista.Id,
                nombreLista = Lista.NombreLista,
                eslogan = Lista.Eslogan,
                logoUrl = string.IsNullOrEmpty(Lista.Logo) ? null : $"{CtEstaticas.StrGoogleDrive}{Lista.Logo}",
                procesoElectoralId = Lista.ProcesoElectoralId,
                usuarioCreacion = Lista.UsuarioCreacion,
                usuarioModificacion = Lista.UsuarioModificacion,
                estado = Lista.Estado,
            }
        };



        IEnumerable<DtoLista> MapearListaEntidadADtoLista(IEnumerable<Pe05_Lista> Lista)
        {
            return Lista.Select(x => new DtoLista()
            {
                Id = x.Id,
                nombreLista = x.NombreLista,
                procesoElectoralId = x.ProcesoElectoralId,
                nombreProcesoElectoral = x.ProcesoElectoral.NombreProcesoElectoral,
                eslogan = x.Eslogan,
                logoUrl = string.IsNullOrEmpty(x.Logo) ? null : $"{CtEstaticas.StrGoogleDrive}{x.Logo}",
                usuarioCreacion = x.UsuarioCreacion,
                usuarioModificacion = x.UsuarioModificacion,
                estado = x.Estado

            });
        }


        void Crear(Pe05_Lista Lista)
        {
            _listaRepository.Create(Lista);
            _listaRepository.Save();
        }

        void Crearcuenta(CB02_Cuenta cuenta)
        {
            _cuentasRespository.Create(cuenta);
            _cuentasRespository.Save();
        }

        void EliminarCandidatos(long listaId)
            => _candidatoService.EliminarCandidatosLista(listaId);


        void Actualizar(Pe05_Lista Lista)
        {
            _listaRepository.Update(Lista);
            _listaRepository.Save();
        }

        void Eliminar(long idLista)
        {
            _listaRepository.Delete<Pe05_Lista>(idLista);
            _listaRepository.Save();
        }

        public DtoApiResponseMessage CrearClienteCB(DtoLista dto)
        {
            throw new NotImplementedException();
        }

        public DtoApiResponseMessage ObtenerCuentasByCedula(string cedula)
        {
            try
            {
                var listaCuentas = _cuentasRespository.Get<CB02_Cuenta>(cuenta => cuenta.Cliente.Cedula.Equals(cedula));

                return _apiResponseMessage.CrearDtoApiResponseMessage(MapearCuentasEntidadADto(listaCuentas), "MSG001");
            }
            catch(Exception ex)
            {
                return _apiResponseMessage.crearDtoErrorExceptionMessage(ex);
            }
            
        }


        public DtoApiResponseMessage CrearCuenta(DtoCuenta dto)
        {
            
            var cuenta = mapearDtoCBAEntidad(dto);
            Crearcuenta(cuenta);
            var dtoMapeado = mapearEntidadCuentaADto(cuenta);
            return _apiResponseMessage.CrearDtoApiResponseMessage(dtoMapeado, "MSG001");
        }

        IEnumerable<DtoCuenta> MapearCuentasEntidadADto(IEnumerable<CB02_Cuenta> ListaCuentas)
        {

            return ListaCuentas?.Select(x => new DtoCuenta()
            {
                Saldo = x.Saldo,
                FechaCreacion = x.FechaCreacion,
                NumeroCuenta = x.NumeroCuenta,
                TipoCuenta = x.TipoCuenta,

            });
        }

        #endregion


    }
}
