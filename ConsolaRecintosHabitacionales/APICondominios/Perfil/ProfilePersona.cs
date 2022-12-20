using AutoMapper;
using ConjuntosEntidades.Entidades;
using DTOs.Persona;
using DTOs.Torre;

namespace APICondominios.Perfil
{
    public class ProfilePersona : Profile
    {
        public ProfilePersona()
        {
            CreateMap<Persona, PersonaDTOCompleto>();
            CreateMap<PersonaDTOCompleto, Persona>();

            CreateMap<Persona, PersonaDTOCrear>();
            CreateMap<PersonaDTOCrear, Persona>();
            
            CreateMap<Persona, PersonaDTOEditar>();
            CreateMap<PersonaDTOEditar, Persona>();

            CreateMap<TipoPersona, TipoPersonaDTO>()
                .ForMember(x => x.NombrePersona, y => y.MapFrom(y => y.IdPersonaNavigation.NombresPersona + " " + y.IdPersonaNavigation.ApellidosPersona))
                .ForMember(x => x.CodigoDepartamento, y => y.MapFrom(y => y.IdDepartamentoNavigation.CodigoDepartamento));
            CreateMap<TipoPersonaDTO, TipoPersona>();
           
        }
       
    }
}
