using CesgranSec.Infrastructure;
using CesgranSec.Models.DTOs;
using CesgranSec.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace CesgranSec.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class CryptoController : ControllerBase
    {
        private readonly IEncryptorService _encryptorService;
        private readonly UsersContext _usersContext;

        public CryptoController(IEncryptorService encryptor, UsersContext usersContext)
        {
            _encryptorService = encryptor;
            _usersContext = usersContext;
        }

        [HttpPost]
        [Route("api/upload")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            var userId = GetUserId();

            if (string.IsNullOrEmpty(userId) || string.IsNullOrWhiteSpace(userId))
                return Unauthorized("Não foi possível identificar o id do usuário.");

            var name = file.FileName;
            var contentType = file.ContentType;

            using (var stream = new MemoryStream())
            {
                file.CopyTo(stream);
                var data = stream.ToArray();
                var base64 = Convert.ToBase64String(data);

                await _usersContext.CryptoFiles.AddAsync(new()
                {
                    EncryptedBase64file = base64,
                    FileExtension = contentType,
                    FileName = name,
                    ResponsibleUserId = userId
                }); 
            }

            if (await _usersContext.SaveChangesAsync() > 0)
                return Ok();
            else
                return BadRequest("Houve um problema ao salvar o arquivo. Tente novamente.");
        }

        [HttpGet]
        [Route("api/download")]
        public async Task<IActionResult> DownloadFile(string fileName)
        {
            var userId = GetUserId();

            if (string.IsNullOrEmpty(userId) || string.IsNullOrWhiteSpace(userId))
                return Unauthorized("Não foi possível identificar o id do usuário.");

            var file = await _usersContext.CryptoFiles.FirstOrDefaultAsync(x => x.FileName == fileName && x.ResponsibleUserId == userId);
            if (file is not null)
            {
                var data = Convert.FromBase64String(file.EncryptedBase64file);

                return File(data, file.FileExtension, file.FileName);
            }
            else
            {
                return NotFound();
            }

        }

        private string? GetUserId()
        {
            return User?.Claims?.FirstOrDefault(x => x.Type == "userId")?.Value.ToString();
        }

        private KeysDTO? GetKeysFromUser()
        {
            var user = _usersContext.Users.FirstOrDefault(x => x.Id == GetUserId());

            if (user is null)
                return null;

            if (user.PrivateKeyModulus.IsNullOrEmpty() || 
                user.PrivateKeyD.IsNullOrEmpty() || 
                user.PublicKeyExponent.IsNullOrEmpty() || 
                user.PrivateKeyModulus.IsNullOrEmpty())
                return null;

            var keys = new KeysDTO()
            {
                PublicKeyExponent = user.PublicKeyExponent,
                PrivateKeyD = user.PrivateKeyD,
                PrivateKeyModulus = user.PrivateKeyModulus,
                PublicKeyModulus = user.PublicKeyModulus
            };

            return keys;
        }
    }
}

