
using AutoMapper;
using VeterinariaAPI.Datos.Repositories.Interfaces;
using VeterinariaAPI.Entidades.DTOs;
using VeterinariaAPI.Entidades.Models;
using VeterinariaAPI.Negocio.Exceptions;
using VeterinariaAPI.Negocio.Interfaces;

namespace VeterinariaAPI.Negocio.Services;

public class PropietarioService : IPropietarioService
{
    private readonly IPropietarioRepository _repository;
    private readonly IMapper _mapper;

    public PropietarioService(
        IPropietarioRepository repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<PropietarioDto>> ObtenerTodosAsync()
    {
        var propietarios = await _repository.ObtenerTodosAsync();

        return _mapper.Map<List<PropietarioDto>>(propietarios);
    }

    public async Task<PropietarioDto?> ObtenerPorIdAsync(int id)
    {
        var propietario = await _repository.ObtenerPorIdAsync(id);

        if (propietario == null)
            return null;

        return _mapper.Map<PropietarioDto>(propietario);
    }

    public async Task<PropietarioDto> CrearAsync(PropietarioCrearDto dto)
    {
        // REGLA 1: El correo debe ser único.

        bool existeCorreo = await _repository.ExisteCorreoAsync(dto.Correo);

        if (existeCorreo)
        {
            throw new ReglaNegocioException(
                "Ya existe un propietario registrado con ese correo."
            );
        }

        var propietario = _mapper.Map<Propietario>(dto);

        await _repository.AgregarAsync(propietario);

        return _mapper.Map<PropietarioDto>(propietario);
    }

    public async Task<bool> ActualizarAsync(
        int id,
        PropietarioActualizarDto dto)
    {
        var propietario = await _repository.ObtenerPorIdAsync(id);

        if (propietario == null)
            return false;

        bool existeCorreo = await _repository.ExisteCorreoAsync(
            dto.Correo,
            id
        );

        if (existeCorreo)
        {
            throw new ReglaNegocioException(
                "El correo ya está siendo utilizado por otro propietario."
            );
        }

        _mapper.Map(dto, propietario);

        await _repository.ActualizarAsync(propietario);

        return true;
    }

    public async Task<bool> EliminarAsync(int id)
    {
        var propietario = await _repository.ObtenerPorIdAsync(id);

        if (propietario == null)
            return false;

        // REGLA 2: No eliminar propietarios con mascotas.

        bool tieneMascotas = await _repository.TieneMascotasAsync(id);

        if (tieneMascotas)
        {
            throw new ReglaNegocioException(
                "No se puede eliminar el propietario porque tiene mascotas registradas."
            );
        }

        await _repository.EliminarAsync(propietario);

        return true;
    }
}
