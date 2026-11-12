
using Microsoft.AspNetCore.Mvc;
using VeterinariaAPI.Entidades.DTOs;
using VeterinariaAPI.Negocio.Exceptions;
using VeterinariaAPI.Negocio.Interfaces;

namespace VeterinariaAPI.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropietariosController : ControllerBase
{
    private readonly IPropietarioService _service;

    public PropietariosController(IPropietarioService service)
    {
        _service = service;
    }

    // GET: api/Propietarios

    [HttpGet]
    public async Task<ActionResult<List<PropietarioDto>>> ObtenerTodos()
    {
        var propietarios = await _service.ObtenerTodosAsync();

        return Ok(propietarios);
    }

    // GET: api/Propietarios/1

    [HttpGet("{id:int}")]
    public async Task<ActionResult<PropietarioDto>> ObtenerPorId(int id)
    {
        var propietario = await _service.ObtenerPorIdAsync(id);

        if (propietario == null)
            return NotFound(new { mensaje = "Propietario no encontrado." });

        return Ok(propietario);
    }

    // POST: api/Propietarios

    [HttpPost]
    public async Task<ActionResult<PropietarioDto>> Crear(
        PropietarioCrearDto dto)
    {
        try
        {
            var propietario = await _service.CrearAsync(dto);

            return CreatedAtAction(
                nameof(ObtenerPorId),
                new { id = propietario.IdPropietario },
                propietario
            );
        }
        catch (ReglaNegocioException ex)
        {
            return Conflict(new { mensaje = ex.Message });
        }
    }

    // PUT: api/Propietarios/1

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Actualizar(
        int id,
        PropietarioActualizarDto dto)
    {
        try
        {
            bool actualizado = await _service.ActualizarAsync(id, dto);

            if (!actualizado)
                return NotFound(new { mensaje = "Propietario no encontrado." });

            return NoContent();
        }
        catch (ReglaNegocioException ex)
        {
            return Conflict(new { mensaje = ex.Message });
        }
    }

    // DELETE: api/Propietarios/1

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Eliminar(int id)
    {
        try
        {
            bool eliminado = await _service.EliminarAsync(id);

            if (!eliminado)
                return NotFound(new { mensaje = "Propietario no encontrado." });

            return NoContent();
        }
        catch (ReglaNegocioException ex)
        {
            return Conflict(new { mensaje = ex.Message });
        }
    }
}
