using AutoMapper;
using ConjuntosEntidades.Entidades;
using DTOs.Adeudo;
using DTOs.Persona;
using DTOs.Torre;

namespace APICondominios.Perfil
{
    public class ProfileAdeudo : Profile
    {
        public ProfileAdeudo()
        {
            CreateMap<Adeudo, AdeudoDTOCrear>();
            CreateMap<AdeudoDTOCrear, Adeudo>();

           
           
        }
       
    }
}
