using EcVotoElectronico.Repositorios;
using System;
using System.Transactions;
using VotoElectronico.Entidades;
using VotoElectronico.Generico;
using VotoElectronico.Generico.Enumeraciones;
using VotoElectronico.Util;
using VotoElectronico.Generico.Propiedades;

namespace VotoElectronico.LogicaNegocio.Servicios
{
    public class MesaVotoService : IMesaVotoService
    {

        private readonly IApiResponseMessage _apiResponseMessage;
        private readonly IRsaHelper _rsaHelper;
        private readonly IAesService _aesService;
        private readonly IVotoRepository _votoRepository;
        private readonly IOpcionRepository _opcionRepository;


        public MesaVotoService(
            IApiResponseMessage apiResponseMessage,
            IRsaHelper rsaHelper,
            IAesService aesService,
            IVotoRepository votoRepository,
            IOpcionRepository opcionRepository
            )
        {
            _apiResponseMessage = apiResponseMessage;
            _rsaHelper = rsaHelper;
            _aesService = aesService;
            _votoRepository = votoRepository;
            _opcionRepository = opcionRepository;
        }

        public DtoApiResponseMessage EmitirVoto(DtoAES dtoAes)
        {
            //1.- Verificar voto firmado
            var votoAesVerificado = _rsaHelper.VerificarFirmaDigital(dtoAes.VotoCifradoAes+dtoAes.mascara, dtoAes.VotoFirmado);
            if (votoAesVerificado)
            {
                //2.- Desencriptar voto con llaves AES
                var listaOpciones = _aesService.DecryptStringAES(dtoAes.VotoCifradoAes, dtoAes.Key, dtoAes.vectorInicializacion);

                //3.- Guardar voto en base 
                var voto = new Mv01_Voto
                {
                    Mascara = dtoAes.mascara,
                    ProcesoElectoralId = dtoAes.procesoElectoralId,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                };

                using var transaccion = new TransactionScope();
                try
                {
                    if (listaOpciones == null)
                    {
                        voto.Estado = nameof(EstadosVoto.NULO);
                        CrearVoto(voto);
                    }
                    else if (listaOpciones.Count ==0)
                    {
                        voto.Estado = nameof(EstadosVoto.BLANCO);
                        CrearVoto(voto);
                    }
                    else
                    {
                        voto.Estado = nameof(EstadosVoto.VALIDO);
                        CrearVoto(voto);
                        //4.- guardar opciones del voto
                        foreach (DtoOpcion opcion in listaOpciones)
                        {
                            CrearOpcion(new Mv02_Opcion
                            {
                                VotoId = voto.Id,
                                ListaId = Convert.ToInt64(opcion.listaId),
                                CandidatoId = Convert.ToInt64(opcion.candidatoId),
                                EscanioId = Convert.ToInt64(opcion.escanioId),
                                Estado = Auditoria.EstadoActivo,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now,
                            });

                        }
                    }
                    
                    transaccion.Complete();
                }
                catch (Exception ex)
                {
                    _apiResponseMessage.CrearDtoApiResponseMessage(null, "VE_PEL_MV_002");
                }
                finally
                {
                    transaccion.Dispose();
                }

                return _apiResponseMessage.CrearDtoApiResponseMessage(null, "VE_PEL_MV_003");
            }
            else
            {
                // Si el voto no coincide con la firma se anula el voto en la mesa de identificacion y se retorna un error.
                return _apiResponseMessage.CrearDtoApiResponseMessage(null, "VE_PEL_MV_001");
            }





            #region Métodos Privados

            void CrearVoto(Mv01_Voto voto)
            {
                _votoRepository.Create<Mv01_Voto>(voto);
                _votoRepository.Save();
            }


            void ActualizarVoto(Mv01_Voto voto)
            {
                _votoRepository.Update<Mv01_Voto>(voto);
                _votoRepository.Save();
            }


            void CrearOpcion(Mv02_Opcion opcion)
            {
                _opcionRepository.Create<Mv02_Opcion>(opcion);
                _opcionRepository.Save();
            }


            void ActualizarOpcion(Mv02_Opcion opcion)
            {
                _opcionRepository.Update<Mv02_Opcion>(opcion);
                _opcionRepository.Save();
            }


            #endregion

        }
    }
}
