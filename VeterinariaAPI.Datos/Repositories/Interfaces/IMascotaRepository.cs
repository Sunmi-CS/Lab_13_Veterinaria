using VeterinariaAPI.Entidades.Models;

namespace VeterinariaAPI.Datos.Repositories.Interfaces;

public interface IMascotaRepository
{
    Task<List<Mascota>> ObtenerTodosAsync();

    Task<Mascota?> ObtenerPorIdAsync(int id);

    Task<List<Mascota>> ObtenerPorEspecieAsync(string especie);

    Task<List<Mascota>> ObtenerPorPropietarioAsync(int idPropietario);

    Task AgregarAsync(Mascota mascota);

    Task ActualizarAsync(Mascota mascota);

    Task EliminarAsync(Mascota mascota);
}
