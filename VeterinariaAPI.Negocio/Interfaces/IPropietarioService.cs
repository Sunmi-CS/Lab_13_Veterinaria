using VeterinariaAPI.Entidades.DTOs;

namespace VeterinariaAPI.Negocio.Interfaces;

public interface IPropietarioService
{
    Task<List<PropietarioDto>> ObtenerTodosAsync();

    Task<PropietarioDto?> ObtenerPorIdAsync(int id);

    Task<PropietarioDto> CrearAsync(PropietarioCrearDto dto);

    Task<bool> ActualizarAsync(int id, PropietarioActualizarDto dto);

    Task<bool> EliminarAsync(int id);
}
