using EcVotoElectronico.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using VotoElectronico.Entidades.Pk.PkSeguridad;
using VotoElectronico.Generico;
using VotoElectronico.Generico.Propiedades;
using VotoElectronico.LogicaCondicional;
using VotoElectronico.Util;

namespace VotoElectronico.LogicaNegocio.Servicios
{
    public class PersonaService : IPersonaService
    {

        private readonly IPersonaRepository _personaRepository;
        private IApiResponseMessage _utilitarios;
        private ISesionService _sesionService;

        public PersonaService(IPersonaRepository personaRepository, IApiResponseMessage utilitarios, ISesionService sesionService)
        {
            _personaRepository = personaRepository;
            _utilitarios = utilitarios;
            _sesionService = sesionService;
        }

        #region Métodos Publicos

        // public DtoApiResponseMessage ObtenerPersonasPorNombres(string nombreUno, string nombreDos)
        public IEnumerable<DtoPersona> ObtenerPersonasPorParametros(string identificacion)
        {
            var spec = new Sg02_PersonaCondicional().FiltrarPersonaPorIdentificacion(identificacion);
            var dtoMapeado = MapearListaEntidadADtoPersona(_personaRepository.FiltrarPersonasEspecificacion(spec));

            if (dtoMapeado.Count() != 0)
            {
                //return _utilitarios.crearDtoApiResponseMessage(dtoMapeado, "VE_SEG_PER_005");
                return dtoMapeado;
            }
            else
            {
                //return _utilitarios.crearDtoApiResponseMessage(dtoMapeado, "VE_SEG_PER_006");
                return dtoMapeado;
            }

        }

        public Sg02_Persona ObtenerPersonaIdentificacion(string identificacion, string estado = "")
            => _personaRepository.GetOneOrDefault<Sg02_Persona>(x => x.Identificacion.Equals(identificacion) && (string.IsNullOrEmpty(estado) || estado.Equals(estado)));



        public DtoApiResponseMessage ObtenerPersonaMedianteId(DtoPersona dto)
        {
            var persona = ObtenerPersonaId(dto.Id);
            var dtoMapeado = mapearEntidadADto(persona);
            return _utilitarios.CrearDtoApiResponseMessage(dtoMapeado, "VE_SEG_PER_004");
        }

        public DtoApiResponseMessage CrearPersona(DtoPersona dto, string token)
            => _utilitarios.CrearDtoApiResponseMessage(mapearEntidadADto(InsertarPersona(dto, token)), "VE_SEG_PER_001");

        public Sg02_Persona InsertarPersona(DtoPersona dto, string token)
        {
            var persona = mapearDtoAEntidad(dto, token);
            Crear(persona);
            return persona;
        }

        public DtoApiResponseMessage ActualizarPersona(DtoPersona dto, string token)
        {
            var persona = ObtenerPersonaId(dto.Id);

            if (persona != null)
            {
                persona.NombreUno = dto.nombreUno;
                persona.NombreDos = dto.nombreDos;
                persona.ApellidoUno = dto.apellidoUno;
                persona.ApellidoDos = dto.apellidoDos;

                persona.Identificacion = dto.identificacion;
                persona.Telefono = dto.telefono;
                persona.Estado = dto.estado;
                persona.UsuarioModificacion = _sesionService.ObtenerUsuarioPorToken(token)?.NombreUsuario;
                persona.FechaModificacion = DateTime.Now;
                Actualizar(persona);
                var dtoMapeado = mapearEntidadADto(persona);
                return _utilitarios.CrearDtoApiResponseMessage(dtoMapeado, "VE_SEG_PER_002");
            }
            else
            {
                return _utilitarios.CrearDtoApiResponseMessage(null, "VE_SEG_PER_007");
            }


        }

        public bool ExistePersonaActivoInactivo(string identificacion)
            => _personaRepository.GetExists<Sg02_Persona>(x => x.Identificacion.Equals(identificacion));
        public bool ExistePersonaEstado(string identificacion, string estado)
            => _personaRepository.GetExists<Sg02_Persona>(x => x.Estado.Equals(estado) && x.Identificacion.Equals(identificacion));



        //public void EliminarPersona(DtoPersona dto)
        //{
        //    //var Persona = ObtenerPersonaId(id);
        //    //Persona.Estado = "INACTIVO";
        //    //Actualizar(Persona);
        //    //_PersonaRepository.Delete<Sg02_Persona>(dto.Id);


        public DtoApiResponseMessage EliminarPersona(DtoPersona dto)
        {
            var persona = ObtenerPersonaId(dto.Id);
            if (persona != null)
            {
                _personaRepository.Delete<Sg02_Persona>(persona.Id);
                _personaRepository.Save();

                var dtoMapeado = mapearEntidadADto(persona);
                return _utilitarios.CrearDtoApiResponseMessage(dtoMapeado, "VE_SEG_PER_003");
            }
            else
            {
                return _utilitarios.CrearDtoApiResponseMessage(null, "VE_SEG_PER_007");
            }

        }

        public void ReactivarPersona(DtoPersona dto)
        {
            var Persona = ObtenerPersonaId(dto.Id);
            ReactivarEntidadPersona(Persona);
        }


        public void ReactivarEntidadPersona(Sg02_Persona persona)
        {
            persona.Estado = "ACTIVO";
            Actualizar(persona);
        }

        public void InabilitarPersona(DtoPersona dto)
        {
            var Persona = ObtenerPersonaId(dto.Id);
            Persona.Estado = "INACTIVO";
            Actualizar(Persona);
        }





        #endregion

        #region Métodos Privados
        Sg02_Persona ObtenerPersonaId(long id)
        => _personaRepository.GetById<Sg02_Persona>(id);

        Sg02_Persona mapearDtoAEntidad(DtoPersona dto, string token)
            => new Sg02_Persona()
            {
                NombreUno = dto.nombreUno,
                NombreDos = dto.nombreDos,
                ApellidoUno = dto.apellidoUno,
                ApellidoDos = dto.apellidoDos,
                Identificacion = dto.identificacion,
                Telefono = dto.telefono,
                Email = dto.email,
                Estado = Auditoria.EstadoActivo,
                UsuarioCreacion = _sesionService.ObtenerUsuarioPorToken(token)?.NombreUsuario,
                FechaCreacion = DateTime.Now,
            };

        IEnumerable<DtoPersona> mapearEntidadADto(Sg02_Persona Persona)
        {
            DtoPersona dto = new DtoPersona();
            dto.Id = Persona.Id;
            dto.nombreUno = Persona.NombreUno;
            dto.nombreDos = Persona.NombreDos;
            dto.apellidoUno = Persona.ApellidoUno;
            dto.apellidoDos = Persona.ApellidoDos;
            dto.identificacion = Persona.Identificacion;
            dto.telefono = Persona.Telefono;
            //if (Persona.FechaCreacion != null)
            //{
            //    dto.fechaa = (DateTime)Persona.FechaCreacion;
            //}
            //dto.PersonaModificacion = Persona.PersonaModificacion;
            //if (Persona.FechaModificacion != null)
            //{
            //    dto.fechaModificacion = (DateTime)Persona.FechaModificacion;

            dto.estado = Persona.Estado;

            List<DtoPersona> lista = new List<DtoPersona>();
            lista.Add(dto);
            return lista;
        }

        IEnumerable<DtoPersona> MapearListaEntidadADtoPersona(IEnumerable<Sg02_Persona> persona)
        {
            return persona.Select(x => new DtoPersona()
            {
                dt1 = x.Identificacion,
                Id = x.Id,
                nombreUno = x.NombreUno,
                nombreDos = x.NombreDos,
                apellidoUno = x.ApellidoUno,
                apellidoDos = x.ApellidoDos,
                telefono = x.Telefono,
        });
        }


        void Crear(Sg02_Persona persona)
        {
            _personaRepository.Create<Sg02_Persona>(persona);
            _personaRepository.Save();
        }

        void Actualizar(Sg02_Persona persona)
        {
            _personaRepository.Update<Sg02_Persona>(persona);
            _personaRepository.Save();
        }

        void Eliminar(Sg02_Persona persona)
        {
            _personaRepository.Delete<Sg02_Persona>(persona.Id);
            _personaRepository.Save();
        }
        #endregion


    }
}
