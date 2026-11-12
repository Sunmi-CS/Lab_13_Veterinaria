namespace VeterinariaAPI.Entidades.DTOs;

public class MascotaDto
{
    public int IdMascota { get; set; }

    public string Nombre { get; set; } = string.Empty;

    public string Especie { get; set; } = string.Empty;

    public string Raza { get; set; } = string.Empty;

    public int Edad { get; set; }

    public int IdPropietario { get; set; }

    public string NombrePropietario { get; set; } = string.Empty;
}
