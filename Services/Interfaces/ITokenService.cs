using CesgranSec.Models;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace CesgranSec.Services.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(ApplicationUser user);
    }
}
