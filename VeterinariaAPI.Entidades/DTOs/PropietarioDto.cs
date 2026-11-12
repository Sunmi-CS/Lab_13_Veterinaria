
namespace VeterinariaAPI.Entidades.DTOs;

public class PropietarioDto
{
    public int IdPropietario { get; set; }

    public string Nombres { get; set; } = string.Empty;

    public string Apellidos { get; set; } = string.Empty;

    public string Telefono { get; set; } = string.Empty;

    public string Correo { get; set; } = string.Empty;

    public int CantidadMascotas { get; set; }
}
