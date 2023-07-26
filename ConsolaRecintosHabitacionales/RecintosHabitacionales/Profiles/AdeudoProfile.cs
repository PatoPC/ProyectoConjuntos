using AutoMapper;
using DTOs.Adeudo;
using DTOs.Menu;
using DTOs.Modulo;
using DTOs.Permiso;
using DTOs.Roles;

namespace RecintosHabitacionales.Profiles
{
    public class AdeudoProfile : Profile
    {
        public AdeudoProfile()
        {
            CreateMap<AdeudoDTOCrear, AdeudoDTOCompleto>()
                .ForMember(x => x.Nombre, y => y.MapFrom(f => f.NombresPersona))
                .ForMember(x => x.Apellido, y => y.MapFrom(f => f.ApellidosPersona));
        }
    }
}
