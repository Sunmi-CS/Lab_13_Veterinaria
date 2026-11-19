
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VeterinariaAPI.Entidades.DTOs;
using VeterinariaAPI.Negocio.Interfaces;

namespace VeterinariaAPI.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _service;

    public AuthController(IAuthService service)
    {
        _service = service;
    }

    // POST: api/auth/registro

    [AllowAnonymous]
    [HttpPost("registro")]
    public async Task<ActionResult<TokenDto>> Registrar(
        UsuarioRegistroDto dto)
    {
        try
        {
            var resultado = await _service.RegistrarAsync(dto);

            return StatusCode(
                StatusCodes.Status201Created,
                resultado
            );
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { mensaje = ex.Message });
        }
    }

    // POST: api/auth/login

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ActionResult<TokenDto>> Login(LoginDto dto)
    {
        var resultado = await _service.LoginAsync(dto);

        if (resultado == null)
        {
            return Unauthorized(new
            {
                mensaje = "Correo o contraseña incorrectos."
            });
        }

        return Ok(resultado);
    }

    // GET: api/auth/perfil

    [Authorize]
    [HttpGet("perfil")]
    public IActionResult Perfil()
    {
        var perfil = new UsuarioPerfilDto
        {
            IdUsuario = int.Parse(
                User.FindFirstValue(ClaimTypes.NameIdentifier)!
            ),

            Correo = User.FindFirstValue(ClaimTypes.Name)!,

            Rol = User.FindFirstValue(ClaimTypes.Role)!
        };

        return Ok(perfil);
    }
}
