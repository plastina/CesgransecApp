using CesgranSec.Models.DTOs;
using CesgranSec.Services.Interfaces;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace CesgranSec.Services
{
    public class EncryptorService : IEncryptorService
    {
        public string Encrypt(KeysDTO keys, string base64file)
        {
            try
            {
                var fileBytes = Convert.FromBase64String(base64file);

                var publicModulusBytes = Convert.FromBase64String(keys.PublicKeyModulus);
                var privateModulusBytes = Convert.FromBase64String(keys.PrivateKeyModulus);
                var exponentBytes = Convert.FromBase64String(keys.PublicKeyExponent);
                var secretBytes = Convert.FromBase64String(keys.PrivateKeyD);

                RSAParameters publicKeyParams = new RSAParameters();
                publicKeyParams.Modulus = publicModulusBytes;
                publicKeyParams.Exponent = exponentBytes;

                RSAParameters privateKeyParams = new RSAParameters();
                privateKeyParams.Modulus = privateModulusBytes;
                privateKeyParams.D = secretBytes;

                var rsa = new RSACryptoServiceProvider(2048);
                rsa.ImportParameters(publicKeyParams);
                rsa.ImportParameters(privateKeyParams);
                var encryptedFileBytes = rsa.Encrypt(fileBytes, false);

                var encryptedFileBase64 = Convert.ToBase64String(encryptedFileBytes);

                return encryptedFileBase64;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public byte[] Decrypt(KeysDTO keys, string encryptedBase64File)
        {
            try
            {
                var encryptedFileBytes = Convert.FromBase64String(encryptedBase64File);

                var publicModulusBytes = Convert.FromBase64String(keys.PublicKeyModulus);
                var privateModulusBytes = Convert.FromBase64String(keys.PrivateKeyModulus);
                var exponentBytes = Convert.FromBase64String(keys.PublicKeyExponent);
                var secretBytes = Convert.FromBase64String(keys.PrivateKeyD);

                RSAParameters publicKeyParams = new RSAParameters();
                publicKeyParams.Modulus = publicModulusBytes;
                publicKeyParams.Exponent = exponentBytes;

                RSAParameters privateKeyParams = new RSAParameters();
                privateKeyParams.Modulus = privateModulusBytes;
                privateKeyParams.D = secretBytes;

                var rsa = new RSACryptoServiceProvider(2048);
                rsa.ImportParameters(publicKeyParams);
                rsa.ImportParameters(privateKeyParams);

                var decryptedFileBytes = rsa.Decrypt(encryptedFileBytes, true);

                return decryptedFileBytes;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }
        
    }
}




