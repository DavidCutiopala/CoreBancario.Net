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
    public class MovimientoService : IMovimientosService
    {

        private readonly IListaRepository _listaRepository;
        private IApiResponseMessage apiResponseMessage;
        private readonly IGoogleDriveService _googleDriveService;
        private readonly ICandidatoService _candidatoService;
        private readonly ICandidatoRepository _candidatoRepository;
        private readonly string pathListas = ConfigurationManager.AppSettings["path-listas"];
        private readonly IClienteRepository  _clienteRepository;
        private readonly IMovimientoRepository _movimientoRepository;
        private readonly ICuentasRepository _cuentasRepository;
        private readonly IApiResponseMessage _apiResponseMessage;
        public MovimientoService(
            IListaRepository listaRepository,
            IApiResponseMessage utilitarios,
            IGoogleDriveService googleDriveService,
            ICandidatoService candidatoService,
            ICandidatoRepository candidatoRepository,
            IClienteRepository clientesRepository,
             IMovimientoRepository movimientoRepository,
            ICuentasRepository cuentasRepository,
            IApiResponseMessage apiResponseMessage
            )
        {
            _listaRepository = listaRepository;
            _googleDriveService = googleDriveService;
            apiResponseMessage = utilitarios;
            _candidatoService = candidatoService;
            _candidatoRepository = candidatoRepository;
            _clienteRepository = clientesRepository;
            _movimientoRepository = movimientoRepository;
            _cuentasRepository = cuentasRepository;
            _apiResponseMessage = apiResponseMessage;
        }

        #region Métodos Publicos

        public DtoApiResponseMessage ObtenerListasMedianteParams(string nombreLista, string estado)
        {
            var spec = new Pe05_ListaCondicional().FiltrarListaPorNombre(nombreLista, estado);
            var dtoMapeado = MapearListaEntidadADtoLista(_listaRepository.FiltrarListaPorNombre(spec));

            if (dtoMapeado.Count() != 0)
            {
                return apiResponseMessage.CrearDtoApiResponseMessage(dtoMapeado, "VE_PEL_LIS_005");
            }
            else
            {
                return apiResponseMessage.CrearDtoApiResponseMessage(null, "VE_PEL_LIS_006");
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
                return apiResponseMessage.CrearDtoApiResponseMessage(dtoMapeado, "VE_PEL_LIS_005");
            }
            else
            {
                return apiResponseMessage.CrearDtoApiResponseMessage(null, "VE_PEL_LIS_006");
            }
        }


        public DtoApiResponseMessage ObtenerListaMedianteId(DtoLista dto)
        {
            var Lista = ObtenerListaId(dto.Id);

            if (Lista != null)
            {
                var dtoMapeado = mapearEntidadADto(Lista);
                return apiResponseMessage.CrearDtoApiResponseMessage(dtoMapeado, "VE_PEL_LIS_004");
            }
            else
            {
                return apiResponseMessage.CrearDtoApiResponseMessage(null, "VE_PEL_LIS_007");
            }
        }

        public DtoApiResponseMessage CrearLista(DtoLista dto)
        {
            if (dto.logoObjeto != null)
                dto.logoUrl = _googleDriveService.UploadBase64(dto.logoObjeto.base64, dto.logoObjeto.tipo, dto.logoObjeto.extension, pathListas);
            var Lista = mapearDtoAEntidad(dto);
            Crear(Lista);
            var dtoMapeado = mapearEntidadADto(Lista);
            return apiResponseMessage.CrearDtoApiResponseMessage(dtoMapeado, "VE_PEL_LIS_001");
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
                return apiResponseMessage.CrearDtoApiResponseMessage(dtoMapeado, "VE_PEL_LIS_002");
            }
            else
            {
                return apiResponseMessage.CrearDtoApiResponseMessage(null, "VE_PEL_LIS_007");
            }
        }

        public DtoApiResponseMessage EliminarLista(DtoLista dto)
        {
            var lista = ObtenerListaId(dto.Id);
            if (lista != null)
            {
                EliminarListaEntidad(lista);
                var dtoMapeado = mapearEntidadADto(lista);
                return apiResponseMessage.CrearDtoApiResponseMessage(dtoMapeado, "VE_PEL_LIS_003");
            }
            return apiResponseMessage.CrearDtoApiResponseMessage(null, "VE_PEL_LIS_007");
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
                return apiResponseMessage.CrearDtoApiResponseMessage(dtoMapeado, "VE_PEL_LIS_005");
            }
            else
            {
                return apiResponseMessage.CrearDtoApiResponseMessage(null, "VE_PEL_LIS_006");
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
        public DtoApiResponseMessage CrearClienteCB(DtoClienteCB dto)
        {
            var ClienteCB = mapearDtoCBAEntidad(dto);
            CrearCB(ClienteCB);
            var dtoMapeado = mapearEntidadADto(ClienteCB);
            return apiResponseMessage.CrearDtoApiResponseMessage(dtoMapeado, "VE_PEL_LIS_001");
        }


     

           CB01_Cliente mapearDtoCBAEntidad(DtoClienteCB dto)
            => new CB01_Cliente()
            {
                Nombre = dto.Nombre,
                Apellido = dto.Apellido,
                Cedula = dto.Cedula,
                Telefono = dto.Telefono,
                Email = dto.Email,
            };


        void CrearCB(CB01_Cliente clienteCB)
        {
            _clienteRepository.Create(clienteCB);
            _clienteRepository.Save();
        }

        IEnumerable<DtoClienteCB> mapearEntidadADto(CB01_Cliente clienteCb)
       => new List<DtoClienteCB>() { new DtoClienteCB()
            {
                Nombre = clienteCb.Nombre,
                Apellido= clienteCb.Apellido,
                Cedula = clienteCb.Cedula,
                Direccion = clienteCb.Direccion,
                Telefono =  clienteCb.Telefono,
                Email = clienteCb.Email,
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


        public DtoApiResponseMessage RealizarTransferencia(DtoTranferencia dto)
        {
            var cuentaOrigen = _cuentasRepository.GetFirst<CB02_Cuenta>(cuenta => cuenta.NumeroCuenta.Equals(dto.CuentaOrigen));
            var cuentaDestino = _cuentasRepository.GetFirst<CB02_Cuenta>(cuenta => cuenta.NumeroCuenta.Equals(dto.CuentaDestino));

            if(cuentaOrigen == null)
            {
                return _apiResponseMessage.CrearDtoApiResponseMessage(null, "MSG007");
            }else if (cuentaDestino == null)
            {
                return _apiResponseMessage.CrearDtoApiResponseMessage(null, "MSG008");
            }


            using var transaccion = new TransactionScope();
            try
            {

                if (dto.Monto > cuentaOrigen.Saldo)
                {
                    return _apiResponseMessage.CrearDtoApiResponseMessage(null, "MSG005");
                }

                cuentaOrigen.Saldo = cuentaOrigen.Saldo - dto.Monto;
                _cuentasRepository.Update(cuentaOrigen);
                _cuentasRepository.Save();

                cuentaDestino.Saldo = cuentaDestino.Saldo + dto.Monto;
                _cuentasRepository.Update(cuentaDestino);
                _cuentasRepository.Save();

                var entidadMovimientoOrigen = new CB03_Movimiento
                {
                    FechaCreacion = new DateTime(),
                    TipoMovimiento = "DEBITO",
                    Valor = dto.Monto,
                    SaldoFinal = cuentaOrigen.Saldo - dto.Monto,
                    CuentaOrigen = cuentaOrigen.NumeroCuenta,
                    CuentaDestino = cuentaDestino.NumeroCuenta,
                    CuentaId = cuentaOrigen.Id,
                };

                CrearMovimiento(entidadMovimientoOrigen);
                //_movimientoRepository.Create(entidadMovimientoOrigen);
                //_movimientoRepository.Save();

                var entidadMovimientoDestino = new CB03_Movimiento
                {
                    FechaCreacion = new DateTime(),
                    TipoMovimiento = "CREDITO",
                    Valor = dto.Monto,
                    SaldoFinal = cuentaDestino.Saldo + dto.Monto,
                    CuentaOrigen = cuentaDestino.NumeroCuenta,
                    CuentaDestino = cuentaDestino.NumeroCuenta,
                    CuentaId = cuentaDestino.Id
                };

                CrearMovimiento(entidadMovimientoDestino);
                //_movimientoRepository.Create(entidadMovimientoDestino);
                //_movimientoRepository.Save();

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
            //var dtoMapeado = mapearEntidadMovimientoADto(ProcesoElectoral);
            return _apiResponseMessage.CrearDtoApiResponseMessage(null, "MSG006");
        }


        void CrearMovimiento(CB03_Movimiento Movimiento)
        {
            _movimientoRepository.Create(Movimiento);
            _movimientoRepository.Save();
        }


        IEnumerable<DtoLista> mapearEntidadMovimientoADto(Pe05_Lista Lista)
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

        public DtoApiResponseMessage ObtenerMovimientosByClientes(long ClienteId)
        {
            var listaMovimientos = _movimientoRepository.Get<CB03_Movimiento>(
                movimiento => movimiento.Cuenta.ClienteId.Equals(ClienteId)
                );

            return _apiResponseMessage.CrearDtoApiResponseMessage(MapearMovimientoEntidadADto(listaMovimientos), "MSG001");
        }


        IEnumerable<DtoMovimientoInfo> MapearMovimientoEntidadADto(IEnumerable<CB03_Movimiento> ListaMovimiento)
        {

            return ListaMovimiento?.Select(x => new DtoMovimientoInfo()
            {
                Id = x.Id,
                FechaCreacion = x.FechaCreacion,
                TipoMovimiento = x.TipoMovimiento,
                Valor = x.Valor,
                SaldoFinal = x.SaldoFinal,
                CuentaOrigen = x.CuentaOrigen,
                CuentaDestino = x.CuentaDestino,
                CuentaId = x.CuentaId,

                

            });
        }



        #endregion


    }
}
