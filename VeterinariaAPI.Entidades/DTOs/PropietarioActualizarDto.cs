using System.ComponentModel.DataAnnotations;

namespace VeterinariaAPI.Entidades.DTOs;

public class PropietarioActualizarDto
{
    [Required(ErrorMessage = "Los nombres son obligatorios.")]
    [StringLength(80, MinimumLength = 2)]
    public string Nombres { get; set; } = string.Empty;

    [Required(ErrorMessage = "Los apellidos son obligatorios.")]
    [StringLength(100, MinimumLength = 2)]
    public string Apellidos { get; set; } = string.Empty;

    [Required(ErrorMessage = "El teléfono es obligatorio.")]
    [StringLength(15, MinimumLength = 7)]
    public string Telefono { get; set; } = string.Empty;

    [Required(ErrorMessage = "El correo es obligatorio.")]
    [EmailAddress]
    [StringLength(120)]
    public string Correo { get; set; } = string.Empty;
}
