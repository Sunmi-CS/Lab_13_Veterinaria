using System;
using System.Collections.Generic;

namespace VeterinariaAPI.Datos.Models;

public partial class Propietario
{
    public int IdPropietario { get; set; }

    public string Nombres { get; set; } = null!;

    public string Apellidos { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public virtual ICollection<Mascota> Mascota { get; set; } = new List<Mascota>();
}
