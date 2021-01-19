using System;
using System.Collections.Generic;
using VotoElectronico.AccesoDatos.Repositorios;
using VotoElectronico.Entidades;
using VotoElectronico.Generico;

namespace VotoElectronico.Util
{
    public class ApiResponseMessage : IApiResponseMessage
    {
        private readonly IMensajeRepository _mensajeRepository;

        public ApiResponseMessage (IMensajeRepository mensajeRepository)
        {
            _mensajeRepository = mensajeRepository;
        }

        public DtoApiResponseMessage CrearDtoApiResponseMessage(IEnumerable<object> responseList = null, string codigoMesaje = "")
        {

            var entidadMensaje = _mensajeRepository.GetById<Ge01_Mensaje>(codigoMesaje);
            
            var _dtoApiResponseMessage = new DtoApiResponseMessage();
            
            var dtoMensaje = new DtoMensaje();
            if (entidadMensaje != null)
            {
                _dtoApiResponseMessage.responseList = responseList;
            }
            else
            {
                entidadMensaje = _mensajeRepository.GetById<Ge01_Mensaje>("ERR_MESSAGE");
            }

            dtoMensaje.CodigoMensaje = entidadMensaje?.CodigoMensaje; 
            dtoMensaje.TipoMensajeId = entidadMensaje?.TipoMensaje.NombreMensaje; 
            dtoMensaje.Titulo = entidadMensaje?.Titulo;
            dtoMensaje.Descripcion = entidadMensaje?.Descripcion;
            _dtoApiResponseMessage.informationMessage = dtoMensaje;

            return _dtoApiResponseMessage;
        }

        public DtoApiResponseMessage crearDtoErrorExceptionMessage(Exception ex)
        {
            var innerException = "";

            if (ex.InnerException != null)
            {
                innerException = ex.InnerException.InnerException.Message;
            }

            var _dtoApiResponseMessage = new DtoApiResponseMessage();
            var entidadMensaje = _mensajeRepository.GetById<Ge01_Mensaje>("ERR_EXCEPTION");
            var dtoMensaje = new DtoMensaje();
            dtoMensaje.CodigoMensaje = entidadMensaje.CodigoMensaje;
            dtoMensaje.TipoMensajeId = entidadMensaje.TipoMensaje.NombreMensaje;
            dtoMensaje.Titulo = ex.Message;
            dtoMensaje.Descripcion = string.IsNullOrEmpty(innerException) ? string.IsNullOrEmpty(ex.Message)? "General Error" : ex.Message:innerException ;

            _dtoApiResponseMessage.errorExceptionMessage = dtoMensaje;
            return _dtoApiResponseMessage;
        }
    }
}
