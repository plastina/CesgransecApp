using CesgranSec.Infrastructure;
using CesgranSec.Models;
using CesgranSec.Models.DTOs;
using CesgranSec.Services;
using CesgranSec.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Collections;
using System.Security.Cryptography;
using System.Text;

namespace CesgranSec.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{

    private readonly UserManager<ApplicationUser> _userManager;
    private readonly UsersContext _context;
    private readonly ITokenService _tokenService;
    public AuthController(UserManager<ApplicationUser> userManager, 
        UsersContext context, 
        ITokenService tokenService)
    {
        _userManager = userManager;
        _context = context;
        _tokenService = tokenService;
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register(RegistrationRequestDTO request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var rsa = new RSACryptoServiceProvider(2048);

        var publicKey = rsa.ExportParameters(false);
        var privateKey = rsa.ExportParameters(true);

        var publicKeyModulus = Convert.ToBase64String(publicKey.Modulus!);
        var publicKeyExponent = Convert.ToBase64String(publicKey.Exponent!);

        var privateKeyModulus = Convert.ToBase64String(privateKey.Modulus!);
        var privateKeyD = Convert.ToBase64String(privateKey.D!);

        var result = await _userManager.CreateAsync(
            new ApplicationUser
            {
                UserName = request.Username,
                Email = request.Email,
                ExtensionNumber = request.ExtensionNumber,
                Registration = request.Registration,
                PublicKeyExponent = publicKeyExponent,
                PrivateKeyD = privateKeyD,
                PublicKeyModulus = publicKeyModulus,
                PrivateKeyModulus = privateKeyModulus
            },
            request.Password
        );

        if (result.Succeeded)
        {
            request.Password = "";
            return CreatedAtAction(nameof(Register), new { email = request.Email }, request);
        }
        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(error.Code, error.Description);
        }
        return BadRequest(ModelState);
    }

    [HttpPost]
    [Route("login")]
    public async Task<ActionResult<AuthResponseDTO>> Authenticate([FromBody] AuthRequestDTO request)
    {
        var managedUser = await _context.Users.FirstOrDefaultAsync(
            x => (!request.Email.IsNullOrEmpty() && x.Email == request.Email)
            || (!request.Registration.IsNullOrEmpty() && x.Registration == request.Registration)
            || (!request.ExtensionNumber.IsNullOrEmpty() && x.ExtensionNumber == request.ExtensionNumber));

        if (managedUser == null)
            return BadRequest("Usuário ou senha incorretos.");

        var isPasswordValid = await _userManager.CheckPasswordAsync(managedUser, request.Password);

        if (!isPasswordValid)
            return BadRequest("Usuário ou senha incorretos.");

        var accessToken = _tokenService.CreateToken(managedUser);

        await _context.SaveChangesAsync();

        return Ok(new AuthResponseDTO
        {
            Username = managedUser.UserName!,
            Email = managedUser.Email!,
            Token = accessToken,
        });
    }
}
