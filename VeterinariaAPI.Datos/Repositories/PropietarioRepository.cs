using Microsoft.EntityFrameworkCore;
using VeterinariaAPI.Datos.Repositories.Interfaces;
using VeterinariaAPI.Entidades.Models;

namespace VeterinariaAPI.Datos.Repositories;

public class PropietarioRepository : IPropietarioRepository
{
    private readonly VeterinariaContext _context;

    public PropietarioRepository(VeterinariaContext context)
    {
        _context = context;
    }

    public async Task<List<Propietario>> ObtenerTodosAsync()
    {
        return await _context.Propietarios
            .Include(p => p.Mascotas)
            .ToListAsync();
    }

    public async Task<Propietario?> ObtenerPorIdAsync(int id)
    {
        return await _context.Propietarios
            .Include(p => p.Mascotas)
            .FirstOrDefaultAsync(p => p.IdPropietario == id);
    }

    public async Task<bool> ExisteCorreoAsync(string correo, int? excluirId = null)
    {
        return await _context.Propietarios.AnyAsync(p =>
            p.Correo == correo &&
            (!excluirId.HasValue || p.IdPropietario != excluirId.Value));
    }

    public async Task<bool> TieneMascotasAsync(int id)
    {
        return await _context.Mascotas
            .AnyAsync(m => m.IdPropietario == id);
    }

    public async Task AgregarAsync(Propietario propietario)
    {
        await _context.Propietarios.AddAsync(propietario);
        await _context.SaveChangesAsync();
    }

    public async Task ActualizarAsync(Propietario propietario)
    {
        await _context.SaveChangesAsync();
    }

    public async Task EliminarAsync(Propietario propietario)
    {
        _context.Propietarios.Remove(propietario);
        await _context.SaveChangesAsync();
    }
}
