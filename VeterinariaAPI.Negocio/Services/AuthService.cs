
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using VeterinariaAPI.Datos.Repositories.Interfaces;
using VeterinariaAPI.Entidades.DTOs;
using VeterinariaAPI.Entidades.Models;
using VeterinariaAPI.Negocio.Interfaces;

namespace VeterinariaAPI.Negocio.Services;

public class AuthService : IAuthService
{
    private readonly IUsuarioRepository _repository;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;

    public AuthService(
        IUsuarioRepository repository,
        IMapper mapper,
        IConfiguration configuration)
    {
        _repository = repository;
        _mapper = mapper;
        _configuration = configuration;
    }

    public async Task<TokenDto> RegistrarAsync(UsuarioRegistroDto dto)
    {
        string correo = dto.Correo.Trim().ToLowerInvariant();

        bool existe = await _repository.ExisteCorreoAsync(correo);

        if (existe)
            throw new InvalidOperationException(
                "El correo ya se encuentra registrado."
            );

        var usuario = _mapper.Map<Usuario>(dto);

        usuario.Correo = correo;

        // BCrypt genera el hash de la contraseña.
        usuario.ClaveHash = BCrypt.Net.BCrypt.HashPassword(dto.Clave);

        // Los nuevos registros reciben el rol básico.
        usuario.Rol = "Recepcionista";

        await _repository.AgregarAsync(usuario);

        return GenerarToken(usuario);
    }

    public async Task<TokenDto?> LoginAsync(LoginDto dto)
    {
        string correo = dto.Correo.Trim().ToLowerInvariant();

        var usuario = await _repository.ObtenerPorCorreoAsync(correo);

        if (usuario == null)
            return null;

        bool contraseñaValida =
            BCrypt.Net.BCrypt.Verify(dto.Clave, usuario.ClaveHash);

        if (!contraseñaValida)
            return null;

        return GenerarToken(usuario);
    }

    private TokenDto GenerarToken(Usuario usuario)
    {
        string key = _configuration["Jwt:Key"]
            ?? throw new InvalidOperationException(
                "No se configuró Jwt:Key."
            );

        string issuer = _configuration["Jwt:Issuer"]
            ?? throw new InvalidOperationException(
                "No se configuró Jwt:Issuer."
            );

        string audience = _configuration["Jwt:Audience"]
            ?? throw new InvalidOperationException(
                "No se configuró Jwt:Audience."
            );

        int minutos = int.TryParse(
            _configuration["Jwt:DurationMinutes"],
            out int duracion
        ) ? duracion : 60;

        var claims = new List<Claim>
        {
            new Claim(
                JwtRegisteredClaimNames.Sub,
                usuario.IdUsuario.ToString()
            ),

            new Claim(
                JwtRegisteredClaimNames.Email,
                usuario.Correo
            ),

            new Claim(
                ClaimTypes.NameIdentifier,
                usuario.IdUsuario.ToString()
            ),

            new Claim(
                ClaimTypes.Name,
                usuario.Correo
            ),

            new Claim(
                ClaimTypes.Role,
                usuario.Rol
            )
        };

        var securityKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(key)
        );

        var credentials = new SigningCredentials(
            securityKey,
            SecurityAlgorithms.HmacSha256
        );

        var expiracion = DateTime.UtcNow.AddMinutes(minutos);

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: expiracion,
            signingCredentials: credentials
        );

        return new TokenDto
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            Expiracion = expiracion,
            Correo = usuario.Correo,
            Rol = usuario.Rol
        };
    }
}
