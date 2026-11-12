
using Microsoft.EntityFrameworkCore;
using VeterinariaAPI.Datos;
using VeterinariaAPI.Datos.Repositories;
using VeterinariaAPI.Datos.Repositories.Interfaces;
using VeterinariaAPI.Negocio;
using VeterinariaAPI.Negocio.Interfaces;
using VeterinariaAPI.Negocio.Services;

var builder = WebApplication.CreateBuilder(args);

// ==========================================
// 1. CONTROLADORES
// ==========================================

builder.Services.AddControllers();

// ==========================================
// 2. SWAGGER
// ==========================================

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ==========================================
// 3. CONEXION A SQL SERVER
// ==========================================

var connectionString = builder.Configuration
    .GetConnectionString("VeterinariaConnection")
    ?? throw new InvalidOperationException(
        "No se encontró la cadena de conexión VeterinariaConnection."
    );

builder.Services.AddDbContext<VeterinariaContext>(options =>
    options.UseSqlServer(connectionString)
);

// ==========================================
// 4. INYECCION DE REPOSITORIOS
// ==========================================

builder.Services.AddScoped<
    IPropietarioRepository,
    PropietarioRepository
>();

builder.Services.AddScoped<
    IMascotaRepository,
    MascotaRepository
>();

// ==========================================
// 5. INYECCION DE SERVICIOS
// ==========================================

builder.Services.AddScoped<
    IPropietarioService,
    PropietarioService
>();

builder.Services.AddScoped<
    IMascotaService,
    MascotaService
>();

// ==========================================
// 6. AUTOMAPPER
// ==========================================

builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<MappingProfile>();
});

// ==========================================
// 7. CONSTRUIR APLICACION
// ==========================================

var app = builder.Build();

// ==========================================
// 8. CONFIGURAR SWAGGER
// ==========================================

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI();
}

// ==========================================
// 9. CONFIGURAR HTTP
// ==========================================

app.UseHttpsRedirection();

app.UseAuthorization();

// ==========================================
// 10. MAPEAR CONTROLADORES
// ==========================================

app.MapControllers();

app.Run();
