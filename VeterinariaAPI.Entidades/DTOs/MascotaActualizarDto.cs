using System.ComponentModel.DataAnnotations;

namespace VeterinariaAPI.Entidades.DTOs;

public class MascotaActualizarDto
{
    [Required(ErrorMessage = "El nombre de la mascota es obligatorio.")]
    [StringLength(80, MinimumLength = 2)]
    public string Nombre { get; set; } = string.Empty;

    [Required(ErrorMessage = "La especie es obligatoria.")]
    [StringLength(50)]
    public string Especie { get; set; } = string.Empty;

    [Required(ErrorMessage = "La raza es obligatoria.")]
    [StringLength(80)]
    public string Raza { get; set; } = string.Empty;

    [Range(0, 100, ErrorMessage = "La edad debe estar entre 0 y 100.")]
    public int Edad { get; set; }

    [Range(1, int.MaxValue)]
    public int IdPropietario { get; set; }
}
