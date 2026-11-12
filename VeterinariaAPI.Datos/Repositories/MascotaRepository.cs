using Microsoft.EntityFrameworkCore;
using VeterinariaAPI.Datos.Repositories.Interfaces;
using VeterinariaAPI.Entidades.Models;

namespace VeterinariaAPI.Datos.Repositories;

public class MascotaRepository : IMascotaRepository
{
    private readonly VeterinariaContext _context;

    public MascotaRepository(VeterinariaContext context)
    {
        _context = context;
    }

    public async Task<List<Mascota>> ObtenerTodosAsync()
    {
        return await _context.Mascotas
            .Include(m => m.Propietario)
            .ToListAsync();
    }

    public async Task<Mascota?> ObtenerPorIdAsync(int id)
    {
        return await _context.Mascotas
            .Include(m => m.Propietario)
            .FirstOrDefaultAsync(m => m.IdMascota == id);
    }

    public async Task<List<Mascota>> ObtenerPorEspecieAsync(string especie)
    {
        return await _context.Mascotas
            .Include(m => m.Propietario)
            .Where(m => m.Especie == especie)
            .ToListAsync();
    }

    public async Task<List<Mascota>> ObtenerPorPropietarioAsync(int idPropietario)
    {
        return await _context.Mascotas
            .Include(m => m.Propietario)
            .Where(m => m.IdPropietario == idPropietario)
            .ToListAsync();
    }

    public async Task AgregarAsync(Mascota mascota)
    {
        await _context.Mascotas.AddAsync(mascota);
        await _context.SaveChangesAsync();
    }

    public async Task ActualizarAsync(Mascota mascota)
    {
        await _context.SaveChangesAsync();
    }

    public async Task EliminarAsync(Mascota mascota)
    {
        _context.Mascotas.Remove(mascota);
        await _context.SaveChangesAsync();
    }
}
