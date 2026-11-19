
using VeterinariaAPI.Entidades.Models;

namespace VeterinariaAPI.Datos.Repositories.Interfaces;

public interface IUsuarioRepository
{
    Task<Usuario?> ObtenerPorCorreoAsync(string correo);

    Task<bool> ExisteCorreoAsync(string correo);

    Task AgregarAsync(Usuario usuario);
}
