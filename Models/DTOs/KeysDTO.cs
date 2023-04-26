namespace CesgranSec.Models.DTOs
{
    public class KeysDTO
    {
        public string PublicKeyModulus { get; set; } = null!;
        public string PrivateKeyModulus { get; set; } = null!;
        public string PrivateKeyD { get; set; } = null!;
        public string PublicKeyExponent { get; set; } = null!;
    }
}
