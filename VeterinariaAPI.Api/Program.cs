
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using VeterinariaAPI.Datos;
using VeterinariaAPI.Datos.Repositories;
using VeterinariaAPI.Datos.Repositories.Interfaces;

using VeterinariaAPI.Negocio;
using VeterinariaAPI.Negocio.Interfaces;
using VeterinariaAPI.Negocio.Services;

var builder = WebApplication.CreateBuilder(args);

// 1. CONTROLADORES
builder.Services.AddControllers();

// 2. AUTORIZACION
builder.Services.AddAuthorization();

// 3. SWAGGER
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "VeterinariaAPI",
        Version = "v1",
        Description = "API de gestión veterinaria con autenticación JWT"
    });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Escribe el token JWT."
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// 4. CONEXION SQL SERVER
var connectionString = builder.Configuration
    .GetConnectionString("VeterinariaConnection")
    ?? throw new InvalidOperationException(
        "No se encontró la cadena de conexión."
    );

builder.Services.AddDbContext<VeterinariaContext>(options =>
    options.UseSqlServer(connectionString)
);

// 5. REPOSITORIOS
builder.Services.AddScoped<
    IPropietarioRepository,
    PropietarioRepository>();

builder.Services.AddScoped<
    IMascotaRepository,
    MascotaRepository>();

builder.Services.AddScoped<
    IUsuarioRepository,
    UsuarioRepository>();

// 6. SERVICIOS
builder.Services.AddScoped<
    IPropietarioService,
    PropietarioService>();

builder.Services.AddScoped<
    IMascotaService,
    MascotaService>();

builder.Services.AddScoped<
    IAuthService,
    AuthService>();

// 7. AUTOMAPPER
builder.Services.AddAutoMapper(
    cfg => { },
    typeof(MappingProfile).Assembly
);
// 8. CONFIGURACION JWT
var jwtKey = builder.Configuration["Jwt:Key"]
    ?? throw new InvalidOperationException("Falta Jwt:Key.");

var jwtIssuer = builder.Configuration["Jwt:Issuer"]
    ?? throw new InvalidOperationException("Falta Jwt:Issuer.");

var jwtAudience = builder.Configuration["Jwt:Audience"]
    ?? throw new InvalidOperationException("Falta Jwt:Audience.");

builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme =
            JwtBearerDefaults.AuthenticationScheme;

        options.DefaultChallengeScheme =
            JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters =
            new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,

                ValidIssuer = jwtIssuer,
                ValidAudience = jwtAudience,

                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(jwtKey)
                ),

                ClockSkew = TimeSpan.Zero
            };
    });

// 9. CONSTRUIR
var app = builder.Build();

// 10. SWAGGER
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// 11. MIDDLEWARE
app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

// 12. CONTROLADORES
app.MapControllers();

app.Run();
