using VotoElectronico.Generico.Enumeraciones;

namespace VotoElectronico.Generico.Propiedades
{
    public static class Auditoria
    {
     
        public static string EstadoActivo => nameof(EstadosEntidad.Activo).ToUpper();

        public static string EstadoInactivo => nameof(EstadosEntidad.Inactivo).ToUpper();

        public static string TodosEstado => nameof(EstadosEntidad.Todos).ToUpper();

        //public static long VotoRealizado => VotoEstado.Realizado;

    }
}
