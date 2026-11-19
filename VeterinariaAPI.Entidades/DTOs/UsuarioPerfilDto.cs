
namespace VeterinariaAPI.Entidades.DTOs;

public class UsuarioPerfilDto
{
    public int IdUsuario { get; set; }

    public string Correo { get; set; } = string.Empty;

    public string Rol { get; set; } = string.Empty;
}
