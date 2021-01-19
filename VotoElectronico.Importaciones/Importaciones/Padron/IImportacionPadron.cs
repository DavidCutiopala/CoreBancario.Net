using VotoElectronico.Generico;

namespace VotoElectronico.Importaciones.Importaciones.Padron
{
    public interface  IImportacionPadron
    {
        DtoGenerico LeerArchivo(string nombreArchivo, long procesoId, string token);
    }
}
