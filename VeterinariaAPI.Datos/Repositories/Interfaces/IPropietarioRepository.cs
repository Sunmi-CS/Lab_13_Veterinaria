using VeterinariaAPI.Entidades.Models;

namespace VeterinariaAPI.Datos.Repositories.Interfaces;

public interface IPropietarioRepository
{
    Task<List<Propietario>> ObtenerTodosAsync();

    Task<Propietario?> ObtenerPorIdAsync(int id);

    Task<bool> ExisteCorreoAsync(string correo, int? excluirId = null);

    Task<bool> TieneMascotasAsync(int id);

    Task AgregarAsync(Propietario propietario);

    Task ActualizarAsync(Propietario propietario);

    Task EliminarAsync(Propietario propietario);
}
