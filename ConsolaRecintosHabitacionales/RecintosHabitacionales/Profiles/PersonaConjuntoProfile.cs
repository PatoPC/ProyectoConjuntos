using AutoMapper;
using DTOs.Menu;
using DTOs.Modulo;
using DTOs.Permiso;
using DTOs.Persona;
using DTOs.Roles;

namespace RecintosHabitacionales.Profiles
{
    public class PersonaConjuntoProfile : Profile
    {
        public PersonaConjuntoProfile()
        {
            CreateMap<PersonaDTOCompleto, PersonaDTOConjunto>();
            CreateMap<PersonaDTOConjunto, PersonaDTOCompleto>();

            CreateMap<PersonaDTOConjunto, TipoPersonaDTO>()
                .ForMember(x => x.IdDepartamento, y => y.MapFrom(fuente => fuente.IdDepartamento))
                .ForMember(x => x.IdPersona, y => y.MapFrom(fuente => fuente.IdPersona));

        }
    }
}
