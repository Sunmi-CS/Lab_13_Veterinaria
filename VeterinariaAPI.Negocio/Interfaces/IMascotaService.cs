
using VeterinariaAPI.Entidades.DTOs;

namespace VeterinariaAPI.Negocio.Interfaces;

public interface IMascotaService
{
    Task<List<MascotaDto>> ObtenerTodosAsync();

    Task<MascotaDto?> ObtenerPorIdAsync(int id);

    Task<List<MascotaDto>> ObtenerPorEspecieAsync(string? especie);

    Task<List<MascotaDto>?> ObtenerPorPropietarioAsync(int idPropietario);

    Task<MascotaDto> CrearAsync(MascotaCrearDto dto);

    Task<bool> ActualizarAsync(int id, MascotaActualizarDto dto);

    Task<bool> EliminarAsync(int id);
}
