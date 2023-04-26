using CesgranSec.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace CesgranSec.Services.Interfaces
{
    public interface IEncryptorService
    {
        string Encrypt(KeysDTO keys, string base64file);
        byte[] Decrypt(KeysDTO keys, string encryptedBase64File);
    }
}
