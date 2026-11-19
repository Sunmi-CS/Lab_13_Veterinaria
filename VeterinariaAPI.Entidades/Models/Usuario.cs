using System.ComponentModel.DataAnnotations;

namespace VeterinariaAPI.Entidades.Models;

public class Usuario
{
    [Key]
    public int IdUsuario { get; set; }

    public string Correo { get; set; } = string.Empty;

    public string ClaveHash { get; set; } = string.Empty;

    public string Rol { get; set; } = string.Empty;
}