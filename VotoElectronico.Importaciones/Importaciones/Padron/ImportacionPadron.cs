using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using VotoElectronico.Entidades.Pk.PkSeguridad;
using VotoElectronico.Generico;
using VotoElectronico.Generico.Configs;
using VotoElectronico.Generico.Enumeraciones;
using VotoElectronico.Generico.Propiedades;
using VotoElectronico.Importaciones.Configs;
using VotoElectronico.LogicaNegocio.Servicios;
using VotoElectronico.Util;

namespace VotoElectronico.Importaciones.Importaciones.Padron
{
    public class ImportacionPadron : IImportacionPadron
    {
        private readonly IPadronVotacionService _padronVotacionService;
        private readonly ICargoService _cargoService;
        private readonly IPersonaService _personaService;
        private readonly IUsuarioService _usuarioService;
        private readonly IUsuarioCargoService _usuarioCargoService;
        private readonly IUsuarioRolService _usuarioRolService;
        private readonly IRolService _rolService;
        private readonly string pathArchivos;
        public ImportacionPadron(IPadronVotacionService padronVotacionService, ICargoService cargoService, IUsuarioService usuarioService, IUsuarioCargoService usuarioCargoService, IPersonaService personaService, IUsuarioRolService usuarioRolService, IRolService rolService)
        {
            _padronVotacionService = padronVotacionService;
            _cargoService = cargoService;
            _usuarioService = usuarioService;
            _usuarioCargoService = usuarioCargoService;
            _personaService = personaService;
            _usuarioRolService = usuarioRolService;
            pathArchivos = ConfigurationManager.AppSettings["fld-tmp"];
            _rolService = rolService;
        }

        public DtoGenerico LeerArchivo(string nombreArchivo, long procesoId, string token)
        {
            DtoGenerico detalleImportacion = new DtoGenerico
            {
                Dt2 = "Importación padrón",
                Dt20 = token,
            };

            try
            {
                using FileStream fileStream = new FileStream(nombreArchivo, FileMode.Open);
                ExcelPackage excel = new ExcelPackage(fileStream);
                var workSheet = excel.Workbook.Worksheets.First();
                IEnumerable<DtoImportacionPadron> lispaEmpadronados = workSheet.ConvertSheetToObjects<DtoImportacionPadron>().ToList();
                var registrosValidados = ValidarArchivo(lispaEmpadronados);
                if (registrosValidados.Any(d => !d.TieneError))
                {
                    using var transaccion = new TransactionScope();
                    try
                    {
                        EliminarPadronesProceso(procesoId);
                        CrearPadronEntidadesRelacionadas(registrosValidados.Where(f => !f.TieneError).ToList(), token, procesoId);
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
                    var archivoImportados = GenerarArchivoPadron(registrosValidados.Where(f => !f.TieneError), "Importacion_Padron");
                    detalleImportacion.Dt5 = archivoImportados.Bdt1 ? archivoImportados.Dt2 : string.Empty;

                }
                detalleImportacion.Ndt2 = registrosValidados.Count(x => !x.TieneError);
                detalleImportacion.Ndt3 = registrosValidados.Count(x => x.TieneError);
                var archivoExportacion = GenerarArchivoPadron(registrosValidados.Where(f => f.TieneError), "ErroresPadron");
                detalleImportacion.Dt4 = archivoExportacion.Bdt1 ? archivoExportacion.Dt2 : string.Empty;
                detalleImportacion.Ndt1 = detalleImportacion.Ndt2 + detalleImportacion.Ndt3;
          
            }
            catch (Exception e)
            {
                return new DtoGenerico { Bdt1 = false, Dt1 = "Error Importacion Archivo" };
            }
            return GenerarRespuestaImportacion((int)detalleImportacion.Ndt1, (int)detalleImportacion.Ndt2, (int)detalleImportacion.Ndt3, detalleImportacion.Dt4);

        }
        void EliminarPadronesProceso(long procesoId)
            => _padronVotacionService.EliminarPadronesPorProceso(procesoId);


        public IEnumerable<DtoImportacionPadron> ValidarArchivo(IEnumerable<DtoImportacionPadron> padrones)
        {
            var camposObligatorios = ValidarCamposObligatorios(padrones);

            if (validarLista(camposObligatorios))
            {
                var listaValida = ValidarRepetido(camposObligatorios.Where(r => !r.TieneError)?.ToList());
                //if (validarLista(listaValida))
                //    listaValida = ValidarEmpadronadoExistente(listaValida.Where(r => !r.TieneError)?.ToList());
                if (validarLista(listaValida))
                    listaValida = ValidarExisteCargo(listaValida.Where(r => !r.TieneError)?.ToList());
                if (validarLista(listaValida))
                    listaValida = ValidarTelefono(listaValida.Where(r => !r.TieneError)?.ToList());
            }
            return camposObligatorios;
        }

        bool validarLista(IEnumerable<DtoImportacionPadron> registroPadron)
            => registroPadron != null && registroPadron.Count() > 0;


        IEnumerable<DtoImportacionPadron> ValidarRepetido(IEnumerable<DtoImportacionPadron> pedrones)
        {
            pedrones.GroupBy(d => d.Identificacion).ToList().ForEach(x =>
                {
                    if (x.AsEnumerable().Count() > 0)
                    {
                        var i = 0;
                        x.ToList().ForEach(y =>
                        {
                            if (i != 0)
                            {
                                y.TieneError = true;
                                y.MensajeError = string.Format("Identificación Repetida");
                            }
                            i++;
                        });
                    }
                });
            return pedrones;
        }


        IEnumerable<DtoImportacionPadron> ValidarCamposObligatorios(IEnumerable<DtoImportacionPadron> padrones)
        {
            foreach (var registro in padrones)
            {
                var mensajeError = string.Empty;
                if (string.IsNullOrEmpty(registro.NombreUno))
                    mensajeError += string.Format("Nombre Uno es requerido") + " --- ";

                if (string.IsNullOrEmpty(registro.ApellidoUno))
                    mensajeError += string.Format("Apellido Requerido") + " --- ";

                if (string.IsNullOrEmpty(registro.Identificacion))
                    mensajeError += string.Format("Identificación Requerida") + " --- ";

                if (string.IsNullOrEmpty(registro.Email))
                    mensajeError += string.Format("Email Requerido") + " --- ";

                if (string.IsNullOrEmpty(registro.Cargo))
                    mensajeError += string.Format("Cargo Requerido") + " --- ";

                if (!string.IsNullOrEmpty(mensajeError))
                {
                    registro.TieneError = true;
                    registro.MensajeError = mensajeError.Trim().Remove(mensajeError.Length - 5, 4);
                }
            }
            return padrones;
        }

        IEnumerable<DtoImportacionPadron> ValidarEmpadronadoExistente(IEnumerable<DtoImportacionPadron> padrones)
        {
            padrones?.ToList()?.ForEach(x =>
            {
                if (_padronVotacionService.ExisteEmpadronado(x.Identificacion, Auditoria.EstadoActivo))
                {
                    x.TieneError = true;
                    x.MensajeError = "Empadronado ya se encuentra Registrado";
                }
                if (_padronVotacionService.ExisteEmpadronado(x.Identificacion, Auditoria.EstadoInactivo))
                {
                    var padron = _padronVotacionService.ObtenerPadronVotacionMedianteIdentificacion(x.Identificacion, x.ProcesoId, Auditoria.EstadoInactivo);
                    x.PadronId = padron.Id;
                    x.MensajeDosError = "Reactivar Empadronado";

                }
            });
            return padrones;
        }

        IEnumerable<DtoImportacionPadron> ValidarExisteCargo(IEnumerable<DtoImportacionPadron> padrones)
        {
            padrones?.ToList()?.ForEach(x =>
            {
                if (!_cargoService.ExisteCargo(x.Cargo))
                {
                    x.TieneError = true;
                    x.MensajeError = "Cargo no existe";
                }
                else
                {
                    var cargo = _cargoService.ObtenerEntidadesCargosPorNombre(x.Cargo)?.FirstOrDefault();
                    if (cargo.Estado.Equals(Auditoria.EstadoInactivo))
                    {
                        x.TieneError = true;
                        x.MensajeError = "Cargo Inactivo";
                    }
                    else
                    {
                        x.CargoId = cargo.Id;
                    }
                }
            });
            return padrones;
        }

        IEnumerable<DtoImportacionPadron> ValidarTelefono(IEnumerable<DtoImportacionPadron> padrones)
        {
            padrones?.ToList()?.ForEach(x =>
            {
                if (!string.IsNullOrEmpty(x.Telefono))
                {
                    if (x.Telefono?.Length > 10 || x.Telefono?.Length < 9)
                    {
                        x.TieneError = true;
                        x.MensajeError = "Longitud de télefono inválido";
                    }
                    else if (x.Telefono?.Length == 9)
                    {
                        x.Telefono = "0" + x.Telefono;
                    }
                }
            });
            return padrones;
        }

        public void CrearPadronEntidadesRelacionadas(IEnumerable<DtoImportacionPadron> padronesValidos, string token, long procesoElectoralId)
        {
            var rolVotante = _rolService.ObtenerRolPorNombre(Enumeration.ObtenerDescripcion(Roles.Elector));
            if (rolVotante != null)
            {
                foreach (var padronValido in padronesValidos)
                {
                    if (!string.IsNullOrEmpty(padronValido.MensajeDosError) && padronValido.MensajeDosError.Equals("Reactivar Empadronado"))
                    {
                        var padron = _padronVotacionService.ObtenerPadronVotacionId(padronValido.PadronId);
                        _padronVotacionService.HabilitarEntodadPadronVotacion(padron);
                    }

                    else if (!_personaService.ExistePersonaActivoInactivo(padronValido.Identificacion))
                    {
                        var persona = _personaService.InsertarPersona(new DtoPersona() { nombreUno = padronValido.NombreUno, nombreDos = padronValido.NombreDos, apellidoUno = padronValido.ApellidoUno, apellidoDos = padronValido.ApellidoDos, identificacion = padronValido.Identificacion, telefono = padronValido.Telefono, email = padronValido.Email }, token);
                        crearEntidadesRelacionadas(persona, padronValido, token, procesoElectoralId, rolVotante?.Id ?? 0);
                    }
                    else if (_personaService.ExistePersonaEstado(padronValido.Identificacion, Auditoria.EstadoActivo))
                    {
                        var persona = _personaService.ObtenerPersonaIdentificacion(padronValido.Identificacion, Auditoria.EstadoActivo);
                        crearEntidadesRelacionadas(persona, padronValido, token, procesoElectoralId, rolVotante?.Id ?? 0);
                    }
                    else if (_personaService.ExistePersonaEstado(padronValido.Identificacion, Auditoria.EstadoInactivo))
                    {
                        var personaInactiva = _personaService.ObtenerPersonaIdentificacion(padronValido.Identificacion, estado: Auditoria.EstadoInactivo);
                        _personaService.ReactivarEntidadPersona(personaInactiva);
                        crearEntidadesRelacionadas(personaInactiva, padronValido, token, procesoElectoralId, rolVotante?.Id ?? 0);
                    }
                };
            }
            else
            {
                throw new Exception("Rol para electór no existe");

            }



        }

        void crearEntidadesRelacionadas(Sg02_Persona persona, DtoImportacionPadron padron, string token, long procesoElectoralId, long rolId)
        {
            var usuario = _usuarioService.ValidarInsertarUsuario(new DtoUsuario() { nombreUsuario = padron.Identificacion, PersonaId = persona.Id, Clave = UtilEncodeDecode.Sha256Hash("generic_user_" + padron.Identificacion) }, token);
            _usuarioCargoService.ValidarInsertarUsuarioCargo(new DtoUsuarioCargo() { usuarioId = usuario.Id, cargoId = padron.CargoId }, token);
            _usuarioRolService.ValidarInsertarUsuarioRol(new DtoUsuarioRol() { UsuarioId = usuario.Id, RolId = rolId }, token);
            _padronVotacionService.InsertarPadroVotacion(new DtoPadronVotacion() { usuarioId = usuario.Id, procesoElectoralId = procesoElectoralId }, token);
        }

        public DtoGenerico GenerarArchivoPadron(IEnumerable<DtoImportacionPadron> registrosPadron, string nombreFile)
        {
            string nombreArchivo = FormatoArchivoFechaHora(nombreFile, ".xlsx");
            DtoGenerico exportacion = ExportarPadron("#C00000", registrosPadron, nombreArchivo);
            return new DtoGenerico()
            {
                Bdt1 = exportacion.Bdt1,
                Dt1 = exportacion.Dt2,
                Dt2 = exportacion.Dt3
            };
        }

        string FormatoArchivoFechaHora(string archivo, string extencion = "")
        {
            return $"{archivo}_{DateTime.Now:yyyyMMdd}_{DateTime.Now:HHmmss}{extencion}".Trim();
        }

        private DtoGenerico ExportarPadron(string color, IEnumerable<DtoImportacionPadron> padrones, string nombreArchivo)
        {
            var dto = new DtoGenerico();
            try
            {

                var rowRecord = 2;

                var archivogenerado = new FileInfo(pathArchivos + nombreArchivo);
                var paquete = new ExcelPackage();
                var libro = paquete.Workbook.Worksheets.Add("Padron");

                using (var rango = libro.Cells["A1:I1"])
                {
                    rango.Style.Font.Bold = true;
                    rango.Style.WrapText = true;
                    rango.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    rango.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    rango.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    rango.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(int.Parse(color.Replace("#", ""), NumberStyles.HexNumber)));
                    rango.Style.Font.Size = 12;
                    rango.Style.Font.Color.SetColor(Color.White);
                }

                libro.Cells["A1"].Value = "Nombre Uno";
                libro.Cells["B1"].Value = "Nombre Dos";
                libro.Cells["C1"].Value = "Apellido Uno";
                libro.Cells["D1"].Value = "Apellido Dos";
                libro.Cells["E1"].Value = "Email";
                libro.Cells["F1"].Value = "Identificación";
                libro.Cells["G1"].Value = "Cargo";
                libro.Cells["H1"].Value = "Teléfono";
                libro.Cells["I1"].Value = "Error";


                libro.Column(1).Width = 20;
                libro.Column(2).Width = 20;
                libro.Column(3).Width = 20;
                libro.Column(4).Width = 20;
                libro.Column(5).Width = 20;
                libro.Column(6).Width = 20;
                libro.Column(7).Width = 20;
                libro.Column(8).Width = 20;
                libro.Column(9).Width = 20;

                try
                {
                    foreach (var item in padrones.ToList())
                    {
                        if (item.TieneError)
                        {

                            libro.Cells["A" + rowRecord + ":" + "O" + rowRecord].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            libro.Cells["A" + rowRecord + ":" + "O" + rowRecord].Style.Fill.BackgroundColor.SetColor(Color.LightSkyBlue);
                        }
                        libro.Cells["A" + rowRecord].Value = item.NombreUno;
                        libro.Cells["B" + rowRecord].Value = item.NombreDos;
                        libro.Cells["C" + rowRecord].Value = item.ApellidoUno;
                        libro.Cells["D" + rowRecord].Value = item.ApellidoDos;
                        libro.Cells["E" + rowRecord].Value = item.Email;
                        libro.Cells["F" + rowRecord].Value = item.Identificacion;
                        libro.Cells["G" + rowRecord].Value = item.Cargo;
                        libro.Cells["H" + rowRecord].Value = item.Telefono;
                        libro.Cells["I" + rowRecord].Value = item.MensajeError;

                        rowRecord++;
                    }

                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
                Utilidades.ValidarExisteCarpeta(pathArchivos);
                if (File.Exists(pathArchivos + nombreArchivo))
                {
                    File.Delete(pathArchivos + nombreArchivo);
                }

                paquete.Workbook.Properties.Title = nombreArchivo;
                paquete.Workbook.Properties.Author = string.Join(Environment.NewLine, "EVOTE", "EVOTE");
                paquete.Workbook.Properties.Company = "Escuela Politécnica Nacional";
                paquete.SaveAs(archivogenerado);
                dto.Bdt1 = true;
                dto.Dt1 = "Archivo " + nombreArchivo + " se ha generado exitosamente.";
                dto.Dt2 = nombreArchivo;
                dto.Dt3 = pathArchivos + nombreArchivo;
            }
            catch (Exception ex)
            {
                dto.Bdt1 = false;
                dto.Dt1 = ex.Message;
            }

            return dto;
        }


        public DtoGenerico GenerarArchivoErroresCanton(IEnumerable<DtoImportacionPadron> registrosErroneos)
        {
            string nombreArchivo = FormatoArchivoFechaHora("ERRORES_PADRON", ".xlsx");
            DtoGenerico exportacion = ExportarPadron("#C00000", registrosErroneos, nombreArchivo);
            return new DtoGenerico()
            {
                Bdt1 = exportacion.Bdt1,
                Dt1 = exportacion.Dt2,
                Dt2 = exportacion.Dt3
            };
        }

        DtoGenerico GenerarRespuestaImportacion(int registroTotal, int registroIngresado, int registroError = 0, string pathError = "")
            => new DtoGenerico
            {
                Bdt1 = true,
                Dt1 = string.IsNullOrEmpty(pathError) ?
               "Registros totales: " + registroTotal + " " +
               "registros ingresados: " + registroIngresado :
               "Registros totales: " + registroTotal + " " +
               "registros ingresados: " + registroIngresado +
               ", registros con error: " + registroError,
                Dt2 = string.IsNullOrEmpty(pathError) ? string.Empty : pathError,
                Ndt1 = registroTotal,
                Ndt2 = registroIngresado,
                Ndt3 = registroError

            };



        //IEnumerable<DtoImportacionPadron> ValidarExistencia(IEnumerable<DtoImportacionPadron> registroCanton)
        //{

        //    foreach (var registro in registroCanton)
        //    {
        //        var existeRegistro = _cantonRepository.GetExists<Ct012_Canton>(x => x.NombreCanton == registro.NombreCanton && x.Provincia.NombreProvincia == registro.Provincia && x.Provincia.Pais.NombrePais == registro.NombrePais);
        //        if (existeRegistro)
        //        {
        //            registro.TieneError = true;
        //            registro.MensajeError += string.Format(MensajesImportacion.CantonYaExiste, registro.NombreCanton);
        //        }
        //    }
        //    return registroCanton;
        //}


        ///// <summary>
        ///// Validar Existencia
        ///// </summary>
        ///// <param name="registrosCantones"></param>
        ///// <returns></returns>
        //IEnumerable<DtoImportacionPadron> ValidarProvinciaExiste(List<DtoImportacionPadron> registrosCantones)
        //{
        //    IEnumerable<ExcelDtoCt012> paisesCanton = null;
        //    foreach (var pais in registrosCantones.GroupBy(d => d.NombrePais))
        //    {
        //        var paisEncontrado = _paisRepository.GetOneOrDefault<Ct010_Pais>(r => r.NombrePais == pais.Key);
        //        if (paisEncontrado != null)
        //        {
        //            foreach (var provincia in registrosCantones.GroupBy(x => x.Provincia))
        //            {
        //                var provinciaEncontrada = _provinciaRepository.GetOneOrDefault<Ct011_Provincia>(p => p.NombreProvincia == provincia.Key && p.PaisId == paisEncontrado.Id);

        //                if (provinciaEncontrada != null)
        //                {
        //                    paisesCanton = AgregarDatosProvincia(registrosCantones, pais.Key, provincia.Key, provinciaId: provinciaEncontrada.Id);
        //                }
        //                else
        //                {
        //                    paisesCanton = AgregarDatosProvincia(registrosCantones, pais.Key, provincia.Key);
        //                }

        //            }
        //        }
        //        else
        //        {
        //            AgregarDatosPaisError(registrosCantones, pais.Key);
        //        }

        //    }
        //    return paisesCanton;
        //}
    }
}
