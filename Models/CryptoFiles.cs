namespace CesgranSec.Models
{
    public class CryptoFiles
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string EncryptedBase64file { get; set; } = null!;
        public string ResponsibleUserId { get; set; } = null!;
        public string FileName { get; set; } = null!;
        public string FileExtension { get; set; } = null!;
        public virtual ApplicationUser ApplicationUser { get; set; } = null!;
    }
}
