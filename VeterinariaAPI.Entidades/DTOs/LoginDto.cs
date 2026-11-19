
using System.ComponentModel.DataAnnotations;

namespace VeterinariaAPI.Entidades.DTOs;

public class LoginDto
{
    [Required(ErrorMessage = "El correo es obligatorio.")]
    [EmailAddress]
    public string Correo { get; set; } = string.Empty;

    [Required(ErrorMessage = "La contraseña es obligatoria.")]
    public string Clave { get; set; } = string.Empty;
}
