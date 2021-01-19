using VotoElectronico.Generico.Configs;

namespace VotoElectronico.Generico.Enumeraciones
{
    public enum EstadosEntidad
    {
        [Enumeration("ACTIVO")]
        Activo = 1,
        [Enumeration("INACTIVO")]
        Inactivo = 2,
        [Enumeration("TODOS")]
        Todos = 3
    }
    public enum Roles
    {
        [Enumeration("ELECTOR")]
        Elector,
        [Enumeration("ADMINISTRADOR")]
        Administrador,
    }
    public enum TipoRecurso
    {
        [Enumeration("MENU")]
        Menu,
        [Enumeration("RUTA")]
        Ruta,
    }


    public enum EstadosVoto
    {
        [Enumeration("VALIDO")]
        VALIDO,
        [Enumeration("NULO")]
        NULO,
        [Enumeration("BLANCO")]
        BLANCO
    }

}
