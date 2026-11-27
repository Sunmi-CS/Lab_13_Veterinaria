
using CatalogoVeterinarioAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CatalogoVeterinarioAPI.Controllers;

[Authorize]
[ApiController]
[Route("api/especialidades")]
public class EspecialidadesController : ControllerBase
{
    private static readonly List<Especialidad> Especialidades =
    [
        new Especialidad
        {
            Id = 1,
            Nombre = "Medicina interna y medicina preventiva",
            Descripcion = "Diagnóstico y tratamiento de enfermedades sistémicas, vacunación y control epidemiológico."
        },
        new Especialidad
        {
            Id = 2,
            Nombre = "Cirugía y anestesiología",
            Descripcion = "Intervenciones de tejidos blandos, traumatología, ortopedia y manejo anestésico o del dolor."
        },
        new Especialidad
        {
            Id = 3,
            Nombre = "Urgencias y cuidados intensivos",
            Descripcion = "Atención inmediata de pacientes críticos, traumatismos o emergencias."
        },
        new Especialidad
        {
            Id = 4,
            Nombre = "Dermatología",
            Descripcion = "Diagnóstico y tratamiento de enfermedades y afecciones de la piel."
        },
        new Especialidad
        {
            Id = 5,
            Nombre = "Oftalmología",
            Descripcion = "Diagnóstico y tratamiento de enfermedades y problemas relacionados con la visión."
        },
        new Especialidad
        {
            Id = 6,
            Nombre = "Cardiología",
            Descripcion = "Evaluación, diagnóstico y tratamiento de enfermedades del sistema cardiovascular."
        },
        new Especialidad
        {
            Id = 7,
            Nombre = "Oncología",
            Descripcion = "Diagnóstico y manejo de tumores y enfermedades cancerígenas."
        },
        new Especialidad
        {
            Id = 8,
            Nombre = "Neurología",
            Descripcion = "Diagnóstico y tratamiento de trastornos del sistema nervioso."
        },
        new Especialidad
        {
            Id = 9,
            Nombre = "Gastroenterología",
            Descripcion = "Diagnóstico y tratamiento de enfermedades y problemas del sistema digestivo."
        },
        new Especialidad
        {
            Id = 10,
            Nombre = "Etología clínica y nutrición",
            Descripcion = "Corrección de trastornos de conducta y elaboración de planes alimentarios terapéuticos."
        },
        new Especialidad
        {
            Id = 11,
            Nombre = "Reproducción (teriogenología)",
            Descripcion = "Atención de la salud reproductiva, fertilidad y trastornos reproductivos."
        },
        new Especialidad
        {
            Id = 12,
            Nombre = "Fisioterapia",
            Descripcion = "Rehabilitación física y recuperación de la movilidad de las mascotas."
        }
    ];

    // GET: api/especialidades
    [HttpGet]
    public ActionResult<List<Especialidad>> ObtenerTodas()
    {
        return Ok(Especialidades);
    }

    // GET: api/especialidades/1
    [HttpGet("{id:int}")]
    public ActionResult<Especialidad> ObtenerPorId(int id)
    {
        var especialidad = Especialidades
            .FirstOrDefault(e => e.Id == id);

        if (especialidad == null)
        {
            return NotFound(new
            {
                mensaje = "Especialidad no encontrada."
            });
        }

        return Ok(especialidad);
    }
}
