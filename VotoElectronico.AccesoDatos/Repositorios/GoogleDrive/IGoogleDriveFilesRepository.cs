using System.Web;

namespace VotoElectronico.AccesoDatos.Repositorio
{
    public interface IGoogleDriveFilesRepository
    {

        string UploadFile(HttpPostedFileBase file, string idCarpetaDrive);

        string UploadPath(string path, string idCarpetaDrive);
        public string GuardarArchivoTemporal(HttpPostedFileBase file, string pathGuardado);

        public void DeteteFile(string FileId);

    }
}
