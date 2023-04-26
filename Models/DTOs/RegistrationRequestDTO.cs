using System.ComponentModel.DataAnnotations;

namespace CesgranSec.Models.DTOs
{
    public class RegistrationRequestDTO
    {
        [Required]
        public string Email { get; set; } = null!;
        [Required]
        public string Username { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
        [Required]
        public string ExtensionNumber { get; set; } = null!;
        [Required]
        public string Registration { get; set; } = null!;
    }
}
