
using AutoMapper;
using VeterinariaAPI.Datos.Repositories.Interfaces;
using VeterinariaAPI.Entidades.DTOs;
using VeterinariaAPI.Entidades.Models;
using VeterinariaAPI.Negocio.Exceptions;
using VeterinariaAPI.Negocio.Interfaces;

namespace VeterinariaAPI.Negocio.Services;

public class MascotaService : IMascotaService
{
    private readonly IMascotaRepository _repository;
    private readonly IPropietarioRepository _propietarioRepository;
    private readonly IMapper _mapper;

    public MascotaService(
        IMascotaRepository repository,
        IPropietarioRepository propietarioRepository,
        IMapper mapper)
    {
        _repository = repository;
        _propietarioRepository = propietarioRepository;
        _mapper = mapper;
    }

    public async Task<List<MascotaDto>> ObtenerTodosAsync()
    {
        var mascotas = await _repository.ObtenerTodosAsync();

        return _mapper.Map<List<MascotaDto>>(mascotas);
    }

    public async Task<MascotaDto?> ObtenerPorIdAsync(int id)
    {
        var mascota = await _repository.ObtenerPorIdAsync(id);

        if (mascota == null)
            return null;

        return _mapper.Map<MascotaDto>(mascota);
    }

    public async Task<List<MascotaDto>> ObtenerPorEspecieAsync(
        string? especie)
    {
        var mascotas = string.IsNullOrWhiteSpace(especie)
            ? await _repository.ObtenerTodosAsync()
            : await _repository.ObtenerPorEspecieAsync(especie.Trim());

        return _mapper.Map<List<MascotaDto>>(mascotas);
    }

    public async Task<List<MascotaDto>?> ObtenerPorPropietarioAsync(
        int idPropietario)
    {
        var propietario = await _propietarioRepository
            .ObtenerPorIdAsync(idPropietario);

        if (propietario == null)
            return null;

        var mascotas = await _repository
            .ObtenerPorPropietarioAsync(idPropietario);

        return _mapper.Map<List<MascotaDto>>(mascotas);
    }

    public async Task<MascotaDto> CrearAsync(MascotaCrearDto dto)
    {
        var propietario = await _propietarioRepository
            .ObtenerPorIdAsync(dto.IdPropietario);

        if (propietario == null)
        {
            throw new ReglaNegocioException(
                "El propietario indicado no existe."
            );
        }

        var mascota = _mapper.Map<Mascota>(dto);

        mascota.Propietario = propietario;

        await _repository.AgregarAsync(mascota);

        return _mapper.Map<MascotaDto>(mascota);
    }

    public async Task<bool> ActualizarAsync(
        int id,
        MascotaActualizarDto dto)
    {
        var mascota = await _repository.ObtenerPorIdAsync(id);

        if (mascota == null)
            return false;

        var propietario = await _propietarioRepository
            .ObtenerPorIdAsync(dto.IdPropietario);

        if (propietario == null)
        {
            throw new ReglaNegocioException(
                "El nuevo propietario indicado no existe."
            );
        }

        _mapper.Map(dto, mascota);

        mascota.Propietario = propietario;

        await _repository.ActualizarAsync(mascota);

        return true;
    }

    public async Task<bool> EliminarAsync(int id)
    {
        var mascota = await _repository.ObtenerPorIdAsync(id);

        if (mascota == null)
            return false;

        await _repository.EliminarAsync(mascota);

        return true;
    }
}
