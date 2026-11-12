namespace VeterinariaAPI.Entidades.Models;
public partial class Propietario
{
    public int IdPropietario { get; set; }
    public string Nombres { get; set; } = null!;
    public string Apellidos { get; set; } = null!;
    public string Telefono { get; set; } = null!;
    public string Correo { get; set; } = null!;
    public virtual ICollection<Mascota> Mascotas { get; set; }
        = new List<Mascota>();
}
