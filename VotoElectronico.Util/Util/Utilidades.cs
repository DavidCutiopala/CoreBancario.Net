using System.IO;
using System.Web;

namespace VotoElectronico.Util
{
    public static class Utilidades
    {
        public static void ValidarExisteCarpeta(string strCarpeta)
        {
            if (!System.IO.Directory.Exists(strCarpeta))
            {
                System.IO.Directory.CreateDirectory(strCarpeta);
            }
        }

        public static void VerificarExistenciArchivo(string pathArchivo)
        {
            if (File.Exists(pathArchivo))
            {
                File.Delete(pathArchivo);
            }

        }

        public static bool ValidarExtencionArchivo(this HttpPostedFile archivo, string extencion, string extencion2 = "")
        {
            return archivo.FileName.EndsWith(extencion) || archivo.FileName.EndsWith(extencion2);
        }
    }
}
