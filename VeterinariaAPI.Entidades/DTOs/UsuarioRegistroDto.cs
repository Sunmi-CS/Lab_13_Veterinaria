
using System.ComponentModel.DataAnnotations;

namespace VeterinariaAPI.Entidades.DTOs;

public class UsuarioRegistroDto
{
    [Required(ErrorMessage = "El correo es obligatorio.")]
    [EmailAddress(ErrorMessage = "El correo no tiene un formato válido.")]
    [StringLength(150)]
    public string Correo { get; set; } = string.Empty;

    [Required(ErrorMessage = "La contraseña es obligatoria.")]
    [StringLength(100, MinimumLength = 8)]
    public string Clave { get; set; } = string.Empty;
}
