using Microsoft.AspNetCore.Identity;

namespace CesgranSec.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string ExtensionNumber { get; set; } = null!;
        public string Registration { get; set; } = null!;
        public string PublicKeyExponent { get; set; } = null!;
        public string PrivateKeyD { get; set; } = null!;
        public string PublicKeyModulus { get; set; } = null!;
        public string PrivateKeyModulus { get; set; } = null!;
        public virtual List<CryptoFiles> CryptoFiles { get; set; } = null!;
    }
}
