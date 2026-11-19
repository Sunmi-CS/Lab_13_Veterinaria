
namespace VeterinariaAPI.Entidades.DTOs;

public class TokenDto
{
    public string Token { get; set; } = string.Empty;

    public DateTime Expiracion { get; set; }

    public string Correo { get; set; } = string.Empty;

    public string Rol { get; set; } = string.Empty;
}
