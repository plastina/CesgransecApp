namespace CesgranSec.Models.DTOs;

public class AuthResponseDTO
{
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Token { get; set; } = null!;
}