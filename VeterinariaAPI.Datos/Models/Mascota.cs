using System;
using System.Collections.Generic;

namespace VeterinariaAPI.Datos.Models;

public partial class Mascota
{
    public int IdMascota { get; set; }

    public string Nombre { get; set; } = null!;

    public string Especie { get; set; } = null!;

    public string Raza { get; set; } = null!;

    public int Edad { get; set; }

    public int IdPropietario { get; set; }

    public virtual Propietario IdPropietarioNavigation { get; set; } = null!;
}
