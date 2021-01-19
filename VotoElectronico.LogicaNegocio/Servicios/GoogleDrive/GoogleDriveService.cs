using System;
using System.IO;
using System.Web.Configuration;

using VotoElectronico.AccesoDatos.Repositorio;
using VotoElectronico.Generico.Configs;
using VotoElectronico.Util;

namespace VotoElectronico.LogicaNegocio.Servicios.GoogleDrive
{
    public class GoogleDriveService : IGoogleDriveService
    {
        private readonly IGoogleDriveFilesRepository _googleRepository;

        public GoogleDriveService(IGoogleDriveFilesRepository googleRepository)
        {
            this._googleRepository = googleRepository;
            SettingsDefault();
        }


        #region Métodos privados
        private void SettingsDefault()
        {
            Utilidades.ValidarExisteCarpeta(WebConfigurationManager.AppSettings["path-GoogleDriveSettings"]);
            Utilidades.ValidarExisteCarpeta(WebConfigurationManager.AppSettings["path-GoogleDriveFilesTemp"]);
        }
        #endregion

        #region Métodos publicos

        public string UploadBase64(string base64, string type, string extension, string idCarpetaDrive)
        {
            var file = CrearArchivoTemporal(base64, type, extension);
            return _googleRepository.UploadFile(file, idCarpetaDrive);
        }

        public bool ValidarImagenBase64(string imagen)
          => imagen.Contains("data:image");

        public void DeleteFile(string fileId)
        {
            _googleRepository.DeteteFile(fileId);
        }
        /// <summary>
        /// Crear archivo temporal 
        /// </summary>
        /// <param name="base64"></param>
        /// <param name="type"></param>
        /// <param name="extension"></param>
        /// <returns></returns>
        public HttpPostedFileBaseCustom CrearArchivoTemporal(string base64, string type, string extension)
        {
            var contentType = type;
            var fileName = Guid.NewGuid() + extension;
            HttpPostedFileBaseCustom HttpPostedFileBaseCustom = new HttpPostedFileBaseCustom(GetSteamFromBase64String(base64), contentType, fileName);
            return HttpPostedFileBaseCustom;

        }


        public string GuardarArchivoTemporal(HttpPostedFileBaseCustom file, string path)
        {

            return _googleRepository.GuardarArchivoTemporal(file, path);
        }







        public string UploadPath(string path, string idCarpetaDrive)
        {
            return _googleRepository.UploadPath(path, idCarpetaDrive);
        }

        public void DownloadFile(string blobId, string savePath)
        {

        }

        #endregion

        private MemoryStream GetSteamFromBase64String(string imageBase64)
        {
            if (imageBase64.IndexOf(',') > 0)
            {
                imageBase64 = imageBase64.Substring(imageBase64.IndexOf(',') + 1);
            }
            byte[] bytes = Convert.FromBase64String(imageBase64);
            var ms = new MemoryStream(bytes);

            return ms;
        }
    }
}
