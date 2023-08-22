using AutoMapper;
using ConjuntosEntidades.Entidades;
using DTOs.Adeudo;
using DTOs.Comunicado;
using DTOs.Persona;
using DTOs.Torre;

namespace APICondominios.Perfil
{
    public class ProfileComunicado : Profile
    {
        public ProfileComunicado()
        {
            CreateMap<Comunicado, ComunicadoDTOCompleto>();
            CreateMap<ComunicadoDTOCompleto, Comunicado>();

            CreateMap<Comunicado, ComunicadoDTOEditar>();
            CreateMap<ComunicadoDTOEditar, Comunicado>();

            CreateMap<Comunicado, ComunicadoDTOCrear>();
            CreateMap<ComunicadoDTOCrear, Comunicado>();
        }

    }
}
