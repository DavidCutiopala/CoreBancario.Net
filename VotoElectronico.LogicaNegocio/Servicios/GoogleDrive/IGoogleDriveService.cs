using VotoElectronico.Generico.Configs;

namespace VotoElectronico.LogicaNegocio.Servicios.GoogleDrive
{
    public interface IGoogleDriveService
    {
        string UploadBase64(string base64, string type, string extension, string idCarpetaDrive);

        string UploadPath(string path, string idCarpetaDrive);
        HttpPostedFileBaseCustom CrearArchivoTemporal(string base64, string type, string extension);

        string GuardarArchivoTemporal(HttpPostedFileBaseCustom file, string path);
        bool ValidarImagenBase64(string imagen);
        void DeleteFile(string fileId);


    }
}
