
using Microsoft.AspNetCore.Mvc;
using VeterinariaAPI.Entidades.DTOs;
using VeterinariaAPI.Negocio.Exceptions;
using VeterinariaAPI.Negocio.Interfaces;

namespace VeterinariaAPI.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MascotasController : ControllerBase
{
    private readonly IMascotaService _service;

    public MascotasController(IMascotaService service)
    {
        _service = service;
    }

    // GET: api/Mascotas

    [HttpGet]
    public async Task<ActionResult<List<MascotaDto>>> ObtenerTodos(
        [FromQuery] string? especie)
    {
        var mascotas = await _service.ObtenerPorEspecieAsync(especie);

        return Ok(mascotas);
    }

    // GET: api/Mascotas/1

    [HttpGet("{id:int}")]
    public async Task<ActionResult<MascotaDto>> ObtenerPorId(int id)
    {
        var mascota = await _service.ObtenerPorIdAsync(id);

        if (mascota == null)
            return NotFound(new { mensaje = "Mascota no encontrada." });

        return Ok(mascota);
    }

    // GET: api/Propietarios/1/mascotas

    [HttpGet("~/api/Propietarios/{idPropietario:int}/mascotas")]
    public async Task<ActionResult<List<MascotaDto>>> ObtenerPorPropietario(
        int idPropietario)
    {
        var mascotas = await _service.ObtenerPorPropietarioAsync(idPropietario);

        if (mascotas == null)
            return NotFound(new { mensaje = "Propietario no encontrado." });

        return Ok(mascotas);
    }

    // POST: api/Mascotas

    [HttpPost]
    public async Task<ActionResult<MascotaDto>> Crear(
        MascotaCrearDto dto)
    {
        try
        {
            var mascota = await _service.CrearAsync(dto);

            return CreatedAtAction(
                nameof(ObtenerPorId),
                new { id = mascota.IdMascota },
                mascota
            );
        }
        catch (ReglaNegocioException ex)
        {
            return Conflict(new { mensaje = ex.Message });
        }
    }

    // PUT: api/Mascotas/1

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Actualizar(
        int id,
        MascotaActualizarDto dto)
    {
        try
        {
            bool actualizado = await _service.ActualizarAsync(id, dto);

            if (!actualizado)
                return NotFound(new { mensaje = "Mascota no encontrada." });

            return NoContent();
        }
        catch (ReglaNegocioException ex)
        {
            return Conflict(new { mensaje = ex.Message });
        }
    }

    // DELETE: api/Mascotas/1

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Eliminar(int id)
    {
        bool eliminado = await _service.EliminarAsync(id);

        if (!eliminado)
            return NotFound(new { mensaje = "Mascota no encontrada." });

        return NoContent();
    }
}
