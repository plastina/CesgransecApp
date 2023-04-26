namespace CesgranSec.Models.DTOs;

public class AuthRequestDTO
{
    public string? Email { get; set; }
    public string? ExtensionNumber { get; set; }
    public string? Registration { get; set; }
    public string Password { get; set; } = null!;
}