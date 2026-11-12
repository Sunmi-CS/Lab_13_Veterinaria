using AutoMapper;
using VeterinariaAPI.Entidades.DTOs;
using VeterinariaAPI.Entidades.Models;

namespace VeterinariaAPI.Negocio;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Mapeos de Propietario

        CreateMap<Propietario, PropietarioDto>()
            .ForMember(
                destino => destino.CantidadMascotas,
                opcion => opcion.MapFrom(
                    origen => origen.Mascotas.Count
                )
            );

        CreateMap<PropietarioCrearDto, Propietario>();

        CreateMap<PropietarioActualizarDto, Propietario>();

        // Mapeos de Mascota

        CreateMap<Mascota, MascotaDto>()
            .ForMember(
                destino => destino.NombrePropietario,
                opcion => opcion.MapFrom(
                    origen => origen.Propietario.Nombres
                        + " " + origen.Propietario.Apellidos
                )
            );

        CreateMap<MascotaCrearDto, Mascota>();

        CreateMap<MascotaActualizarDto, Mascota>();
    }
}
