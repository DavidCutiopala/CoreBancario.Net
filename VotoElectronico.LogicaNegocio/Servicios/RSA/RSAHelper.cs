
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace VotoElectronico.LogicaNegocio.Servicios
{
    public class RsaHelper : IRsaHelper
    {
        private readonly RSACryptoServiceProvider _privateKey;
        private readonly RSACryptoServiceProvider _publicKey;

        public RsaHelper()
        {
            string public_pem = @"D:\Proyectos\Tesis\back-end-voto-electronico\VotoElectronico.LogicaNegocio\Servicios\KeysRSA\publickey.pem";
            string private_pem = @"D:\Proyectos\Tesis\back-end-voto-electronico\VotoElectronico.LogicaNegocio\Servicios\KeysRSA\privatekey.pem";

            _privateKey = GetPrivateKeyFromPemFile(private_pem);
            _publicKey = GetPublicKeyFromPemFile(public_pem);


        }

        public string FirmaDigital(string text)
        {
            //// Write the message to a byte array using UTF8 as the encoding.
            var encoder = new UTF8Encoding();
            var bytesText = encoder.GetBytes(text);
            //var encryptedBytes = _privateKey.SignData(bytesText, false);
            // Hash and sign the data.
            var signedData = HashAndSignBytes(bytesText, _privateKey);
            return Convert.ToBase64String(signedData);
        }

        public bool VerificarFirmaDigital(string datosOriginales, string datosFirmados)
        {
            var encoder = new UTF8Encoding();
            var datosOriginalesBytes = encoder.GetBytes(datosOriginales);
            var datosFirmadosBytes = Convert.FromBase64String(datosFirmados);
            //var decryptedBytes = _publicKey.VerifyData(Convert.FromBase64String(encrypted), false);
            return VerifySignedHash(datosOriginalesBytes, datosFirmadosBytes, _publicKey);
           //return Encoding.UTF8.GetString(decryptedBytes, 0, decryptedBytes.Length);
        }

        private RSACryptoServiceProvider GetPrivateKeyFromPemFile(string filePath)
        {
            using (TextReader privateKeyTextReader = new StringReader(File.ReadAllText(filePath)))
            {
                AsymmetricCipherKeyPair readKeyPair = (AsymmetricCipherKeyPair)new PemReader(privateKeyTextReader).ReadObject();

                RSAParameters rsaParams = DotNetUtilities.ToRSAParameters((RsaPrivateCrtKeyParameters)readKeyPair.Private);
                RSACryptoServiceProvider csp = new RSACryptoServiceProvider();
                csp.ImportParameters(rsaParams);
                return csp;
            }
        }

        private RSACryptoServiceProvider GetPublicKeyFromPemFile(String filePath)
        {
            using (TextReader publicKeyTextReader = new StringReader(File.ReadAllText(filePath)))
            {
                RsaKeyParameters publicKeyParam = (RsaKeyParameters)new PemReader(publicKeyTextReader).ReadObject();

                RSAParameters rsaParams = DotNetUtilities.ToRSAParameters(publicKeyParam);

                RSACryptoServiceProvider csp = new RSACryptoServiceProvider();
                csp.ImportParameters(rsaParams);
                return csp;
            }
        }

        public static byte[] HashAndSignBytes(byte[] DataToSign, RSACryptoServiceProvider RSAalg)
        {
            try
            {
                return RSAalg.SignData(DataToSign, CryptoConfig.MapNameToOID("SHA512"));
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);

                return null;
            }
        }


        public static bool VerifySignedHash(byte[] DataToVerify, byte[] SignedData, RSACryptoServiceProvider RSAalg)
        {
            return RSAalg.VerifyData(DataToVerify, CryptoConfig.MapNameToOID("SHA512"), SignedData);

            //try
            //{
            //    return RSAalg.VerifyData(DataToVerify, SHA512.Create(), SignedData);
            //}
            //catch (CryptographicException e)
            //{
            //    Console.WriteLine(e.Message);

            //    return false;
            //}
        }

    }
}
