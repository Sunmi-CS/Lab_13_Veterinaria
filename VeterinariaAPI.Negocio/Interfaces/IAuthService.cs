
using VeterinariaAPI.Entidades.DTOs;

namespace VeterinariaAPI.Negocio.Interfaces;

public interface IAuthService
{
    Task<TokenDto> RegistrarAsync(UsuarioRegistroDto dto);

    Task<TokenDto?> LoginAsync(LoginDto dto);
}
