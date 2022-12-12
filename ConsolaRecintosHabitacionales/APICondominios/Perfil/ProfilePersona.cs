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
           
        }
       
    }
}
