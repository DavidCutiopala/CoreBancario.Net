using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.IO;
using System.Threading;
using System.Web;
using System.Web.Configuration;

namespace VotoElectronico.AccesoDatos.Repositorio
{
    public class GoogleDriveFilesRepository : IGoogleDriveFilesRepository
    {
        // If modifying these scopes, delete your previously saved credentials
        // at ~/.credentials/drive-dotnet-quickstart.json
        static string[] Scopes = { DriveService.Scope.Drive };
        static string ApplicationName = "VotoEpn2";

        private static DriveService GetService()
        {
            UserCredential credential;
            string pathCredenditial = Path.Combine(WebConfigurationManager.AppSettings["path-GoogleDriveSettings"],
                "credentials.json");

            using (var stream = new FileStream(pathCredenditial, FileMode.Open, FileAccess.Read))
            {
                string credPath = Path.Combine(WebConfigurationManager.AppSettings["path-GoogleDriveSettings"],
                "token.json");
                try
                {
                    var taskConexion = GoogleWebAuthorizationBroker.AuthorizeAsync(
                        GoogleClientSecrets.Load(stream).Secrets,
                        Scopes,
                        "user",
                        CancellationToken.None,
                        new FileDataStore(credPath, true)
                    );

                    taskConexion.Wait(); // Espera la tarea.

                    if(taskConexion.IsCanceled || taskConexion.IsFaulted) //verifica el estado de la tarea.
                        throw taskConexion.Exception;

                    credential = taskConexion.Result; //Obtiene el resultado.

                    return new DriveService(new BaseClientService.Initializer()
                    {
                        HttpClientInitializer = credential,
                        ApplicationName = ApplicationName,
                    });
                }
                catch
                {
                    throw new Exception("Error al conectarse con los servicio de google. Verifique credenciales de acceso");
                }
            }
            
        }

        /// <summary>
        /// guardar File tipo HttpPostedBase en un path especifico
        /// </summary>
        /// <param name="file"></param>
        /// <param name="pathGuardado"></param>
        /// <returns></returns>
        public string GuardarArchivoTemporal(HttpPostedFileBase file, string pathGuardado)
        {

            string path = Path.Combine(pathGuardado,
                Path.GetFileName(file.FileName));
            file.SaveAs(path);
            return path;
        }

        public string UploadFile(HttpPostedFileBase file, string idCarpetaDrive)
        {
            var path = GuardarArchivoTemporal(file, WebConfigurationManager.AppSettings["path-GoogleDriveFilesTemp"]);
            return Upload(path, idCarpetaDrive);

        }

        public void DeteteFile(string FileId) {
            Delete(FileId);
        }

        public string UploadPath(string path, string idCarpetaDrive) {

            return Upload(path, idCarpetaDrive);
        }


        private string Upload(string path, string idCarpetaDrive) {

            string[] folders = new string[1];
            folders[0] = idCarpetaDrive;

            var fileName = Path.GetFileName(path);
            var mimeType = MimeMapping.GetMimeMapping(fileName);

            DriveService service = GetService();

            var FileMetaData = new Google.Apis.Drive.v3.Data.File();

            FileMetaData.Name = fileName;
            FileMetaData.MimeType = mimeType;
            FileMetaData.Parents = folders;
      
            FilesResource.CreateMediaUpload request;

            using (var stream = new System.IO.FileStream(path, System.IO.FileMode.Open))
            {
                request = service.Files.Create(FileMetaData, stream, FileMetaData.MimeType);
                request.Fields = "id";
                request.Upload();
            }
            if(request == null)
                throw new Exception("No se pudo leer y cargar el archivo, talvez esta siendo utlizado por otro proceso");

            EliminarArchivoTemporal(path);

            if (request.ResponseBody == null)            
                throw new Exception("No se ha podido subir los archivos a la nube");                           
            return request.ResponseBody.Id;
        }

        public void DownloadFile(string blobId, string savePath)
        {
            var service = GetService();
            var request = service.Files.Get(blobId);
            var stream = new MemoryStream();
            // Add a handler which will be notified on progress changes.
            // It will notify on each chunk download and when the
            // download is completed or failed.
            request.MediaDownloader.ProgressChanged += (Google.Apis.Download.IDownloadProgress progress) =>
            {
                switch (progress.Status)
                {
                    case Google.Apis.Download.DownloadStatus.Downloading:
                        {
                            Console.WriteLine(progress.BytesDownloaded);
                            break;
                        }
                    case Google.Apis.Download.DownloadStatus.Completed:
                        {
                            Console.WriteLine("Download complete.");
                            //SaveStream(stream, savePath);
                            break;
                        }
                    case Google.Apis.Download.DownloadStatus.Failed:
                        {
                            Console.WriteLine("Download failed.");
                            break;
                        }
                }
            };
            request.Download(stream);
        }

        private static void SaveStream(MemoryStream stream, string saveTo)
        {
            using (FileStream file = new FileStream(saveTo, FileMode.Create, FileAccess.Write))
            {
                stream.WriteTo(file);
            }
        }

        /// <summary>
        /// Formato del archivo: nombre + fecha + hora
        /// </summary>
        /// <param name="pathArchivo"></param>
        /// <returns></returns>
        public bool EliminarArchivoTemporal(string pathArchivo)
        {
            if (System.IO.File.Exists(pathArchivo))
            {
                System.IO.File.Delete(pathArchivo);
                return true;
            }
            return true;

        }

        /// <summary>
        /// Delete file or folder 
        /// </summary>
        /// <param name="fileId"></param>
        private void Delete(string fileId) {
            try {
                DriveService service = GetService();
                FilesResource.DeleteRequest request;
                request = service.Files.Delete(fileId);
                request.Execute();
            }
            catch (Exception e)
            {
                throw new Exception(e?.Message);
            }
   
        }

    }
}
